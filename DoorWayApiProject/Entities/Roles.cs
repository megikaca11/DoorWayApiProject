using System.ComponentModel.DataAnnotations;

namespace DoorWayApiProject.Entities
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}
