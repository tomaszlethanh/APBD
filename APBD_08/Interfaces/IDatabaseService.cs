using APBD_08.DTOs;
using APBD_08.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_08.Interfaces
{
    public interface IDatabaseService
    {
        Task<GetDoctorRequestDto> GetDoctorAsync(int idDoctor);
        Task<int> AddDoctorAsync(AddUpdateDoctorRequestDto doctor);
        Task<int?> UpdateDoctorAsync(int idDoctor, AddUpdateDoctorRequestDto doctor);
        Task<int> DeleteDoctorAsync(int idDoctor);
        Task<GetPrescriptionRequestDto> GetPrescriptionAsync(int idReceipts);
    }
}
