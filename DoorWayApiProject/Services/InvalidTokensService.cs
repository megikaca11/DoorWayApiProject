

using AutoMapper;
using BCrypt.Net;
using DoorWayApiProject.Authorization;
using DoorWayApiProject.Entities;
using DoorWayApiProject.Helpers;
using DoorWayApiProject.Models.InvalidTokens;
using DoorWayApiProject.Models.Users;

namespace DoorWayApiProject.Services

{

    public interface IInvalidTokensService
    {
       // AuthenticateResponse Authenticate(AuthenticateRequest model);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
        //void Register(CreateRequest model);
        //void Update(int id, UpdateRequest model);
        //void Delete(int id);
        InvalidTokens getToken(int id);
        void InvalidTokenCreate(CreateInvalidTokenRequest model);
    }
    public class InvalidTokensService : IInvalidTokensService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public InvalidTokensService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public InvalidTokens getToken(int id)
        {
           var  invalidToken= _context.InvalidTokens.Find(id);
            if (invalidToken == null) throw new KeyNotFoundException("Token not found");
            return invalidToken;
        }

        public void InvalidTokenCreate(CreateInvalidTokenRequest model)
        {


            // map model to new user object
            var token = _mapper.Map<InvalidTokens>(model);

           // token.InvalidToken = invalidToken;
            // save user
            _context.InvalidTokens.Add(token);
            _context.SaveChanges();
        }


    }
}
