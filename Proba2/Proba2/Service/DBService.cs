using Microsoft.EntityFrameworkCore;
using Proba2.COnetxt;
using Proba2.Models;

namespace Proba2.Service;

public class DBService : IDBService
{
    
    private readonly DataBaseContext _context;
    public DBService(DataBaseContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Order>> GetOrders(string? lastName)
    {
        return await _context.Orders
            .Include(e => e.Client)
            .Include(e => e.OrderPastries)
            .ThenInclude(e => e.Pastry)
            .Where(e => lastName == null || e.Client.LastName == lastName)
            .ToListAsync();
    }

    public async Task<bool> DoesClientExist(int clientId)
    {
        return await _context.Clients.AnyAsync(e => e.Id == clientId);
    }

    public async Task<bool> DoesEmployeeExist(int employeeId)
    {
        return await _context.Employees.AnyAsync(e => e.Id == employeeId);
    }

    public async Task AddNewOrder(Order order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Pastry?> GetPastryByName(string name)
    {
        return await _context.Pastries.FirstOrDefaultAsync(e => e.Name == name);
    }

    public async Task AddOrderPastries(IEnumerable<OrderPastry> orderPastries)
    {
        await _context.AddAsync(orderPastries);
        await _context.SaveChangesAsync();
    }
}