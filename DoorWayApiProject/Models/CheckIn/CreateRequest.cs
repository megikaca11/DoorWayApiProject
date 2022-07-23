namespace DoorWayApiProject.Models.CheckIn;




public class CreateCheckInRequest
{
    
    public int Tag_ID { get; set; }
    
    
    public int Validity_ID { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; 
    }
}