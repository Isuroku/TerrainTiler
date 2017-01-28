using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerrainTiler
{
    public enum ECellType { Plain, Hill }

    class CCell
    {
        CGrid _owner;

        public Vector2i Coord { get; private set; }
        public ECellType CellType { get; private set; }
        
        public CCell(CGrid owner, Vector2i inCoord, ECellType inCellType)
        {
            _owner = owner;
            Coord = inCoord;
            CellType = inCellType;
        }
    }

    class CGrid
    {
        Form1 _owner;

        int _cell_size;

        PointF _center;

        List<CCell> _cells;

        public CGrid(Form1 owner)
        {
            _owner = owner;

            _cell_size = 30;

            _center = new PointF(0, 0);

            _cells = new List<CCell>();
            _cells.Add(new CCell(this, new Vector2i(0, 0), ECellType.Plain));
            _cells.Add(new CCell(this, new Vector2i(-1, -1), ECellType.Hill));
            _cells.Add(new CCell(this, new Vector2i(1, 1), ECellType.Hill));
        }

        public static Point Plus(Point p1, Point p2) { return new Point(p1.X + p2.X, p1.Y + p2.Y); }
        public static Point Minus(Point p1, Point p2) { return new Point(p1.X - p2.X, p1.Y - p2.Y); }

        Rectangle GetCellRect(Rectangle inWindowRect, Vector2i inCoord)
        {
            Vector2i wndcenter = new Vector2i(inWindowRect.Size) / 2;

            Vector2i centercoord = new Vector2i((int)(_center.X * _cell_size), (int)(-_center.Y * _cell_size));

            Vector2i cell_pos = inCoord * _cell_size;

            Vector2i wnd_cell_pos = wndcenter + centercoord + cell_pos;

            return new Rectangle(wnd_cell_pos.ToPoint(), new Size(_cell_size, _cell_size));
        }

        Brush _brHill = new SolidBrush(Color.Brown);
        Brush _brPlain = new SolidBrush(Color.Green);

        Brush GetBrushByCellType(ECellType ct)
        {
            switch(ct)
            {
                case ECellType.Hill: return _brHill;
                case ECellType.Plain: return _brPlain;
            }
            return null;
        }

        internal void OnDraw(Rectangle rect, Graphics graphics)
        {

            _cells.ForEach(c =>
            {
                Rectangle rct = GetCellRect(rect, c.Coord);

                graphics.FillRectangle(GetBrushByCellType(c.CellType), rct);
            });
            
        }

        internal void OnMouseDown(MouseButtons button, Point location)
        {
            
        }

        internal void OnMouseWheel(int delta)
        {
            _cell_size += Math.Sign(delta) * 10;
        }

        internal void OnMouseShift(Point shift)
        {
            _center = new PointF(_center.X + shift.X / (float)_cell_size, _center.Y - shift.Y / (float)_cell_size);
        }
    }
}
