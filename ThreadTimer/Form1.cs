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

namespace ThreadTimer
{
    public partial class Form1 : Form
    {
        private int _countdown = 0;
        private int _counter = 0;
        private int _callTime = 2;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _countdown = 180;

            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            lbCounter.Invoke((MethodInvoker)(() => lbCounter.Text = $"Count down {_countdown}"));

            if (_countdown == 0)
            {
                timer1.Stop();
            }

            _countdown--;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            if (_counter % _callTime == 0)
            {
                Thread.Sleep(3000);
            }
            timer2.Start();
            _counter++;
        }
    }
}
