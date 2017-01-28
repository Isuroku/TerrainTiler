using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerrainTiler
{
    public enum ELogLevel : byte { DEBUG = 0, INFO = 1, IMPORTANT_INFO = 2, WARNING = 3, ERROR = 4, EXCEPTION = 5, NONE = 6 }

    public class CPanel: Panel
    {
        protected override void OnPaintBackground(PaintEventArgs e) {}
    }

    public partial class Form1 : Form
    {
        Pen _panelborderpen;
        CGrid _grid;

        public Form1()
        {
            InitializeComponent();

            _panelborderpen = new Pen(Color.Black);

            _grid = new CGrid(this);

            pnlGrid.MouseWheel += PnlGrid_MouseWheel;
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

        private void pnlControls_Paint(object sender, PaintEventArgs e)
        {
            DrawPanelBorder(e.ClipRectangle, e.Graphics);
        }

        private void pnlGrid_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;
            DrawPanelBorder(rect, e.Graphics);
            _grid.OnDraw(e.ClipRectangle, e.Graphics);
        }

        private void pnlColors_Paint(object sender, PaintEventArgs e)
        {
            DrawPanelBorder(e.ClipRectangle, e.Graphics);
        }

        void DrawPanelBorder(Rectangle rect, Graphics graphics)
        {
            rect.Inflate(-1, -1);
            graphics.DrawRectangle(_panelborderpen, rect);
        }
        

        private void PnlGrid_MouseWheel(object sender, MouseEventArgs e)
        {
            _grid.OnMouseWheel(e.Delta);
            pnlGrid.Invalidate();
        }

        bool _gridcapture = false;
        private void pnlGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pnlGrid.Capture = true;
                _gridcapture = true;
                _prev_mouse_pos = e.Location;
            }
            else
                _grid.OnMouseDown(e.Button, e.Location);
        }

        private void pnlGrid_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pnlGrid.Capture = false;
                _gridcapture = false;
            }
        }

        Point _prev_mouse_pos;
        private void pnlGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (_gridcapture)
            {
                Point shift = CGrid.Minus(e.Location, _prev_mouse_pos);
                _grid.OnMouseShift(shift);
                pnlGrid.Invalidate();
                _prev_mouse_pos = e.Location;
            }
        }
    }
}
