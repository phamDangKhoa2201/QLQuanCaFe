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
    public partial class Per_Detail : Form
    {
        //Chi Tiết Nhóm Quyền
        QLCFDataContext data = new QLCFDataContext();
        public Per_Detail()
        {
            InitializeComponent();
            loadDataPer();
            loadDataPerDel();
            loadCB();
        }
        void loadDataPer()
        {
            var user = from per in data.tbl_permisions select per;
            dgvPer.DataSource = user;
        }
        void loadDataPerDel()
        {
            var dt = from perd in data.tbl_permision_dels select new { perd.id_pd, perd.code_action, perd.id_per };
            dgvPerDel.DataSource = dt;
        }
        void loadCB()
        {
            string[] dataArray = new string[] { "ALL", "QL Khach Hang", "Bao Cao", "Thu Chi" };

            List<string> dataList = dataArray.ToList();
            cbCode.DataSource = dataList;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                tbl_permision_del pd = new tbl_permision_del();
                pd.code_action = cbCode.SelectedItem.ToString();
                pd.id_per = int.Parse(dgvPer.CurrentRow.Cells[0].Value.ToString());
                data.tbl_permision_dels.InsertOnSubmit(pd);
                data.SubmitChanges();
                MessageBox.Show("Thêm Thành Công");
                loadDataPerDel();
            }
            catch (Exception)
            {

                MessageBox.Show("Thêm Thất Bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            tbl_permision_del per = data.tbl_permision_dels.Where(us => us.id_pd == int.Parse(dgvPerDel.CurrentRow.Cells[0].Value.ToString())).FirstOrDefault();
            if (per != null)
            {
                data.tbl_permision_dels.DeleteOnSubmit(per);
                data.SubmitChanges();
                MessageBox.Show("Xóa Thàng Công");
                loadDataPerDel();
            }
            else
            {
                MessageBox.Show("Xóa Thất Bại");
            }
        }
    }
}
