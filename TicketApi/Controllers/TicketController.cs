using Azure.Storage.Queues;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace TicketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly IConfiguration _configuration;
        public TicketController(ILogger<TicketController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from Tickets Controller - GET");
        }

        [HttpPost]
        public async Task<IActionResult> Post(TicketOrder ticketOrder)
        {
            // Validate the ticket information
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            //
            // Post ticketOrder to Storage Queue
            //

            string queueName = "tickets";

            // Get connection string from secrets.json
            string? connectionString = _configuration["AzureStorageConnectionString"];

            if (string.IsNullOrEmpty(connectionString))
            {
                return BadRequest("An error was encountered");
            }

            QueueClient queueClient = new QueueClient(connectionString, queueName);

            // serialize an object to json
            string message = JsonSerializer.Serialize(ticketOrder);

            // send string message to queue (must encode as base64 to work properly)
            var plainTextBytes = Encoding.UTF8.GetBytes(message);
            await queueClient.SendMessageAsync(Convert.ToBase64String(plainTextBytes));

            // send string message to queue
            //await queueClient.SendMessageAsync(message);

            return StatusCode(StatusCodes.Status201Created, new { message = "Success - message posted to Storage Queue" });
            //return Ok("Success - message posted to Storage Queue");

            //return StatusCode(StatusCodes.Status201Created);
        }
    }
}
