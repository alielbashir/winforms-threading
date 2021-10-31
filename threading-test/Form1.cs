using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace threading_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += new EventHandler(Button_Clicked);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var progress = new Progress<string>(s => label1.Text = s);
            await Task.Factory.StartNew(() => SecondThreadConcern.LongWork(progress),
                                        TaskCreationOptions.LongRunning);
            label1.Text = "completed";
        }
    }
}
class SecondThreadConcern
{
    public static void LongWork(IProgress<string> progress)
    {
        // Perform a long running work...
        for (var i = 0; i < 10; i++)
        {
            Task.Delay(500).Wait();
            progress.Report(i.ToString());
        }
    }
}