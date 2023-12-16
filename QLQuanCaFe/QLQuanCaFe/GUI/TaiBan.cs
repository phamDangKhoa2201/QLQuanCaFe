using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dynamic_Controls;
using DAL_BLL;
using System.Globalization;
using QLQuanCaFe.GUI;
using QLQuanCaFe.Report;

namespace QLQuanCaFe
{
    public partial class TaiBan : Form
    {
        public static int MADH;
        QLCFDataContext data = new QLCFDataContext();
        Ban_DAL_BLL b = new Ban_DAL_BLL();
        TheLoaiDAL_BLL category = new TheLoaiDAL_BLL();
        DoUongDAL_BLL sp = new DoUongDAL_BLL();
        HoaDonDAL_BLL hd = new HoaDonDAL_BLL();
        CTHoaDon cthd = new CTHoaDon();
        ToppingDAL_BLL tp = new ToppingDAL_BLL();
        CTTopping cttp = new CTTopping();

        public TaiBan()
        {
            InitializeComponent();
        }

        private void TaiBan_Load(object sender, EventArgs e)
        {
            getCBTheLoai();
            getCBSanPham();
            getTopping();
            loadTable();
            getCBBan();
            numSL.Value = 1;
        }

        private void loadTable()
        {
            flpTable.Controls.Clear();

            List<DAL_BLL.Ban> tableList = b.getBan();

            foreach (DAL_BLL.Ban item in tableList)
            {
                Button btn = new Button() { Width = 90, Height = 90 };
                btn.Text = item.Name + Environment.NewLine + item.status;
                btn.Click += Button_Click;
                btn.Tag = item;

                switch (item.status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }

                flpTable.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            listView1.Items.Clear();
            // Xóa tất cả các controls (Label) trong ListView
            foreach (Control control in lsvBill.Controls.OfType<Label>().ToList())
            {
                lsvBill.Controls.Remove(control);
            }

            int totalPrice = 0;
            var query = from donHang in data.DonHangs
                        where donHang.IDTable == id && donHang.TongTien == null
                        select new
                        {
                            DonHang = donHang,
                            ChiTietDonHangs = data.ChiTietDonHangs.Where(ctdh => ctdh.MaDonHang == donHang.MaDonHang).ToList(),
                            ChiTietToppings = data.ChiTietToppings.Where(cttp => cttp.MaDonHang == donHang.MaDonHang).ToList()
                        };
            if (query == null)
            {
                b.updateBan2(id);
            }
            foreach (var item in query)
            {
                DonHang donHang = item.DonHang;
                List<DAL_BLL.ChiTietDonHang> ctdh = item.ChiTietDonHangs;
                List<ChiTietTopping> cttp = item.ChiTietToppings;
                foreach (var ch in ctdh)
                {
                    int labelHeight = 20; // Đặt chiều cao của mỗi label
                                          // Vị trí Y cho label mới
                    int newY = lsvBill.Controls.Count * labelHeight;
                    Label lb = new Label();
                    int tam = ch.SanPham.TenSanPham.Length;
                    lb.Text = $"{ch.SanPham.TenSanPham,-10} {ch.SoLuong,+20} {ch.SanPham.GiaTien,+10}";
                    lb.MaximumSize = new Size(lsvBill.ClientSize.Width, 0);
                    lb.AutoSize = true;
                    lb.Font = new Font(Font.FontFamily, 10);
                    lb.Location = new Point(0, newY); // Đặt vị trí mới cho label
                    lsvBill.Controls.Add(lb);
                    totalPrice += (int)(ch.SanPham.GiaTien * ch.SoLuong);
                }
                foreach (var tp in cttp)
                {
                    ListViewItem lsvItem = new ListViewItem(tp.Topping.Ten.ToString());
                    lsvItem.SubItems.Add(tp.SoLuong.ToString());
                    lsvItem.SubItems.Add(tp.Topping.Gia.ToString());
                    listView1.Items.Add(lsvItem);
                    totalPrice += (int)(tp.SoLuong * tp.Topping.Gia);
                }

            }

            /*List<QuanLyQuanCafe.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            foreach (QuanLyQuanCafe.DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }*/
            CultureInfo culture = new CultureInfo("vi-VN");

            //Thread.CurrentThread.CurrentCulture = culture;

            txbTotalPrice.Text = totalPrice.ToString("c", culture);

        }
        private void Button_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as DAL_BLL.Ban).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }

        private void ban1_Click(object sender, EventArgs e)
        {

        }
        private void getCBTheLoai()
        {
            cbTheLoai.DataSource = category.getTheLoai();
            cbTheLoai.ValueMember = "MaTheLoai";
            cbTheLoai.DisplayMember = "TenTheLoai";
        }

        private void getCBSanPham()
        {
            cbSanPham.DataSource = sp.GetSanPhams();
            cbSanPham.ValueMember = "MaSanPham";
            cbSanPham.DisplayMember = "TenSanPham";
        }

        private void getTopping()
        {
            cbTopping.DataSource = tp.getToppping();
            cbTopping.ValueMember = "MaTopping";
            cbTopping.DisplayMember = "Ten";
        }

        private void getCBBan()
        {
            cbBan.DataSource = b.getBan();
            cbBan.ValueMember = "ID";
            cbBan.DisplayMember = "Name";
        }

        private void cbTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTheLoai.SelectedItem == null && cbTheLoai.SelectedValue.ToString() == "")
                return;
            else
            {
                var a = from sp in data.SanPhams where sp.MaTheLoai == int.Parse(cbTheLoai.SelectedValue.ToString()) select sp;
                cbSanPham.DataSource = a; //sp.GetSanPhamByTheLoai(cbTheLoai.SelectedValue.ToString());
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DAL_BLL.Ban table = lsvBill.Tag as DAL_BLL.Ban;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }

            int idBill = hd.GetUncheckBillIDByTableID(table.ID);
            int SanPhamID = (cbSanPham.SelectedItem as SanPham).MaSanPham;
            int count = (int)numSL.Value;

            if (idBill == 0)
            {
                hd.InsertBill(table.ID, FormLogin.ID_USER);
                cthd.InsertBillInfo(hd.getMaxIDHD(), SanPhamID, count);
                b.updateBan(table.ID);
            }
            else
            {
                cthd.InsertBillInfo(idBill, SanPhamID, count);
                b.updateBan(table.ID);
            }

            ShowBill(table.ID);

            loadTable();
        }

        private void btnThem2_Click(object sender, EventArgs e)
        {
            DAL_BLL.Ban table = lsvBill.Tag as DAL_BLL.Ban;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }

            int idBill = hd.GetUncheckBillIDByTableID(table.ID);
            int ToppingID = (cbTopping.SelectedItem as Topping).MaTopping;
            int count = (int)numSL.Value;

            if (idBill == 0)
            {
                MessageBox.Show("Chưa Có Hóa Đơn Sản Phẩm");
                return;
            }
            else
            {
                cttp.insertToppingInfo(idBill, ToppingID, count);
            }

            ShowBill(table.ID);

            loadTable();

            loadTopping(idBill);
        }

        private void loadTopping(int mahd)
        {
            listView1.Items.Clear();
            var t = (from tp in data.ChiTietToppings where tp.MaDonHang == mahd select tp).ToList();
            foreach (ChiTietTopping item in t)
            {
                ListViewItem lsvItem = new ListViewItem(item.Topping.Ten.ToString());
                lsvItem.SubItems.Add(item.SoLuong.ToString());
                lsvItem.SubItems.Add(item.Topping.Gia.ToString());
                listView1.Items.Add(lsvItem);
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            DAL_BLL.Ban table = lsvBill.Tag as DAL_BLL.Ban;
            int idBill = hd.GetUncheckBillIDByTableID(table.ID);
            MADH = idBill;
            int totalPrice = int.Parse(txbTotalPrice.Text.Replace(".", "").Split(',')[0]);

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\nTổng tiền :  {1}", table.Name, totalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    hd.CheckOut(idBill, (int)totalPrice);
                    ShowBill(table.ID);

                    b.updateBan2(table.ID);
                    loadTable();
                }
            }
            //Form2 form = new Form2();
            //form.Show();
        }

        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as DAL_BLL.Ban).ID;

            int id2 = (cbBan.SelectedItem as DAL_BLL.Ban).ID;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}", (lsvBill.Tag as DAL_BLL.Ban).Name, (cbBan.SelectedItem as DAL_BLL.Ban).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                b.chuyenBan(id1, id2);

                loadTable();
                ShowBill(id1);
                ShowBill(id2);
            }
        }
    }
}
