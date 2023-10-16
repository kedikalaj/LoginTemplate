using LoginTemplate.Application.Interfaces;
using LoginTemplate.Application.Models;
using LoginTemplate.Model.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginTemplate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel model, string role)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return Unauthorized("The user already exists!");
            }
            IdentityUser user = new IdentityUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Could not create user.");
            }

            if (await _roleManager.RoleExistsAsync(role))
            {
                var addroles = await _userManager.AddToRoleAsync(user, role);
                return Ok();
            }
            else
            {
                return BadRequest("The role doesnt exists.");
            }
        }
        [HttpPost]
        public IActionResult TestEmail()
        {
            try
            {
                string content = "<p>The content</p>";
                var msg = new Message(new string[] { "testemai@email.test" }, "Email test", content);
                _emailService.SendEmail(msg);
                return Ok("Message sent successfully");
            }
            catch
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
