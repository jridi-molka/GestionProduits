using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Pages.Produits;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context) => _context = context;

    [BindProperty]
    public Produit Produit { get; set; } = default!;

    public SelectList? Categories { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
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

        _context.Produits.Add(Produit);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }

    private async Task ChargerCategoriesAsync()
    {
        Categories = new SelectList(
            await _context.Categories.ToListAsync(), "Id", "Nom");
    }
}