using DAL_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static DAL_BLL.DoUongDAL_BLL;

namespace QLQuanCaFe.GUI
{
    public partial class ThongKe : Form
    {
        QLCFDataContext data = new QLCFDataContext();
        DoUongDAL_BLL du = new DoUongDAL_BLL();
        public ThongKe()
        {
            InitializeComponent();
        }
        void LoadListBillByDate(DateTime checkFrom, DateTime checkTo)
        {
            string Text = FormLogin.NAME_USER;
            var query = from donHang in data.DonHangs
                        where donHang.NgayDatHang >= checkFrom && donHang.NgayDatHang<= checkTo && donHang.TongTien !=null
                        select new
                        {
                            DonHang = donHang,
                            ChiTietDonHangs = data.ChiTietDonHangs.Where(ctdh => ctdh.MaDonHang == donHang.MaDonHang).ToList()
                        };

            foreach (var item in query)
            {
                DonHang donHang = item.DonHang;
                List<ChiTietDonHang> ctdh = item.ChiTietDonHangs;
                foreach (var ch in ctdh)
                {
                    dtgvBill.Rows.Add(donHang.MaDonHang, donHang.NgayDatHang, ch.SoLuong, ch.SanPham.TenSanPham, ch.DonHang.TongTien);
                }

            }

        }
        void setDataGridView()
        {
            dtgvBill.Columns.Add("MaDonHang", "Mã Đơn Hàng");
            dtgvBill.Columns.Add("NgayDatHang", "Ngày Đặt Hàng");
           // dtgvBill.Columns.Add("TenNhanVien", "Tên Nhân Viên");
            dtgvBill.Columns.Add("SoLuong", "Số Lượng");
            dtgvBill.Columns.Add("TenSanPham", "Sản Phẩm");
            dtgvBill.Columns.Add("TongTien", "Tổng Tiền");
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            dtgvBill.Rows.Clear();
            LoadListBillByDate(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            setDataGridView();
            DateTime today = DateTime.Now;
            dateTimePicker1.Value = new DateTime(today.Year, today.Month, 1);
            dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1).AddDays(-1);
            List<TopProductDTO> topProducts = du.Top5SanPham();

            // Gán danh sách này làm nguồn dữ liệu cho DataGridView
            dataGridView1.DataSource = topProducts;

            // Optional: Tùy chỉnh hiển thị cột (bạn có thể thay đổi tên cột theo ý muốn)
            dataGridView1.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            dataGridView1.Columns["TongSoLuongDaBan"].HeaderText = "Tổng Số Lượng Đã Bán";
            //LoadListBillByDate(dateTimePicker1.Value);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng SaveFileDialog
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            // Thiết lập tiêu đề của hộp thoại
            saveFileDialog1.Title = "Lưu tệp";

            // Thiết lập bộ lọc tệp cho hộp thoại (ví dụ: chỉ cho phép lưu tệp văn bản)
            saveFileDialog1.Filter = "Tệp Văn bản|*.txt|Tất cả các tệp|*.*";

            // Thiết lập thư mục mặc định khi hộp thoại mở lên (tùy chọn)
            saveFileDialog1.InitialDirectory = @"C:\";

            // Hiển thị hộp thoại SaveFileDialog và kiểm tra xem người dùng đã chọn một vị trí và tên tệp chưa
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn đầy đủ của tệp được chọn
                string filePath = saveFileDialog1.FileName;

                // Tạo một chuỗi để lưu dữ liệu từ DataGridView
                string dataToSave = "";

                // Lưu tên cột đầu tiên vào chuỗi (header)
                foreach (DataGridViewColumn column in dtgvBill.Columns)
                {
                    dataToSave += column.HeaderText.PadRight(20) + "\t"; // Sử dụng PadRight để đảm bảo độ rộng là 20 ký tự
                }
                dataToSave += Environment.NewLine; // Xuống dòng để tách dòng

                // Duyệt qua các dòng trong DataGridView và lưu dữ liệu vào chuỗi
                foreach (DataGridViewRow row in dtgvBill.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            dataToSave += cell.Value.ToString().PadRight(20) + "\t"; // Sử dụng PadRight để đảm bảo độ rộng là 20 ký tự
                        }
                    }
                    dataToSave += Environment.NewLine; // Xuống dòng để tách dòng
                }
                // Tạo dòng "Thống kê Hóa đơn" và đặt nó vào giữa
                string header = "Thống kê Hóa đơn";
                dataToSave = header + Environment.NewLine + dataToSave;

                // Lưu chuỗi dữ liệu vào tệp văn bản
                File.WriteAllText(filePath, dataToSave);

                // Thông báo lưu thành công
                MessageBox.Show("Dữ liệu đã được lưu tại: " + filePath, "Lưu tệp", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
