using APBD_08.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD_08.EfConfigurations
{
    public class PrescriptionMedicamentEntityTypeConfiguration : IEntityTypeConfiguration<Prescription_Medicament>
    {
        public void Configure(EntityTypeBuilder<Prescription_Medicament> opt)
        {
            opt.HasKey(e => new { e.IdMedicament, e.IdPrescription });
            opt.HasOne(pm => pm.Medicament)
            .WithMany(m => m.Prescription_Medicaments)
            .HasForeignKey(pm => pm.IdMedicament);

            opt.HasOne(pm => pm.Prescription)
            .WithMany(m => m.Prescription_Medicaments)
            .HasForeignKey(pm => pm.IdPrescription);

            opt.Property(e => e.Dose).IsRequired(false);
            opt.Property(e => e.Details).IsRequired().HasMaxLength(100);

            opt.HasData(
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "ABC" },
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 2, Dose = 1, Details = "DEF"}
                );
        }
    }
}
