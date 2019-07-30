using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace SerializerClassLib
{
    public delegate void UpdateHandler(string name, string address, string phone);
    public delegate void MessageHandler(string msg);



    public class PersonManager
    {
        private UpdateHandler handleUpdate;
        private MessageHandler handleMessage;
        private IObjectSaver saver;
        private string dirPath;
        private FileInfo[] files;
        private DirectoryInfo root;
        private int maxSerial = 0;
        private int fileIndex = 0;

        public PersonManager(string dirPath, IObjectSaver saver, UpdateHandler updateHandler, MessageHandler errorHandler)
        {
            handleUpdate = updateHandler;
            handleMessage = errorHandler;
            this.saver = saver;
            this.dirPath = dirPath;
            root = new DirectoryInfo(dirPath);
            files = root.GetFiles();
        }
        public void Save(string name, string address, string phone)
        {
            int phoneNumber = 0;
            try
            {
                phoneNumber = int.Parse(phone);
                maxSerial++;
                Person person = new Person(name, address, phoneNumber, DateTime.Now, maxSerial);

                string fileName = string.Format(@"{0}\person{1}.pr", dirPath, person.SerialNumber);
                saver.Save(person, fileName);
                RefreshFileInfo();
                GoLast();
                validateSave(fileName);
                
            } catch (FormatException)
            {
                handleMessage("Invalid phone number");
            }

        }

        private void validateSave(string fileName)
        {
            FileInfo validate = new FileInfo(fileName);
            if (validate.Exists)
            {
                handleMessage("Person succesfully saved");
            }
            else
            {
                handleMessage("Save error!");
            }
        }

        private Person Load(FileInfo file)
        {
            Person person = (Person)saver.Load(file);

            
            person.SerialNumber = FetchSerial(file.Name);
            return person;
        }

        public void LoadDefault()
        {
            RefreshFileInfo();
            int length = files.Length;
            if (length > 0)
            {
                Person p = Load(files[length - 1]);
                fileIndex = files.Length - 1;
                Update(p);
            }
        }

        private void RefreshFileInfo()
        {
            files = root.GetFiles("*.pr");
            int max = 0;
            foreach (FileInfo file in files)
            {
                int serial = FetchSerial(file.Name);
                if (serial > max)
                {
                    max = serial;
                }
            }
            maxSerial = max;
        }

        private void Update(Person person)
        {
            handleUpdate(person.Name, person.Address, person.PhoneNumber.ToString());

        }

        private int FetchSerial(string filename)
        {
            Match m = Regex.Match(filename, @"(\d+).pr$");
            return  int.Parse(m.Groups[1].ToString());
        }

        private void MoveTo(int index)
        {
            try
            {
                Person p = Load(files[index]);
                Update(p);
                fileIndex = index;
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        public void GoNext()
        {
            MoveTo(fileIndex + 1);
        }

        public void GoPrevious()
        {
            MoveTo(fileIndex - 1);
        }

        public void GoFirst()
        {
            MoveTo(0);
        }

        public void GoLast()
        {
            MoveTo(files.Length - 1);
        }
    }
}
