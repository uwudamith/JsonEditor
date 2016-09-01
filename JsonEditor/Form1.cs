using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace JsonEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "JSON files|*.json";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (theDialog.FileName.Trim() != string.Empty)
                    {
                        using (StreamReader r = new StreamReader(theDialog.FileName))
                        {
                            string json = r.ReadToEnd();
                            Tasks items = JsonConvert.DeserializeObject<Tasks>(json);
                            dgTasks.DataSource = items.tasks;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tasks tasks = new Tasks();
            tasks.tasks = (List<Task>)dgTasks.DataSource;
            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Filter = "JSON Image|*.json";
            saveFileDialog1.Title = "Save a JSON File";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(tasks, Formatting.Indented));
            }

        }
    }
}
