using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace person_files
{
    public partial class Form1 : Form
    {
        List<Person> list;
        string filename;

        public Form1()
        {
            InitializeComponent();
            list = new List<Person>();
            list.Add(new Person("Roman", new DateTime(1991, 10, 25), "Programmer"));

            dataGridView1.DataSource = list;
            
        }

        //add new person
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();

            if (f.textBox1.Text.Length > 0 && f.textBox3.Text.Length > 0)
                list.Add(new Person(f.textBox1.Text, f.dateTimePicker1.Value, f.textBox3.Text));
            else
                MessageBox.Show("Wrong input data.");

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = list;
        }

        //edit person
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            f.dateTimePicker1.Value = (DateTime)dataGridView1.SelectedRows[0].Cells[2].Value;
            f.textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

            f.ShowDialog();
            list[dataGridView1.SelectedRows[0].Index].Name = f.textBox1.Text;
            list[dataGridView1.SelectedRows[0].Index].setAge(f.dateTimePicker1.Value);
            list[dataGridView1.SelectedRows[0].Index].Birthday = f.dateTimePicker1.Value;
            list[dataGridView1.SelectedRows[0].Index].Position = f.textBox3.Text;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = list;
        }

        //delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            list.RemoveAt(dataGridView1.SelectedRows[0].Index);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = list;
        }

        //write in file
        private void buttonWrite_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                filename = textBox1.Text + ".dat";
            else
                filename = "people.dat";
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, list);
                    MessageBox.Show("List of people was saved in file " + filename);
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }

            
        }

        //read from file
        private void buttonRead_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                filename = textBox1.Text + ".dat";
            else
                filename = "people.dat";
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    list = (List<Person>)formatter.Deserialize(fs);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = list;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
