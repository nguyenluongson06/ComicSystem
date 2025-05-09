using ComicSystem.Data;
using ComicSystem.Dto;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController(AppDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterCustomer([FromBody] CustomerInfoDto dto)
    {
        try
        {
            var existing = await context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == dto.PhoneNumber);
            if (existing != null)
                return Ok(existing); // Already registered

            var customer = new Customer
            {
                FullName = dto.FullName, PhoneNumber = dto.PhoneNumber
            };
            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            return Ok(customer);
        }
        catch (Exception e)
        {
            return Ok("Error registering:" + e.Message);
        }
    }
    
}