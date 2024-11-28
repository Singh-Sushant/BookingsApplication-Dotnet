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
using BookingsApplication.API.DTOs;
using Microsoft.AspNetCore.Authorization;

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
            
            var existingUser= await userManager.FindByEmailAsync(model.Email);

            if(existingUser != null){
                return BadRequest(new {
                    message = "user already exists for this email"
                });
            }

            var user = new User{
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PreferredLanguage = model.PreferredLanguage,
                PreferredCurrency = model.PreferredCurrency,
                ProfilePictureUrl = model.ProfilePictureUrl
            };
            

            if(user.PhoneNumber.Length != 10){
                return BadRequest(new {
                    message = "Phone number not valid "
                });
            }
            var result = await userManager.CreateAsync(user , model.Password);

            if(result.Succeeded){
                return Ok(new {
                    Message = " USER REGISTERD SUCCESSFULLY "
                });
            }

            return BadRequest(new{
               allErrors = result.Errors
        });
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
                   new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                   new Claim(JwtRegisteredClaimNames.Email, user.Email),
                   new Claim(ClaimTypes.Email, user.Email)
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
            [Authorize]
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
                var bookingList = await dBcontext.Bookings.Where(x => x.BillingEmail == Email).ToListAsync();
                
                foreach (var booking in bookingList)
                {
                    if (long.TryParse(PhoneNumber, out long parsedPhoneNumber))
                    {
                        booking.BillingPhoneNumber = parsedPhoneNumber;
                    }
                    else
                    {
                        // Handle the case where PhoneNumber is not a valid long value
                        booking.BillingPhoneNumber = 0; // or another default value
                    }
                }


                await dBcontext.SaveChangesAsync();

                return Ok("Phone number updated successfully");
            }


            [HttpPut]
            [Route("updatePassword")]
            public async Task<IActionResult> UpdatePassword([FromBody]  UpdatePasswordDTO updatePasswordDTO)
            {
                var user = await userManager.FindByEmailAsync(updatePasswordDTO.Email);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Check if the current password is correct
                var isPasswordCorrect = await userManager.CheckPasswordAsync(user, updatePasswordDTO.CurrentPassword);
                if (!isPasswordCorrect)
                {
                    return BadRequest(new{
                        message = "current password incorrect",
                        currentPass= updatePasswordDTO.CurrentPassword
                    });
                }

                // Update the password
                var result = await userManager.ChangePasswordAsync(user, updatePasswordDTO.CurrentPassword, updatePasswordDTO.NewPassword);
                
                if (!result.Succeeded)
                {
                    
                    return BadRequest(new{
                        error = result.Errors,
                        
                    });
                }

                return Ok("Password updated successfully");
}

    }
}