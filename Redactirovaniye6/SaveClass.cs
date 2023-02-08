using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RedactFile
{
    public class SaveClass
    {

        static JsonSruct toClass(Dictionary<int, string> text)
        {
            JsonSruct result = new JsonSruct();

            result.firstName = text[0];
            result.lastName = text[1];
            result.gender = text[2];
            result.age = Convert.ToInt32(text[3]);
            result.address.streetAddress = text[4];
            result.address.city = text[5];
            result.address.state = text[6];
            result.phoneNumbers.type = text[7];
            result.phoneNumbers.number = text[8];


            return result;
        }
        public static void saveFile(Dictionary<int, string> ser)
        {
            Console.WriteLine("¬ведите директорию в, которую сохраните файл:");
            string path = Console.ReadLine();
            string ext = Path.GetExtension(path);
            JsonSruct cl = toClass(ser);

            switch (ext)
            {
                case ".txt":

                    File.Delete(path);




                    foreach (var i in ser.Keys)
                    {
                        if (File.Exists(path))
                        {
                            File.AppendAllText(path, ser[i] + "\n");
                        }
                        else
                        {
                            File.WriteAllText(path, ser[i] + "\n");
                        }

                    }
                    break;

                case ".json":
                    string json = JsonConvert.SerializeObject(cl);
                    File.WriteAllText(path, json);
                    break;

                case ".xml":

                    JsonSruct js = new JsonSruct();
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(JsonSruct));
                        xml.Serialize(fs, cl);
                    }
                    break;
            }

            Console.Clear();
        }
    }
}