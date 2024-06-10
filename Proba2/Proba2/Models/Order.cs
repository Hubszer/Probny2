using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proba2.Models;
[Table("order")]
public class Order
{
    [Key] public int Id { get; set; }
    public DateTime AcceptedAt { get; set; }
    public DateTime? FullfilledAt { get; set; }
    [MaxLength(300)] 
    public string? Comments { get; set; }
    
    
    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))] public Employee Employee { get; set; }

    public int ClientId { get; set; }
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }

    public ICollection<OrderPastry> OrderPastries { get; set; } = new HashSet<OrderPastry>();
}