using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace TerrainTiler
{
    public static class JsonToFile<T> where T : new()
    {
        public static void Save(string inFilePath, string inFileName, T inObject)
        {
            string sFP = inFilePath + "\\" + inFileName;

            string s = JsonConvert.SerializeObject(inObject, Formatting.Indented);

            //var coder = new UnicodeEncoding();
            var coder = UTF8Encoding.GetEncoding(1251);
            Byte[] encodedBytes = coder.GetBytes(s);

            using (FileStream fs = File.Open(sFP, FileMode.Create, FileAccess.Write))
            {
                fs.Write(encodedBytes, 0, encodedBytes.Length);
            }
        }

        public static T Load(string inFilePath, string inFileName)
        {
            string sFP = inFilePath + "\\" + inFileName;
            if (!File.Exists(sFP))
            {
                var instance = new T();
                Save(inFilePath, inFileName, instance);
                return instance;
            }

            //var coder = new UnicodeEncoding(false, true);
            var coder = UTF8Encoding.GetEncoding(1251);

            byte[] buff = null;
            using (FileStream fs = File.Open(sFP, FileMode.Open, FileAccess.Read))
            {
                buff = new byte[fs.Length];
                if (fs.Read(buff, 0, buff.Length) == 0)
                    buff = null;
            }

            if (buff != null)
            {
                string s = coder.GetString(buff);
                var instance = JsonConvert.DeserializeObject<T>(s);
                return instance;
            }

            return default(T);
        }
    }
}
