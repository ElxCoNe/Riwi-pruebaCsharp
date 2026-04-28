using System.ComponentModel.DataAnnotations;
using PruebaRiwi.Enums;

namespace PruebaRiwi.Models;

public class Reservation
{
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }

    [Required]
    public int SportPlaceId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    public ReservationStatus Status { get; set; } = ReservationStatus.Active;

    //Navegation
    public  SportPlace SportPlace { get; set; }
    public User User { get; set; }

}  