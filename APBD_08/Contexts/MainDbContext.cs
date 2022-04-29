using APBD_08.EfConfigurations;
using APBD_08.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_08.Models
{
    public class MainDbContext : DbContext, IPrescriptionDbContext
    {

        private IConfiguration _configuration;

        public MainDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MainDbContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ProductionDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MedicamentEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new PrescriptionMedicamentEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new PrescriptionEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new DoctorEntityTypeConfiguration());
        }

    }
}
