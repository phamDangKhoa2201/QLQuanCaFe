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
    public partial class permision : Form
    {
        //Bảng Nhóm Quyền
        QLCFDataContext data = new QLCFDataContext();
        public permision()
        {
            InitializeComponent();
            loadData();
        }
        void loadData()
        {
            var user = from per in data.tbl_permisions select per;
            dataGridView1.DataSource = user;
        }
        /*void loadCB()
        {
            string[] dataArray = new string[] { "True","False" };

            // Chuyển mảng thành danh sách List<int>
            List<string> dataList = dataArray.ToList();
            cbDes.DataSource = dataList;
        }*/

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNamePer.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); ;
            txtDes.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtNamePer.Text == "")
            {
                MessageBox.Show("Tên Nhóm Đang Trống");
            }
            try
            {

                tbl_permision per = new tbl_permision();
                per.name_per = txtNamePer.Text;
                per.description = txtDes.Text;
                data.tbl_permisions.InsertOnSubmit(per);
                data.SubmitChanges();
                MessageBox.Show("Thêm Thành Công");
                loadData();
            }
            catch (Exception)
            {

                MessageBox.Show("Thêm Thất Bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == "")
                {
                    MessageBox.Show("Không Được Để Trống ID");
                    return;
                }
                tbl_permision per = data.tbl_permisions.Where(us => us.id_per == int.Parse(txtID.Text)).FirstOrDefault();
                if (per != null)
                {
                    data.tbl_permisions.DeleteOnSubmit(per);
                    data.SubmitChanges();
                    MessageBox.Show("Xóa Thàng Công");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Xóa Thất Bại");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xoá vì tài khoản đang được truy cập vào nhóm quyền này");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Không Được Để Trống ID");
                return;
            }
            tbl_permision per = data.tbl_permisions.Where(t => t.id_per == int.Parse(txtID.Text.ToString())).FirstOrDefault();
            if (per != null)
            {
                per.name_per = txtNamePer.Text;
                per.description = txtDes.Text;
                data.SubmitChanges();
                MessageBox.Show("Sửa Thành Công");
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }
    }
}
