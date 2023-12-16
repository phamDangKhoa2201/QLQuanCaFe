using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaFe.GUI
{
    public partial class User_PhanQuyen : Form
    {
        public User_PhanQuyen()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_body.Controls.Add(childForm);
            panel_body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new user());
            label1.Text = "NHÂN VIÊN";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new permision());
            label1.Text = "Nhóm Quyền";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Per_Detail());
            label1.Text = "Chi Tiết Nhóm Quyền";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Per_relationship());
            label1.Text = "Mối quan hệ Nhóm Quyền Và User";
        }

        private void User_PhanQuyen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc là muốn thoát không?", "Quản Lý Quán Cafe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
