namespace PruebaRiwi.Models;

public class User
{
    int Id { get; set; }
    string Name { get; set; }
    string Document { get; set; }
    string Phone { get; set; }
    string Email { get; set; }
    
    
    //Navigation
    public ICollection<Reservation> Reservations { get; set; }
}