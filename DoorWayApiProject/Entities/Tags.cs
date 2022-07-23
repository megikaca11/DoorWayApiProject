using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorWayApiProject.Entities
{
    public class Tags
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_User")]
        public int User_ID { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FK_Status")]
        public int Status_ID { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FK_TagValidity")]
        public int Validity_ID { get; set; }
        public DateTime AsignDate { get; set; }
        


    
}
}
