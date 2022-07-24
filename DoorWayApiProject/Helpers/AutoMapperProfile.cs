namespace DoorWayApiProject.Helpers;

using AutoMapper;
using DoorWayApiProject.Entities;
using DoorWayApiProject.Models.Users;
using DoorWayApiProject.Models.Tags;
using DoorWayApiProject.Entities;
using DoorWayApiProject.Models.CheckIn;
using DoorWayApiProject.Models.InvalidTokens;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse
        CreateMap<User, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<CreateRequest, User>();
        CreateMap<CreateTagRequest, Tags>();
        CreateMap<CreateCheckInRequest, CheckIn>();
        CreateMap<CreateInvalidTokenRequest, InvalidTokens>();


        CreateMap<UpdateTagRequest, Tags>();
        CreateMap<UpdateCheckInRequest, CheckIn>();


        //.ForAllMembers(x => x.Condition(
        //    (src, dest, prop) =>
        //    {
        //        // ignore null & empty string properties
        //        if (prop == null) return false;
        //        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

        //        return true;
        //    }
        //));


        // UpdateRequest -> User
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}