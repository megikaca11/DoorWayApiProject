namespace DoorWayApiProject.Models.Users;


using System.ComponentModel.DataAnnotations;

public class CreateRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
    [Required]
    public int role_ID { get; set; }
}