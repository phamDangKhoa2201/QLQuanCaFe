using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class HoaDonDAL_BLL
    {
        public HoaDonDAL_BLL()
        {

        }
        QLCFDataContext qlcf = new QLCFDataContext();
        public void ThemHoaDon(DonHang dh1)
        {
            DonHang dh = new DonHang();
            dh.MaKhachHang = dh1.MaKhachHang;
            dh.MaNhanVien=dh1.MaNhanVien;
            dh.NgayDatHang= dh1.NgayDatHang;
            dh.TongTien=dh1.TongTien;
            qlcf.DonHangs.InsertOnSubmit(dh);
            qlcf.SubmitChanges();
        }
        public int LayMaDH()
        {
            int maxMaDonHang = qlcf.DonHangs.Max(dh => dh.MaDonHang);
            return maxMaDonHang;
        }
         public int GetUncheckBillIDByTableID(int id)
        {
            var hd= from h in qlcf.DonHangs where h.IDTable == id && h.TongTien==null select h.MaDonHang;
            if(hd !=null)
            {
                return int.Parse(hd.FirstOrDefault().ToString());
            }
            return 0;
        }

        public void InsertBill(int id,string manv)
        {
            DonHang dh = new DonHang();
            dh.IDTable = id;
            dh.MaNhanVien = int.Parse(manv);
            dh.NgayDatHang = DateTime.Now;
            qlcf.DonHangs.InsertOnSubmit(dh);
            qlcf.SubmitChanges();
        }

        public int getMaxIDHD()
        {
            var hd = from h in qlcf.DonHangs select h.MaDonHang;
            return hd.Max();
        }

        public void CheckOut(int madh,int tt)
        {
            DonHang dh=qlcf.DonHangs.Where(t=>t.MaDonHang==madh).FirstOrDefault();
            if(dh!=null)
            {
                dh.TongTien = tt;
                qlcf.SubmitChanges();
            }    
            
        }
        public void SuaHDtheoKH(int makh)
        {
            DonHang khSua = qlcf.DonHangs.Where(khs => khs.MaKhachHang == makh).FirstOrDefault();
            if (khSua != null)
            {
                khSua.MaKhachHang = null;
                qlcf.SubmitChanges();

            }
        }
        public void SuaHDtheoBan(int makh)
        {
            DonHang khSua = qlcf.DonHangs.Where(khs => khs.IDTable == makh).FirstOrDefault();
            if (khSua != null)
            {
                khSua.IDTable = null;
                qlcf.SubmitChanges();

            }
        }

    }
}
