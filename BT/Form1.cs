using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace BT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // BT is COM4
            string[] portName = SerialPort.GetPortNames();
            foreach (string name in portName)
            {
                comboBox1.Items.Add(name);
            }
            comboBox1.SelectedIndex = 0;

            serialPort1.BaudRate = 9600;
            textBox2.ScrollBars = ScrollBars.Vertical;

            button1.Text = "Open";
            button2.Text = "Close";
            button3.Text = "Send";
            button4.Text = "Clear";
            button5.Text = "Test";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                string data = serialPort1.ReadLine();
                textBox2.Invoke(new EventHandler(delegate
                {
                    textBox2.Text += data + '\n';
                }));
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("send;" + textBox1.Text);
            }
            else
            {
                textBox2.Text += "Not Connect!\r\n";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("test;");
            }
            else
            {
                textBox2.Text += "Not Connect!\r\n";
            }
        }
    }
}
