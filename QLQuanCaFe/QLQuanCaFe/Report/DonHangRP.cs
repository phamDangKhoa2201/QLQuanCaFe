namespace QLQuanCaFe.Report
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHangRP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHangRP()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietToppings = new HashSet<ChiTietToppingRP>();
        }

        [Key]
        public int MaDonHang { get; set; }

        public int? MaKhachHang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDatHang { get; set; }

        public int? MaNhanVien { get; set; }

        public decimal? TongTien { get; set; }

        public int? IDTable { get; set; }

        public virtual Ban Ban { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietToppingRP> ChiTietToppings { get; set; }

        public virtual KhachHangRP KhachHang { get; set; }

        public virtual NhanVienRP NhanVien { get; set; }
        
    }
}
