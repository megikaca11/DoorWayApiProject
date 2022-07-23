namespace DoorWayApiProject.Models.Tags;




public class CreateTagRequest
{
    
    public int User_ID { get; set; }
    
    public int Status_ID { get; set; }
    
    public int Validity_ID { get; set; }
    public DateTime AsignDate { get; set; }
}