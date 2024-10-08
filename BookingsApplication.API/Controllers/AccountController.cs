using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookingsApplication.API.CustomActionFilter;
using BookingsApplication.API.Data;
using BookingsApplication.API.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookingsApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        
        private readonly BookingAppDBcontext dBcontext;

        public AccountController(IConfiguration configuration , UserManager<User> userManager ,BookingAppDBcontext dBcontext)
        {
            this.dBcontext = dBcontext;
            this.configuration = configuration;
            this.userManager = userManager;
        }

        

        [HttpPost]
        [Route("Register")]
        [ValidateModel]
        public  async Task<IActionResult> Register([FromBody] RegisterModel model){
            
            

            var user = new User{
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            var result = await userManager.CreateAsync(user , model.Password);

            if(result.Succeeded){
                return Ok(new {
                    Message = " USER REGISTERD SUCCESSFULLY "
                });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model){
            var user = await userManager.FindByEmailAsync(model.Email);
             
            // user found 

            if(user!= null && await userManager.CheckPasswordAsync(user , model.Password)){

                var authClaims = new[]
                {
                   new Claim(JwtRegisteredClaimNames.Sub , user.UserName),
                   new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
                };

                var authSignedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignedKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok( new {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    email = user.Email,
                    phoneNumber = user.PhoneNumber
                });
            }
            return Unauthorized();
        }


            [HttpPut]
            [Route("updatePhone")]
          public async Task<IActionResult> UpdateUserPhone(string PhoneNumber , string Email){

                if(PhoneNumber.Length != 10){
                    return BadRequest();
                }


                var user = await userManager.FindByEmailAsync(Email);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                user.PhoneNumber = PhoneNumber;

                // Update the user in the Identity database
                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                // Update related bookings
                var bookingList = await dBcontext.Bookings.Where(x => x.Email == Email).ToListAsync();
                
                foreach (var booking in bookingList)
                {
                    booking.PhoneNumber = PhoneNumber;
                }

                await dBcontext.SaveChangesAsync();

                return Ok("Phone number updated successfully");
            }
    }
}