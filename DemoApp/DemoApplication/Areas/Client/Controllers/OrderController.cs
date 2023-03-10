using DemoApplication.Areas.Client.ViewModels.OrderProduct;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("order")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public OrderController(
            DataContext dataContext,
            IFileService fileService,
            IUserService userService,
            IOrderService orderService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _userService = userService;
            _orderService = orderService;
        }

        #region CheckOut'

        [HttpGet("checkout", Name = "client-order-checkout")]
        public async Task<IActionResult> CheckOut()
        {
            var model = new OrderProductViewModel
            {
                Products = await _dataContext.BasketProducts
                .Where(p => p.Basket!.UserId == _userService.CurrentUser.Id)
                  .Select(p => new OrderProductViewModel.ListItem
                  {
                      Name = p.Product!.Name,
                      Price = p.Product.Price,
                      Quantity = p.Quantity,
                      Total = p.Product.Price * p.Quantity,
                  }).ToListAsync(),

                Summary = new OrderProductViewModel.SummaryTotal
                {
                    Total = await _dataContext.BasketProducts
                   .Where(pu => pu.Basket!.UserId == _userService.CurrentUser.Id)
                    .SumAsync(bp => bp.Product!.Price * bp.Quantity)
                }
            };

            return View(model);
        }
        #endregion


        #region PlaceOrder'

        [HttpPost("placeorder", Name = "client-order-placeorder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var basketProducts = await _dataContext.BasketProducts
                .Include(p => p.Product)
                .Where(p => p.Basket!.UserId == _userService.CurrentUser.Id)
                .ToListAsync();

            var order = await CreateOrder();

            await CreateFullOrderProductAync(order, basketProducts);
            order.SumTotalPrice = order.OrderProducts.Sum(p => p.Total);

            await ResetBasketAsync(basketProducts);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-order");


            //*************************************************************

            async Task ResetBasketAsync(List<BasketProduct> basketProducts)
            {
                await Task.Run(() => _dataContext.RemoveRange(basketProducts));
            }

            async Task CreateFullOrderProductAync(Order order, List<BasketProduct> basketProducts)
            {
                foreach (var item in basketProducts)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.Product!.Price,
                        Quantity = item.Quantity,
                        Total = item.Product.Price * item.Quantity,
                    };
                    await _dataContext.OrderProducts.AddAsync(orderProduct);
                }

            }

            async Task<Order> CreateOrder()
            {
                var order = new Order
                {
                    Id = await _orderService.GenerateUniqueTrackingCodeAsync(),
                    UserId = _userService.CurrentUser.Id,
                    Status = Database.Models.Enum.OrderStatus.Created
                };
                await _dataContext.Orders.AddAsync(order);
                return order;
            }
        } 
        #endregion
    }
}
