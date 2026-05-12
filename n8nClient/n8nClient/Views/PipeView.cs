using n8nClient.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace n8nClient.Views
{
    public partial class PipeView : Form
    {
        public PipeView()
        {
            InitializeComponent();
        }

        private async void PipeView_Load(object sender, EventArgs e)
        {
            // load every needed data
            lblGreet.Text = "Welcome : " + await PipeViewController.GetGreetings();

        }
    }
}
