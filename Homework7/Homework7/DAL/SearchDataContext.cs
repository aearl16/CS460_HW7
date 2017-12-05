namespace Homework7.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SearchDataContext : DbContext
    {
        public SearchDataContext()
            : base("name=SearchDataContext")
        {
        }

        public virtual DbSet<DataLog> datalogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataLog>()
                .Property(e => e.SearchRequest)
                .IsUnicode(false);

            modelBuilder.Entity<DataLog>()
                .Property(e => e.Agent)
                .IsUnicode(false);

            modelBuilder.Entity<DataLog>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);
        }
    }
}
