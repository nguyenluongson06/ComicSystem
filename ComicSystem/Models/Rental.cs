using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models;

public enum RentalStatus
{
    NotAvailable = 0,
    Available = 1,
}

public class Rental
{
    public int Id { get; set; }
    
    [Required]
    public required int CustomerId { get; set; }
    
    public DateTime RentalDate { get; set; } = DateTime.Now;
    public DateTime ReturnDate { get; set; }
    public RentalStatus Status { get; set; }
    
    public Customer? Customer { get; set; }
    
    public ICollection<RentalDetail> RentalDetails { get; set; }
}