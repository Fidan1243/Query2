using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string Loaded { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            Person person = new Person
            {
                Name = nameBox.Text,
                Surname = snameBox.Text,
                Phone = PhoneBox.Text,
                Email = MailBox.Text,
                BirthDate = DateBox.Value
            };
            nameBox.Text = null;
            snameBox.Text = null;
            PhoneBox.Text = null;
            MailBox.Text = null;
            DateBox.Text = null;
            PersonList.Items.Add(person);
            PersonList.DisplayMember = "Name";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var item = PersonList.SelectedItem as Person;
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter($"{item.Name}.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(jw, item);
                }
            }
            if (!File.Exists("items.txt"))
            {
                using (FileStream fs = new FileStream("items.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    byte[] bytes = Encoding.Default.GetBytes($"{item.Name}.json ");
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                using (FileStream fs = new FileStream("items.txt", FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    byte[] bytes = Encoding.Default.GetBytes($"{item.Name}.json ");
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            MessageBox.Show("Saved!");


        }

        private void PersonList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("items.txt"))
            {
                Person person = new Person();
                string text;
                using (FileStream fs = new FileStream("items.txt", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);

                    text = Encoding.Default.GetString(bytes);
                }
                string[] texx = text.Split(' ');
                for (int i = 0; i < texx.Length; i++)
                {
                    var serializer = new JsonSerializer();
                    using (var sr = new StreamReader(texx[i]))
                    {
                        using (var jr = new JsonTextReader(sr))
                        {
                            person = serializer.Deserialize<Person>(jr);
                        }
                        PersonList.Items.Add(person);
                        PersonList.DisplayMember = "Name";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var item = PersonList.SelectedItem as Person;
            label6.Text = $"{item.Name}.json";
            nameBox.Text = item.Name;
            snameBox.Text = item.Surname;
            PhoneBox.Text = item.Phone;
            MailBox.Text = item.Email;
            DateBox.Value = item.BirthDate;
            button1.Enabled = false;
            change.Enabled = true;
            Loaded = $"{item.Name}.json";
            PersonList.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void change_Click(object sender, EventArgs e)
        {
            Person person = new Person
            {
                Name = nameBox.Text,
                Surname = snameBox.Text,
                Phone = PhoneBox.Text,
                Email = MailBox.Text,
                BirthDate = DateBox.Value
            };
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(Loaded))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(jw, person);
                }
            }
            PersonList.SelectedItem = person;
            PersonList.DisplayMember = "Name";
            nameBox.Text = null;
            snameBox.Text = null;
            PhoneBox.Text = null;
            MailBox.Text = null;
            DateBox.Text = null;
            button1.Enabled = true;
            PersonList.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            MessageBox.Show("Saved!");
        }
    }
}
