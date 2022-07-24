namespace DoorWayApiProject.Services;

using AutoMapper;
using BCrypt.Net;
using DoorWayApiProject.Authorization;
using DoorWayApiProject.Entities;
using DoorWayApiProject.Helpers;
using DoorWayApiProject.Models.CheckIn;
using DoorWayApiProject.Models.Tags;
using DoorWayApiProject.Services;

using Microsoft.EntityFrameworkCore;

public interface ICheckInService
{
    //AuthenticateResponse Authenticate(AuthenticateRequest model);
    //IEnumerable<User> GetAll();
    //User GetById(int id);
    void CheckInCreate(CreateCheckInRequest model);
    void Update(int id, UpdateCheckInRequest model);
    IEnumerable<CheckIn> FilterCheckInByUserId(int userId);
    bool allowCheckIn(CreateCheckInRequest model);


    //void Update(int id, UpdateRequest model);
}

public class CheckInService : ICheckInService, ITagsService
{


    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly ITagsService _tagsService;

    public CheckInService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper, ITagsService tagsService)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _tagsService = tagsService;
    }

    public bool allowCheckIn(CreateCheckInRequest model)
    {
        var checkIn = _mapper.Map<CheckIn>(model);

        checkIn.CheckInDate = DateTime.Now;
        var tag = _tagsService.getTag(checkIn.Tag_ID);

        checkTokenValidity(tag);
        ////////////// UPDATED TAG
        if (checkTagStatus(tag))
        {
            return true;

        }
        else return false;
    
    }
    public  void CheckInCreate(CreateCheckInRequest model)
    {
        

            // map model to new checkin object
            var checkIn = _mapper.Map<CheckIn>(model);

            checkIn.CheckInDate = DateTime.Now;
           
               _context.CheckIn.Add(checkIn);
                _context.SaveChanges();
                

    }
    public void checkTokenValidity(Tags tag)
    {
        if (tag.Validity_ID == 1)
        {
            if (DateTime.Now > tag.AsignDate.AddYears(1))
                //update status to expired
                _context.Tags.Update(tag);
        }
        else if (tag.Validity_ID == 2)
        {
            if (DateTime.Now > tag.AsignDate.AddHours(1))
                //update status to expired
                _context.Tags.Update(tag);
        }
        else if (tag.Validity_ID == 3)
        {
            if (DateTime.Now > tag.AsignDate.AddYears(3))
                //update status to expired
                _context.Tags.Update(tag);
        }
    }

    public bool checkTagStatus(Tags tag)
    {
        var updatedTag = _tagsService.getTag(tag.Id);
        if (updatedTag.Status_ID == 2)
        {
            return true;
        }
        return false;
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



    private int getLastCheckInRecordId(int tagId)
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
            .FromSqlRaw("SELECT a.Id,a.Tag_ID,a.CheckInDate,a.CheckOutDate FROM CheckIn AS a INNER JOIN (select * from Tags  where Tags.USER_ID=" + userId + ") as b ON a.Tag_ID = b.Id ;").ToList();


        return CheckInHistoryForUsers;


    }

    public void TagCreate(CreateTagRequest model)
    {
        _tagsService.TagCreate(model);
    }

    public void Update(int id, UpdateTagRequest model)
    {
        _tagsService.Update(id, model);
    }

    public List<Tags> FilterTagsByStatus(int statusId)
    {
        return _tagsService.FilterTagsByStatus(statusId);
    }

    public Tags getTag(int id)
    {
        return _tagsService.getTag(id);
    }
}