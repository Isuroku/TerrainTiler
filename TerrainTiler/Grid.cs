using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TerrainTiler
{
    struct SCellInfo
    {
        public string tile;
        public int x;
        public int y;
    }

    class CCell
    {
        CGrid _owner;

        public Vector2i Coord { get; private set; }
        public CTileDescr TileDescr { get; private set; }
        
        public CCell(CGrid owner, Vector2i inCoord, CTileDescr inTileDescr)
        {
            _owner = owner;
            Coord = inCoord;
            TileDescr = inTileDescr;
        }

        public void SetTileDescr(CTileDescr inTileDescr)
        {
            TileDescr = inTileDescr;
        }

        public SCellInfo GetCellInfo()
        {
            return new SCellInfo() { tile = TileDescr.Terrain, x = Coord.x, y = Coord.y };
        }
    }

    class CGrid
    {
        Form1 _owner;

        int _cell_size;

        PointF _center;

        Dictionary<Vector2i, CCell> _cells;

        SizeF _char_size;

        public CGrid(Form1 owner)
        {
            _owner = owner;

            _cell_size = 30;

            _center = new PointF(0, 0);

            _cells = new Dictionary<Vector2i, CCell>();

            _char_size = SizeF.Empty;
        }

        public static Point Plus(Point p1, Point p2) { return new Point(p1.X + p2.X, p1.Y + p2.Y); }
        public static Point Minus(Point p1, Point p2) { return new Point(p1.X - p2.X, p1.Y - p2.Y); }

        Vector2i GetCenterCoord() { return new Vector2i((int)(_center.X * _cell_size), (int)(_center.Y * _cell_size)); }

        Rectangle GetCellRect(Rectangle inWindowRect, int inCoordX, int inCoordY)
        {
            Vector2i wndcenter = new Vector2i(inWindowRect.Size) / 2;

            Vector2i centercoord = GetCenterCoord();

            return GetCellRect(wndcenter, GetCenterCoord(), inCoordX, inCoordY);
        }

        Rectangle GetCellRect(Vector2i wndcenter, Vector2i centercoord, int inCoordX, int inCoordY)
        {
            Vector2i cell_pos = new Vector2i(inCoordX * _cell_size, -inCoordY * _cell_size);

            Vector2i wnd_cell_pos = wndcenter + centercoord + cell_pos;

            return new Rectangle(wnd_cell_pos.ToPoint(), new Size(_cell_size, _cell_size));
        }

        Dictionary<Color, Brush> _brushes = new Dictionary<Color, Brush>();

        Brush GetBrushByCellType(Color clr)
        {
            if (_brushes.ContainsKey(clr))
                return _brushes[clr];

            Brush br = new SolidBrush(clr);
            _brushes.Add(clr, br);
            return br;
        }

        internal void OnDraw(Rectangle rect, Graphics graphics)
        {
            //graphics.FillRectangle(_brYellow, rect);

            //float wnd_width_in_cells = rect.Width / (float)_cell_size;
            //float wnd_height_in_cells = rect.Height / (float)_cell_size;

            float left_part = rect.Width / 2 + _center.X * _cell_size;
            float right_part = rect.Width / 2 - _center.X * _cell_size;

            float top_part = rect.Height / 2 + _center.Y * _cell_size;
            float bottom_part = rect.Height / 2 - _center.Y * _cell_size;



            int left = (int)(-left_part / (float)_cell_size - 1);
            int right = (int)(right_part / (float)_cell_size + 1);
            int top = (int)(top_part / (float)_cell_size + 1);
            int bottom = (int)(-bottom_part / (float)_cell_size - 1);

            Vector2i wndcenter = new Vector2i(rect.Size) / 2;
            Vector2i centercoord = GetCenterCoord();

            for (int i = left; i <= right; i++)
                for (int j = bottom; j <= top; j++)
                {
                    if (!_cells.ContainsKey(new Vector2i(i, j)))
                    {
                        Rectangle rct = GetCellRect(wndcenter, centercoord, i, j);
                        DrawCell(rct, Color.LightBlue, Char.MinValue, graphics);
                    }
                }


            foreach (var c in _cells.Values)
            {
                Rectangle rct = GetCellRect(rect, c.Coord.x, c.Coord.y);
                DrawCell(rct, c.TileDescr.TileColor, c.TileDescr.TileMarker, graphics);
            };
            
        }

        Pen _panelborderpen = new Pen(Color.Black);

        Brush _brBlack = new SolidBrush(Color.Black);

        

        void DrawCell(Rectangle rect, Color color, char Marker, Graphics graphics)
        {
            graphics.FillRectangle(GetBrushByCellType(color), rect);
            graphics.DrawRectangle(_panelborderpen, rect);

            if(_char_size.IsEmpty)
            {
                _char_size = graphics.MeasureString(Marker.ToString(), _owner.GetCellFont());
            }
            PointF p = new PointF(rect.X + rect.Width / 2 - _char_size.Width / 2, rect.Y + rect.Height / 2 - _char_size.Height / 2);

            graphics.DrawString(Marker.ToString(), _owner.GetCellFont(), _brBlack, p);
        }

        Vector2i MouseCoordToCellCood(Rectangle inWindowRect, Point location)
        {
            var mouse_coord = new Vector2i(location);

            Vector2i wndcenter = new Vector2i(inWindowRect.Size) / 2;

            Vector2i mouse_loc_coord = mouse_coord - wndcenter;

            Vector2i centercoord = GetCenterCoord();

            Vector2i localcoord = new Vector2i(mouse_loc_coord.x - centercoord.x, mouse_loc_coord.y - centercoord.y);

            Vector2i cellcoord = new Vector2i(localcoord.x / _cell_size, -localcoord.y / _cell_size);
            if (localcoord.x < 0)
                cellcoord.x = cellcoord.x - 1;
            if (localcoord.y < 0)
                cellcoord.y = cellcoord.y + 1;

            return cellcoord;
        }

        internal void RemoveCell(Rectangle inWindowRect, Point location)
        {
            Vector2i cellcoord = MouseCoordToCellCood(inWindowRect, location);

            if (!_cells.ContainsKey(cellcoord))
                return;

            _cells.Remove(cellcoord);
            _owner.AddLogToConsole($"Remove cell: {cellcoord}", ELogLevel.INFO);
        }

        internal void AddCell(Rectangle inWindowRect, Point location, CTileDescr tiledescr)
        {
            Vector2i cellcoord = MouseCoordToCellCood(inWindowRect, location);

            if (_cells.ContainsKey(cellcoord))
            {
                CCell cell = _cells[cellcoord];
                if (cell.TileDescr != tiledescr)
                {
                    cell.SetTileDescr(tiledescr);
                    _owner.AddLogToConsole($"Change cell: {cellcoord}", ELogLevel.INFO);
                }
            }
            else
            {
                _cells.Add(cellcoord, new CCell(this, cellcoord, tiledescr));
                _owner.AddLogToConsole($"Add cell: {cellcoord}", ELogLevel.INFO);
            }
        }

        internal bool OnMouseWheel(int delta)
        {
            int cell_size = _cell_size + Math.Sign(delta) * 10;

            if (cell_size < 10)
                return false;
            if (cell_size > 100)
                return false;

            _cell_size = cell_size;
            return true;
        }

        internal void OnMouseShift(Point shift)
        {
            _center = new PointF(_center.X + shift.X / (float)_cell_size, _center.Y + shift.Y / (float)_cell_size);

            //_owner.AddLogToConsole(_center.ToString(), ELogLevel.INFO);
        }

        internal string Save(string filename, bool inNewFile)
        {
            if (inNewFile)
            {
                var di = new DirectoryInfo(Environment.CurrentDirectory);
                FileInfo[] fls = di.GetFiles(filename + Form1.FileExt);
                string suffix = "+";
                while (fls.Any())
                {
                    filename = filename + suffix;
                    fls = di.GetFiles(filename + Form1.FileExt);
                }
            }

            List < SCellInfo > cells = _cells.Select(c => c.Value.GetCellInfo()).ToList();
            JsonToFile<List<SCellInfo>>.Save(Environment.CurrentDirectory, filename + Form1.FileExt, cells);

            _owner.AddLogToConsole($"{filename} was saved", ELogLevel.IMPORTANT_INFO);

            return filename;
        }

        internal void Load(string filename)
        {
            _cells.Clear();

            List<SCellInfo> cells = JsonToFile<List<SCellInfo>>.Load(Environment.CurrentDirectory, filename + Form1.FileExt);
            if (cells == null || !cells.Any())
            {
                _owner.AddLogToConsole($"{filename} was empty", ELogLevel.IMPORTANT_INFO);
                return;
            }

            cells.ForEach(c =>
            {
                var cellcoord = new Vector2i(c.x, c.y);
                CTileDescr descr = _owner.GetTileDescrByTerrein(c.tile);
                if (descr != null)
                    _cells.Add(cellcoord, new CCell(this, cellcoord, descr));
                else
                    _owner.AddLogToConsole($"Can't find descr for {c.tile} in {cellcoord}", ELogLevel.WARNING);
            });

            _owner.AddLogToConsole($"{filename} was loaded", ELogLevel.IMPORTANT_INFO);
        }
    }
}
