using ComicSystem.Data;
using ComicSystem.Dto;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;

[ApiController] [Route("api/rentalDetail")]
public class RentalDetailController(AppDbContext context) : ControllerBase
{
    [HttpPost] public async Task<ActionResult> Post([FromBody] RentalDetailDto dto)
    {
        try
        {
            var comicBook = await context.ComicBooks.FindAsync(dto.ComicBookId);
            if (comicBook == null)
            {
                return Ok("Comic book not found");
            }

            var rental = await context.Rentals.FindAsync(dto.RentalId);
            if (rental == null)
            {
                return Ok("Rental not found");
            }

            var rentalDetail = new RentalDetail
            {
                RentalId = dto.RentalId,
                ComicBookId = comicBook.Id,
                Quantity = dto.Quantity,
                PricePerDay = comicBook.PricePerDay,
            };
            await context.RentalDetails.AddAsync(rentalDetail);
            await context.SaveChangesAsync();
            return Ok("Rental detail added");
        }
        catch (Exception ex)
        {
            return Ok("Error creating rental details:" + ex.Message);
        }
    }
}