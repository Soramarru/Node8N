using n8nClient.Controllers;
using n8nClient.Modeles;

namespace n8nClient
{
    public partial class AuthPage : Form
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            ApiResult res = await AuthController.AttemptLogin(txbUsername.Text, txbPassword.Text);
            lblOutput.Text = res.Message;
            lblOutput.ForeColor = res.getColorFromStatus();

        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            ApiResult res = await AuthController.AttemptRegister(txbUsername.Text, txbPassword.Text);
            lblOutput.Text = res.Message;
            lblOutput.ForeColor = res.getColorFromStatus();
        }
    }
}
