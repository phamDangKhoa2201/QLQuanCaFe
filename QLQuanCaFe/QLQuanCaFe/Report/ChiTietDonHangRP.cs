namespace QLQuanCaFe.Report
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        [Key]
        public int MaChiTietDonHang { get; set; }

        public int? MaDonHang { get; set; }

        public int? MaSanPham { get; set; }

        public int? SoLuong { get; set; }

        public virtual DonHangRP DonHang { get; set; }

        public virtual SanPhamRP SanPham { get; set; }
    }
}
