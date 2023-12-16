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
    public partial class Per_relationship : Form
    {
        //Bảng Mối Quan hệ Giữa User và Nhóm Quyền
        QLCFDataContext data = new QLCFDataContext();
        public Per_relationship()
        {
            InitializeComponent();
            loadData();
            loadCBUser();
            loadCBPer();
        }
        void loadData()
        {
            var dt = from p in data.tbl_per_relationships select new { p.id_rel, p.NhanVien.TenNhanVien, p.tbl_permision.name_per, p.suspended };
            dataGridView1.DataSource = dt;
        }
        void loadCBUser()
        {
            var user = from us in data.NhanViens select us;
            cbNameUser.DataSource = user;
            cbNameUser.DisplayMember = "TenNhanVien";
            cbNameUser.ValueMember = "MaNhanVien";
        }
        void loadCBPer()
        {
            var user = from per in data.tbl_permisions select per;
            cbNamePer.DataSource = user;
            cbNamePer.DisplayMember = "name_per";
            cbNamePer.ValueMember = "id_per";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {

                tbl_per_relationship pd = new tbl_per_relationship();
                pd.id_user_rel = int.Parse(cbNameUser.SelectedValue.ToString());
                pd.id_per_rel = int.Parse(cbNamePer.SelectedValue.ToString());
                if (checkBox1.Checked)
                {
                    pd.suspended = Boolean.Parse("True");
                }
                else
                {
                    pd.suspended = Boolean.Parse("False");
                }
                data.tbl_per_relationships.InsertOnSubmit(pd);
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
            tbl_per_relationship per = data.tbl_per_relationships.Where(us => us.id_rel == int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())).FirstOrDefault();
            if (per != null)
            {
                data.tbl_per_relationships.DeleteOnSubmit(per);
                data.SubmitChanges();
                MessageBox.Show("Xóa Thàng Công");
                loadData();
            }
            else
            {
                MessageBox.Show("Xóa Thất Bại");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy id_user
            /*int u=int.Parse(cbNameUser.SelectedValue.ToString());
            var user=from us in data.tbl_users where us.name_user=="u" select new{us.id_user};
            //Lấy id_per
             int p=int.Parse(cbNamePer.SelectedValue.ToString());
            var per=from us in data.tbl_permisions where us.name_per=="p" select new{us.id_per};
            */
            cbNameUser.SelectedValue = (dataGridView1.CurrentRow.Cells[1].Value.ToString());
            cbNamePer.SelectedValue = (dataGridView1.CurrentRow.Cells[2].Value.ToString());
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            tbl_per_relationship per = data.tbl_per_relationships.Where(us => us.id_rel == int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())).FirstOrDefault();
            if (per != null)
            {
                var dt1= (from p in data.tbl_per_relationships where p.NhanVien.TenNhanVien == dataGridView1.CurrentRow.Cells[1].Value.ToString() && p.id_rel==per.id_rel select p).FirstOrDefault();

                per.id_user_rel = dt1.id_user_rel;
                var dt2 = (from p in data.tbl_per_relationships where p.tbl_permision.name_per== dataGridView1.CurrentRow.Cells[2].Value.ToString() && p.id_rel == per.id_rel select p).FirstOrDefault();
                per.id_per_rel = dt2.id_per_rel;
                if (checkBox1.Checked)
                {
                    per.suspended = Boolean.Parse("True");
                }
                else
                {
                    per.suspended = Boolean.Parse("False");
                }
                data.SubmitChanges();
                MessageBox.Show("Sửa Thàng Công");
                loadData();
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }
    }
}
