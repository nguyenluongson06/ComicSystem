using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models;

public class RentalDetail
{
    public int Id { get; set; }
    
    [Required]
    public required int RentalId { get; set; }
    
    [Required]
    public required int ComicBookId { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal PricePerDay { get; set; }
    
    public Rental? Rental { get; set; }
    
    public ComicBook? ComicBook { get; set; }
}