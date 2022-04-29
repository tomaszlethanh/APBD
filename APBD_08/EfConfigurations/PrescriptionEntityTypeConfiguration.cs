using System;
using APBD_08.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD_08.EfConfigurations
{
    public class PrescriptionEntityTypeConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> opt)
        {
            opt.HasKey(e => e.IdPrescription);
            opt.Property(e => e.IdPrescription).ValueGeneratedOnAdd();
            opt.Property(e => e.Date).IsRequired();
            opt.Property(e => e.DueDate).IsRequired();
            opt.HasOne(e => e.Patient).WithMany(p => p.Prescriptions).HasForeignKey(e => e.IdPatient);
            opt.HasOne(e => e.Doctor).WithMany(p => p.Prescriptions).HasForeignKey(e => e.IdDoctor);

            opt.HasData(
                    new Prescription { IdPrescription = 1, Date = new DateTime(2018, 6, 28), DueDate = new DateTime(2018, 12, 28), IdPatient = 1, IdDoctor = 1},
                    new Prescription { IdPrescription = 2, Date = new DateTime(2020, 3, 26), DueDate = new DateTime(2020, 10, 16), IdPatient = 2, IdDoctor = 1 }
                );
        }
    }
}
