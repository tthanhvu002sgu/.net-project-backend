using Microsoft.EntityFrameworkCore;

namespace DoAn_API.Data
{
    public class MyDbContext : DbContext
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
        //public DbSet<product> Products { get; set; }
        //public DbSet<Type> Types { get; set; }
        //public DbSet<DonHang> DonHangs { get; set; }
        //public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Specialization> Specializations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                    e.HasOne(e => e.doctor).WithMany(e => e.appointments).HasForeignKey(e => e.doctorId);
                    e.HasOne(e => e.schedule).WithOne(e => e.appointment).HasForeignKey<Schedule>(e => e.scheduleId);
                    e.HasOne(e => e.patient).WithMany(e => e.appointments).HasForeignKey(e => e.patientId);
                });
            modelBuilder.Entity<Doctor>(
                e =>
                {
                    e.ToTable("Doctor");
                    e.HasKey(e => e.doctorId);
                    e.HasMany(e => e.schedules).WithOne(e => e.doctor).HasForeignKey(e => e.scheduleId);
                    e.HasMany(e => e.specializations).WithMany(e => e.doctors).UsingEntity<Dictionary<string, object>>(
                        "DoctorSpecialization",
                        e => e.HasOne<Specialization>().WithMany().HasForeignKey("specializationId"),
                        e => e.HasOne<Doctor>().WithMany().HasForeignKey("doctorId"),
                        e =>
                        {
                            e.HasKey("doctorId", "specializationId");
                            e.ToTable("DoctorSpecialization");
                        }

                    );


                });
            modelBuilder.Entity<Patient>(
                e =>
                {
                    e.ToTable("Patient");
                    e.HasKey(e => e.patientId);
                }

            );
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
