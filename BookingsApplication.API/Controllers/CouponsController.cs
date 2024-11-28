using Microsoft.AspNetCore.Mvc;
using BookingsApplication.API.Models.Domains;
using BookingsApplication.API.Data;

[ApiController]
[Route("api/[controller]")]
public class CouponsController : ControllerBase
{
    private readonly BookingAppDBcontext _context;

    public CouponsController(BookingAppDBcontext context)
    {
        _context = context;
    }

    [HttpGet("validate/{couponCode}/{subtotal}")]
    public IActionResult ValidateCoupon(string couponCode, decimal subtotal)
    {
        var coupon = _context.CouponCodes.FirstOrDefault(c => c.CouponName == couponCode && c.IsActive);
        if (coupon == null)
        {
            return NotFound(new { Message = "Invalid or expired coupon code." });
        }

        if (subtotal < coupon.MiniBillAmountRequired)
        {
            return BadRequest(new { Message = $"Coupon valid only for bills above ₹{coupon.MiniBillAmountRequired}." });
        }

        decimal discountAmount = coupon.CouponType == "Percentage"
            ? (subtotal * coupon.Discount / 100)
            : coupon.Discount;

        return Ok(new
        {
            CouponName = coupon.CouponName,
            Discount = discountAmount,
            FinalSubtotal = subtotal - discountAmount
        });
    }
}
