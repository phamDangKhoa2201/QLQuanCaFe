using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLQuanCaFe.Report
{
    public partial class CFContext : DbContext
    {
        public CFContext()
            : base("name=CFContext")
        {
        }

        public virtual DbSet<Ban> Bans { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ChiTietToppingRP> ChiTietToppings { get; set; }
        public virtual DbSet<DonHangRP> DonHangs { get; set; }
        public virtual DbSet<KhachHangRP> KhachHangs { get; set; }
        public virtual DbSet<NhanVienRP> NhanViens { get; set; }
        public virtual DbSet<SanPhamRP> SanPhams { get; set; }
        public virtual DbSet<ToppingRP> Toppings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ban>()
                .HasMany(e => e.DonHangs)
                .WithOptional(e => e.Ban)
                .HasForeignKey(e => e.IDTable);

            modelBuilder.Entity<DonHangRP>()
                .Property(e => e.TongTien)
                .HasPrecision(10, 2);

            modelBuilder.Entity<NhanVienRP>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVienRP>()
                .Property(e => e.pass)
                .IsUnicode(false);

            modelBuilder.Entity<SanPhamRP>()
                .Property(e => e.GiaTien)
                .HasPrecision(10, 2);
        }
    }
}
