using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Proba2.DTos;
using Proba2.Models;
using Proba2.Service;

namespace Proba2;
[Route("api/[controller]")]
[ApiController]
public class Controller : ControllerBase
{
    private readonly IDBService _dbService;
    public Controller(IDBService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(string? lastName = null)
    {
        var orders = await _dbService.GetOrders(lastName);
        
        return Ok(orders.Select(e => new GetOrderDTO()
        {
            Id = e.Id,
            AcceptedAt = e.AcceptedAt,
            FullfilledAt = e.FullfilledAt,
            Comments = e.Comments,
            Pastries = e.OrderPastries.Select(p => new GetOrdersPastryDTO
            {
                Name = p.Pastry.Name,
                Price = p.Pastry.Price,
                Amount = p.Amount
            }).ToList()
            
        }));
    }

    

}