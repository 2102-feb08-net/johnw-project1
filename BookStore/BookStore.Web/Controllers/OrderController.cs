using BookStore.DataAccess;
using BookStore.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repo;

        public OrderController(IOrderRepository orderRepository)
        {
            _repo = orderRepository;
        }

        [HttpGet("api/orders")]
        public IEnumerable<Domain.Order> GetAllOrders()
        {
            return _repo.GetAllOrders();
        }

        [HttpGet("api/orders/{id?}")]
        public Domain.Order GetOrderByID(int id)
        {
            return _repo.GetOrderByID(id);
        }

        [HttpPost("api/orders")]
        public void AddOrder(Domain.Order o)
        {
            _repo.AddOrder(o);
        }

        [HttpPut("api/orders")]
        public void UpdateOrder(Domain.Order o)
        {
            _repo.UpdateOrder(o);
        }

        [HttpGet("api/locations/{id?}/orders")]
        public IEnumerable<Domain.Order> GetOrdersByLocationID(int id)
        {
            return _repo.GetOrdersByLocationID(id);
        }

        [HttpGet("api/customers/{id?}/orders")]
        public IEnumerable<Domain.Order> GetOrdersByCustomerID(int id)
        {
            return _repo.GetOrdersByCustomerID(id);
        }
    }
}
