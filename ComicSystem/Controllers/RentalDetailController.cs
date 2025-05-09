using ComicSystem.Data;
using ComicSystem.Dto;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;

[ApiController] [Route("api/rentalDetail")]
public class RentalDetailController(AppDbContext context) : ControllerBase
{

}