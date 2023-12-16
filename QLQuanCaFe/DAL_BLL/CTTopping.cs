using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class CTTopping
    {
        public CTTopping() { }  
        QLCFDataContext qlcf = new QLCFDataContext();
        public void ThemCTTopping(ChiTietTopping cdtp1)
        {
            ChiTietTopping cttp = new ChiTietTopping();
            cttp.MaDonHang = cdtp1.MaDonHang;
            cttp.MaTopping = cdtp1.MaTopping;
            cttp.SoLuong = cdtp1.SoLuong;
            qlcf.ChiTietToppings.InsertOnSubmit(cttp);
            qlcf.SubmitChanges();
        }
        public void insertToppingInfo(int madh,int matopping,int sl)
        {
            ChiTietTopping cttp = new ChiTietTopping();
            var tp = from t in qlcf.ChiTietToppings where t.MaTopping == matopping && t.MaDonHang == madh select t.MaChiTietTopping;
            int a=int.Parse(tp.FirstOrDefault().ToString());
            if(a!=0)
            {
                ChiTietTopping tt=qlcf.ChiTietToppings.Where(t => t.MaChiTietTopping==tp.FirstOrDefault()).FirstOrDefault();
                if (tt!=null)
                {
                    int tam = 0;
                    tam += int.Parse(tt.SoLuong.ToString()) + sl;

                    qlcf.ChiTietToppings.DeleteOnSubmit(tt);
                    qlcf.SubmitChanges();

                    cttp.MaDonHang = madh;
                    cttp.MaTopping = matopping;
                    cttp.SoLuong = tam;
                    qlcf.ChiTietToppings.InsertOnSubmit(cttp);
                    qlcf.SubmitChanges();
                }
            }
            else
            {
                cttp.MaDonHang = madh;
                cttp.MaTopping = matopping;
                cttp.SoLuong = sl;
                qlcf.ChiTietToppings.InsertOnSubmit(cttp);
                qlcf.SubmitChanges();
            }    
        }
    }
}
