using APBD_08.DTOs;
using APBD_08.Interfaces;
using APBD_08.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APBD_08.Controllers
{
    [Route("api")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {

        private IDatabaseService _dbService;

        public PrescriptionsController(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("doctors/{idDoctor}")]
        public async Task<IActionResult> GetDoctorAsync(int idDoctor)
        {
            var doc = await _dbService.GetDoctorAsync(idDoctor);
            if (doc == null)
                return NotFound("Doctor not found");
            
            return Ok(doc);
        }

        
        [HttpPost("doctors")]
        public async Task<IActionResult> AddDoctorAsync(AddUpdateDoctorRequestDto doctor)
        {
            await _dbService.AddDoctorAsync(doctor);
            return Ok("Added doctor: " + doctor.FirstName + " " + doctor.LastName);
        }


        [HttpPut("doctors/{idDoctor}")]
        public async Task<IActionResult> UpdateDoctorAsync(int idDoctor, [FromBody] AddUpdateDoctorRequestDto doctor)
        {
            var doc = await _dbService.UpdateDoctorAsync(idDoctor, doctor);
            if (doc == null)
                return NotFound("Doctor not found");
            return Ok("Doctor updated");
        }

       

        // DELETE api/<ValuesController>/5
        [HttpDelete("doctors/{idDoctor}")]
        public async Task<IActionResult> DeleteDoctorAsync(int idDoctor)
        {
            
            var res = await _dbService.DeleteDoctorAsync(idDoctor);
            if (res == 1)
                return NotFound("Doctor not found");
            if (res == 2)
                return Conflict("Can't delete. Doctor has prescriptions");
            return Ok("Doctor " + idDoctor + " deleted");
        }

        [HttpGet("prescriptions/{idPrescription}")]
        public async Task<IActionResult> GetPrescriptionAsync(int idPrescription)
        {
            var res = await _dbService.GetPrescriptionAsync(idPrescription);
            if (res == null)
                return NotFound("Prescription not found");
            return Ok(res);
        }
    }
}
