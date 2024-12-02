using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mindEaseAPI.Context;
using mindEaseAPI.Models;
using mindEaseAPI.Parameters;

namespace mindEaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientInfoController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ClientInfoController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] client_information_model body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var guid = Guid.NewGuid().ToString().Replace("-", "");

                var client = new client_information
                {
                    clientId = guid,
                    clientName = body.clientFirstname + " " +body.clientLastName,
                    clientEmail= body.clientEmail,
                    clientNumber = body.clientNumber,
                    clientAge = body.clientAge,
                    clientGender = body.clientGender,
                    clientUsername = body.clientUsername,
                    clientPassword = body.clientPassword,
                    reservedAppointment = false
                };

                _context.Add(client);
                _context.SaveChanges();

                return Ok(new {message = "Uploaded Successfully."});

            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn([FromBody] login_model body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var checkAdmin = await _context.client_information.Where(o => o.clientUsername.Equals(body.clientUsername) && o.clientUsername.Equals("admin")).FirstOrDefaultAsync();

                if (checkAdmin != null)
                {
                    if (checkAdmin.clientPassword.Equals(body.clientPassword))
                    {
                        return Ok(new { message = "Admin login Successfully." });
                    }
                    else
                    {
                        return BadRequest(new { message = "Admin wrong Password." });
                    }
                }

                var checkUser = await _context.client_information.Where(o => o.clientUsername.Equals(body.clientUsername)).FirstOrDefaultAsync();

                if(checkUser != null)
                {
                    if (checkUser.clientPassword.Equals(body.clientPassword)) 
                    {
                        return Ok(new {message = "Login Successfully.", clientId = checkUser.clientId});
                    }
                    else
                    {
                        return BadRequest(new { message = "Wrong Password." });
                    }
                }
                else
                {
                    return BadRequest(new { message = "This username does not exist." });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("getClientDetails/{clientId}")]
        public async Task<IActionResult> GetClientDetails([FromRoute] string clientId)
        {
            var getClient = await _context.client_information.Where(o => o.clientId.Equals(clientId)).FirstOrDefaultAsync();

            var client = new client_details
            {
                clientName = getClient.clientName,
                clientGender = getClient.clientGender,
                clientEmail = getClient.clientEmail,
                clientAge = getClient.clientAge,
                clientNumber = getClient.clientNumber
            };

            return Ok(client);
        }

        [HttpGet("getAllClientDetails")]
        public async Task<IActionResult> GetAllClientDetails()
        {
            var getClient = await _context.client_information.ToListAsync();

            List<Object> details = new List<Object>();

            foreach (var client in getClient)
            {
                var obj = new client_details
                {
                    clientName = client.clientName,
                    clientGender = client.clientGender,
                    clientEmail = client.clientEmail,
                    clientAge = client.clientAge,
                    clientNumber = client.clientNumber
                };
                details.Add(obj);
            }

            return Ok(details);
        }
    }
}
