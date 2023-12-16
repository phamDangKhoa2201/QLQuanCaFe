using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class NhanVienDAL_BLL
    {
        public NhanVienDAL_BLL() { }
        QLCFDataContext qlcf = new QLCFDataContext();
        public List<NhanVien> getKhachHang()
        {
            var khachhangs = qlcf.NhanViens.Select(d => d).ToList();
            //(from kh in qlcf.KhachHangs select kh).ToList();
            return khachhangs;
        }
        public List<NhanVien> getKhachHangTheoMa(int makh)
        {
            var khachhangs = (from kh in qlcf.NhanViens
                              where kh.MaNhanVien == makh
                              select kh).ToList();

            return khachhangs;
        }
        public List<NhanVien> getKhachHangTheoTen(string name)
        {
            var khachhangs = (from kh in qlcf.NhanViens
                              where kh.TenNhanVien.Contains(name)
                              select kh).ToList();

            return khachhangs;
        }
        public void ThemNV(NhanVien nv1)
        {
            NhanVien nv = new NhanVien();
            nv.MaNhanVien = nv1.MaNhanVien;
            nv.TenNhanVien = nv1.TenNhanVien;
            nv.SoDienThoai = nv1.SoDienThoai;
            nv.DiaChi = nv1.DiaChi;
            nv.user_name = nv1.user_name;
            nv.pass=nv1.pass;
            nv.Email = nv1.Email;
            nv.Luong = nv1.Luong;
            qlcf.NhanViens.InsertOnSubmit(nv);
            qlcf.SubmitChanges();
        }
        public void SuaNV(NhanVien nv1)
        {
            NhanVien nvSua = qlcf.NhanViens.Where(nvs => nvs.MaNhanVien == nv1.MaNhanVien).FirstOrDefault();
            if (nvSua != null)
            {
                nvSua.TenNhanVien = nv1.TenNhanVien;
                nvSua.SoDienThoai = nv1.SoDienThoai;
                nvSua.DiaChi = nv1.DiaChi;
                nvSua.user_name = nv1.user_name;
                nvSua.pass = nv1.pass;
                nvSua.Email = nv1.Email;
                nvSua.Luong = nv1.Luong;
                qlcf.SubmitChanges();

            }
        }
        public bool KTMaNV(int ma)
        {
            var existingCustomer = qlcf.NhanViens.SingleOrDefault(kh => kh.MaNhanVien == ma);
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
            var existingCustomer = qlcf.NhanViens.SingleOrDefault(kh => kh.SoDienThoai == sdt);
            if (existingCustomer != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void XoaNV(int ma)
        {
            NhanVien nvXoa = qlcf.NhanViens.Where(kh => kh.MaNhanVien == ma).FirstOrDefault();
            if (nvXoa != null)
            {
                qlcf.NhanViens.DeleteOnSubmit(nvXoa);
                qlcf.SubmitChanges();
            }
        }
        public decimal? GetTotalSpendingByStaff(int manv)
        {
            decimal? totalSpending = qlcf.DonHangs
                .Where(donHang => donHang.MaNhanVien == manv)
                .Sum(donHang => (decimal?)donHang.TongTien);

            return totalSpending ?? 0;
        }
    }
}
