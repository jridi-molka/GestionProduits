using System.ComponentModel.DataAnnotations;
namespace GestionProduits.Models;

public class Categorie
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Le nom est obligatoire")]
    [StringLength(100)]
    public string Nom { get; set; } = string.Empty;
    public ICollection<Produit>? Produits { get; set; }
}