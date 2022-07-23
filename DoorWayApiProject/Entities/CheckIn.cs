using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorWayApiProject.Entities
{
    public class CheckIn
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_Tags")]
        public int Tag_ID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        
     }
}
