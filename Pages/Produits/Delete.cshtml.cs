using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Pages.Produits;

class DeleteModel : PageModel
{
    private readonly AppDbContext _context;
    public DeleteModel(AppDbContext context) => _context = context;
    [BindProperty]
    public Produit Produit { get; set; } = default!;
    public SelectList? Categories { get; set; }
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Produit = await _context.Produits
            .Include(p => p.Categorie)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (Produit == null)
        {
            return NotFound();
        }
        await ChargerCategoriesAsync();
        return Page();
    }
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Produit = await _context.Produits.FindAsync(id);
        if (Produit != null)
        {
            _context.Produits.Remove(Produit);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("/Index");
    }
    private async Task ChargerCategoriesAsync()
    {
        Categories = new SelectList(
            await _context.Categories.ToListAsync(),
            nameof(Categorie.Id),
            nameof(Categorie.Nom));
    }
}