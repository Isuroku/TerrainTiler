using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainTiler
{
    public struct Vector2i : IComparable<Vector2i>
    {
        public int x;
        public int y;

        public Vector2i(Vector2i inCellCoords)
        {
            x = inCellCoords.x;
            y = inCellCoords.y;
        }

        public Vector2i(Point inCellCoords)
        {
            x = inCellCoords.X;
            y = inCellCoords.Y;
        }

        public Vector2i(Size inCellCoords)
        {
            x = inCellCoords.Width;
            y = inCellCoords.Height;
        }

        public Vector2i(int inx, int iny)
        {
            x = inx;
            y = iny;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", x, y);
        }

        public Point ToPoint()
        {
            return new Point(x, y);
        }

        public Vector2i GetShift(int shx, int shy)
        {
            return new Vector2i(x + shx, y + shy);
        }

        public long GetKey()
        {
            long r = x;
            r = r << 32;
            r += y;
            return r;
        }

        public int CompareTo(Vector2i other)
        {
            if (x != other.x)
                return x.CompareTo(other.x);
            return y.CompareTo(other.y);
        }

        public bool Equals(Vector2i other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector2i)obj);
        }

        //https://en.wikipedia.org/wiki/Pairing_function#Cantor_pairing_function
        public override int GetHashCode()
        {
            return (x + y) * (x + y + 1) / 2 + y;
        }

        public static bool operator ==(Vector2i left, Vector2i right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vector2i left, Vector2i right)
        {
            return !Equals(left, right);
        }

        public static Vector2i operator +(Vector2i left, Vector2i right)
        {
            return new Vector2i(left.x + right.x, left.y + right.y);
        }

        public static Vector2i operator -(Vector2i left, Vector2i right)
        {
            return new Vector2i(left.x - right.x, left.y - right.y);
        }

        public static Vector2i operator /(Vector2i left, int div)
        {
            return new Vector2i(left.x / div, left.y / div);
        }

        public static Vector2i operator *(Vector2i left, int mul)
        {
            return new Vector2i(left.x * mul, left.y * mul);
        }
    }
}
