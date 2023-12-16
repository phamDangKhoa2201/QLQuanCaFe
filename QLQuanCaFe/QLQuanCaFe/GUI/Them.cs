using QLQuanCaFe.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaFe
{
    public partial class Them : Form
    {
        public Them()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User_PhanQuyen form = new User_PhanQuyen();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QLSanPham form = new QLSanPham();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            QLNhanVien_GUI form = new QLNhanVien_GUI();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            QLTopping form = new QLTopping();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            QLBan form = new QLBan();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            QLTheLoai form = new QLTheLoai();
            form.Show();
        }
    }
}
