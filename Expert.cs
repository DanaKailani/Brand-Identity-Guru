using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mommosoft.ExpertSystem;
using System.Diagnostics;

namespace expert
{



    public partial class Expert : Form
    {
        private Mommosoft.ExpertSystem.Environment Env = new Mommosoft.ExpertSystem.Environment();
        

        public Expert()
        {
            InitializeComponent();
            Env.AddRouter(new DebugRouter());
            Env.Load("D:\\AIU\\EXP\\expert\\colors.clp"); //Path to the clips program
            Env.Reset();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            NextUIState();
        }

        private void NextUIState()
        {
            nextButton.Visible = false;
            prevButton.Visible = false;
            choicesPanel.Controls.Clear();
            Env.Run();

            // Get the state-list.
            String evalStr = "(find-all-facts ((?f state-list)) TRUE)";
            using (FactAddressValue allFacts = (FactAddressValue)((MultifieldValue)Env.Eval(evalStr))[0])
            {
                string currentID = allFacts.GetFactSlot("current").ToString();
                evalStr = "(find-all-facts ((?f UI-state)) " +
                               "(eq ?f:id " + currentID + "))";
            }


            using (FactAddressValue evalFact = (FactAddressValue)((MultifieldValue)Env.Eval(evalStr))[0])
            {
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, pictureBox1.Width - 3, pictureBox1.Height - 3);
                Region rg = new Region(gp);
                pictureBox1.Region = rg;
                pictureBox2.Region = rg;
                pictureBox3.Region = rg;
                string state = evalFact.GetFactSlot("state").ToString();
                if (state.Equals("initial"))
                {
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    nextButton.Visible = true;
                    nextButton.Tag = "Next";
                    nextButton.Text = "Next";
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalPurple"))
                {
                    choicesPanel.Visible = false;
                    pictureBox1.BringToFront();
                    pictureBox1.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(318, 150);
                    pictureBox1.BackColor = Color.FromArgb(173, 3, 252);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalYellow"))
                {
                    pictureBox1.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(318, 150);
                    pictureBox1.BackColor = Color.FromArgb(255, 255, 38);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalGreen"))
                {
                    pictureBox1.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(318, 150);
                    pictureBox1.BackColor = Color.FromArgb(17, 217, 34);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalBlue"))
                {
                    pictureBox1.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(318, 150);
                    pictureBox1.BackColor = Color.FromArgb(38, 107, 255);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalRed"))
                {
                    pictureBox1.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(318, 150);
                    pictureBox1.BackColor = Color.FromArgb(255, 38, 38);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalBeige"))
                {
                    pictureBox1.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(318, 150);
                    pictureBox1.BackColor = Color.FromArgb(247, 231, 139);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalNavy"))
                {
                    pictureBox1.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(318, 150);
                    pictureBox1.BackColor = Color.FromArgb(22, 8, 66);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalOrange"))
                {
                    pictureBox1.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(318, 150);
                    pictureBox1.BackColor = Color.FromArgb(255, 157, 0);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalPink"))
                {
                    pictureBox1.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(318, 150);
                    pictureBox1.BackColor = Color.FromArgb(255, 0, 132);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalOrangeGreen"))
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(280, 150);
                    pictureBox2.Location = new Point(360, 150);
                    pictureBox1.BackColor = Color.FromArgb(255, 157, 0);
                    pictureBox2.BackColor = Color.FromArgb(17, 217, 34);
                    prevButton.Visible = false;
                }
                else if (state.Equals("finalGreenWhiteBeige"))
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    pictureBox1.Location = new Point(250, 150);
                    pictureBox2.Location = new Point(330, 150);
                    pictureBox3.Location = new Point(390, 150);
                    pictureBox1.BackColor = Color.FromArgb(17, 217, 34);
                    pictureBox2.BackColor = Color.White;
                    pictureBox3.BackColor = Color.FromArgb(247, 231, 139);
                    prevButton.Visible = false;
                }
                else
                {
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    nextButton.Visible = true;
                    nextButton.Tag = "Next";
                    prevButton.Tag = "Prev";
                    prevButton.Visible = true;
                }



                using (MultifieldValue validAnswers = (MultifieldValue)evalFact.GetFactSlot("valid-answers"))
                {
                    String selected = evalFact.GetFactSlot("response").ToString();
                    for (int i = 0; i < validAnswers.Count; i++)
                    {
                        RadioButton rb = new RadioButton();
                        rb.Text = (SymbolValue)validAnswers[i];
                        rb.Tag = rb.Text;
                        rb.Visible = true;
                        rb.Location = new Point(200, 30 * (i + 1));
                        choicesPanel.Controls.Add(rb);
                    }
                }
                messageLabel.Text = GetString((SymbolValue)evalFact.GetFactSlot("display"));

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ShowChoices(bool visible)
        {
            foreach (Control control in choicesPanel.Controls)
            {
                control.Visible = visible;
            }
        }

        private string GetString(string name)
        {
            return Form1.ResourceManager.GetString(name);
        }

        private RadioButton GetCheckedChoiceButton()
        {
            foreach (RadioButton control in choicesPanel.Controls)
            {
                if (control.Checked)
                {
                    return control;
                }
            }
            return null;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // Get the state-list.
            String evalStr = "(find-all-facts ((?f state-list)) TRUE)";
            using (FactAddressValue f = (FactAddressValue)((MultifieldValue)Env.Eval(evalStr))[0])
            {
                string currentID = f.GetFactSlot("current").ToString();

                if (button.Tag.Equals("Next"))
                {
                    if (GetCheckedChoiceButton() == null) { Env.AssertString("(next " + currentID + ")"); }
                    else
                    {
                        string temp = (string)GetCheckedChoiceButton().Tag;
                        temp = temp.Replace(' ', '_');
                        //MessageBox.Show(temp);
                        Env.AssertString("(next " + currentID + " " +
                                           temp + ")");
                    }
                    NextUIState();
                }
                else if (button.Tag.Equals("Restart"))
                {
                    Env.Reset();
                    NextUIState();
                }
                else if (button.Tag.Equals("Prev"))
                {
                    Env.AssertString("(prev " + currentID + ")");
                    NextUIState();
                }
            }
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // Get the state-list.
            String evalStr = "(find-all-facts ((?f state-list)) TRUE)";
            using (FactAddressValue f = (FactAddressValue)((MultifieldValue)Env.Eval(evalStr))[0])
            {
                string currentID = f.GetFactSlot("current").ToString();

                if (button.Tag.Equals("Next"))
                {
                    if (GetCheckedChoiceButton() == null) { Env.AssertString("(next " + currentID + ")"); }
                    else
                    {
                        Env.AssertString("(next " + currentID + " " +
                                           (string)GetCheckedChoiceButton().Tag + ")");
                    }
                    NextUIState();
                }
                else if (button.Tag.Equals("Restart"))
                {
                    Env.Reset();
                    NextUIState();
                }
                else if (button.Tag.Equals("Prev"))
                {
                    Env.AssertString("(prev " + currentID + ")");
                    NextUIState();
                }
            }
        }



    }
}
