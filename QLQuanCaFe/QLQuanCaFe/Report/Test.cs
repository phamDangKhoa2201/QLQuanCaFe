using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaFe.Report
{
    public class Test
    {
        public Test() { }
        public int MaHD { get; set; }
        public string TenKH { get; set; }
        public string TenNV { get; set; }
        public string TenBan { get; set; }
        public int TongTien { get; set; }
        public DateTime Ngay { get; set; }
        public List<ChiTietDonHang> ctsp { get; set; }
        public List<ChiTietToppingRP> tps { get; set; }
    }
}
