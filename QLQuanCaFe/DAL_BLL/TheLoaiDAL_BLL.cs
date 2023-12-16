using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class TheLoaiDAL_BLL
    {
        public TheLoaiDAL_BLL() { }
        QLCFDataContext qlcf = new QLCFDataContext();
        public List<TheLoai> getTheLoai()
        {
            var khachhangs = qlcf.TheLoais.Select(d => d).ToList();
            //(from kh in qlcf.KhachHangs select kh).ToList();
            return khachhangs;
        }
        public List<TheLoai> getTheLoaiTheoMa(int makh)
        {
            var khachhangs = (from kh in qlcf.TheLoais
                              where kh.MaTheLoai == makh
                              select kh).ToList();

            return khachhangs;
        }
        public List<TheLoai> getTheLoaiTheoTen(string ten)
        {
            var khachhangs = (from kh in qlcf.TheLoais
                              where kh.TenTheLoai.Contains(ten)
                              select kh).ToList();

            return khachhangs;
        }
        public void ThemTL( string ten)
        {
            TheLoai tp = new TheLoai();
            
            tp.TenTheLoai = ten;
            qlcf.TheLoais.InsertOnSubmit(tp);
            qlcf.SubmitChanges();
        }
        public void SuaTL(int ma, string ten)
        {
            TheLoai tpSua = qlcf.TheLoais.Where(khs => khs.MaTheLoai == ma).FirstOrDefault();
            if (tpSua != null)
            {
                tpSua.TenTheLoai = ten;
                qlcf.SubmitChanges();

            }
        }
        public bool KTMaTP(int ma)
        {
            var existingCustomer = qlcf.TheLoais.SingleOrDefault(kh => kh.MaTheLoai == ma);
            if (existingCustomer != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool KTTenTL(string ten)
        {
            var existingCustomer = qlcf.TheLoais.SingleOrDefault(kh => kh.TenTheLoai == ten);
            if (existingCustomer != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void XoaTP(int ma)
        {
            TheLoai khXoa = qlcf.TheLoais.Where(kh => kh.MaTheLoai == ma).FirstOrDefault();
            if (khXoa != null)
            {
                qlcf.TheLoais.DeleteOnSubmit(khXoa);
                qlcf.SubmitChanges();
            }
        }
        public void SuaSPtheoTheLoai(int makh)
        {
            SanPham khSua = qlcf.SanPhams.Where(khs => khs.MaTheLoai == makh).FirstOrDefault();
            if (khSua != null)
            {
                khSua.MaTheLoai = null;
                qlcf.SubmitChanges();

            }
        }


    }
}
