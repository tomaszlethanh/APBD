using APBD_08.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD_08.EfConfigurations
{
    public class DoctorEntityTypeConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> opt)
        {
            opt.HasKey(e => e.IdDoctor);
            opt.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
            opt.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            opt.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            opt.Property(e => e.Email).IsRequired().HasMaxLength(100);

            opt.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Han", LastName = "Solo", Email = "hansolo@gmail.com" },
                    new Doctor { IdDoctor = 2, FirstName = "Rec", LastName = "Covery", Email = "recovery@gmail.com" }
                );
        }
    }
}

