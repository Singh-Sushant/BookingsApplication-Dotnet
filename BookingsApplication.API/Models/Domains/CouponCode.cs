using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingsApplication.API.Models.Domains
{
    public class CouponCode
    {
        [Key]
        public Guid Id { get; set; }
        public string CouponName { get; set; } // actual coupon code
        public string CouponType { get; set; } // flat or percentage

        [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MiniBillAmountRequired { get; set; } // valid on billAmount above 1500
        public bool IsActive { get; set; } = true;

    }
}