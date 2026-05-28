using n8nClient.Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Uno.UI.Xaml;
using Windows.UI.Xaml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace n8nClient.Views
{
    public partial class MultiInputForm : Form
    {
        ValidationConstraint rules;
        public dynamic? result;

        public MultiInputForm(ValidationConstraint constraint)
        {
            InitializeComponent();
            rules = constraint;
        }

        /// <summary>
        /// Check the requested type form and adapt it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiInputForm_Load(object sender, EventArgs e)
        {
            // load and build the form in real time
            switch (rules.WantedType.ToLower())
            {
                case "string":
                    // do nothing its the base form
                    break;
                case "boolean":
                case "bool":
                    SwapInputToRadio();
                    break;
                case "int":
                case "float":
                case "double":
                    SwapInputToNumPick();
                    break;

            }
            setupForm();
        }
        /// <summary>
        /// Change the basic rich text box to a radio button
        /// </summary>
        private void SwapInputToRadio()
        {
            Point baseLoc = rtbInputString.Location;
            RadioButton myRadio1 = new RadioButton();
            myRadio1.Text = "true";
            RadioButton myRadio2 = new RadioButton();
            myRadio2.Text = "false";
            myRadio1.Location = baseLoc;
            myRadio2.Location = new Point(baseLoc.X, baseLoc.Y + myRadio1.Height + 20);
            this.Controls.Add(myRadio1);
            this.Controls.Add(myRadio2);
            this.Controls.Remove(rtbInputString);
            // since placeholder cant be held on the input i added a "subtitle" 
        }
        /// <summary>
        /// Change the basic rich text box to a numeric up down
        /// </summary>
        private void SwapInputToNumPick()
        {
            NumericUpDown myNumUpD = new NumericUpDown();
            myNumUpD.Location = new Point(this.Width / 2 - myNumUpD.Width, this.Height / 2 - myNumUpD.Height);
            Console.WriteLine(myNumUpD.Location);
            this.Controls.Add(myNumUpD);
            this.Controls.Remove(rtbInputString);
        }

        /// <summary>
        /// Setup the question for the user to answer
        /// </summary>
        private void setupForm()
        {
            lblTitle.Text = rules.Title;
            if(rules.Required)
                lblTitle.Text += "*";
            lblSubTitle.Text = rules.Placeholder;
        }

        /// <summary>
        /// Return the expected type of control shown on the form 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string getTypeOfControl()
        {
            return rules.WantedType switch
            {
                "string" => typeof(RichTextBox).ToString(),
                "boolean" => typeof(RadioButton).ToString(),
                "bool" => typeof(RadioButton).ToString(),
                "int" => typeof(NumericUpDown).ToString(),
                "float" => typeof(NumericUpDown).ToString(),
                "double" => typeof(NumericUpDown).ToString(),
                _ => throw new NotImplementedException()
            };
        }


        /// <summary>
        /// Do work depending on the awaited type , search for the wanted control try to cast it to the wanted type and return it 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            string wantedControlType = getTypeOfControl();
            List<Control> target = Controls.Cast<Control>().Where(c => c.GetType().ToString() == wantedControlType).ToList();
            if (target.Count == 0)
                displayError("Form is likely broken try again");
            // in this case we need to check both
            if (target.Count == 2 && wantedControlType == typeof(RadioButton).ToString())
            {
                RadioButton trueButton = (RadioButton)target[0];
                RadioButton falseButton = (RadioButton)target[1];
                // radio button check
                if (!trueButton.Checked && !falseButton.Checked)
                    displayError("You must check a case !");
                else
                    result = trueButton.Checked;
            }
            else if (wantedControlType == typeof(NumericUpDown).ToString())
            {
                NumericUpDown numUd = (NumericUpDown)target[0];
                result = Convert.ToInt32(numUd.Value);
            }
            else if (wantedControlType == typeof(RichTextBox).ToString())
            {

                RichTextBox rtbRes = (RichTextBox)target[0];
                string inputRes = rtbRes.Text;
                if (rules.Required && inputRes == "")
                {
                    MessageBox.Show("An input is required !");
                    return;
                }
                result = inputRes;
            } 
            else
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void displayError(string error)
        {
            lblErrorDisplay.Text = error;
        }

    }
}
