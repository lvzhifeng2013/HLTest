using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLTest
{
    public partial class TFTForm : Form
    {
        public TFTForm()
        {
            InitializeComponent();
        }

        List<byte> redList = new List<byte>();
        List<byte> greenList = new List<byte>();
        List<byte> blueList = new List<byte>();
        List<byte> whiteList = new List<byte>();


        private void Form1_Load(object sender, EventArgs e)
        {
            redList.Add(255);
            redList.Add(0);
            redList.Add(0);


            greenList.Add(0);
            greenList.Add(255);
            greenList.Add(0);


            blueList.Add(0);
            blueList.Add(0);
            blueList.Add(255);

            whiteList.Add(255);
            whiteList.Add(255);
            whiteList.Add(255);

            cmbOP.SelectedIndex = 0;
            serialPort1.PortName = "COM1";
            serialPort1.BaudRate = 38400;
            serialPort1.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TFTHelper.ClearScreenToBlue(serialPort1);
            Thread.Sleep(200);
            bool isQualified;
            if (radioButton1.Checked)
            {
                isQualified = true;
            }
            else
            {
                isQualified = false;
            }
            TFTHelper.WriteLCD(txtVIN.Text.Trim(), cmbOP.Text.Trim(), textBox1.Text.Trim(), textBox2.Text.Trim(), isQualified, serialPort1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TFTHelper.ClearScreenToBlue(serialPort1);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            TFTHelper.ClearScreenToGreen(serialPort1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TFTHelper.SetBackColor(serialPort1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TFTHelper.SetPenColor(whiteList, serialPort1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TFTHelper.SetDirection(true, serialPort1);
            //Thread.Sleep(3000);
            //TFTHelper.SetDirection(false, serialPort1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TFTHelper.SetBackLight(true, serialPort1);
            //Thread.Sleep(3000);
            TFTHelper.SetBackLight(false, serialPort1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TFTHelper.DrawCircle(50, 50, 10, serialPort1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TFTHelper.DrawLine(0, 100, 255, 100, serialPort1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TFTHelper.DrawPoint(100, 100, serialPort1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TFTHelper.SetAreaColor(0, 0, 50, 50, serialPort1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            TFTHelper.ClearScreenToBlue(serialPort1);
            Thread.Sleep(100);
            //TFTHelper.SendStr(txtVIN.Text.Trim(),2, serialPort1);
        }
    }
}
