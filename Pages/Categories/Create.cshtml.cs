using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionProduits.Data;
using GestionProduits.Models;
 
namespace GestionProduits.Pages.Categories;

class CreateModel : PageModel
{
    private readonly AppDbContext _context;
    public CreateModel(AppDbContext context)
    {
        _context = context;
    }
    [BindProperty]
    public Categorie Categorie { get; set; } = new Categorie();
    public IActionResult OnGet()
    {
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        _context.Categories.Add(Categorie);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Index");
    }
}