    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class KhachHangDAL_BLL
    {
        public KhachHangDAL_BLL() { }
        QLCFDataContext qlcf = new QLCFDataContext();
        public void loadKhachHang()
        {
        }
        public List<KhachHang> getKhachHang()
        {
            var khachhangs = qlcf.KhachHangs.Select(d => d).ToList();
                //(from kh in qlcf.KhachHangs select kh).ToList();
            return khachhangs;
        }
        public List<KhachHang> getKhachHangTheoMa(int makh)
        {
            var khachhangs = (from kh in qlcf.KhachHangs
                              where kh.MaKhachHang == makh
                              select kh).ToList();

            return khachhangs;
        }
        public List<KhachHang> getKhachHangTheoTen(string name)
        {
            var khachhangs = (from kh in qlcf.KhachHangs
                              where kh.TenKhachHang.Contains(name)
                              select kh).ToList();

            return khachhangs;
        }
        public void ThemKH( string ten, string sodt, string diachi)
        {
            KhachHang kh = new KhachHang();
            
            kh.TenKhachHang = ten;
            kh.SoDienThoai = sodt;
            kh.DiaChi = diachi;
            qlcf.KhachHangs.InsertOnSubmit(kh);
            qlcf.SubmitChanges();
        }
        public void SuaKH(int ma, string ten, string sodt, string diachi)
        {
            KhachHang khSua = qlcf.KhachHangs.Where(khs => khs.MaKhachHang == ma).FirstOrDefault();
            if (khSua != null)
            {
                khSua.TenKhachHang = ten;
                khSua.SoDienThoai = sodt;
                khSua.DiaChi = diachi;
                qlcf.SubmitChanges();

            }
        }
        public bool KTMaKH(int ma)
        {
            var existingCustomer = qlcf.KhachHangs.SingleOrDefault(kh => kh.MaKhachHang == ma);
            if (existingCustomer != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool KTSDT(string sdt)
        {
            var existingCustomer = qlcf.KhachHangs.SingleOrDefault(kh => kh.SoDienThoai == sdt);
            if (existingCustomer != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void XoaKH(int ma)
        {
            KhachHang khXoa = qlcf.KhachHangs.Where(kh => kh.MaKhachHang == ma).FirstOrDefault();
            if (khXoa != null)
            {
                qlcf.KhachHangs.DeleteOnSubmit(khXoa);
                qlcf.SubmitChanges();
            }
        }
        public decimal? GetTotalSpendingByCustomer(int maKhachHang)
        {
            decimal? totalSpending = qlcf.DonHangs
                .Where(donHang => donHang.MaKhachHang == maKhachHang)
                .Sum(donHang => (decimal?)donHang.TongTien);

            return totalSpending ?? 0;
        }
    }
}
