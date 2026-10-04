using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GestionProduits.Models;
 
public class Produit
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Le nom est obligatoire")]
    [StringLength(100)]
    public string Nom { get; set; } = string.Empty;
    [Required(ErrorMessage = "Le prix est obligatoire")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0")]
    public decimal Prix { get; set; }
    [Required(ErrorMessage = "La catégorie est obligatoire")]
    public int CategorieId { get; set; }
    [ForeignKey("CategorieId")]
    public Categorie? Categorie { get; set; }
}