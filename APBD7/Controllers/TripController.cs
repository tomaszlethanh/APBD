using APBD7.Interfaces;
using APBD7.Models;
using APBD7.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APBD7.Controllers
{
    [Route("api")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripDbContext _context;


        public TripController(ITripDbContext context)
        {
            _context = context;
        }


        [HttpGet("trips")]
        public IActionResult GetTrips()
        {
            var ct = _context.CountryTrips;
            var result = _context.Trips.OrderByDescending(e => e.DateFrom).Select(e => new GetTripsRequestDto
            {
                Name = e.Name,
                Description = e.Description,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                MaxPeople = e.MaxPeople
            });
            
            return Ok(result);
        }

        
        
        [HttpPost("trips/{idTrip}/clients")]
        public IActionResult CreateClientAndTrip([FromBody] CreateClientAndTripRequestDto jsonText)
        {
            bool exists = _context.Clients.Any(e => e.Pesel == jsonText.Pesel);
            if (!exists)
            {
                _context.Clients.Add(new Client
                {
                    IdClient = _context.Clients.Max(e => e.IdClient) + 1,
                    FirstName = jsonText.FirstName,
                    LastName = jsonText.LastName,
                    Email = jsonText.Email,
                    Telephone = jsonText.Telephone,
                    Pesel = jsonText.Pesel
                });
                _context.SaveChanges();
            }

            bool tripExists = _context.Trips.Any(e => e.IdTrip == jsonText.IdTrip && e.Name == jsonText.TripName);
            if (!tripExists)
            {
                return NotFound("Trip not found");
            }

            Client client = _context.Clients.FirstOrDefault(e => e.Pesel == jsonText.Pesel);

            bool tripAssigned = _context.ClientTrips.Any(e => e.IdClient == client.IdClient && e.IdTrip == jsonText.IdTrip);
            if (tripAssigned)
            {
                return Conflict("Client already assigned to trip");
            }

            _context.ClientTrips.Add(new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = jsonText.IdTrip,
                PaymentDate = jsonText.PaymentDate,
                RegisteredAt = DateTime.Now
            });

            _context.SaveChanges();

            return Ok("ClientTrip added");
        }

        

        [HttpDelete("clients/{idClient}")]
        public IActionResult Delete(int idClient)
        {
            Client client = _context.Clients.FirstOrDefault(e => e.IdClient == idClient);
            bool hasTrips = _context.ClientTrips.Any(e => e.IdClient == idClient);
            if (hasTrips)
                return Conflict("Can't delete. Client has trips.");

            _context.Clients.Remove(client);
            _context.SaveChanges();
            return Ok("Client removed");
             
        }
    }
}
