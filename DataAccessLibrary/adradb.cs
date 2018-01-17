namespace DataAccessLibrary
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class adradb : DbContext
    {
        public adradb()
            : base("name=adradb")
        {
        }

        public virtual DbSet<accountbalance> accountbalances { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.fname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.lname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.salt)
                .IsUnicode(false);
        }
    }
}
