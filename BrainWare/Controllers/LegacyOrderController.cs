#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrainWare.Models;

namespace BrainWare.Controllers
{
    // Hard coded to support the legacy API
    [Route("api/order")]
    [ApiController]
    public class LegacyOrderController : ControllerBase
    {
        private readonly BrainWareContext _context;

        public LegacyOrderController(BrainWareContext context)
        {
            _context = context;
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LegacyOrder>>> GetLegacyOrders()
        {
            // The legacy API returns item 1 vs a list of items.
            return await GetLegacyOrder(1);
        }

        // GET: api/order/1
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LegacyOrder>>> GetLegacyOrder(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            List<LegacyOrder> legacyOrders = new List<LegacyOrder>();
            foreach (var order in company.Orders)
            {
                legacyOrders.Add(new LegacyOrder
                {
                    OrderId = order.OrderId,
                    CompanyName = company.Name,
                    Description = order.Description,
                    OrderTotal = 0,
                    OrderProducts = new List<LegacyOrderProduct>()
                });
                foreach (var Orderproduct in order.Orderproducts)
                {
                    legacyOrders[^1].OrderProducts.Add(new LegacyOrderProduct
                    {
                        OrderId = Orderproduct.OrderId,
                        ProductId = Orderproduct.ProductId,
                        Quantity = Orderproduct.Quantity,
                        Price = (decimal)Orderproduct.Price,
                        Product = new LegacyProduct
                        {
                            Name = Orderproduct.Product.Name,
                            Price = (decimal)Orderproduct.Product.Price
                        }
                    });
                    legacyOrders[^1].OrderTotal += legacyOrders[^1].OrderProducts[^1].Price
                        * legacyOrders[^1].OrderProducts[^1].Quantity;
                }

            }
            return legacyOrders;
        }
    }
}
