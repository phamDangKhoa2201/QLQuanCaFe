using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class DoUongDAL_BLL
    {
        public DoUongDAL_BLL() { }
        QLCFDataContext qlcf = new QLCFDataContext();
        public List<SanPham> GetSanPhams()
        {
            var sanphams = qlcf.SanPhams.Select(d => d).ToList();
            //(from kh in qlcf.KhachHangs select kh).ToList();
            return sanphams;
          
        }
        public List<SanPhamWithTheLoai> GetallSanPhams()
        {
            var result = from sanpham in qlcf.SanPhams
                         join theloai in qlcf.TheLoais on sanpham.MaTheLoai equals theloai.MaTheLoai
                         select new SanPhamWithTheLoai
                         {
                             MaSanPham = sanpham.MaSanPham,
                             TenSanPham = sanpham.TenSanPham,
                             GiaTien = (decimal)sanpham.GiaTien,
                             HinhAnh=sanpham.HinhAnh,
                             MaTheLoai = theloai.MaTheLoai,
                             TenTheLoai = theloai.TenTheLoai
                         };
            var sanphams = result.ToList();
            //(from kh in qlcf.KhachHangs select kh).ToList();
            return sanphams;

        }

        public List<SanPham> GetSanPhamByTheLoai(string id)
        {
            var sanphams = (from sp in qlcf.SanPhams where sp.MaTheLoai == int.Parse(id.ToString()) select sp);
            //(from kh in qlcf.KhachHangs select kh).ToList();
            List<SanPham> sps= new List<SanPham>();
            foreach (var s in sanphams)
            {
                sps.Add(s);
            }    
            return sps;

        }

        public List<SanPham> GetSanPhams1()
        {
            var query = from sp in qlcf.SanPhams
                        join tl in qlcf.TheLoais on sp.MaTheLoai equals tl.MaTheLoai
                        select sp;

            List<SanPham> results = query.ToList();
            return results;

        }
        public List<SanPham> GetSanPhamsTheoMa(int ma)
        {
            var khachhangs = (from kh in qlcf.SanPhams
                              where kh.MaSanPham == ma
                              select kh).ToList();

            return khachhangs;

        }
        public SanPham Get1SanPhamsTheoMa(int ma)
        {
            var sanpham = qlcf.SanPhams.FirstOrDefault(kh => kh.MaSanPham == ma);

            return sanpham;

        }
        public List<SanPham> GetSanPhamsTheoTen(string ma)
        {
            var khachhangs = (from kh in qlcf.SanPhams
                              where kh.TenSanPham.Contains(ma)
                              select kh).ToList();

            return khachhangs;

        }
        public void ThemSP(string ten, decimal gia, int maloai, byte[] hinh)
        {
            SanPham sp = new SanPham();
            //.MaKhachHang = ma;
            sp.TenSanPham = ten;
            sp.GiaTien = gia;
            sp.MaTheLoai = maloai;
            sp.HinhAnh=hinh;
            qlcf.SanPhams.InsertOnSubmit(sp);
            qlcf.SubmitChanges();
        }
        public void SuaSP(int masp,string ten, decimal gia, int maloai, byte[] hinh)
        {
            SanPham spSua = qlcf.SanPhams.Where(sps => sps.MaSanPham== masp).FirstOrDefault();
            if (spSua != null)
            {
                spSua.TenSanPham = ten;
                spSua.GiaTien = gia;
                spSua.MaTheLoai = maloai;
                spSua.HinhAnh=hinh;
                qlcf.SubmitChanges();

            }
        }
        public void XoaSP(int masp)
        {
            SanPham khXoa = qlcf.SanPhams.Where(kh => kh.MaSanPham == masp).FirstOrDefault();
            if (khXoa != null)
            {
                qlcf.SanPhams.DeleteOnSubmit(khXoa);
                qlcf.SubmitChanges();
            }
        }
        public bool KTMaSP(int ma)
        {
            var existingCustomer = qlcf.SanPhams.SingleOrDefault(kh => kh.MaSanPham == ma);
            if (existingCustomer != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool KTTenSP(string ten)
        {
            var existingCustomer = qlcf.SanPhams.SingleOrDefault(kh => kh.TenSanPham == ten);
            if (existingCustomer != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public List<TopProductDTO> Top5SanPham()
        {
            var top5Products = (
                from sp in qlcf.SanPhams
                join ctdh in qlcf.ChiTietDonHangs on sp.MaSanPham equals ctdh.MaSanPham
                group ctdh by sp.TenSanPham into g
                orderby g.Sum(ct => ct.SoLuong) descending
                select new TopProductDTO
                {
                    TenSanPham = g.Key,
                    TongSoLuongDaBan = (int)g.Sum(ct => ct.SoLuong)
                }
            ).Take(5).ToList();

            return top5Products;
        }
        public class TopProductDTO
        {
            public string TenSanPham { get; set; }
            public int TongSoLuongDaBan { get; set; }
        }
        public void SuaSPtheoCTHoaDon(int makh)
        {
            ChiTietDonHang khSua = qlcf.ChiTietDonHangs.Where(khs => khs.MaSanPham == makh).FirstOrDefault();
            if (khSua != null)
            {
                khSua.MaSanPham = null;
                qlcf.SubmitChanges();

            }
        }
    }
}
