using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SerializerClassLib;

namespace Serializer
{
    public partial class SerializerForm : Form
    {
        PersonManager personManager;
        public SerializerForm()
        {
            InitializeComponent();
            IObjectSaver saver = new BinaryObjectSaver();
            personManager = new PersonManager(".", saver, Update, HandleMessage);
            personManager.LoadDefault();

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            personManager.Save(nameTextBox.Text, addressTextBox.Text, phoneTextBox.Text);
        }

        private void Update(string name, string address, string phone)
        {
            nameTextBox.Text = name;
            addressTextBox.Text = address;
            phoneTextBox.Text = phone;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            personManager.GoNext();
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            personManager.GoPrevious();

        }

        private void FirstButton_Click(object sender, EventArgs e)
        {
            personManager.GoFirst();
        }

        private void LastButton_Click(object sender, EventArgs e)
        {
            personManager.GoLast();
        }

        private void HandleMessage(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
