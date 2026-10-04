using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionProduits.Pages;

public class IndexModel : PageModel
{
    public string MessageAccueil { get; set; }

    public void OnGet()
    {
        MessageAccueil = "Bienvenue dans notre application !";
    }
}