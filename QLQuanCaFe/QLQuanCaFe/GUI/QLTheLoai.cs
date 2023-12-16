using DAL_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaFe.GUI
{
    public partial class QLTheLoai : Form
    {
        TheLoaiDAL_BLL tl = new TheLoaiDAL_BLL();
        public QLTheLoai()
        {
            InitializeComponent();
            List<TheLoai> tps = tl.getTheLoai();
            dgvKhachhang.DataSource = tps;
            txtTimkiem.Text = "Tìm kiếm theo tên...";
            txtTimkiem.ForeColor = System.Drawing.Color.Gray;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void QLTheLoai_Load(object sender, EventArgs e)
        {
            dgvKhachhang.Columns[0].HeaderText = "Mã TL";
            dgvKhachhang.Columns[1].HeaderText = "Tên TL";

            List<TheLoai> khs = tl.getTheLoai();
            dgvKhachhang.DataSource = khs;
            txtTenKH.Enabled = false;
            txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbCheck.Text = "Sua";
            //txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
        }

        private void dgvKhachhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvKhachhang.Rows[e.RowIndex];
            int masv = int.Parse(row.Cells[0].Value.ToString());
            List<TheLoai> tps = new List<TheLoai>();
            tps = tl.getTheLoaiTheoMa(masv);
            if (tps.Count > 0)
            {
                TheLoai topping = tps[0];
                txtMaKH.Text = topping.MaTheLoai.ToString();
                txtTenKH.Text = topping.TenTheLoai;
            }
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimkiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                QLTheLoai_Load(sender, e);
            }
            else
            {
                List<TheLoai> khs = new List<TheLoai>();
                khs = tl.getTheLoaiTheoTen(searchText);
                dgvKhachhang.DataSource = khs;
            }
        }

        private void txtTimkiem_Leave(object sender, EventArgs e)
        {
            if (txtTimkiem.Text == "Tìm kiếm theo tên...")
            {
                txtTimkiem.Text = "";
                txtTimkiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimkiem_Enter(object sender, EventArgs e)
        {
            if (txtTimkiem.Text == "Tìm kiếm theo tên...")
            {
                txtTimkiem.Text = "";
                txtTimkiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lbCheck.Text = "Them";
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            btnLuu.Enabled = true;
            button2.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (lbCheck.Text == "Them")
            {
               // int ma = int.Parse(txtMaKH.Text);
                string ten = txtTenKH.Text.ToString().Trim();
                if (string.IsNullOrEmpty(ten))
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {

                    try
                    {
                        if (tl.KTTenTL(ten))
                        {
                            tl.ThemTL(ten);
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                            QLTheLoai_Load(sender, e);
                            txtMaKH.Text = "";
                            txtTenKH.Text = ""; button2.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Tên thể loại đã tồn tại", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                        
                    catch (Exception)
                    {
                        MessageBox.Show("Thêm thất bại", "Thông báo", MessageBoxButtons.OK);
                    }

                }

            }
            if (lbCheck.Text == "Sua")
            {
                int ma = int.Parse(txtMaKH.Text);
                string ten = txtTenKH.Text.ToString().Trim();
                if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(txtMaKH.Text) )
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        tl.SuaTL(ma, ten);
                        MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                        QLTheLoai_Load(sender, e);
                        txtMaKH.Text = "";
                        txtTenKH.Text = "";
                        btnThem.Enabled = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sửa thất bại", "Thông báo", MessageBoxButtons.OK);
                    }
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaKH.Text);
            if (tl.KTMaTP(ma) == true)
            {
                MessageBox.Show("Mã thể loại không hợp lệ. Vui lòng nhập lại!!", "Thông báo", MessageBoxButtons.OK);

            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        //tl.SuaSPtheoTheLoai(ma);
                        tl.XoaTP(ma);
                        MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK);
                        QLTheLoai_Load(sender, e);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Xoá thất bại", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    return;
                }

            }
        }
    }
}
