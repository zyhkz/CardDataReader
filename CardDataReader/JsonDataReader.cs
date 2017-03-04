using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDataReader
{
    class JsonDataReader
    {

        public static JObject ReadJsonText(string jsonText)
        {
            return (JObject)JsonConvert.DeserializeObject(jsonText);
        }

        public static JObject ReadJsonFile(string jsonfile)
        {
            using (FileStream fs = new FileStream(jsonfile, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    return ReadJsonText(reader.ReadToEnd());
                }
            }
        }

    }
}
