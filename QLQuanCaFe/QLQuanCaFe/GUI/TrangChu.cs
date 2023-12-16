using DAL_BLL;
using QLQuanCaFe.GUI;
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

namespace QLQuanCaFe
{
    public partial class TrangChu : Form
    {
        QLCFDataContext data = new QLCFDataContext();
        List<string> list_detail;
        public TrangChu()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void openChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();

            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pn_main.Controls.Add(childForm);
            pn_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btn_TaiBan_Click(object sender, EventArgs e)
        {
            //Kiểm tra có quyền mới cho vào
            if (checkper("ALL") == true || checkper("Thu Chi") == true)
            {
                openChildForm(new TaiBan());
            }
            else
            {
                MessageBox.Show("Bạn không có quyền để truy cập");
            }
        }

        private void btn_TaiQuay_Click(object sender, EventArgs e)
        {
            //Kiểm tra có quyền mới cho vào
            if (checkper("ALL") == true || checkper("Thu Chi") == true)
            {
                openChildForm(new DoUong());
            }
            else
            {
                MessageBox.Show("Bạn không có quyền để truy cập");
            }
        }

        private void btn_BaoCao_Click(object sender, EventArgs e)
        {
            //Kiểm tra có quyền mới cho vào
            if (checkper("ALL") == true || checkper("Bao Cao") == true)
            {
                openChildForm(new ThongKe());
            }
            else
            {
                MessageBox.Show("Bạn không có quyền để truy cập");
            }
        }

        private void btn_KhachHang_Click(object sender, EventArgs e)
        {
            //Kiểm tra có quyền mới cho vào
            if (checkper("ALL") == true || checkper("QL Khach Hang")==true)
            {
                openChildForm(new KhachHangGUI());
            }
            else
            {
                MessageBox.Show("Bạn không có quyền để truy cập");
            }

        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            //Kiểm tra có quyền mới cho vào
            lb_User.Text = FormLogin.NAME_USER;
            if (checkper("ALL") == true)
            {
                MessageBox.Show("Chào Admin");
                openChildForm(new Them());
            }
            else
            {
                MessageBox.Show("Bạn không có quyền để truy cập");
            }
        }

        private void ptb_TrangChu_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();

            }
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            //openChildForm(new TaiBan());
            list_detail = list_per(id_per(FormLogin.ID_USER));
            label1.Text= FormLogin.NAME_USER;
        }

        private void ban1_Click(object sender, EventArgs e)
        {

        }

        // user permision phân quyền
        //Lấy mã Nhóm Quyền
        private string id_per(string id_user)
        {
            string id = "";
            try
            {
                int idUr = int.Parse(id_user.ToString());
                var query = from per in data.tbl_per_relationships
                            where per.id_user_rel == idUr && per.suspended == false
                            select per.id_per_rel;
                id = query.FirstOrDefault().ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            return id;
        }
        //Danh Sách Quyền
        private List<string> list_per(string id_per)
        {
            List<string> termsList = new List<string>();
            try
            {
                int idP = int.Parse(id_per.ToString());
                var query = from perm in data.tbl_permision_dels
                            where perm.id_per == idP
                            select perm.code_action;

                termsList = query.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            return termsList;
        }
        //Check Quyền
        private Boolean checkper(string code)
        {
            Boolean check = false;
            foreach (string item in list_detail)
            {
                if (item == code)
                {
                    check = true;
                }
            }
            return check;
        }

        private void TrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc là muốn thoát không?", "Quản Lý Quán Cafe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc là muốn đăng xuất không không?", "Quản Lý Quán Cafe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FormLogin f = new FormLogin();
                f.Show();
                this.Hide();
                
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
