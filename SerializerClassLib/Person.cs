using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SerializerClassLib
{
    [Serializable]
    public class Person : ISerializable
    {
        public string Address { get; private set; }
        public string Name { get; private set; }
        public int PhoneNumber { get; private set; }
        public Nullable<int> SerialNumber { get; set; }
        public Nullable<DateTime> Created { get; private set; }

        public Person(string name, string address, int phoneNumber, Nullable<DateTime> created, Nullable<int> serialNumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            SerialNumber = serialNumber;
            Created = created;
        }

        public Person(string name, string address, int phoneNumber, DateTime created) : this(name, address, phoneNumber, created, null) { }

        // Deserialization constructor
        protected Person(SerializationInfo info, StreamingContext context)
        {
            Name = (string) info.GetValue("Name", typeof(string));
            Address = (string)info.GetValue("Address", typeof(string));
            PhoneNumber = (int)info.GetValue("PhoneNumber", typeof(int));
            Created = (DateTime)info.GetValue("Created", typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)

        {
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("Address", Address, typeof(string));
            info.AddValue("PhoneNumber", PhoneNumber, typeof(int));
            info.AddValue("Created", Created, typeof(DateTime));
        }
    }

}
