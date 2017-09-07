using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerrainTiler
{
    public enum ELogLevel : byte { DEBUG = 0, INFO = 1, IMPORTANT_INFO = 2, WARNING = 3, ERROR = 4, EXCEPTION = 5, NONE = 6 }

    public partial class Form1 : Form
    {
        public const string FileExt = ".json";

        CAppSettings _settings;

        bool _alreadyactivated = false;

        Pen _panelborderpen;
        CGrid _grid;

        Dictionary<object, CTileDescr> _panel_descr = new Dictionary<object, CTileDescr>();

        TransparentPanel _pnlGrid;

        CTileDescr _selected_descr;

        public Form1()
        {
            InitializeComponent();

            tbTerrain.Text = "NewTerrain";

            _panelborderpen = new Pen(Color.Black);

            _grid = new CGrid(this);

            _pnlGrid = new TransparentPanel();
            _pnlGrid.Location = pnlBaseGrid.Location;
            _pnlGrid.Name = "pnlGrid";
            _pnlGrid.Size = pnlBaseGrid.Size;

            _pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            //_pnlGrid.BackColor = Color.Black;

            pnlBaseGrid.Enabled = false;
            pnlBaseGrid.Location = new Point(-10000, 100000);


            _pnlGrid.Paint += pnlGrid_Paint;
            _pnlGrid.MouseDown += pnlGrid_MouseDown;
            _pnlGrid.MouseMove += pnlGrid_MouseMove;
            _pnlGrid.MouseUp += pnlGrid_MouseUp;
            _pnlGrid.MouseWheel += pnlGrid_MouseWheel;

            Controls.Add(_pnlGrid);

        }

        void LoadSettings()
        {
            try
            {
                _settings = JsonToFile<CAppSettings>.Load(Environment.CurrentDirectory, "TerrainTilerSettings.dat");
            }
            catch (Exception ex)
            {
                _settings = new CAppSettings();
                AddLogToConsole(string.Format("Can't parse settings: {0}", ex), Color.Red);
                return;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (_alreadyactivated)
                return;

            _alreadyactivated = true;

            LoadSettings();

            AddLogToConsole(string.Format("settings: {0}", _settings), Color.Gray);

            int i = 0;
            foreach (var t in _settings)
            {
                if (_selected_descr == null)
                    SelectDescr(t);

                AddColorPanel(5, 5, i, 5, pnlColors.Width - 30, 50, t);
                i++;
            }

            LoadTerrains(true);
        }

        void AddColorPanel(int left, int top, int index, int margin_h, int width, int height, CTileDescr tile)
        {
            var pnl = new Panel();
            pnl.Location = new Point(left, top + index * (margin_h + height));
            pnl.Size = new Size(width, height);
            pnlColors.Controls.Add(pnl);

            var cpnl = new Panel();
            int imargin = 2;
            cpnl.Location = new Point(imargin, imargin);
            cpnl.Size = new Size(height - 2* imargin, height - 2 * imargin);
            cpnl.BackColor = tile.TileColor;
            cpnl.BorderStyle = BorderStyle.FixedSingle;
            cpnl.MouseDown += MouseDownOnColor;
            pnl.Controls.Add(cpnl);

            var lblSymbol = new Label();
            lblSymbol.Text = tile.TileMarker.ToString();
            lblSymbol.TextAlign = ContentAlignment.MiddleCenter;
            lblSymbol.Font = pnl.Font;
            lblSymbol.Location = new Point(0, 0);
            lblSymbol.Size = new Size(cpnl.Size.Width, cpnl.Size.Height);
            lblSymbol.Enabled = false; //чтоб не загораживал нажатие мыши
            cpnl.Controls.Add(lblSymbol);

            var lbl = new Label();
            lbl.Text = tile.Descr;
            lbl.Font = pnl.Font;
            lbl.Location = new Point(height, height / 2 - lbl.Size.Height/2);
            lbl.Size = new Size(pnl.Size.Width - lbl.Location.X, height - 2 * imargin);
            pnl.Controls.Add(lbl);

            _panel_descr.Add(cpnl, tile);
        }

        internal CTileDescr GetTileDescrByTerrein(string tile)
        {
            return _settings.FirstOrDefault(c => string.Equals(c.Terrain, tile));
        }

        #region LogWindow Output
        void AddLogToRichText(string inText, Color inClr)
        {
            if (m_uiLogLinesCount > 1000)
            {
                rtLog.Text = string.Empty;
                m_uiLogLinesCount = 0;
            }

            int length = rtLog.TextLength;  // at end of text
            rtLog.AppendText(inText);
            rtLog.SelectionStart = length;
            rtLog.SelectionLength = inText.Length;
            rtLog.SelectionColor = inClr;
            rtLog.SelectionStart = rtLog.TextLength;
            rtLog.SelectionLength = 0;
        }

        uint m_uiLogLinesCount = 0;
        public void AddLogToConsole(string inText, Color inClr)
        {
            if (rtLog.IsDisposed)
                return;

            string sres = string.Format("{0}: {1}{2}", m_uiLogLinesCount.ToString(), inText, Environment.NewLine);
            rtLog.BeginInvoke(new Action<string>(s => AddLogToRichText(s, inClr)), sres);
            m_uiLogLinesCount++;
        }

        public void AddLogToConsole(string inText, ELogLevel inLogLevel)
        {
            if (rtLog.IsDisposed)
                return;

            string sres = string.Format("{0}: {1}{2}", m_uiLogLinesCount.ToString(), inText, Environment.NewLine);

            Color clr = Color.Black;
            switch (inLogLevel)
            {
                case ELogLevel.DEBUG: clr = Color.Black; break;
                case ELogLevel.INFO: clr = Color.Green; break;
                case ELogLevel.IMPORTANT_INFO: clr = Color.Firebrick; break;
                case ELogLevel.WARNING: clr = Color.Red; break;
                case ELogLevel.ERROR: clr = Color.Red; break;
                case ELogLevel.EXCEPTION: clr = Color.Red; break;
            }

            rtLog.BeginInvoke(new Action<string>(s => AddLogToRichText(s, clr)), sres);
            m_uiLogLinesCount++;
        }

        #endregion //LogWindow Output

        private void pnlGrid_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;
            _grid.OnDraw(_pnlGrid.DisplayRectangle, e.Graphics);
            //DrawPanelBorder(_pnlGrid.DisplayRectangle, e.Graphics);
        }

        void DrawPanelBorder(Rectangle rect, Graphics graphics)
        {
            rect.Width -= 1;
            rect.Height -= 1;
            graphics.DrawRectangle(_panelborderpen, rect);
        }
        

        private void pnlGrid_MouseWheel(object sender, MouseEventArgs e)
        {
            if(_grid.OnMouseWheel(e.Delta))
                _pnlGrid.Invalidate();
        }

        MouseButtons _gridcapture = MouseButtons.None;
        Point _prev_mouse_pos;

        private void pnlGrid_MouseDown(object sender, MouseEventArgs e)
        {
            _gridcapture = e.Button;
            _pnlGrid.Capture = true;
            _prev_mouse_pos = e.Location;

            if (e.Button == MouseButtons.Right)
            {
                _grid.RemoveCell(_pnlGrid.DisplayRectangle, e.Location);
                _pnlGrid.Invalidate();
            }
            else if (e.Button == MouseButtons.Left)
            {
                _grid.AddCell(_pnlGrid.DisplayRectangle, e.Location, _selected_descr);
                _pnlGrid.Invalidate();
            }
        }

        private void pnlGrid_MouseUp(object sender, MouseEventArgs e)
        {
            _pnlGrid.Capture = false;
            _gridcapture = MouseButtons.None;
        }

        
        private void pnlGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (_gridcapture == MouseButtons.Middle)
            {
                Point shift = CGrid.Minus(e.Location, _prev_mouse_pos);
                _grid.OnMouseShift(shift);
                _pnlGrid.Invalidate();
                _prev_mouse_pos = e.Location;
            }

            if (e.Button == MouseButtons.Right)
            {
                _grid.RemoveCell(_pnlGrid.DisplayRectangle, e.Location);
                _pnlGrid.Invalidate();
            }
            else if (e.Button == MouseButtons.Left)
            {
                _grid.AddCell(_pnlGrid.DisplayRectangle, e.Location, _selected_descr);
                _pnlGrid.Invalidate();
            }
        }

        private void MouseDownOnColor(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_panel_descr.ContainsKey(sender))
                    SelectDescr(_panel_descr[sender]);
                else
                    AddLogToConsole("Unknown Select", ELogLevel.ERROR);
            }
        }

        void SelectDescr(CTileDescr descr)
        {
            _selected_descr = descr;
            AddLogToConsole($"Select {_selected_descr.Descr}", ELogLevel.INFO);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int i = -1;
            switch(e.KeyChar)
            {
                case '1': i = 0; break;
                case '2': i = 1; break;
                case '3': i = 2; break;
                case '4': i = 3; break;
                case '5': i = 4; break;
                case '6': i = 5; break;
                case '7': i = 6; break;
                case '8': i = 7; break;
                case '9': i = 8; break;
            }
            if(i != -1)
            {
                CTileDescr descr = _settings.ElementAtOrDefault(i);
                if (descr != null)
                    SelectDescr(descr);
            }
        }

        private void btnSaveTerrain_Click(object sender, EventArgs e)
        {
            string filename = _grid.Save(tbTerrain.Text, true);
            LoadTerrains(false);
            lblLastLoaded.Text = "LastLoaded: " + filename;
        }

        private void btnRewriteTerrain_Click(object sender, EventArgs e)
        {
            string filename = _grid.Save(tbTerrain.Text, false);
            LoadTerrains(false);
            lblLastLoaded.Text = "LastLoaded: " + filename;
        }

        private void btnLoadTerrain_Click(object sender, EventArgs e)
        {
            string filename = lbTerrains.SelectedItem as string;
            _grid.Load(filename);
            _pnlGrid.Invalidate();
            lblLastLoaded.Text = "LastLoaded: " + filename;
        }

        void LoadTerrains(bool inReloadMap)
        {
            lbTerrains.Items.Clear();
            var di = new DirectoryInfo(Environment.CurrentDirectory);
            foreach(var f in di.GetFiles("*" + FileExt))
            {
                string fn = Path.GetFileNameWithoutExtension(f.Name);
                lbTerrains.Items.Add(fn);
            }
            if (lbTerrains.Items.Count > 0)
            {
                lbTerrains.SelectedIndex = 0;
                if (inReloadMap)
                    btnLoadTerrain_Click(this, EventArgs.Empty);
            }
            else
                lblLastLoaded.Text = "Files not found";
        }

        private void lbTerrains_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbTerrain.Text = lbTerrains.SelectedItem as string;
        }

        FileInfo GetSelectedFile()
        {
            string filename = lbTerrains.SelectedItem as string;
            string filename_ext = filename + FileExt;
            var di = new DirectoryInfo(Environment.CurrentDirectory);
            FileInfo[] fl = di.GetFiles(filename_ext);
            if (fl.Length == 0)
            {
                AddLogToConsole($"Can't find {filename_ext}", ELogLevel.ERROR);
                return null;
            }
            return fl[0];
        }

        private void btnCopyTerrain_Click(object sender, EventArgs e)
        {
            FileInfo fl = GetSelectedFile();
            try
            {
                fl.CopyTo(_settings.PathToUnity + fl.Name, true);
                AddLogToConsole($"Was copied to {_settings.PathToUnity + fl.Name}", ELogLevel.IMPORTANT_INFO);
            }
            catch(Exception ex)
            {
                AddLogToConsole(ex.Message, ELogLevel.EXCEPTION);
            }
        }

        private void btnDeleteTerrain_Click(object sender, EventArgs e)
        {
            FileInfo fl = GetSelectedFile();
            if (fl == null)
                return;
            fl.CopyTo(Environment.CurrentDirectory + "\\" + fl.Name + "_bak", true);
            fl.Delete();
            LoadTerrains(false);
        }

        internal Font GetCellFont()
        {
            return pnlBaseGrid.Font;
        }
    }
}
