using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionProduits.Data;
using GestionProduits.Models;

namespace GestionProduits.Pages.Categories;
class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    public IndexModel(AppDbContext context) => _context = context;
    public List<Categorie> Categories { get; set; } = new();
    public async Task OnGetAsync()
    {
        Categories = await _context.Categories
            .OrderBy(c => c.Nom)
            .ToListAsync();
    }
}