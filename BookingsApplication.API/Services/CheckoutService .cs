using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingsApplication.API.Data;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Models.Domains;
using BookingsApplication.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace api.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly BookingAppDBcontext _context;
        public CheckoutService(BookingAppDBcontext context)
        {
            _context = context;
        }
        public async Task<decimal> GetSubTotal(List<AddBookingItemDto> bookingItems){
            decimal subTotalAmount = 0;

            foreach(var item in bookingItems)
            {
                var ticketModel = await _context.TicketTypes.FirstOrDefaultAsync(t => t.Id == item.TicketTypeId);
                
                // Check if ticketModel is not null before calculating subtotal
                if (ticketModel != null)
                {
                    subTotalAmount += ticketModel.Price * item.Quantity;
                    System.Console.WriteLine("                                     lslaksdlfjlsdkf    " + subTotalAmount + "                                                ");

                }
            }

            return subTotalAmount;
        }

        public async Task<Guid?> TicketTypeValidation(List<AddBookingItemDto> bookingItems)
        {
            foreach(var item in bookingItems)
            {
                var ticketModel = await _context.TicketTypes.FirstOrDefaultAsync(t => t.Id == item.TicketTypeId);
                if(ticketModel == null) return item.TicketTypeId;  // anythiing other than 0 means that tickettype id is invalid
            }
            return null;  // 0 means all valid ids
        }

        public decimal GetTotalAmount(decimal subTotal, CouponCode? couponModel)
        {
            decimal totalAmountAfterTax = subTotal + (subTotal*18/100);  // 18 percent GST on subtotal amount
            if(couponModel == null) return totalAmountAfterTax;

            //subtract the coupon discount
            var couponType = couponModel.CouponType;
            var discount = couponModel.Discount;
            if(couponType == "flat")
            {
                subTotal -= discount;
                totalAmountAfterTax = subTotal + (subTotal*18/100);
            }
            else if(couponType == "percent")
            {
                subTotal -= subTotal * discount/100;
                totalAmountAfterTax = subTotal + (subTotal*18/100);
            }

            return totalAmountAfterTax;
            
        }

        public async Task<CouponCode?> ValidateCouponCode(string coupon)
        {
            var couponModel = await _context.CouponCodes.FirstOrDefaultAsync(c => c.CouponName == coupon);
            if(couponModel == null) return null;
            return couponModel;
        }

        public async Task<List<string>> ValidateQuantities(List<AddBookingItemDto> bookingItems)
        {
            List<string> invalidQuantities = [];
            foreach(var ticket in bookingItems)
            {
                var ticketModel = await _context.TicketTypes.FirstOrDefaultAsync(t => t.Id == ticket.TicketTypeId);
                if(ticketModel != null && ticket.Quantity > ticketModel.AvailableNoOfTickets)
                {
                    invalidQuantities.Add($"ticketTypeId {ticket.TicketTypeId} has only {ticketModel.NoOfTickets - ticketModel.AvailableNoOfTickets} available");
                }
            }

            return invalidQuantities;
        }

        
    }
}