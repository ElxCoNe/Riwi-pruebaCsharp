using System.ComponentModel.DataAnnotations;

namespace PruebaRiwi.Models;

public class User
{
    public int Id { get; set; }
    
    [Required,MaxLength(125)]
    public string Name { get; set; }
    
    [Required,MaxLength(20)]
    public string Document { get; set; }
    
    [Required, MaxLength(20)]
    public string Phone { get; set; }
    
    [Required, MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; }
    
    
    //Navigation
    public ICollection<Reservation> Reservations { get; set; }
}