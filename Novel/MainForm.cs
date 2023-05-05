using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Novel
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure To Close Application?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        void onload()
        {
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            this.CenterToScreen();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MasterUser form = new MasterUser()
            {
                TopLevel = false,
                TopMost = true,
            }; 
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(form);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MasterNovel form = new MasterNovel()
            {
                TopLevel = false,
                TopMost = true,
            };
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(form);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MasterGenre form = new MasterGenre()
            {
                TopLevel = false,
                TopMost = true,
            };
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(form);
            form.Show();
        }
    }
}
