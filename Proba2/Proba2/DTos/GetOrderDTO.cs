namespace Proba2.DTos;

public class GetOrderDTO
{
    public int Id { get; set; }
    public DateTime AcceptedAt { get; set; }
    public DateTime? FullfilledAt { get; set; }
    public string? Comments { get; set; }
    public ICollection<GetOrdersPastryDTO> Pastries { get; set; }
}


public class GetOrdersPastryDTO
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    
}