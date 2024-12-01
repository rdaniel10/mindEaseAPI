using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mindEaseAPI.Context;
using mindEaseAPI.Models;
using mindEaseAPI.Parameters;
using System.Text.Json.Nodes;

namespace mindEaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BookingController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost("SubmitBooking")]
        public async Task<IActionResult> BookingSubmit([FromBody] booking_details_model body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //var checkActiveAppoint = await _context.booking_information.Where(o => o.clientId.Equals(body.clientId) && o.activeAppointment == true).FirstOrDefaultAsync();

                //if (checkActiveAppoint == null)
                //{

                //}

                var guid = Guid.NewGuid().ToString().Replace("-", "");

                var booking = new booking_information
                {
                    bookingId = guid,
                    clientId = body.clientId,
                    doctorName = body.therapist,
                    appointmentDate = body.appointmentDate,
                    appointmentSession = body.appointmentSession,
                    activeAppointment = true
                };

                _context.Add(booking);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Booking uploaded successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBookedAppointments()
        {
            try
            {
                var allBooked = await _context.booking_information.ToListAsync();

                List<Object> details = new List<Object>();

                foreach (var booking in allBooked)
                {
                    var getClient = await _context.client_information.Where(o => o.clientId.Equals(booking.clientId)).FirstOrDefaultAsync();

                    var obj = new get_all_details
                    {
                        doctorName = booking.doctorName,
                        appointmentDate = booking.appointmentDate,
                        appointmentSession = booking.appointmentSession,
                        clientName = getClient.clientName,
                        clientGender = getClient.clientGender,
                        activeAppointment = booking.activeAppointment
                    };
                    details.Add(obj);
                }

                return Ok(details);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBookedAppointment/{clientId}")]
        public async Task<IActionResult> GetClientBookedAppointments([FromRoute] string clientId)
        {
            try
            {
                var getClientBooked = await _context.booking_information.Where(o => o.clientId.Equals(clientId)).OrderByDescending(o => o.appointmentDate).ToListAsync();

                List<Object> details = new List<Object>();

                foreach (var booking in getClientBooked)
                {
                    var book = new booking_information
                    {
                        doctorName = booking.doctorName,
                        appointmentDate = booking.appointmentDate,
                        appointmentSession = booking.appointmentSession,
                        activeAppointment = booking.activeAppointment
                    };
                    details.Add(book);
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("CheckAppointment/{clientId}")]
        public async Task<IActionResult> CheckAppointment([FromRoute] string clientId)
        {
            try
            {
                var checkActiveAppoint = await _context.booking_information.Where(o => o.clientId.Equals(clientId) && o.activeAppointment == true).FirstOrDefaultAsync();

                if (checkActiveAppoint == null)
                {
                    return Ok(new { message = "No upcoming appointment." });
                }
                else
                {
                    return Accepted(new { message = "Upcoming appointment detected." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
