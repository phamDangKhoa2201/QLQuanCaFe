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
using DAL_BLL;

namespace QLQuanCaFe.GUI
{
    public partial class FormLogin : Form
    {
        QLCFDataContext data = new QLCFDataContext();
        public FormLogin()
        {
            InitializeComponent();
            txtUser.Focus();
        }
        public static string ID_USER = "";
        public static string NAME_USER = "";
        //Lấy ID
        private string getID(string username, string pass)
        {
            string id = "";
            try
            {
                var query = from user in data.NhanViens
                            where user.user_name == username && user.pass == pass
                            select user.MaNhanVien;
                id = query.FirstOrDefault().ToString();
               
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            return id;
        }
        //Lấy Tên Người Đăng Nhập
        private string getName(string username, string pass)
        {
            string id = "";
            try
            {
                var query = from user in data.NhanViens
                            where user.user_name == username && user.pass == pass
                            select user.TenNhanVien;
                id = query.FirstOrDefault().ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xảy ra: " + ex.Message);
            }
            return id;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ID_USER = getID(txtUser.Text, txtPass.Text);  
            if (!string.IsNullOrEmpty(ID_USER) && ID_USER!="0")
            {
                NAME_USER = getName(txtUser.Text, txtPass.Text);
                TrangChu fmain = new TrangChu();
                fmain.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tài khoản và mật khẩu không đúng !");
            }
        }
        //Hiển Thị Mật Khẩu
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPass.PasswordChar = (char)0;
            }
            else
            {
                txtPass.PasswordChar = '*';
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc là muốn thoát không?", "Quản Lý Quán Cafe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
