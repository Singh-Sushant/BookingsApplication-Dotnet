using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Models.Domains;
using BookingsApplication.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BookingsApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public CartController(IHttpContextAccessor httpContextAccessor,  ICartRepository cartRepository, IMapper mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        // [Authorize]
        // make sure to authorize this 
        public async Task<IActionResult> getAllCartElements(){
             var email = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new {
                    message = "Email not found in token.",
                    email = email
                });
            }

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { message = "Email is required." });
            }

            var cartItems = await cartRepository.getCartItemsByEmailAsync(email);

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound(new { message = "No cart items found for this email." });
            }

            
            return Ok(mapper.Map<List<CartDTO>>(cartItems));
        }   


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> addToCart(AddToCartDTO addToCartDTO){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var cartElement = new Cart{
                Email = addToCartDTO.Email,
                EventId = addToCartDTO.EventId,
                NumberOfTickets = addToCartDTO.NumberOfTickets,
                TotalPrice = addToCartDTO.TotalPrice,
                TicketTypeId = addToCartDTO.TicketTypeId
            };

            try
            {
                var response = await cartRepository.addToCartAsync(cartElement);

                if (response == null)
                {
                    return BadRequest(new { message = "Unable to add to cart due to invalid data." });
                }

                return Ok(mapper.Map<CartDTO>(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding to cart.", error = ex.Message });
            }


        }

        [HttpDelete]
        [Route("{cartElementId}")]
        public async Task<IActionResult> deleteCartElementById([FromRoute] Guid cartElementId){
             
            
            var deletedCartElement = await cartRepository.deleteCartElementByIdAsync(cartElementId);
            if(deletedCartElement == null){
                return BadRequest(new {
                    message = "No Cart Item to delete"
                });
            }

            return Ok(new {
                deletedCartElement
            });
        }

        [HttpPut]
        [Route("{cartElementId}")]
        [Authorize]
        public async Task<IActionResult> UpdateCart([FromRoute] Guid cartElementId, [FromBody] UpdateCartDTO updateCartDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cartElementId != updateCartDTO.CartElementId)
            {
                return BadRequest(new { message = "Cart ID mismatch." });
            }

            var email = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { message = "User email not found in token." });
            }

            try
            {
                var updatedCart = await cartRepository.UpdateCartAsync(cartElementId, updateCartDTO.NumberOfTickets, updateCartDTO.TotalPrice);

                if (updatedCart == null)
                {
                    return NotFound(new { message = "Cart item not found." });
                }

                return Ok(mapper.Map<CartDTO>(updatedCart));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the cart item.", error = ex.Message });
            }
        }

        
    }
}