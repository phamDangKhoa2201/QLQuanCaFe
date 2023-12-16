namespace QLQuanCaFe.Report
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietTopping")]
    public partial class ChiTietToppingRP
    {
        [Key]
        public int MaChiTietTopping { get; set; }

        public int? MaDonHang { get; set; }

        public int? MaTopping { get; set; }

        public int? SoLuong { get; set; }

        public virtual DonHangRP DonHang { get; set; }

        public virtual ToppingRP Topping { get; set; }
    }
}
