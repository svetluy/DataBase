using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace DataBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<XmlDocument> docs = new List<XmlDocument>();

        public List<XmlDocument> Docs { get => docs; set => docs = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            //$"Data Source=10.4.24.25:1522/orcl;User Id={userId};Password={password};"
            string oradb = $"Data Source = 127.0.0.1:1521; User Id = {textBox1.Text}; Password = {textBox2.Text};";
            bool IsOpen;

            using (OracleConnection conn = new OracleConnection(oradb))
            {
                try
                {
                    conn.Open();
                    IsOpen = true;
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                    IsOpen = false;
                }

                if (IsOpen)
                {
                    OracleCommand cmd = new OracleCommand();
                    int i = 0;
                    foreach (var xmlDoc in Docs)
                    {
                        cmd.CommandText = $"INSERT INTO xmld_buffer_xml_files VALUES ({i++}, {xmlDoc}, FILESYSTEM, TO_DATE('{DateTime.Now}','DD.MM.YYYY.MI.SS'))";
                    }
                }
                else
                {
                    MessageBox.Show("Connection is not open");
                }
            }

            //OracleDataReader dr = cmd.ExecuteReader();
            //dr.Read();
            //label1.Text = dr.GetString(0);
            //conn.Dispose();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] xmlDocs;
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "xml docs (*.xml)|*.xml"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                xmlDocs = openFile.FileNames;
                foreach (var xmlDoc in xmlDocs)
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(xmlDoc);
                    Docs.Add(xDoc);
                }
            }
        }
    }
}
