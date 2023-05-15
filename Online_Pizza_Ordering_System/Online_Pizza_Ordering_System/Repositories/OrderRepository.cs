using Online_Pizza_Ordering_System.Data;
using Online_Pizza_Ordering_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Pizza_Ordering_System.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly ShoppingCart _shoppingCart;


        public OrderRepository(AppDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        public async Task UpdateCardAsync(CardPayment cardpayment)
        {
            //cardpayment.OrderPlaced = DateTime.Now;
            decimal totalPrice = 0M;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    PizzaId = shoppingCartItem.Pizza.Id,
                    Price = shoppingCartItem.Pizza.Price,
                };
                totalPrice += orderDetail.Price * orderDetail.Amount;
            }
            var card = _context.CardPayment.FirstOrDefault(p => p.CardNumber == cardpayment.CardNumber);
            card.OrderTotal = totalPrice;
            //_context.Orders.Add(order);
            if(card.Balance > card.OrderTotal)
            {
                card.Balance -= card.OrderTotal;
            }
            _context.CardPayment.Update(card);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCardAsync(CardPayment cardpayment)
        {
             _context.CardPayment.Add(cardpayment);
            _context.SaveChangesAsync();
        }
    }
}
