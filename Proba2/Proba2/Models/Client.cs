using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proba2.Models;

[Table("client")]
public class Client
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)] 
    public string FirstName { get; set; }
    [MaxLength(100)] 
    public string LastName { get; set; }

    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}