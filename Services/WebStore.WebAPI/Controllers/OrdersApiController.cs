using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddresses.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrdersApiController(IOrderService OrderService) => _OrderService = OrderService;
        /// <summary> Получение заказов пользователя </summary>
        /// <param name="UserName"> Получения имя пользователя</param>
        /// <returns>Получение списка заказов пользователся </returns>
        [HttpGet("user/{UserName}")]
        public async Task<IActionResult> GetUserOrders(string UserName)
        {
            var orders = await _OrderService.GetUserOrders(UserName);
            return Ok(orders.ToDTO());
        }
        /// <summary> Получение заказа по ИД </summary>
        /// <param name="id">Получение ИД</param>
        /// <returns>Возвращение заказа по ИД</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _OrderService.GetOrderById(id);
            if (order is null)
                return NotFound();
            return Ok(order.ToDTO());
        }
        /// <summary> Создание заказа </summary>
        /// <param name="UserName">Получение имя  пользователя</param>
        /// <param name="OrderModel"> Получения зказа</param>
        /// <returns>присваиване  заказа к имини пользователя</returns>
        [HttpPost("{UserName}")] // POST -> http://localhost:5001/api/orders/Ivanov
        public async Task<IActionResult> CreateOrder(string UserName, [FromBody] CreateOrderDTO OrderModel)
        {
            var order = await _OrderService.CreateOrder(UserName, OrderModel.Items.ToCartView(), OrderModel.Order);
            return Ok(order.ToDTO());
        }
    }
}
