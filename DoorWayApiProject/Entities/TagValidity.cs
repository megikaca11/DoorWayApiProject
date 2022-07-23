using System.ComponentModel.DataAnnotations;

namespace DoorWayApiProject.Entities
{
    public class TagValidity
    {

        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Timeline { get; set; }
       

    }
}
