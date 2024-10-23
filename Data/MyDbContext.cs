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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DonHang>(e =>
            //{
            //    e.ToTable("DonHang");
            //    e.HasKey(dh => dh.MaDonHang);
            //    e.Property(dh => dh.NgayDat).HasDefaultValueSql("current_date()");
            //    e.Property(e => e.Hoten).IsRequired();
            //});
            modelBuilder.Entity<User>(
                e =>
                {
                    e.HasKey(e => e.userId);
                    e.HasMany(e => e.roles).WithMany(e => e.users).UsingEntity<Dictionary<string, object>>(
                       "PatientRole",
                       e => e.HasOne<Role>().WithMany().HasForeignKey("roleId"),
                       e => e.HasOne<User>().WithMany().HasForeignKey("userId"),
                       e =>
                       {
                           e.HasKey("userId", "roleId");
                           e.ToTable("UserRole");
                       }

                   );
                }
            );
            modelBuilder.Entity<Appointment>(
                e =>
                {
                    e.ToTable("Appointment");
                    e.HasKey(e => e.appointmentId);
                    e.HasOne(e => e.payment).WithOne(e => e.appointment).HasForeignKey<Payment>(e => e.paymentId);
                    e.Property(e => e.appointmentStatus).HasConversion<int>();
                });
            modelBuilder.Entity<Doctor>(
                e =>
                {
                    e.ToTable("Doctor");
                    e.HasBaseType<User>();
                    e.HasMany(e => e.appointments).WithOne(e => e.doctor).HasForeignKey(e => e.appointmentId);
                    e.HasMany(e => e.schedules).WithOne(e => e.doctor).HasForeignKey(e => e.scheduleId);
                    e.HasMany(e => e.specializations).WithMany(e => e.doctors).UsingEntity<Dictionary<string, object>>(
                        "DoctorSpecialization",
                        e => e.HasOne<Specialization>().WithMany().HasForeignKey("specializationId"),
                        e => e.HasOne<Doctor>().WithMany().HasForeignKey("userId"),
                        e =>
                        {
                            e.HasKey("userId", "specializationId");
                            e.ToTable("DoctorSpecialization");
                        }

                    );


                });
            modelBuilder.Entity<Patient>(
                e =>
                {
                    e.HasBaseType<User>();

                    e.ToTable("Patient");
                    e.HasMany(e => e.appointments).WithOne(e => e.patient).HasForeignKey(e => e.appointmentId);

                }

            );
            modelBuilder.Entity<Schedule>(
                e =>
                {
                    e.ToTable("Schedule");
                    e.HasKey(e => e.scheduleId);
                }
            );
            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Role");
                e.HasKey(e => e.roleId);
            });

        }
    }
}
