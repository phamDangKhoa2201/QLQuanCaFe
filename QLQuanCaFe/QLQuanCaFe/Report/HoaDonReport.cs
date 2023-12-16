using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaFe.Report
{
    public class HoaDonReport
    {
        public HoaDonReport() { }
        public int MaHD { get; set; }
        public string TenKH { get; set; }
        public string TenNV { get; set;}
        public string TenBan { get; set; }
        public int TongTien { get; set; }
        public DateTime Ngay { get; set; }
        public string TenSP { get; set; }
        public string TenTopping { get; set; }

        public decimal GiaSP { get; set; } 
        public decimal GiaTopping { get; set; } 
        public int SoLuongSP { get; set; }
        public int SoLuongTopping { get; set; }
        public int ThanhTien { get; set; }
       
    }
}
