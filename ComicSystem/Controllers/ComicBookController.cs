using ComicSystem.Data;
using ComicSystem.Dto;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;

[ApiController]
[Route("api/comicbook")]
public class ComicBookController(AppDbContext context) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetComicBooks()
    {
        try
        {
            var result = await context.ComicBooks.ToListAsync();
            return Ok(result);
        }
        catch (Exception e)
        {
            return Ok("Error fetching list: " + e.Message);
        }
    }

    [HttpGet("{id:int}")] public async Task<IActionResult> GetComicBook(int id)
    {
        var comicBook = await context.ComicBooks.FindAsync(id);
        return comicBook == null ? Ok("Not found") : Ok(comicBook);
    }

    [HttpPost] public async Task<IActionResult> AddComicBook([FromBody] ComicBookInfoDto dto)
    {
        try
        {
            var comicBook = new ComicBook
            {
                Title = dto.Title,
                Author = dto.Author,
                PricePerDay = dto.PricePerDay,
            };
            context.ComicBooks.Add(comicBook);
            await context.SaveChangesAsync();
            return Ok("Comic book added successfully");
        }
        catch (Exception e)
        {
            return Ok("Error adding comic book:" + e.Message);
        }
    }

    [HttpPut("{id:int}")] public async Task<IActionResult> UpdateComicBook(int id, [FromBody] ComicBookInfoDto dto)
    {
        try
        {
            var comicBook = await context.ComicBooks.FindAsync(id);
            if (comicBook == null)
            {
                return NotFound("Comic book not found");
            }

            comicBook.Title = dto.Title;
            comicBook.Author = dto.Author;
            comicBook.PricePerDay = dto.PricePerDay;
            await context.SaveChangesAsync();
            return Ok("Comic book updated successfully");
        }
        catch (Exception e)
        {
            return Ok("Error updating comic book:" + e.Message);
        }
    }

    [HttpDelete("{id:int}")] public async Task<IActionResult> DeleteComicBook(int id)
    {
        try
        {
            var comicBook = await context.ComicBooks.FindAsync(id);
            if (comicBook == null)
            {
                return Ok("Comic book not found");
            }

            context.ComicBooks.Remove(comicBook);
            await context.SaveChangesAsync();
            return Ok("Comic book deleted successfully");
        }
        catch (Exception e)
        {
            return Ok("Error deleting comic book:" + e.Message);
        }
    }
}