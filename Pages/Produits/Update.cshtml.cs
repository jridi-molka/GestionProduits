using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Pages.Produits;

public class UpdateModel : PageModel
{
    private readonly AppDbContext _context;
    public UpdateModel(AppDbContext context) => _context = context;
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
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await ChargerCategoriesAsync();
            return Page();
        }
        _context.Attach(Produit).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProduitExists(Produit.Id))
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
    private async Task ChargerCategoriesAsync()
    {
        Categories = new SelectList(
            await _context.Categories.ToListAsync(), "Id", "Nom");
    }
    private bool ProduitExists(int id) =>
        _context.Produits.Any(e => e.Id == id);
}