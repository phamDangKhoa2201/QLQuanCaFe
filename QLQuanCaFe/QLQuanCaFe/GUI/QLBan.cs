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

namespace QLQuanCaFe.GUI
{
    public partial class QLBan : Form
    {
        QLCFDataContext data=new QLCFDataContext();
        Ban_DAL_BLL bn=new Ban_DAL_BLL();
        HoaDonDAL_BLL hd = new HoaDonDAL_BLL();
        public QLBan()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            dataGridView1.DataSource= bn.getBan();
        }

        private void QLBan_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Tên Đang Trống");
            }
            try
            {
                Ban t1=data.Bans.Where(x=>x.Name==txtName.Text.Trim()).FirstOrDefault();
                if(t1!=null)
                {
                    MessageBox.Show($"Đã Tồn Tại Bàn Có Tên :{txtName.Text}");
                    return;
                }    
                Ban ban = new Ban();
                
                ban.Name = txtName.Text;
                bn.themBan(ban);
                MessageBox.Show("Thêm Thành Công");
                txtName.Text = "";
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
                MessageBox.Show("Không Được Để Trống ID");
                return;
            }
            try
            {
                Ban ban = new Ban();
                ban.Name = txtName.Text;
                ban.ID=int.Parse(txtID.Text);
                hd.SuaHDtheoBan(ban.ID);
                bn.xoaBan(ban);
                MessageBox.Show("Xóa Thành Công");
                txtName.Text = "";
                loadData();
            }
            catch (Exception)
            {

                MessageBox.Show("Xóa Thất Bại");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Không Được Để Trống ID");
                return;
            }
            try
            {
                Ban ban = new Ban();
                ban.Name = txtName.Text;
                ban.ID = int.Parse(txtID.Text);
                bn.suaBan(ban);
                txtName.Text = "";
                MessageBox.Show("Sửa Thành Công");
                loadData();
            }
            catch (Exception)
            {

                MessageBox.Show("Sửa Thất Bại");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtStatus.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
