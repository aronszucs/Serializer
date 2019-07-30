using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace SerializerClassLib
{
    public class BinaryObjectSaver : IObjectSaver
    {
        public void Save(object obj, string path)
        {
            using (FileStream file = File.Create(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    ISerializable ser = (ISerializable) obj;
                    bf.Serialize(file, ser);
                }
                catch (InvalidCastException)
                {
                    throw new ArgumentException("object with implemented ISerializable expected");
                }
            }
        }

        public object Load(FileInfo file)
        {
            using (FileStream stream = File.Open(file.FullName, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(stream);
            }
        }
    }
}
