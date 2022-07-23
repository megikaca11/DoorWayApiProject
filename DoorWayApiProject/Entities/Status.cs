using System.ComponentModel.DataAnnotations;

namespace DoorWayApiProject.Entities
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
    }
}
