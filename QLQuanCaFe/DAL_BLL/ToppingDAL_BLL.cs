using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class ToppingDAL_BLL
    {
        public ToppingDAL_BLL() { }
        QLCFDataContext qlcf = new QLCFDataContext();
        
        public List<Topping> getToppping()
        {
            var khachhangs = qlcf.Toppings.Select(d => d).ToList();
            //(from kh in qlcf.KhachHangs select kh).ToList();
            return khachhangs;
        }
        public List<Topping> getToppingTheoMa(int makh)
        {
            var khachhangs = (from kh in qlcf.Toppings
                              where kh.MaTopping == makh
                              select kh).ToList();

            return khachhangs;
        }
        public List<Topping> getToppingTheoTen(string ten)
        {
            var khachhangs = (from kh in qlcf.Toppings
                              where kh.Ten.Contains(ten)
                              select kh).ToList();

            return khachhangs;
        }
        public void ThemTP( string ten, int gia)
        {
            Topping tp = new Topping();
            
            tp.Ten = ten;
            tp.Gia = gia;
            qlcf.Toppings.InsertOnSubmit(tp);
            qlcf.SubmitChanges();
        }
        public void SuaTP(int ma, string ten, int gia)
        {
            Topping tpSua = qlcf.Toppings.Where(khs => khs.MaTopping== ma).FirstOrDefault();
            if (tpSua != null)
            {
                tpSua.Ten = ten;
                tpSua.Gia = gia;
                qlcf.SubmitChanges();

            }
        }
        public bool KTMaTP(int ma)
        {
            var existingCustomer = qlcf.Toppings.SingleOrDefault(kh => kh.MaTopping == ma);
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
            Topping khXoa = qlcf.Toppings.Where(kh => kh.MaTopping == ma).FirstOrDefault();
            if (khXoa != null)
            {
                qlcf.Toppings.DeleteOnSubmit(khXoa);
                qlcf.SubmitChanges();
            }
        }
        public void SuaTPtheoCTTopping(int makh)
        {
            ChiTietTopping khSua = qlcf.ChiTietToppings.Where(khs => khs.MaTopping == makh).FirstOrDefault();
            if (khSua != null)
            {
                khSua.MaTopping = null;
                qlcf.SubmitChanges();

            }
        }
    }
}
