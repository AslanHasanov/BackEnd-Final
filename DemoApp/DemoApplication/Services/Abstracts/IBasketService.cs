
using DemoApplication.Areas.Client.ViewModels.Basket;
using DemoApplication.Database.Models;

namespace DemoApplication.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product);

    }
}
