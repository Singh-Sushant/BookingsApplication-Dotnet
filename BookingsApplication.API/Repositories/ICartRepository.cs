using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.Repositories
{
    public interface ICartRepository
    {
        Task<List<Cart>?> getAllCartAsync();
        Task<Cart?> addToCartAsync(Cart cartElement);
        Task<Cart?> deleteCartElementByIdAsync(Guid cartElementId);
        Task<List<Cart>?> getCartItemsByEmailAsync(string email);
        Task<Cart?> UpdateCartAsync(Guid cartElementId, int numberOfTickets, int totalPrice);
    }

}