using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionProduits.Data;
using GestionProduits.Models;

namespace GestionProduits.Pages.Categories;

class DeleteModel : PageModel
{
    private readonly AppDbContext _context;
    public DeleteModel(AppDbContext context)
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
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Categorie = await _context.Categories.FindAsync(id);
        if (Categorie != null)
        {
            _context.Categories.Remove(Categorie);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("/Index");
    }
}
