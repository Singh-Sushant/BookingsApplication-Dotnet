using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Models.Domains;
using BookingsApplication.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookingsApplication.API.CustomActionFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using api.Extensions;
using System.Security.Claims;

namespace BookingsApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBookingsRepository _bookingRepo;
        private readonly IEventRepository _eventRepo;
        private readonly ICheckoutService _checkoutService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper mapper;

       public BookingsController(IHttpContextAccessor httpContextAccessor,  IBookingsRepository bookingRepo, IEventRepository eventRepo, UserManager<User> userManager, ICheckoutService checkoutService , IMapper mapper)
        {
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _bookingRepo = bookingRepo;
            _eventRepo = eventRepo;
            _userManager = userManager;
            _checkoutService = checkoutService;
        }




        [HttpPost]
        [Authorize]
       public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDTO bookingRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get email from JWT token
            var emailClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(emailClaim))
            {
                return BadRequest(new {
                    message = "Email not found in token.",
                    email = emailClaim
                });
            }

            var user = await _userManager.FindByEmailAsync(emailClaim);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (bookingRequestDTO.BookingItems.Count == 0)
            {
                return BadRequest("No items found for booking. Please add items to make booking.");
            }

            var invalidTicketId = await _checkoutService.TicketTypeValidation(bookingRequestDTO.BookingItems);
            if (invalidTicketId != null)
            {
                return BadRequest($"ticketTypeId {invalidTicketId} is invalid.");
            }

            var invalidQuantities = await _checkoutService.ValidateQuantities(bookingRequestDTO.BookingItems);
            if (invalidQuantities.Count > 0)
            {
                return BadRequest(new { invalidQuantities });
            }

            CouponCode? couponModel = null;
            if (!string.IsNullOrEmpty(bookingRequestDTO.CouponCode))
            {
                couponModel = await _checkoutService.ValidateCouponCode(bookingRequestDTO.CouponCode);
                if (couponModel == null)
                {
                    return BadRequest("Invalid coupon code.");
                }

                var subtotal = await _checkoutService.GetSubTotal(bookingRequestDTO.BookingItems);
                if (subtotal < couponModel.MiniBillAmountRequired)
                {
                    return BadRequest($"Coupon not applicable. Minimum {couponModel.MiniBillAmountRequired} bill amount is required.");
                }

                if (!couponModel.IsActive)
                {
                    return BadRequest("Coupon has expired.");
                }
            }
            
            var subTotal = await _checkoutService.GetSubTotal(bookingRequestDTO.BookingItems);
            var totalAmount = _checkoutService.GetTotalAmount(subTotal, couponModel);

            if (bookingRequestDTO.TotalAmount != totalAmount)
            {
                return BadRequest($"Total amount {totalAmount} is incorrect.");
            }

            var bookingModel = mapper.Map<Booking>(bookingRequestDTO);
            bookingModel.UserId = user.Id;
            bookingModel.Subtotal = subTotal;
            bookingModel.Taxes = subTotal*18/100;
            var updatedBookingModel = await _bookingRepo.AddBookingAsync(bookingModel, user.Id);
            
            if (updatedBookingModel == null)
            {
                return BadRequest(new { msg = "Booking failed" });
            }

            return Ok(new { msg = "Booking successful", bookingModel });
        }


        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var bookingModel = await _bookingRepo.GetByIdAsync(id);

            if(bookingModel==null){
                return NotFound("booking id is invalid or does not exist"); 
            }

            return Ok(mapper.Map<BookingDto>(bookingModel));
        }



         [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new {
                    modelstate = ModelState,
                    message = "model state invalid in get all in booking controller"
                });
            }


            // get the email from the header 
            var emailClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(emailClaim))
            {
                return BadRequest(new {
                    message = "Email not found in token.",
                    email = emailClaim  
                });
            }

            // find the user 
            var appUser = await _userManager.FindByEmailAsync(emailClaim);
            // string appUserEmail = appUser.Email;
            if (appUser == null)
            {
                return Unauthorized("User not found");
            }
            
            var bookings = await _bookingRepo.GetAllAsync(appUser.Id);
            

            return Ok(mapper.Map<List<BookingDto>>(bookings));
        }

        [HttpDelete]
        [Route("{bookingId}")]
        public async Task<IActionResult> deleteBooking([FromRoute] Guid bookingId)
        {
            var emailClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(emailClaim))
            {
                return BadRequest(new {
                    message = "Email not found in token.",
                    email = emailClaim
                });
            }

            var bookingModelResponse = await _bookingRepo.DeleteByIdAsync(bookingId , emailClaim);
            if(bookingModelResponse == null){
                return BadRequest(new {
                    message = "Cancellation cannot be done"
                }); 
            }
            return Ok(new {
                message = bookingModelResponse
            });
        }
    }
}