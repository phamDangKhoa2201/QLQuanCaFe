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
    public partial class QLTopping : Form
    {
        ToppingDAL_BLL tp = new ToppingDAL_BLL();
        public QLTopping()
        {
            InitializeComponent();
            List<Topping> tps = tp.getToppping();
            dgvKhachhang.DataSource = tps;
            txtTimkiem.Text = "Tìm kiếm theo tên...";
            txtTimkiem.ForeColor = System.Drawing.Color.Gray;
        }

        private void QLTopping_Load(object sender, EventArgs e)
        {
            dgvKhachhang.Columns[0].HeaderText = "Mã TP";
            dgvKhachhang.Columns[1].HeaderText = "Tên TP";
            dgvKhachhang.Columns[2].HeaderText = "Giá";
            List<Topping> khs = tp.getToppping();
            dgvKhachhang.DataSource = khs;
            txtSoDT.Enabled = false;
            txtTenKH.Enabled = false;
            txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbCheck.Text = "Sua";
            //txtMaKH.Enabled = true;
            txtSoDT.Enabled = true;
            txtTenKH.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
        }

        private void dgvKhachhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvKhachhang.Rows[e.RowIndex];
            int masv = int.Parse(row.Cells[0].Value.ToString());
            List<Topping> tps = new List<Topping>();
            tps = tp.getToppingTheoMa(masv);
            if (tps.Count > 0)
            {
                Topping topping = tps[0];
                txtMaKH.Text = topping.MaTopping.ToString();
                txtTenKH.Text = topping.Ten;
                txtSoDT.Text = topping.Gia.ToString();
            }
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimkiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                QLTopping_Load(sender, e);
            }
            else
            {
                List<Topping> khs = new List<Topping>();
                khs = tp.getToppingTheoTen(searchText);
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
            txtSoDT.Enabled = true;
            txtTenKH.Enabled = true;
            btnLuu.Enabled = true;
            button2.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (lbCheck.Text == "Them")
            {
                
                string ten = txtTenKH.Text.ToString().Trim();
                int sodt = int.Parse(txtSoDT.Text);
                if (string.IsNullOrEmpty(ten)  || string.IsNullOrEmpty(txtSoDT.Text))
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    
                        try
                        {
                            tp.ThemTP( ten, sodt);
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                            QLTopping_Load(sender, e);
                            txtMaKH.Text = "";
                            txtSoDT.Text = "";
                            txtTenKH.Text = ""; button2.Enabled = true;
                        }
                        catch (Exception) { MessageBox.Show("Thêm thất bại", "Thông báo", MessageBoxButtons.OK); }


                    }
                    

                }

            
            if (lbCheck.Text == "Sua")
            {
                int ma = int.Parse(txtMaKH.Text);
                string ten = txtTenKH.Text.ToString().Trim();
                int sodt = int.Parse(txtSoDT.Text);
                if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(txtMaKH.Text)  || string.IsNullOrEmpty(txtSoDT.Text))
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        tp.SuaTP(ma, ten, sodt);
                        MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                        QLTopping_Load(sender, e);
                        txtMaKH.Text = "";
                        txtSoDT.Text = "";
                        txtTenKH.Text = "";
                        btnThem.Enabled = true;
                    }
                    catch (Exception) { MessageBox.Show("Sửa thất bại", "Thông báo", MessageBoxButtons.OK); }
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaKH.Text);
            if (tp.KTMaTP(ma) == true)
            {
                MessageBox.Show("Mã Topping không hợp lệ. Vui lòng nhập lại!!", "Thông báo", MessageBoxButtons.OK);

            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        tp.SuaTPtheoCTTopping(ma);
                        tp.XoaTP(ma);
                        MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK);
                        QLTopping_Load(sender, e);
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
