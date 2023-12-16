using DAL_BLL;
using Dynamic_Controls;
using QLQuanCaFe.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace QLQuanCaFe.GUI
{
    public partial class DoUong : Form
    {
        public static int MA_HD;
        int manv = int.Parse(FormLogin.ID_USER);
        DoUongDAL_BLL du = new DoUongDAL_BLL();
        ToppingDAL_BLL to = new ToppingDAL_BLL();
        HoaDonDAL_BLL hd = new HoaDonDAL_BLL();
        CTHoaDon cthd = new CTHoaDon();
        CTTopping cttp = new CTTopping();
        //decimal tongTien = 0;
        decimal tiendu = 0;
        //decimal tientopping = 0;
        decimal thanhtoan = 0;
        private SanPham sanPham;
        Button bt;
        NumericUpDown num;
        List<Tuple<SanPham, int>> danhSachSanPham = new List<Tuple<SanPham, int>>();
        List<Topping> danhSachTopping = new List<Topping>();

        public DoUong()
        {
            InitializeComponent();
            flpDSDoUong.FlowDirection = FlowDirection.LeftToRight;
            flpDSDoUong.AutoScroll = true;
        }

        private void grbban_Enter(object sender, EventArgs e)
        {

        }

        private void DoUong_Load(object sender, EventArgs e)
        {
            btnThanhtoan.Enabled = false;
            List<Topping> tp = to.getToppping();
            dataGridView1.DataSource = tp;
            dataGridView1.Columns[0].HeaderText = "STT";
            dataGridView1.Columns[1].HeaderText = "Topping";
            dataGridView1.Columns[2].HeaderText = "Giá";
            pbdouong.FlowDirection = FlowDirection.LeftToRight;
            pbdouong.AutoScroll = true;
            pbdouong.Dock = DockStyle.Fill;
            List<SanPham> sp = du.GetSanPhams();
            pbdouong.Controls.Clear();
            sp.ForEach(x =>
            {
                Button btn = new Button();
                btn.Text = x.TenSanPham + "\n" + x.GiaTien;
                btn.Tag = x.MaSanPham;
                if (x.HinhAnh == null)
                {
                    btn.Image = null;
                }
                else
                {
                    byte[] byteArray = x.HinhAnh.ToArray();
                    using (MemoryStream ms = new MemoryStream(byteArray))
                    {
                        Image image = Image.FromStream(ms);
                        btn.Image = image;
                    }
                }
                btn.AutoEllipsis = false;
                btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
                btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                btn.Font = new Font(btn.Font.FontFamily, 10);
                btn.Width = 120;
                btn.Height = 170;


                btn.Click += Btn_Click;
                pbdouong.Controls.Add(btn);
                
            });
        }

        private void Btn_Click(object sender, EventArgs e)
        {

            // flpDSDoUong.Dock = DockStyle.Fill;
            Button clickedButton = (Button)sender;
            int maSanPham = (int)clickedButton.Tag;
            sanPham = du.Get1SanPhamsTheoMa(maSanPham);

            Panel existingCard = flpDSDoUong.Controls.OfType<Panel>().FirstOrDefault(card => (int)card.Tag == maSanPham);

            if (existingCard != null)
            {
                // Đã tồn tại, tăng giá trị của NumericUpDown lên 1
                NumericUpDown num1 = existingCard.Controls.OfType<NumericUpDown>().FirstOrDefault();
                Label lb = existingCard.Controls.OfType<Label>().FirstOrDefault();
                if (num1 != null)
                {
                    num1.Value++;
                    lb.Text = sanPham.TenSanPham + "       " + sanPham.GiaTien * num1.Value;
                    
                    lbTongTien.Text = (sanPham.GiaTien * num1.Value).ToString();
                }
            }
            else
            {
                Panel card = new Panel();
                card.BorderStyle = BorderStyle.Fixed3D;
                card.Padding = new Padding(10);
                card.Width = 350;
                card.Tag = maSanPham;
                card.Height= 60;

                Label lb = new Label();
                lb.Text = sanPham.TenSanPham + "       " + sanPham.GiaTien;
                lb.MaximumSize = new Size(flpDSDoUong.ClientSize.Width, 0);
                lb.AutoSize = true;
                lb.Font = new Font(Font.FontFamily, 10);


                num = new NumericUpDown();
                
                //num.Enabled = false;
                num.Width = 30;
                num.Location = new Point(300, 0);
                num.Font = new Font(Font.FontFamily, 10);
                num.Value = 1;
                bool canDecrease = true;
                num.Minimum = 1;
                num.ValueChanged += (s, ev) =>
                {
                    int numValue = (int)num.Value;
                    if (numValue == 1 && !canDecrease)
                    {
                        num.Value = 1;
                    }
                    else
                    {
                        lb.Text = sanPham.TenSanPham + "       " + sanPham.GiaTien * num.Value;
                        //thanhtoan = thanhtoan + (decimal)(sanPham.GiaTien * num.Value);
                        //lbTongTien.Text = thanhtoan.ToString();
                    }
                };

                bt = new Button();
                bt.Text = "Hoàn thành";
                bt.Font = new Font(Font.FontFamily, 10);
                bt.Location = new Point(240, 30);
                bt.Width = 100;
                bt.Click += Bt_Click;

                PictureBox pc = new PictureBox();
                pc.Size = new Size(30, 30);
                string path = "D:\\hk1 nam4\\phatTrienPhanMem\\cf\\QuanCaFe\\QLQuanCaFe\\QLQuanCaFe\\Image\\icontrasg.png";
                //string path = "C:\\Users\\PC\\Documents\\GitHub\\QuanCaFe\\QLQuanCaFe\\QLQuanCaFe\\Image\\icontrasg.png";
                //string path = "D:\\Hoc\\cnpmnc\\doan\\doan\\QLQuanCaFe\\QLQuanCaFe\\Image\\icontrasg.png";
                pc.Image = Image.FromFile(path);
                pc.Location = new Point(40, 25);
                pc.Click += Pc_Click;

                card.Controls.Add(lb);
                card.Controls.Add(pc);
                card.Controls.Add(num);
                card.Controls.Add(bt);

                flpDSDoUong.Controls.Add(card);

                Label lineBreakLabel = new Label();
                lineBreakLabel.Text = Environment.NewLine;
                flpDSDoUong.Controls.Add(lineBreakLabel);
                pbdouong.Enabled = false;

                
                

            }

        }

        private void Pc_Click(object sender, EventArgs e)
        {

            PictureBox clickedPictureBox = (PictureBox)sender;
            Panel parentPanel = (Panel)clickedPictureBox.Parent;

            // Truy cập các thông tin trong Panel
            NumericUpDown numUpDown = parentPanel.Controls.OfType<NumericUpDown>().FirstOrDefault();
            Label lb = parentPanel.Controls.OfType<Label>().FirstOrDefault();

            if (numUpDown != null && lb != null)
            {
                // Lấy thông tin cần thiết
                int maSanPham = (int)parentPanel.Tag;
                sanPham = du.Get1SanPhamsTheoMa(maSanPham);

                // Thực hiện các thao tác khác nếu cần
                // ...
                decimal tam = (decimal)sanPham.GiaTien * numUpDown.Value;
                thanhtoan = thanhtoan - tam;
                lbTongTien.Text = thanhtoan.ToString();
                int maSanPhamToRemove = maSanPham;
                Tuple<SanPham, int> itemToRemove = danhSachSanPham.FirstOrDefault(x => x.Item1.MaSanPham == maSanPhamToRemove);
                if (itemToRemove != null)
                {
                    danhSachSanPham.Remove(itemToRemove);
                }
                // Xoá Panel khỏi FlowLayoutPanel
                flpDSDoUong.Controls.Remove(parentPanel);

                
                // Cập nhật tổng giá tiền
                //UpdateTotalPrice();
            }

        }

        private void Bt_Click(object sender, EventArgs e)
        {
            danhSachSanPham.Add(new Tuple<SanPham, int>(sanPham, (int)num.Value));
            foreach (var tuple in danhSachSanPham)
            {
                Console.WriteLine($"{tuple.Item1.TenSanPham} - Số lượng: {tuple.Item2}");

            }
            Console.WriteLine("------------------------------------------------");
            tiendu = (decimal)sanPham.GiaTien * num.Value;
            num.Enabled = false;
            pbdouong.Enabled = true;
            thanhtoan += tiendu;
            lbTongTien.Text = thanhtoan.ToString();
            bt.Enabled = false;
            RecommendProducts(sanPham);
            btnThanhtoan.Enabled = true;
        }

        private void lbDouong_Click(object sender, EventArgs e)
        {
            pbdouong.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            int masv = int.Parse(row.Cells[0].Value.ToString());
            List<Topping> tps = new List<Topping>();
            tps = to.getToppingTheoMa(masv);

            foreach (Topping topping in tps)
            {
                Label toppingLabel = CreateToppingLabel(topping);
                danhSachTopping.Add(topping);
                
                
                // Thêm Panel vào FlowLayoutPanel
                flpDSDoUong.Controls.Add(toppingLabel);

            }
            foreach (var tuple in danhSachTopping)
            {
                Console.WriteLine($"{tuple.Ten} -giá: {tuple.Gia}");

            }
            Console.WriteLine("------------------------------------------------");
        }
        private Label CreateToppingLabel(Topping topping)
        {

            Label lbtop = new Label();
            lbtop.Font = new Font(Font.FontFamily, 10);
            lbtop.Width = 350;
            lbtop.Height = 20;
            lbtop.Text = $"{topping.Ten}     {topping.Gia.ToString()}" + "      X";
            lbtop.Tag= topping.MaTopping;
            lbtop.Location = new Point(10, 10);
            thanhtoan = thanhtoan + (decimal)topping.Gia;
            lbTongTien.Text = thanhtoan.ToString();

            lbtop.Click += Lbtop_Click;

            return lbtop;
        }

        private void Lbtop_Click(object sender, EventArgs e)
        {
            Label clickedLabel = (Label)sender;

            // Lấy thông tin cần thiết
            int maTop = (int)clickedLabel.Tag;
            Topping topping = to.getToppingTheoMa(maTop).FirstOrDefault();
            Topping toppingToRemove = danhSachTopping.FirstOrDefault(t => t.MaTopping == maTop);
            if (toppingToRemove != null) 
            {
                danhSachTopping.Remove(toppingToRemove);
            }
            
           
            flpDSDoUong.Controls.Remove(clickedLabel);

            // Cập nhật tổng giá tiền
            thanhtoan -= (decimal)topping.Gia;
            lbTongTien.Text = thanhtoan.ToString();

        }

        private void txtKhachhang_Click(object sender, EventArgs e)
        {
            KhachHangGUI danhSachKhachHangForm = new KhachHangGUI();
            danhSachKhachHangForm.ShowDialog();

            // Kiểm tra xem có thông tin khách hàng được chọn hay không
            if (danhSachKhachHangForm.KhachHangDuocChon != null)
            {
                // Hiển thị tên khách hàng đã chọn trong TextBox
                txtKhachhang.Text = danhSachKhachHangForm.KhachHangDuocChon.TenKhachHang;
                int makh = danhSachKhachHangForm.KhachHangDuocChon.MaKhachHang;
                lbMaKH.Text = makh.ToString();
                
            }
        }

        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            DateTime ngayDatHang = date.Value;
            DonHang dh1 = new DonHang();
            if (txtKhachhang.Text.Trim() == "")
            {
                //lbMaKH.Text = 23000.ToString();
                dh1.MaKhachHang = null;
            }
            else
            {
                dh1.MaKhachHang = int.Parse(lbMaKH.Text);
            }
            
            dh1.MaNhanVien = manv;
            dh1.NgayDatHang = ngayDatHang;
            dh1.TongTien = decimal.Parse(lbTongTien.Text);
            hd.ThemHoaDon(dh1);
            int  maxMaDonHang = hd.LayMaDH();
            foreach (var tuple in danhSachSanPham)
            {
                DAL_BLL.ChiTietDonHang cthd1 = new DAL_BLL.ChiTietDonHang();
                cthd1.MaDonHang = maxMaDonHang;
                cthd1.MaSanPham = tuple.Item1.MaSanPham;
                cthd1.SoLuong = tuple.Item2;
                cthd.ThemCTHoaDon(cthd1);
            }
            if (danhSachTopping == null)
            {
                Topping tpt= new Topping();
                tpt.MaTopping = 1;
                tpt.Ten = "Không chọn";
                tpt.Gia = 0;
                danhSachTopping.Add(tpt);
                
            }
            foreach (var tuple in danhSachTopping)
            {
                ChiTietTopping cttp1 = new ChiTietTopping();
                cttp1.MaDonHang = maxMaDonHang;
                cttp1.MaTopping = tuple.MaTopping;
                cttp1.SoLuong = 1;
                cttp.ThemCTTopping(cttp1);
            }
            MA_HD = maxMaDonHang;
            danhSachSanPham.Clear();
            danhSachTopping.Clear();
            flpDSDoUong.Controls.Clear();
            MessageBox.Show("Tổng tiền phải trả là: " + lbTongTien.Text);
            
            thanhtoan = 0;
            txtKhachhang.Text = "";
            lbTongTien.Text = "";
            //Form1 form1 = new Form1();
            //form1.Show();
        }
        
        private void RecommendProducts(SanPham selectedDrink)
        {
            // Duyệt qua tất cả sản phẩm và tính toán sự tương đồng với đồ uống đã chọn
            var recommendedProducts = du.GetSanPhams()
                .Where(product => product.MaSanPham != selectedDrink.MaSanPham) // Loại bỏ đồ uống đã chọn
                .OrderByDescending(product => ContentBasedRecommendation.CalculateSimilarity(selectedDrink, product))
                .Take(2); // Lấy ra 5 đồ uống có sự tương đồng cao nhất

            // Hiển thị các sản phẩm được đề xuất (thay thế bằng cách phù hợp với giao diện của bạn)
            foreach (var product in recommendedProducts)
            {
                Console.WriteLine($"Đồ uống được đề xuất: {product.TenSanPham}");
                HighlightButton(product);
            }
        }
        private void HighlightButton(SanPham product)
        {
            Button existingButton = pbdouong.Controls.OfType<Button>().FirstOrDefault(btn => (int)btn.Tag == product.MaSanPham);

            if (existingButton != null)
            {
                // Lưu màu nền và màu văn bản hiện tại
                Color originalBackColor = existingButton.BackColor;
                Color originalForeColor = existingButton.ForeColor;

                // Đặt màu nền và màu văn bản mới
                existingButton.BackColor = Color.Yellow;
                existingButton.ForeColor = Color.Red; // Chọn một màu văn bản phù hợp

                // Tạo một đối tượng Timer để chuyển đổi màu nền và màu văn bản
                Timer timer = new Timer();
                timer.Interval = 5000; // Đặt khoảng thời gian (ví dụ: 500 mili giây)
                timer.Tick += (sender, e) =>
                {
                    // Chuyển đổi màu nền và màu văn bản
                    if (existingButton.BackColor == Color.Yellow)
                    {
                        existingButton.BackColor = originalBackColor;
                        existingButton.ForeColor = originalForeColor;
                    }
                    else
                    {
                        existingButton.BackColor = Color.Yellow;
                        existingButton.ForeColor = Color.Red; // Chọn một màu văn bản phù hợp
                    }
                };

                // Bắt đầu Timer
                timer.Start();

                // Dừng Timer sau 5 giây
                timer.Tick += (sender, e) =>
                {
                    timer.Stop();

                    // Đặt lại màu nền và màu văn bản về màu gốc
                    existingButton.BackColor = originalBackColor;
                    existingButton.ForeColor = originalForeColor;
                };
            }
        }



    }
}
