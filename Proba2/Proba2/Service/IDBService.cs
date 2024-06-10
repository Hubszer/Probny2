using Proba2.Models;

namespace Proba2.Service;

public interface IDBService
{
    Task<ICollection<Order>> GetOrders(string lastName);
    Task<bool> DoesClientExist(int clientId);
    Task<bool> DoesEmployeeExist(int employeeId);
    Task AddNewOrder(Order order);
    Task<Pastry?> GetPastryByName(string name);
    Task AddOrderPastries(IEnumerable<OrderPastry> orderPastries);

}