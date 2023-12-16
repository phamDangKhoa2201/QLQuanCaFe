using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class SanPhamWithTheLoai
    {
        public SanPhamWithTheLoai() { }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal GiaTien { get; set; }
        public decimal MaTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public Binary HinhAnh { get; set; }
    }
}
