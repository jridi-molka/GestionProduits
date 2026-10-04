using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionProduits.Data;
using GestionProduits.Models;

namespace GestionProduits.Pages.Categories;


class UpdateModel : PageModel
{
    private readonly AppDbContext _context;
    public UpdateModel(AppDbContext context)
    {
        _context = context;
    }
    [BindProperty]
    public Categorie Categorie { get; set; } = new Categorie();
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Categorie = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        if (Categorie == null)
        {
            return NotFound();
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        _context.Attach(Categorie).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategorieExists(Categorie.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToPage("/Index");
    }
    private bool CategorieExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
}
