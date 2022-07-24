using System.ComponentModel.DataAnnotations;

namespace DoorWayApiProject.Entities
{
    public class InvalidTokens
    {
        [Key]
        public int Id { get; set; }
        public string InvalidToken { get; set; }
    }
}

