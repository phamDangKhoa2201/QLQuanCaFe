using Microsoft.Reporting.WinForms;
using QLQuanCaFe.GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLQuanCaFe.Report
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        int madh = DoUong.MA_HD;
        private void Form1_Load(object sender, EventArgs e)
        {
            CFContext context = new CFContext();

            DonHangRP dh = context.DonHangs.FirstOrDefault(dh1 => dh1.MaDonHang == madh);
            List<ChiTietDonHang> listCTDH = context.ChiTietDonHangs.Where(ct => ct.MaDonHang == madh).ToList();
            List<ChiTietToppingRP> listCTTP = context.ChiTietToppings.Where(ct => ct.MaDonHang == madh).ToList();
            List<HoaDonReport> tempListRp = new List<HoaDonReport>();
          
            // Tạo một đối tượng HoaDonReport chứa thông tin chung
            HoaDonReport hoaDonChung = new HoaDonReport();
            if (dh.Ban == null)
            {
                hoaDonChung.TenBan = "Mang đi";
            }
            else
            {
                hoaDonChung.TenBan = dh.Ban.Name;
            }

            if (dh.KhachHang == null)
            {
                hoaDonChung.TenKH = "";
            }
            else
            {
                hoaDonChung.TenKH = dh.KhachHang.TenKhachHang;
            }
            hoaDonChung.TenNV = dh.NhanVien.TenNhanVien;
            hoaDonChung.TongTien = (int)dh.TongTien;
            hoaDonChung.Ngay = ((DateTime)dh.NgayDatHang).Date.Date;

            // Thêm đối tượng HoaDonReport chứa thông tin chung vào danh sách
            tempListRp.Add(hoaDonChung);


            foreach (ChiTietDonHang ctdh in listCTDH)
            {
                if (ctdh.SanPham != null && ctdh.SanPham.GiaTien != 0 && ctdh.SoLuong != 0)
                {
                    HoaDonReport temp1 = new HoaDonReport();
                    temp1.GiaSP = (decimal)ctdh.SanPham.GiaTien;
                    temp1.TenSP = ctdh.SanPham.TenSanPham;
                    temp1.SoLuongSP = (int)ctdh.SoLuong;
                    temp1.ThanhTien = (int)temp1.GiaSP * temp1.SoLuongSP;
                    tempListRp.Add(temp1);
                }
            }

            foreach (ChiTietToppingRP cttp in listCTTP)
            {
                if (cttp.Topping != null && cttp.Topping.Gia != 0 && cttp.SoLuong != 0)
                {


                    HoaDonReport temp2 = new HoaDonReport();
                    temp2.GiaTopping = (int)cttp.Topping.Gia;
                    temp2.TenTopping = cttp.Topping.Ten;
                    temp2.SoLuongTopping = (int)cttp.SoLuong;
                    tempListRp.Add(temp2);
                }

            }
            

            //listRp.AddRange(tempListRp);


            reportViewer1.LocalReport.ReportPath = "rptHoaDon.rdlc";
            var source = new ReportDataSource("DataSet1", tempListRp);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);

            this.reportViewer1.RefreshReport();
        }
    }
}
