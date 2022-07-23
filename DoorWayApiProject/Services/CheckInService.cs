namespace DoorWayApiProject.Services;

using AutoMapper;
using BCrypt.Net;
using DoorWayApiProject.Authorization;
using DoorWayApiProject.Entities;
using DoorWayApiProject.Helpers;
using DoorWayApiProject.Models.CheckIn;
using DoorWayApiProject.Models.Users;
using Microsoft.EntityFrameworkCore;

public interface ICheckInService
{
    //AuthenticateResponse Authenticate(AuthenticateRequest model);
    //IEnumerable<User> GetAll();
    //User GetById(int id);
    void CheckInCreate(CreateCheckInRequest model);
    void Update(int id, UpdateCheckInRequest model);
    IEnumerable<CheckIn> FilterCheckInByUserId(int userId);
    
        //void Update(int id, UpdateRequest model);
        //void Delete(int id);
    }

public class CheckInService : ICheckInService
{

    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public CheckInService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }



    public void CheckInCreate(CreateCheckInRequest model)
    {


        // map model to new checkin object
        var checkIn = _mapper.Map<CheckIn>(model);

        checkIn.CheckInDate = DateTime.Now;

        _context.CheckIn.Add(checkIn);
        _context.SaveChanges();
    }




    public void Update(int tagId, UpdateCheckInRequest model)
    {

        var checkInID = getLastCheckInRecordId(tagId);

        var checkIn = getCheckIn(checkInID);

        model.checkOutDate = DateTime.Now;



        _mapper.Map(model, checkIn);
        _context.CheckIn.Update(checkIn);
        _context.SaveChanges();
    }

    public int getLastCheckInRecordId(int tagId)
    {
        List<CheckIn> lastCheckInRecordID = new List<CheckIn>();

        lastCheckInRecordID = _context.CheckIn.FromSqlRaw("SELECT TOP 1 * FROM CheckIn  where Tag_ID =" + tagId + "ORDER BY ID DESC;").ToList();


        var id = lastCheckInRecordID[0].Id;

        return id;

    }

    private CheckIn getCheckIn(int id)
    {
        var checkIn = _context.CheckIn.Find(id);
        if (checkIn == null) throw new KeyNotFoundException("Tag not found");
        return checkIn;
    }


    public IEnumerable<CheckIn> FilterCheckInByUserId(int userId)
    {

        List<CheckIn> CheckInHistoryForUsers = new List<CheckIn>();

        CheckInHistoryForUsers = _context.CheckIn
            .FromSqlRaw("SELECT a.Id,a.Tag_ID,a.CheckInDate,a.CheckOutDate FROM CheckIn AS a INNER JOIN (select * from Tags  where Tags.USER_ID=" + userId+") as b ON a.Tag_ID = b.Id ;").ToList();


        return CheckInHistoryForUsers;


    }
}