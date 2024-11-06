using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoAn_API.Data
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public MyDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("default");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        //dbset


        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<DonHang>(e =>
            //{
            //    e.ToTable("DonHang");
            //    e.HasKey(dh => dh.MaDonHang);
            //    e.Property(dh => dh.NgayDat).HasDefaultValueSql("current_date()");
            //    e.Property(e => e.Hoten).IsRequired();
            //});



            modelBuilder.Entity<Appointment>(
                e =>
                {
                    e.ToTable("Appointment");
                    e.HasKey(e => e.appointmentId);
                    e.HasOne(e => e.payment).WithOne(e => e.appointment).HasForeignKey<Payment>(e => e.appointmentId);
                    e.Property(e => e.appointmentStatus).HasConversion<int>();

                });
            modelBuilder.Entity<Doctor>(
                e =>
                {
                    e.ToTable("Doctor");
                    e.HasKey(e => e.doctorId);

                    e.HasMany(e => e.appointments).WithOne(e => e.doctor).HasForeignKey(e => e.doctorId);
                    e.HasMany(e => e.schedules).WithOne(e => e.doctor).HasForeignKey(e => e.doctorId);

                    e.HasOne(e => e.specialization).WithMany(e => e.doctors).HasForeignKey(e => e.specializationId).OnDelete(DeleteBehavior.Cascade);


                });

            modelBuilder.Entity<Patient>(
                e =>
                {
                    e.ToTable("Patient");

                    e.HasKey(e => e.patientId);

                    // Đúng
                    e.HasMany(e => e.appointments).WithOne(e => e.patient).HasForeignKey(e => e.patientId);
                });




            modelBuilder.Entity<Schedule>(
                e =>
                {
                    e.ToTable("Schedule");
                    e.HasKey(e => e.scheduleId);
                }
            );

        }
    }
}
