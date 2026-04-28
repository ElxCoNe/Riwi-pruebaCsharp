using System.ComponentModel.DataAnnotations;
using PruebaRiwi.Enums;

namespace PruebaRiwi.Models;

public class SportPlace
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public PlaceType Type { get; set; }

    [Required]
    public int Capacity { get; set; }
    
    //Navigation
    public ICollection<Reservation> Reservations { get; set; }
    
}