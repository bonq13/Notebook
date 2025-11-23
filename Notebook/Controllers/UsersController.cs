using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Models;

namespace Notebook.Controllers;

public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;

    
    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

   
    public async Task<IActionResult> Index()
    {
        
        var users = await _context.Users
            .AsNoTracking()
            .Include(u => u.Attributes)
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .ToListAsync();

        return View(users);
    }
}