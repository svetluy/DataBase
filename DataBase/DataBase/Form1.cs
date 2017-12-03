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

        private void button1_Click(object sender, EventArgs e)
        {
            //$"Data Source=10.4.24.25:1522/orcl;User Id={userId};Password={password};"
            string oradb = $"Data Source = 127.0.0.1:1521; User Id = {textBox1.Text}; Password = {textBox2.Text};";
            bool IsOpen;

            OracleConnection conn = new OracleConnection(oradb);  // C#
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
                OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandText = "CREATE TABLE XMLDOCS" +
                  " (ИД NUMBER(5, 0) NOT NULL," +
                  " ЗАЧЕТКА VARCHAR2(10) NOT NULL," +
                  " ФИО VARCHAR2(30) NOT NULL," +
                  " ГРУППА_ИД NUMBER(4, 0) NOT NULL," +
                  " PRIMARY KEY(ИД)," +
                  " FOREIGN KEY(ГРУППА_ИД) REFERENCES ГРУППЫ); ",
                    CommandType = CommandType.Text
                };

                foreach (var xmlDoc in docs)
                {
                    cmd.CommandText = $"INSERT INTO XMLDOCS VALUES ({xmlDoc})";
                }
            }
            else
            {
                MessageBox.Show("Connection is not open");
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
                    docs.Add(xDoc);
                }
            }
        }
    }
}
