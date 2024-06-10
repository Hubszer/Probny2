using System.Transactions;
using Microsoft.AspNetCore.Mvc;

using Proba2.DTos;
using Proba2.Models;
using Proba2.Service;

namespace Proba2
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller2 : ControllerBase // Inherit from ControllerBase
    {
        private readonly IDBService _dbService;

        public Controller2(IDBService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("{clientID}/orders")]
        public async Task<IActionResult> AddNewOrder(int clientID, NewOrderDTO newOrder)
        {
            if (!await _dbService.DoesClientExist(clientID))
                return NotFound($"Client with given ID - {clientID} doesn't exist");

            if (!await _dbService.DoesEmployeeExist(newOrder.EmployeeID))
                return NotFound();

            var order = new Order()
            {
                ClientId = clientID,
                EmployeeId = newOrder.EmployeeID,
                AcceptedAt = newOrder.AcceptedAt,
                Comments = newOrder.Comments
            };

            var pastries = new List<OrderPastry>();
            foreach (var newOrderPastry in newOrder.Pastries)
            {
                var pastry = await _dbService.GetPastryByName(newOrderPastry.Name);
                if (pastry is null)
                {
                    return NotFound();
                }
                pastries.Add(new OrderPastry
                {
                    PastryId = pastry.Id,
                    Amount = newOrderPastry.Amount,
                    Comment = newOrderPastry.Comments,
                    Order = order
                });
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbService.AddNewOrder(order);
                await _dbService.AddOrderPastries(pastries);

                scope.Complete();
            }

            return Created("api/orders", new
            {
                Id = order.Id,
                order.AcceptedAt,
                order.FullfilledAt,
                order.Comments
            });
        }
    }
}
