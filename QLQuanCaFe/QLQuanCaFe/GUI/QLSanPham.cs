using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL_BLL;
using System.IO;

namespace QLQuanCaFe.GUI
{
    public partial class QLSanPham : Form
    {
        DoUongDAL_BLL du = new DoUongDAL_BLL(); 
        TheLoaiDAL_BLL tl=new TheLoaiDAL_BLL();
        public QLSanPham()
        {
            InitializeComponent();
            List<SanPhamWithTheLoai> khs = du.GetallSanPhams();
            dgvSanPham.DataSource = khs;
            txtTimkiem.Text = "Tìm kiếm theo tên...";
            txtTimkiem.ForeColor = System.Drawing.Color.Gray;
        }

        private void QLSanPham_Load(object sender, EventArgs e)
        {
            dgvSanPham.Columns[0].HeaderText = "Mã SP";
            dgvSanPham.Columns[1].HeaderText = "Tên SP";
            dgvSanPham.Columns[2].HeaderText = "Giá Bán";
            dgvSanPham.Columns[3].HeaderText = "Mã TL";
            dgvSanPham.Columns[4].HeaderText = "Hình Ảnh";
            dgvSanPham.Columns[5].HeaderText = "Tên TL";
            List<SanPhamWithTheLoai> sps = du.GetallSanPhams();
            dgvSanPham.DataSource = sps;
            txtMaSP.Enabled = false;
            txtTenSP.Enabled = false;
            txtGiaBan.Enabled = false;
            //txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
            List<TheLoai> tls = tl.getTheLoai();
            cbbLoai.DataSource= tls;
            cbbLoai.DisplayMember = "TenTheLoai";
            cbbLoai.ValueMember = "MaTheLoai";
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r =dgvSanPham.CurrentCell.RowIndex; 
            DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
            int masv = int.Parse(row.Cells[0].Value.ToString());
            //byte[] b = (byte[])dgvSanPham.Rows[r].Cells[4].Value;
            
            //pictureBox1.Image=ByteArrayToImage(byteArray);
            List<SanPham> sps = new List<SanPham>();
            sps = du.GetSanPhamsTheoMa(masv);
            if (sps.Count > 0)
            {
                SanPham sanpham = sps[0];
                txtMaSP.Text = sanpham.MaSanPham.ToString();
                txtTenSP.Text = sanpham.TenSanPham;
                txtGiaBan.Text = sanpham.GiaTien.ToString();
                cbbLoai.Text = sanpham.TheLoai.TenTheLoai.ToString();
                //string hinhanh = sanpham.HinhAnh.ToString();
                //byte[] data = (byte[])sanpham.HinhAnh;
                if (sanpham.HinhAnh == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    System.Data.Linq.Binary binaryData = (System.Data.Linq.Binary)sanpham.HinhAnh;
                    byte[] byteArray = binaryData.ToArray();
                    pictureBox1.Image = ByteArrayToImage(byteArray);
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbCheck.Text = "Sua";
            //txtMaKH.Enabled = true;
            txtGiaBan.Enabled = true;
            txtTenSP.Enabled = true;
            //txtTenKH.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimkiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                QLSanPham_Load(sender, e);
            }
            else
            {
                List<SanPham> sps = new List<SanPham>();
                sps = du.GetSanPhamsTheoTen(searchText);
                dgvSanPham.DataSource = sps;
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
            txtMaSP.Enabled = true;
            txtTenSP.Enabled = true;
            txtGiaBan.Enabled = true;
            pictureBox1.Image = null;
            //txtTenKH.Enabled = true;
            btnLuu.Enabled = true;
            button2.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (lbCheck.Text == "Them")
            {
                //int ma = int.Parse(txtMaKH.Text);
                //byte[] b = ImageToByteArray(pictureBox1.Image);
                byte[] b = PathToArray(this.Text);
                string ten = txtTenSP.Text.ToString().Trim();
                decimal gia = decimal.Parse(txtGiaBan.Text); ;
                int matl = (int)cbbLoai.SelectedValue;
                if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(txtGiaBan.Text) )
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        if (du.KTTenSP(ten) == true)
                        {
                            du.ThemSP(ten, gia, matl, b);
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                            QLSanPham_Load(sender, e);
                            txtGiaBan.Text = "";
                            txtTenSP.Text = "";
                            txtMaSP.Text = "";
                            cbbLoai.SelectedItem = null;
                            //txtDiaChi.Text = "";
                            //txtTenKH.Text = ""; 
                            button2.Enabled = true;
                            pictureBox1.Image = null;
                        }
                        else
                        {
                            MessageBox.Show("Tên sản phẩm đã tồn tại", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                    catch(Exception) { MessageBox.Show("Thêm thất bại", "Thông báo", MessageBoxButtons.OK); }


                }

            }
            if (lbCheck.Text == "Sua")
            {
                int ma = int.Parse(txtMaSP.Text);
                //byte[] b = ImageToByteArray(pictureBox1.Image);
                byte[] b = PathToArray(this.Text);
                string ten = txtTenSP.Text.ToString().Trim();
                decimal gia = decimal.Parse(txtGiaBan.Text); ;
                int matl = (int)cbbLoai.SelectedValue;
                if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(txtMaSP.Text) || string.IsNullOrEmpty(txtGiaBan.Text) )
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        du.SuaSP(ma, ten, gia, matl, b);
                        MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                        QLSanPham_Load(sender, e);
                        txtMaSP.Text = "";
                        txtTenSP.Text = "";
                        txtGiaBan.Text = "";
                        cbbLoai.SelectedItem = null;
                        pictureBox1.Image = null;
                        btnThem.Enabled = true;
                    }
                    catch (Exception) { MessageBox.Show("Sửa thất bại", "Thông báo", MessageBoxButtons.OK); }
                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open =new OpenFileDialog();
            if(open.ShowDialog()==DialogResult.OK )
            {
                pictureBox1.Image=Image.FromFile(open.FileName);    
                this.Text= open.FileName;   
            }
        }
        byte[] ImageToByteArray(Image img)
        {
            MemoryStream m = new MemoryStream();
            img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            return m.ToArray(); 
        }
        byte[] PathToArray(string path)
        {
            //MemoryStream m = new MemoryStream();
            //Image img = Image.FromFile(path);
            //img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            //return m.ToArray();
            return File.ReadAllBytes(path);
        }
        Image ByteArrayToImage(byte[] data)
        {
            MemoryStream m = new MemoryStream(data);
            return Image.FromStream(m);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaSP.Text);
            if (du.KTMaSP(ma) == true)
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ. Vui lòng nhập lại!!", "Thông báo", MessageBoxButtons.OK);

            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        //du.SuaSPtheoCTHoaDon(ma);
                        du.XoaSP(ma);
                        MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK);
                        txtMaSP.Text = "";
                        txtTenSP.Text = "";
                        txtGiaBan.Text = "";
                        cbbLoai.SelectedItem = null;
                        QLSanPham_Load(sender, e);
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
