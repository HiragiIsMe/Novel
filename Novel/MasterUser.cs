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
    public partial class MasterUser : Form
    {
        private int Condition, ID = 0;
        public MasterUser()
        {
            InitializeComponent();
        }
        void onload()
        {
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            textBoxPassword.UseSystemPasswordChar = true;
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
            textBoxPassword.Text = "";
            textBoxUsername.Text = "";
        }
        void Datagrid()
        {
            string query = "select * from Userr";
            dataGridView1.DataSource = Utility.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
        }
        private void MasterUser_Load(object sender, EventArgs e)
        {
            onload();
            Hidee();
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            Showw();
            Clear();
            dataGridView1.Enabled = false;
            Condition = 1;
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if(ID == 0)
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
               if(result == DialogResult.OK)
               {
                    SqlCommand cmd = new SqlCommand("delete from Userr where ID =" + ID + "", Utility.conn);
                    Utility.conn.Open();
                    cmd.ExecuteNonQuery();
                    Utility.conn.Close();

                    MessageBox.Show("Data Success Deleted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ID = 0;
                    Datagrid();
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
        bool ValidateIn()
        {
            if(textBoxtName.Text == "" || textBoxUsername.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("All Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SqlCommand cmd = new SqlCommand("select * from Userr where username=@username", Utility.conn);
            cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
            Utility.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                Utility.conn.Close();
                MessageBox.Show("Username Has Been Used", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Utility.conn.Close();

            if (textBoxPassword.TextLength < 8)
            {
                MessageBox.Show("Password Must Be At Least 8 Character", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        bool ValidateUp()
        {
            if (textBoxtName.Text == "" || textBoxUsername.Text == "")
            {
                MessageBox.Show("All Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(textBoxUsername.Text != dataGridView1.SelectedRows[0].Cells[2].Value.ToString())
            {
                SqlCommand cmd = new SqlCommand("select * from Userr where username=@username", Utility.conn);
                cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                Utility.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    Utility.conn.Close();
                    MessageBox.Show("Username Has Been Used", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                Utility.conn.Close();
            }

            if (textBoxPassword.Text != "")
            {
                if (textBoxPassword.TextLength < 8)
                {
                    MessageBox.Show("Password Must Be At Least 8 Character", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(Condition == 1 && ValidateIn())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into Userr values(@name,@username,@password)", Utility.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxtName.Text);
                    cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                    cmd.Parameters.AddWithValue("@password", Utility.enc(textBoxPassword.Text));
                    Utility.conn.Open();
                    cmd.ExecuteNonQuery();
                    Utility.conn.Close();

                    MessageBox.Show("Data Success Inserted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCancel.PerformClick();
                }
                catch(Exception ex)
                {
                    throw;
                }
            }

            if (Condition == 2 && ValidateUp())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update Userr set name=@name,username=@username,password=@password where id="+ ID +"", Utility.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxtName.Text);
                    cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                    if(textBoxPassword.Text != "")
                    {
                        cmd.Parameters.AddWithValue("@password", dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@password", Utility.enc(textBoxPassword.Text));
                    }
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

        private void textBoxSerach_TextChanged(object sender, EventArgs e)
        {
            string query = "select * from Userr where name like '%" + textBoxSerach.Text + "%' or username like '%" + textBoxSerach.Text + "%'";
            dataGridView1.DataSource = Utility.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {

            }
            else
            {
                dataGridView1.CurrentRow.Selected = true;
                ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                textBoxtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxUsername.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }
    }
}
