using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Models;
using Notebook.ViewModels;

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
    
    
    public IActionResult Create()
    {
        return View("Edit", new UserEditViewModel());
    }


    public async Task<IActionResult> Edit(int id)
    {
        var user = await _context.Users
            .Include(u => u.Attributes)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        var vm = new UserEditViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            Attributes = user.Attributes.Select(a => new UserAttributeViewModel
            {
                Key = a.Key,
                Value = a.Value
            }).ToList()
        };

        
        if (!vm.Attributes.Any())
            vm.Attributes.Add(new UserAttributeViewModel());

        return View("Edit", vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserEditViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DateOfBirth = vm.DateOfBirth,
                Gender = vm.Gender
            };
            
            foreach (var attr in vm.Attributes.Where(a => !string.IsNullOrWhiteSpace(a.Key)))
            {
                user.Attributes.Add(new UserAttribute
                {
                    Key = attr.Key.Trim(),
                    Value = attr.Value.Trim()
                });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View("Edit", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UserEditViewModel vm)
    {
        if (id != vm.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var user = await _context.Users
                .Include(u => u.Attributes)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

           
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.DateOfBirth = vm.DateOfBirth;
            user.Gender = vm.Gender;
            
            
            _context.UserAttributes.RemoveRange(user.Attributes);
            user.Attributes.Clear();

            
            foreach (var attr in vm.Attributes.Where(a => !string.IsNullOrWhiteSpace(a.Key)))
            {
                user.Attributes.Add(new UserAttribute
                {
                    Key = attr.Key.Trim(),
                    Value = attr.Value.Trim()
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View("Edit", vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Users
            .Include(u => u.Attributes)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
    
    
    public async Task<IActionResult> Report()
    {
        var users = await _context.Users
            .AsNoTracking()
            .Include(u => u.Attributes)
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .ToListAsync();

        var csvLines = new List<string>
        {
            "Tytuł;Imię;Nazwisko;Data urodzenia;Wiek;Płeć"
        };

        foreach (var user in users)
        {
            var age = DateTime.Today.Year - user.DateOfBirth.Year;
            if (user.DateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;

            var title = user.Gender == Gender.Male ? "Pan" : "Pani";
            var gender = user.Gender == Gender.Male ? "Mężczyzna" : "Kobieta";

            var line = $"{title};{user.FirstName};{user.LastName};" +
                       $"{user.DateOfBirth:yyyy-MM-dd};{age};{gender}";

            csvLines.Add(line);
        }

        var csvContent = string.Join("\r\n", csvLines);
        var bytes = Encoding.UTF8.GetBytes(csvContent);
        
        var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}.csv";

        return File(bytes, "text/csv", fileName);
    }
}