using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;

namespace AgrarianTradeSystemWebAPI.Services.ProductServices
{
	public interface IShoppingCartServices
	{
	    Task<Cart> AddToCart(int buyerId, int productId, int quantity);
	    List<CartItemDto> GetCartItems(int buyerId);
		List<CartItemDto> DeleteCartItem(int buyerId, int cartItemId);
	}
}
