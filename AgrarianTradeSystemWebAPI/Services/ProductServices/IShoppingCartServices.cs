using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;

namespace AgrarianTradeSystemWebAPI.Services.ProductServices
{
	public interface IShoppingCartServices
	{
	    Task<Cart> AddToCart(string buyerId, int productId, int quantity);
	    List<CartItemDto> GetCartItems(string buyerId);
		List<CartItemDto> DeleteCartItem(string buyerId, int cartItemId);
	}
}
