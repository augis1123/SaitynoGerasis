using SaitynoGerasis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ShopDbContext : IdentityDbContext<naudotojas>
    {
        public DbSet<preke> Preke { get; set; }
        public DbSet<pardavejas> Pardavejas { get; set; }
        public DbSet<saskaita> Saskaita { get; set; }
        public DbSet<perkamapreke> perkamapreke { get; set; }
        //public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseMySQL("Server=duomenubaze.mysql.database.azure.com;UserID =augustasdruceika;Password=askietasS!;Database=mazmenine;SslMode=Required;SslCa=DigiCertGlobalRootCA.crt(3).pem;");
            //optionBuilder.UseMySQL("Data Source=127.0.0.1;port=3306;Initial Catalog=mazmenine;User Id=root;Password=;SslMode=none;Convert Zero Datetime=True;");
            //optionBuilder.UseSqlServer("Data Source=127.0.0.1;port=3306;Initial Catalog=mazmenine;User Id=root;Password=;SslMode=none;Convert Zero Datetime=True;");
        }
    }
}
