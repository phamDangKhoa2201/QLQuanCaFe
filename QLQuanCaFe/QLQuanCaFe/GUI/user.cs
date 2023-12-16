using DAL_BLL;
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
    public partial class user : Form
    {
        //Bảng User
        QLCFDataContext data = new QLCFDataContext();
        public user()
        {
            InitializeComponent();
            loadData();
        }
        void loadData()
        {
            var user = from us in data.NhanViens select us;
            dataGridView1.DataSource = user;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNameUser.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtUserName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtPass.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtDiaChi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtSDT.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtLuong.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtPass.Text == "" || txtNameUser.Text == "")
            {
                MessageBox.Show("Dữ Liệu Đang Trống");
            }
            try
            {
                NhanVien us = new NhanVien();
                us.TenNhanVien = txtNameUser.Text;
                us.user_name = txtUserName.Text;
                us.pass = txtPass.Text;
                us.DiaChi = txtDiaChi.Text;
                us.SoDienThoai = txtSDT.Text;
                us.Email = txtEmail.Text;
                us.Luong = int.Parse(txtLuong.Text);
                if (KiemTraTrung1(us.user_name, us.pass) == 0)
                {
                    data.NhanViens.InsertOnSubmit(us);
                    data.SubmitChanges();
                    MessageBox.Show("Thêm Thành Công");
                }
                else
                {
                    MessageBox.Show("Tên Đăng Nhập Hoặc Mật khẩu đã tồn tại");
                }
                loadData();
            }
            catch (Exception)
            {

                MessageBox.Show("Thêm Thất Bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Không Được Để Trống ID_User");
                return;
            }
            NhanVien _us = data.NhanViens.Where(us => us.MaNhanVien == int.Parse(txtID.Text)).FirstOrDefault();
            if (_us != null)
            {
                tbl_per_relationship re = data.tbl_per_relationships.Where(us => us.id_user_rel == _us.MaNhanVien).FirstOrDefault();
                if (re != null)
                {
                    MessageBox.Show("Không thể xóa vì tài khoản đang được cấp quyền");
                }
                else
                {
                    data.NhanViens.DeleteOnSubmit(_us);
                    data.SubmitChanges();
                    MessageBox.Show("Xóa Thàng Công");
                    loadData();
                }
            }
            else
            {
                MessageBox.Show("Xóa Thất Bại");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Không Được Để Trống ID_User");
                return;
            }

            int id = int.Parse(txtID.Text);
            NhanVien existingUser = data.NhanViens.FirstOrDefault(t => t.MaNhanVien == id);

            if (existingUser != null)
            {
                // Lấy thông tin cũ của user
                string oldUsername = existingUser.user_name;
                string oldPassword = existingUser.pass;

                // Kiểm tra xem bạn đang cố gắng sửa tên đăng nhập và mật khẩu của người dùng khác
                bool isUpdatingOwnAccount = (oldUsername == txtUserName.Text && oldPassword == txtPass.Text);
                if (isUpdatingOwnAccount || KiemTraTrung2(txtUserName.Text, txtPass.Text, id) == 0)
                {
                    // Cập nhật thông tin mới từ giao diện
                    existingUser.TenNhanVien = txtNameUser.Text;
                    existingUser.user_name = txtUserName.Text;
                    existingUser.pass = txtPass.Text;
                    existingUser.DiaChi = txtDiaChi.Text;
                    existingUser.SoDienThoai = txtSDT.Text;
                    existingUser.Email = txtEmail.Text;
                    existingUser.Luong = int.Parse(txtLuong.Text);

                    data.SubmitChanges();
                    MessageBox.Show("Sửa Thành Công");
                }
                else if ((oldUsername != txtUserName.Text || oldPassword != txtPass.Text))
                {
                    MessageBox.Show("Tên Đăng Nhập Hoặc Mật khẩu đã tồn tại.");
                }

            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }



        private int KiemTraTrung2(string name, string pass, int id)
        {
            var query = from us in data.NhanViens
                        where us.MaNhanVien != id && us.user_name == name && us.pass == pass
                        select us.MaNhanVien;
            return query.Count();
        }
        private int KiemTraTrung1(string name, string pass)
        {
            var query = from us in data.NhanViens
                        where us.user_name == name && us.pass == pass
                        select us.MaNhanVien;
            return query.Count();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc là muốn thoát không?", "Quản Lý Quán Cafe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
