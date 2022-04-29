using APBD_08.DTOs;
using APBD_08.Interfaces;
using APBD_08.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace APBD_08.Services
{
    public class SqlServerDatabaseService : IDatabaseService
    {
        private IConfiguration _confugration;
        private IPrescriptionDbContext _dbContext;

        public SqlServerDatabaseService(IConfiguration configuration, IPrescriptionDbContext dbContext)
        {
            _confugration = configuration;
            _dbContext = dbContext;
        }

        public async Task<GetDoctorRequestDto> GetDoctorAsync(int idDoctor)
        {
            var doc =  await _dbContext.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == idDoctor);
            if (doc == null)
                return null;
            GetDoctorRequestDto res = new GetDoctorRequestDto
            {
                IdDoctor = doc.IdDoctor,
                FirstName = doc.FirstName,
                LastName = doc.LastName,
                Email = doc.Email
            };
            return res;
        }

        public async Task<int> AddDoctorAsync(AddUpdateDoctorRequestDto doctor)
        {
            Doctor doc = new Doctor
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
            await _dbContext.Doctors.AddAsync(doc);
            await _dbContext.SaveChangesAsync(new CancellationToken());
            return 1;
        }

        public async Task<int?> UpdateDoctorAsync(int idDoctor, AddUpdateDoctorRequestDto doctor)
        {
            Doctor doc = await _dbContext.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == idDoctor);
            if (doc == null)
                return null;

            doc.FirstName = doctor.FirstName;
            doc.LastName = doctor.LastName;
            doc.Email = doctor.Email;
            await _dbContext.SaveChangesAsync(new CancellationToken());
            return 1;
        }

        public async Task<int> DeleteDoctorAsync(int idDoctor)
        {
            Doctor doc = await _dbContext.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == idDoctor);
            if (doc == null)
                return 1;

            bool hasPrescriptions = await _dbContext.Prescriptions.AnyAsync(e => e.IdDoctor == idDoctor);
            if (hasPrescriptions)
                return 2;

            Doctor doctor = await _dbContext
                .Doctors
                .FirstOrDefaultAsync(e => e.IdDoctor == idDoctor);

            _dbContext.Doctors.Remove(doctor);
            await _dbContext.SaveChangesAsync(new CancellationToken());
            return 0;
        }

        public async Task<GetPrescriptionRequestDto> GetPrescriptionAsync(int idPrescription)
        {
            Prescription pres = await _dbContext
                .Prescriptions
                .FirstOrDefaultAsync(e => e.IdPrescription == idPrescription);
            if (pres == null)
                return null;

            Patient patient = await _dbContext
                .Patients
                .FirstOrDefaultAsync(e => e.IdPatient == pres.IdPatient);

            Doctor doctor = await _dbContext
                .Doctors
                .FirstOrDefaultAsync(e => e.IdDoctor == pres.IdDoctor);

            ICollection<string> medList = new List<string>();
            ICollection<Prescription_Medicament> presmed = pres.Prescription_Medicaments.ToList();

            foreach (Prescription_Medicament pm in presmed)
            {
                medList.Add(pm.Medicament.Name);
            }

            GetPrescriptionRequestDto res = new GetPrescriptionRequestDto
            {
                IdPrescription = idPrescription,
                PatientName = patient.FirstName,
                PatientLastName = patient.LastName,
                PatientBirthDate = patient.BirthDate,
                DoctorName = doctor.FirstName,
                DoctorLastName = doctor.LastName,
                Medicaments = medList
            };
            
            return res;
        }
    }       
}
