using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    [Phone] [Required]
    public required string PhoneNumber { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    
    public ICollection<Rental> Rentals { get; set; }
}