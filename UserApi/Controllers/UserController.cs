using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserApi.Extensions;
using UserApi.Models;
using UserApi.Services.Interfaces;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IOptions<AuthOptions> _authOptions;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenService jwtTokenService,
            IOptions<AuthOptions> authOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _authOptions = authOptions;
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="model">Register model.</param>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterModel model)
        {
            MailAddress address = new MailAddress(model.Email);
            string userName = address.User;

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                Surname = model.Surname,
                UserName = userName,
                Email = model.Email,
                Balance = 0.0m,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                await _userManager.AddToRoleAsync(user, "User");

                var roles = await _userManager.GetRolesAsync(user);

                return Ok(_jwtTokenService.GenerateJwt(user, roles, _authOptions));
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="model">Login model.</param>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return Ok(_jwtTokenService.GenerateJwt(user, roles, _authOptions));
                }
            }

            return Unauthorized();
        }

        /// <summary>
        /// Show user profile.
        /// </summary>
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);

            if (result != null)
            {
                var user = new ProfileModel
                {
                    FirstName = result.FirstName,
                    Surname = result.Surname,
                    Email = result.Email,
                    Timezone = result.Timezone,
                    Balance = result.Balance,
                };

                return Ok(user);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Logout.
        /// </summary>
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Edit user's profile.
        /// </summary>
        /// <param name="user">Profile model(only editable fields).</param>
        [Authorize]
        [HttpPost("profile/edit")]
        public async Task<IActionResult> Edit([FromForm] EditProfileModel user)
        {
            var userBase = await _userManager.FindByNameAsync(User.Identity.Name);
            userBase.FirstName = user.FirstName;
            userBase.Surname = user.Surname;
            var result = await _userManager.UpdateAsync(userBase);
            if (result.Succeeded)
            {
                return Ok();
            }

            return Forbid();
        }
    }
}
