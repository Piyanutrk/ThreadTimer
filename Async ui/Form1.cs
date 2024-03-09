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

namespace Async_ui
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                MessageBox.Show("Another operation is already in progress.");
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _isRunning = true;

            //button1.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;

            try
            {
                await GetDataAsync(_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Operation was canceled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //button1.Enabled = true;
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.MarqueeAnimationSpeed = 0;
                _isRunning = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
        }

        private async Task GetDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(5000, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                throw new OperationCanceledException();
            }
        }


    }
}
