using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class Ban_DAL_BLL
    {
        QLCFDataContext data = new QLCFDataContext();
        public Ban_DAL_BLL() { }
        public List<Ban> getBan()
        {
            var bans = data.Bans.Select(d => d).ToList();
            return bans;
        }

        public void themBan(Ban t)
        {
            Ban per = new Ban();
            per.ID = t.ID;
            per.Name = t.Name;
            per.status = "Trống";
            data.Bans.InsertOnSubmit(per);
            data.SubmitChanges();
        }
        public void xoaBan(Ban t)
        {
            Ban per = data.Bans.Where(us => us.ID == t.ID).FirstOrDefault();
            if (per != null)
            {
                data.Bans.DeleteOnSubmit(per);
                data.SubmitChanges();
            }
        }
        public void suaBan(Ban t)
        {
            Ban per = data.Bans.Where(us => us.ID == t.ID).FirstOrDefault();
            if (per != null)
            {
                per.Name = t.Name;
                data.SubmitChanges();
            }
        }
        public void updateBan(int id)
        {
            Ban b = data.Bans.Where(x => x.ID == id).FirstOrDefault();
            if (b != null)
            {

                b.status = "Có Người";
                data.SubmitChanges();
            }
        }

        public void updateBan2(int id)
        {
            Ban b = data.Bans.Where(x => x.ID == id).FirstOrDefault();
            if (b != null)
            {

                b.status = "Trống";
                data.SubmitChanges();
            }
        }

        public void chuyenBan(int id1, int id2)
        {
            int idFirstBill = (from b in data.DonHangs where b.IDTable == id1 && b.TongTien == null select b.MaDonHang).FirstOrDefault();
            int idSecondBill = (from b in data.DonHangs where b.IDTable == id2 && b.TongTien == null select b.MaDonHang).FirstOrDefault();

            int isFirstTablEmty = 1;
            int isSecondTablEmty = 1;

            if (idFirstBill == 0)
            {
                DonHang t1 = new DonHang();
                t1.NgayDatHang = DateTime.Now;
                t1.IDTable = id1;
                data.DonHangs.InsertOnSubmit(t1);
                data.SubmitChanges();
                idFirstBill = (from b in data.DonHangs where b.IDTable == id1 && b.TongTien == null select b.MaDonHang).Max();
            }
            isFirstTablEmty = (from b in data.ChiTietDonHangs where b.MaDonHang == idFirstBill select b).Count();
            if (idSecondBill == 0)
            {
                DonHang t1 = new DonHang();
                t1.NgayDatHang = DateTime.Now;
                t1.IDTable = id2;
                data.DonHangs.InsertOnSubmit(t1);
                data.SubmitChanges();
                idSecondBill = (from b in data.DonHangs where b.IDTable == id2 && b.TongTien == null select b.MaDonHang).Max();
            }
            isSecondTablEmty = (from b in data.ChiTietDonHangs where b.MaDonHang == idSecondBill select b).Count();

            var IDBillInfoTable = from bi in data.ChiTietDonHangs where bi.MaDonHang == idSecondBill select bi;

            List<ChiTietDonHang> tam = data.ChiTietDonHangs.Where(x => x.MaDonHang == idSecondBill).ToList();

            List<ChiTietDonHang> bt = data.ChiTietDonHangs.Where(x => x.MaDonHang == idFirstBill).ToList();
            foreach (ChiTietDonHang ctdh in bt)
            {
                ctdh.MaDonHang = idSecondBill;
                data.SubmitChanges();
            }
            List<ChiTietTopping> cttp1 = data.ChiTietToppings.Where(tp => tp.MaDonHang == idSecondBill).ToList();
            foreach (var tp in cttp1)
            {
                tp.MaDonHang = idSecondBill; data.SubmitChanges();
            }

            List<ChiTietDonHang> bt2 = data.ChiTietDonHangs.Where(x => x.MaDonHang == idFirstBill).ToList();
            foreach (ChiTietDonHang ctdh in tam)
            {
                ctdh.MaDonHang = idSecondBill;
                data.SubmitChanges();
            }
            List<ChiTietTopping> cttp2 = data.ChiTietToppings.Where(tp => tp.MaDonHang == idFirstBill).ToList();
            foreach (var tp in cttp2)
            {
                tp.MaDonHang = idSecondBill; data.SubmitChanges();
            }
            isFirstTablEmty = (from b in data.ChiTietDonHangs where b.MaDonHang == idFirstBill select b).Count();
            isSecondTablEmty = (from b in data.ChiTietDonHangs where b.MaDonHang == idSecondBill select b).Count();
            if (isFirstTablEmty == 0)
            {
                Ban bn = data.Bans.Where(t => t.ID == id1).FirstOrDefault();
                bn.status = "Trống";
                data.SubmitChanges();
            }
            else
            {
                updateBan(id1);
            }

            if (isSecondTablEmty == 0)
            {
                Ban bn = data.Bans.Where(t => t.ID == id2).FirstOrDefault();
                bn.status = "Trống";
                data.SubmitChanges();
            }
            else
            {
                updateBan(id2);
            }
        }
    }
}
