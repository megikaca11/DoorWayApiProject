﻿namespace  DoorWayApiProject.Services;

using AutoMapper;
using BCrypt.Net;
using DoorWayApiProject.Authorization;
using DoorWayApiProject.Entities;
using DoorWayApiProject.Helpers;
using DoorWayApiProject.Models.Tags;
using DoorWayApiProject.Models.Users;
using Microsoft.EntityFrameworkCore;

public interface ITagsService
{
    //AuthenticateResponse Authenticate(AuthenticateRequest model);
    //IEnumerable<User> GetAll();
    //User GetById(int id);
    void TagCreate(CreateTagRequest model);
    void Update(int id, UpdateTagRequest model);

    List<Tags> FilterTagsByStatus(int statusId);
    //void Update(int id, UpdateRequest model);
    //void Delete(int id);
     Tags getTag(int id);
}

public class TagsService : ITagsService
{

    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public TagsService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }



    public void TagCreate(CreateTagRequest model)
    {


        
        var tag = _mapper.Map<Tags>(model);

        tag.AsignDate = DateTime.Now;
       
        _context.Tags.Add(tag);
        _context.SaveChanges();
    }

    //deactivate tags for users only performed by admin 



    public void Update(int id, UpdateTagRequest model)
    {
        var tag = getTag(id);

        _mapper.Map(model, tag);
        _context.Tags.Update(tag);
        _context.SaveChanges();
    }

    public Tags getTag(int id)
    {
        var tag = _context.Tags.Find(id);
        if (tag == null) throw new KeyNotFoundException("Tag not found");
        return tag;
    }


    public List<Tags> FilterTagsByStatus(int statusId)
    {
        

        List<Tags> tagsList = new List<Tags>();


        
        tagsList = _context.Tags.FromSqlRaw("SELECT * from Tags where Status_ID="+ statusId +";")
     .ToList();
        return tagsList;

    }


    //public void UpdateTagsStatusBasedOnValidity(int id) {
     

    //   var query = _context.Tags.FromSqlRaw("UPDATE Tags SET Status_ID = (CASE WHEN(GETDATE() >(SELECT DATEADD(year, 1, dbo.Tags.AsignDate) from Tags WHERE USER_ID = 1)) THEN 4 ELSE(select Status_ID from Tags Where User_ID = 1) END) WHERE Id =  " + id + "; ").ToList();
       


    //    }
}

