using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace Proba2.Models;
[Table("pastry")]
public class Pastry
{
    [Key] public int Id { get; set; }
    [MaxLength(100)] 
    public string Name { get; set; }
    [DataType("decimal")]
    /*[Precision(10, 2)]*/
    public decimal Price { get; set; }
    [MaxLength(40)] public string Type { get; set; }

    public ICollection<OrderPastry> OrderPastries { get; set; } = new HashSet<OrderPastry>();
    
    
}