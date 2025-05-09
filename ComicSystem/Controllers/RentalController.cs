using ComicSystem.Data;
using ComicSystem.Dto;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;

[ApiController]
[Route("api/rental")]
public class RentalController(AppDbContext context) : ControllerBase
{
    [HttpPost] public async Task<IActionResult> CreateRental([FromBody] RentalDto dto)
    {
        try
        {
            var customer = await context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == dto.PhoneNumber);
            if (customer == null)
            {
                return Ok("Customer not found");
            }

            var rental = new Rental
            {
                CustomerId = customer.Id,
                ReturnDate = DateTime.Now.AddDays(dto.DaysRented),
                Status = RentalStatus.NotAvailable
            };
            context.Rentals.Add(rental);
            await context.SaveChangesAsync();
            return Ok("Rental created");
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }
}