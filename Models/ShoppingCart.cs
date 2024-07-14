
using Microsoft.EntityFrameworkCore;

namespace PieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly PieShopDbContext _pieShopDbContext;
        public ShoppingCart(PieShopDbContext pieShopDbContext)
        {
            _pieShopDbContext = pieShopDbContext;
        }
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;


        //static method that will return a full shopping cart
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            PieShopDbContext context=services.GetService<PieShopDbContext>()?? throw new Exception("Error initializing");
            string cartId=session?.GetString("CartId")??Guid.NewGuid().ToString(); ;
            session?.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Pie pie)
        {
            var item = _pieShopDbContext.ShoppingCartItems.SingleOrDefault(s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            if (item is null)
            {
                item = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };
                _pieShopDbContext.ShoppingCartItems.Add(item);
            }
            else
            {
                item.Amount++;
            }
            _pieShopDbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _pieShopDbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _pieShopDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _pieShopDbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _pieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Pie).ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            var total =_pieShopDbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId).Select(e => e.Amount*e.Pie.Price).Sum();

            return total;

        }

        public int RemoveFromCart(Pie pie)
        {
            var item = _pieShopDbContext.ShoppingCartItems.SingleOrDefault(s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;
            if (item !=null)
            {
              
                if (item.Amount > 1)
                {
                    item.Amount--;
                    localAmount= item.Amount;
                }
                else
                {
                    _pieShopDbContext.ShoppingCartItems.Remove(item);
                }
                
            }

            _pieShopDbContext.SaveChanges();
            return localAmount;
        }
    }
}
