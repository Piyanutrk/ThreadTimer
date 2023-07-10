using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadTimer2
{

    public partial class Form1 : Form
    {
        private int countdownSeconds = 60; // Change this to the desired countdown time in seconds
        private HttpClient httpClient;
        private bool isCallApi = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            httpClient = new HttpClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblCountdown.Text = countdownSeconds.ToString();
            timer1.Start();

            //await CallApi();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            countdownSeconds -= 1;

            if (countdownSeconds <= 0)
            {
                // Stop the timer when the countdown reaches zero
                timer1.Stop();
            }

            lblCountdown.Text = countdownSeconds.ToString();

            // Call the API every 5 seconds in a separate background task
            if (countdownSeconds % 2 == 0)
            {
                if (!isCallApi)
                {
                    isCallApi = true;
                    await Task.Run(CallApi);
                    isCallApi = false;
                }
            }
        }

        private async Task CallApi()
        {
            try
            {
                // Make your API request here using the HttpClient class
                // For example:
                var response = await httpClient.GetAsync("https://www.google.com");

                Thread.Sleep(1000 * 5);
                // Process the API response as needed
                // For example:
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // Do something with the response content
                }
                else
                {
                    // Handle the API error
                    // For example:
                    MessageBox.Show("API request failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the API request
                MessageBox.Show("An error occurred during the API request: " + ex.Message);
            }
        }
    }
}
