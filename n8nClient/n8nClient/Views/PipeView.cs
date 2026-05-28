using n8nClient.Controllers;
using n8nClient.Modeles;
using n8nClient.Requesters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Windows.UI.Xaml.Controls;

namespace n8nClient.Views
{
    public partial class PipeView : Form
    {
        List<Pipeline>? pipelineList;

        public PipeView()
        {
            InitializeComponent();
        }

        private async void PipeView_Load(object sender, EventArgs e)
        {
            // load every needed data
            lblGreet.Text = "Welcome : " + await PipeViewController.GetGreetings();
            refreshPipeList();
        }

        private async void refreshPipeList()
        {
            lbxTask.Items.Clear();
            pipelineList = await PipeViewController.GetPipeList();
            foreach (Pipeline pipe in pipelineList)
                lbxTask.Items.Add(pipe.PipelineName);
        }

        private void lbxTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxTask.SelectedIndex == -1)
                return;
            if (pipelineList == null)
                return;
            Pipeline selPipe = pipelineList[lbxTask.SelectedIndex];
        }

        private void lbxTask_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbxTask.SelectedIndex == -1)
                return;
            if (pipelineList == null)
                return;
            Pipeline selPipe = pipelineList[lbxTask.SelectedIndex];
            string toShow = selPipe.PipelineDesc + " \nNumber of task : " + selPipe.TaskList.Count;
            MessageBox.Show(toShow, "Description");
        }

        private async void btnDelPiepline_Click(object sender, EventArgs e)
        {
            int targetIndex = lbxTask.SelectedIndex;
            if (targetIndex == -1)
            {
                MessageBox.Show("You must select a pipeline !");
                return;
            }
            Pipeline selPipe = pipelineList[targetIndex];
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this task ? '" + selPipe.PipelineName + "'", "Delete task", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ApiResult res = await PipelineRequests.DeletePipelineById(selPipe.MongoId);
                refreshPipeList();
                MessageBox.Show(res.Message);
            }
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            int targetIndex = lbxTask.SelectedIndex;
            if (targetIndex == -1)
            {
                MessageBox.Show("You must select a pipeline !");
                return;
            }
            if (pipelineList == null)
                return;
            Pipeline selPipe = pipelineList[targetIndex];
            AddTask formAddTask = new AddTask(selPipe);
            formAddTask.ShowDialog();
            refreshPipeList();
        }

        private async void btnCreatePipe_Click(object sender, EventArgs e)
        {
            string mongoId = await PipeViewController.HandlePipeCreation();
            // user left it or the result failed
            if (mongoId == "")
                return;
            if (pipelineList == null)
                return;
            int index = pipelineList.FindIndex(pipe => pipe.MongoId == mongoId);
            refreshPipeList();
            lbxTask.SelectedIndex = index;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            refreshPipeList();
        }
    }
}
