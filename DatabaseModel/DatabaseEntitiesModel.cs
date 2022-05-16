using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DatabaseModel
{
    public partial class DatabaseEntitiesModel : DbContext
    {
        public DatabaseEntitiesModel()
            : base("name=DatabaseEntitiesModel")
        {
        }

        public virtual DbSet<Accomodation> Accomodation { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Extras> Extras { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Records> Records { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accomodation>()
                .HasMany(e => e.Records)
                .WithRequired(e => e.Accomodation1)
                .HasForeignKey(e => e.Accomodation);

            modelBuilder.Entity<Company>()
                .Property(e => e.Postcode)
                .IsFixedLength();

            modelBuilder.Entity<Company>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Records>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Records>()
                .Property(e => e.CNP)
                .IsFixedLength();

            modelBuilder.Entity<Records>()
                .Property(e => e.Series)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Records>()
                .Property(e => e.Number)
                .IsFixedLength();
        }
    }
}
