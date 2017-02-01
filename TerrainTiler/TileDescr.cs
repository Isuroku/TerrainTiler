using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TerrainTiler
{
    public class CTileDescr: ISerializable
    {
        string _terrain;
        public Color TileColor { get; private set; }
        public string Descr { get; private set; }

        public string Terrain { get { return _terrain; } }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("terrain", _terrain, _terrain.GetType());
            info.AddValue("color", $"{TileColor.R},{TileColor.G},{TileColor.B}" , typeof(string));
            info.AddValue("descr", Descr, Descr.GetType());
        }

        public CTileDescr(SerializationInfo info, StreamingContext context)
        {
            _terrain = info.GetString("terrain");
            string scolor = info.GetString("color");
            string[] scolors = scolor.Split(new char[] { ',' });
            TileColor = Color.FromArgb(int.Parse(scolors[0].Trim()), int.Parse(scolors[1].Trim()), int.Parse(scolors[2].Trim()));
            Descr = info.GetString("descr");
        }

        public CTileDescr(string terrain, Color color, string descr)
        {
            _terrain = terrain;
            TileColor = color;
            Descr = descr;
        }
    }
}
