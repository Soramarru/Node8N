
using n8nClient.Controllers;
using n8nClient.Modeles;
using n8nClient.Requesters;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace n8nClient.Views
{
    public partial class AddTask : Form
    {
        string mode = "nodeBlock";
        Pipeline? pipeline;
        List<string>? requirementList;


        public AddTask(Pipeline selPipeline)
        {
            InitializeComponent();
            pipeline = selPipeline;
        }

        private void AddTask_Load(object sender, EventArgs e)
        {
            lblEdit.Text = "Editing : " + pipeline?.PipelineName;
            RefreshCurrentTask();
            RefreshRequirement();
        }

        private void RefreshCurrentTask()
        {
            pipeline?.TaskList.ForEach(task => { lbxCurrentTask.Items.Add(task.TaskName); });
        }

        private async void RefreshRequirement()
        {
            lbxTasker.Items.Clear();
            Console.WriteLine(mode);
            if(mode == "nodeBlock")
                requirementList = await RequirementRequests.GetReqNodesNameList();
            else if(mode == "conditionalBlock")
                requirementList = await RequirementRequests.GetReqLogicalNameList();
            if (requirementList == null)
                return;
            foreach (string name in requirementList)
            {
                lbxTasker.Items.Add(name);
            }
        }

        private async void lbxTasker_DoubleClick(object sender, EventArgs e)
        {
            int i = lbxTasker.SelectedIndex;
            if (i == -1)
                return;
            if (requirementList == null)
                return;

            NodeRequirements? req = null;
            if (mode == "nodeBlock")
                req = await RequirementRequests.GetNodeJsonReq(requirementList[i]);
            else if (mode == "conditionalBlock")
                req = await RequirementRequests.GetLogicalJsonReq(requirementList[i]);
            if (req == null)
                return;
            StringBuilder nodeInfo = new StringBuilder();
            nodeInfo.AppendLine("Title : " + req.Title);
            nodeInfo.AppendLine("Description : " + req.Description);
            MessageBox.Show(nodeInfo.ToString());
        }

        private void lbxCurrentTask_DoubleClick(object sender, EventArgs e)
        {
            int i = lbxCurrentTask.SelectedIndex;
            if (i == -1)
                return;
            // technically cant happend but clear the warning
            if (pipeline == null)
                return;
            MessageBox.Show(pipeline.TaskList[i].ToString());

        }

        private async void btnAddToPipe_Click(object sender, EventArgs e)
        {
            int i = lbxTasker.SelectedIndex;
            if (i == -1)
                return;
            if (requirementList == null)
                return;
            if (pipeline == null)
                return;
            if (pipeline.MongoId == null)
                return;
            ApiResult? result = await AddTaskController.addTaskToPipe(mode , requirementList[i] , pipeline.MongoId);
            // user stopped it
            if (result == null)
                return;
            if (result.Status == "success")
                MessageBox.Show("Task : " + requirementList[i] + " added to pipeline !");
            else
                MessageBox.Show("Error : " + result.Message);
        }

        private void btnSwitchTo_Click(object sender, EventArgs e)
        {
            mode = mode == "nodeBlock" ? "conditionalBlock" : "nodeBlock";
            btnSwitchTo.Text = btnSwitchTo.Text == "Switch to conditional block" ? "Switch to nodes block" : "Switch to conditional block";
            RefreshRequirement();
        }
    }
}
