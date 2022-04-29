using APBD_08.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD_08.EfConfigurations
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> opt)
        {
            opt.HasKey(e => e.IdPatient);
            opt.Property(e => e.IdPatient).ValueGeneratedOnAdd();
            opt.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            opt.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            opt.Property(e => e.BirthDate).IsRequired();

            opt.HasData(
                    new Patient { IdPatient = 1, FirstName = "Gizmo", LastName = "Big", BirthDate = new System.DateTime(1999, 2, 14) },
                    new Patient { IdPatient = 2, FirstName = "Jinx", LastName = "Ryoma", BirthDate = new System.DateTime(2000, 8, 18) }
                );
        }
    }
}
