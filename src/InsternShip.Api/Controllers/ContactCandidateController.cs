using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.ContactEmail;
using InsternShip.Service.Models;
using InsternShip.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsternShip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactCandidateController : ControllerBase
    {
        private readonly UserManager<WebUser> _userManager;
        private readonly SignInManager<WebUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public ContactCandidateController(UserManager<WebUser> userManager,
            SignInManager<WebUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        /*[HttpPost]
        [Route("ContactEmail")]
        public async Task <IActionResult> ContactEmail([FromBody] ContactEmailModel contact, string role)
        {
            IdentityUser user = new()
            {
                Email = contact.ToEmail,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = contact.Body
            };

            var subject = contact.Subject;
            var body = contact.Body;

            var message = new Message(new string[] { user.Email! }, subject, body);
            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Email sent to {user.Email} Successfully." });
        }*/

        [HttpPost]
        [Route("ConfirmCVReceipted")]
        public async Task<IActionResult> ConfirmCVReceipted([FromBody] AddContactEmailModel contact)
        {
            IdentityUser user = new()
            {
                Email = contact.ToEmail,
            };

            //var subject = contact.Subject;
            //var body = contact.Body;

            var message = new Message(new string[] { user.Email! }, "Confirm", "Your CV has been received!");
            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Email sent to {user.Email} Successfully." });
        }

        [HttpPost]
        [Route("ConfirmCVApproval")]
        public async Task<IActionResult> ConfirmCVApproval([FromBody] AddContactEmailModel contact)
        {
            IdentityUser user = new()
            {
                Email = contact.ToEmail,
            };

            //var subject = contact.Subject;
            //var body = contact.Body;

            var message = new Message(new string[] { user.Email! }, "Confirm", "Your CV has been Approval!");
            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Email sent to {user.Email} Successfully." });
        }

        [HttpPost]
        [Route("ConfirmPassed")]
        public async Task<IActionResult> ConfirmPassed([FromBody] AddContactEmailModel contact)
        {
            IdentityUser user = new()
            {
                Email = contact.ToEmail,
            };

            //var subject = contact.Subject;
            //var body = contact.Body;

            var message = new Message(new string[] { user.Email! }, "Confirm", "Congratulations on passing the interview!");
            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Email sent to {user.Email} Successfully." });
        }

        [HttpPost]
        [Route("ConfirmInterviewSchedule")]
        public async Task<IActionResult> ConfirmInterviewSchedule([FromBody] UpdateContacEmailModel contact)
        {
            IdentityUser user = new()
            {
                Email = contact.ToEmail,
            };

            //var subject = contact.Subject;
            //var body = contact.Body;

            var message = new Message(new string[] { user.Email! }, "Confirm", $"You will be scheduled for an interview on {contact.DateTime} in {contact.Room} room!");
            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Email sent to {user.Email} Successfully." });
        }
    }
}