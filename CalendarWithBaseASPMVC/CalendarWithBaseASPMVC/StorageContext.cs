namespace CalendarWithBaseASPMVC
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StorageContext : DbContext
    {
        public StorageContext()
            : base("name=StorageContext")
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(e => e.timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.Appointment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attendance>()
                .Property(e => e.timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Person>()
                .Property(e => e.timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);
        }
    }
}
