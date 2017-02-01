using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.Collections;

namespace TerrainTiler
{
    [JsonObject(MemberSerialization.Fields)]
    public class CAppSettings: IEnumerable<CTileDescr>
    {
        List<CTileDescr> _tiles;
        string _unitypath;

        public string PathToUnity { get { return _unitypath; } }

        public CAppSettings()
        {
            _tiles = new List<CTileDescr>();
            _tiles.Add(new CTileDescr("t1", Color.FromArgb(10, 20, 50), "d1"));
            _tiles.Add(new CTileDescr("t2", Color.FromArgb(100, 200, 150), "d2"));

            _unitypath = "/";
        }

        public override string ToString()
        {
            if (_tiles == null)
                return "empty";
            string s = $"tile count {_tiles.Count}";
            return s;
        }

        public IEnumerator<CTileDescr> GetEnumerator()
        {
            if (_tiles == null)
                _tiles = new List<CTileDescr>();
            return _tiles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_tiles == null)
                _tiles = new List<CTileDescr>();
            return _tiles.GetEnumerator();
        }
    }
}
