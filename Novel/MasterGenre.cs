using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Novel
{
    public partial class MasterGenre : Form
    {
        private int Condition, ID = 0;
        public MasterGenre()
        {
            InitializeComponent();
        }
        void onload()
        {
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            Datagrid();
        }
        void Showw()
        {
            panelForm.Show();
            buttonIn.Hide();
            buttonUp.Hide();
            buttonDel.Hide();
        }
        void Hidee()
        {
            panelForm.Hide();
            buttonIn.Show();
            buttonUp.Show();
            buttonDel.Show();
        }
        void Clear()
        {
            textBoxtName.Text = "";
        }
        void Datagrid()
        {
            string query = "select * from Genre";
            dataGridView1.DataSource = Utility.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Visible = false;
        }
        private void MasterGenre_Load(object sender, EventArgs e)
        {
            onload();
            Hidee();
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (ID == 0)
            {
                MessageBox.Show("Please Select One Row To Update Data", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Showw();
                Condition = 2;
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (ID == 0)
            {
                MessageBox.Show("Please Select One Row To Delete Data", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " ?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd = new SqlCommand("delete from Genre where ID =" + ID + "", Utility.conn);
                    Utility.conn.Open();
                    cmd.ExecuteNonQuery();
                    Utility.conn.Close();

                    MessageBox.Show("Data Success Deleted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ID = 0;
                    Datagrid();
                }
            }
        }
        bool validate()
        {
            if(textBoxtName.Text == "")
            {
                MessageBox.Show("All Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Condition == 1 && validate())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into Genre values(@name)", Utility.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxtName.Text);
                    Utility.conn.Open();
                    cmd.ExecuteNonQuery();
                    Utility.conn.Close();

                    MessageBox.Show("Data Success Inserted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCancel.PerformClick();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            if (Condition == 2 && validate())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update Genre set genre=@name where id=" + ID + "", Utility.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxtName.Text);
                    Utility.conn.Open();
                    cmd.ExecuteNonQuery();
                    Utility.conn.Close();

                    MessageBox.Show("Data Success Updated", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCancel.PerformClick();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Clear();
            Condition = 0;
            ID = 0;
            Hidee();
            Datagrid();
            dataGridView1.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {

            }
            else
            {
                dataGridView1.CurrentRow.Selected = true;
                ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                textBoxtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            Showw();
            Clear();
            dataGridView1.Enabled = false;
            Condition = 1;
        }
    }
}
