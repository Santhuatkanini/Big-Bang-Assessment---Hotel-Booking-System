using HotelBookingSample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HotelBookingSample.Repository;

namespace HotelBookingSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IConfiguration configuration, JwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                PostalCode = model.PostalCode,
                MobileNo = model.MobileNo
                
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                
                return Ok();
            }

            
            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var roles = await _userManager.GetRolesAsync(user);

                    
                    var jwtService = new JwtService(_configuration);
                    var token = _jwtService.GenerateJwtToken(user.Id, user.Email, roles.FirstOrDefault());


                    
                    return Ok(new { Token = token });
                }

                return BadRequest("Invalid login attempt");
            }

            return BadRequest("Invalid login data");
        }


        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (ModelState.IsValid)
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var role = new Role
                    {
                        Name = roleName
                    };

                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        return Ok($"Role '{roleName}' created successfully");
                    }

                    return BadRequest(result.Errors);
                }

                return BadRequest($"Role '{roleName}' already exists");
            }

            return BadRequest("Invalid role name");
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, role.Name);

                        if (result.Succeeded)
                        {
                            return Ok($"Role '{role.Name}' assigned to user '{user.UserName}' successfully");
                        }

                        return BadRequest(result.Errors);
                    }

                    return BadRequest($"Role '{roleName}' not found");
                }

                return BadRequest($"User with ID '{userId}' not found");
            }

            return BadRequest("Invalid user ID or role name");
     
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(); 
            }

            var roles = await _userManager.GetRolesAsync(user); 

            
            var userDetails = new
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles 
                              
            };

            return Ok(userDetails); 
        }



    }
}
