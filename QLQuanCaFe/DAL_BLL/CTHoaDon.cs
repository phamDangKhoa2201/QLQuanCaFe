using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class CTHoaDon
    {
        QLCFDataContext qlcf = new QLCFDataContext();
        public CTHoaDon() { }   
        public void ThemCTHoaDon(ChiTietDonHang cdhd1)
        {
            ChiTietDonHang cthd = new ChiTietDonHang();
            cthd.MaDonHang = cdhd1.MaDonHang;
            cthd.MaSanPham= cdhd1.MaSanPham;
            cthd.SoLuong= cdhd1.SoLuong;
            qlcf.ChiTietDonHangs.InsertOnSubmit(cthd);
            qlcf.SubmitChanges();
        }
        public void InsertBillInfo(int id, int sp,int sl)
        {
            ChiTietDonHang cthd = new ChiTietDonHang();
            cthd.MaDonHang = id;
            cthd.MaSanPham = sp;
            cthd.SoLuong= sl;
            qlcf.ChiTietDonHangs.InsertOnSubmit(cthd);
            qlcf.SubmitChanges();
        }
    }
}
