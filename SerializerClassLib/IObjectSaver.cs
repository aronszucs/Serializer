using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SerializerClassLib
{
    public interface IObjectSaver
    {
        void Save(object obj, string path);
        object Load(FileInfo file);
    }
}
