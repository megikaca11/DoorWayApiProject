namespace DoorWayApiProject.Services;

using AutoMapper;
using BCrypt.Net;
using DoorWayApiProject.Authorization;
using DoorWayApiProject.Entities;
using DoorWayApiProject.Helpers;
using DoorWayApiProject.Models.InvalidTokens;
using DoorWayApiProject.Models.Users;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Register(CreateRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);
    void LogOut(CreateInvalidTokenRequest model);

}

public class UserService : IUserService,IInvalidTokensService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly IInvalidTokensService _tokensService;

    public UserService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper,
        IInvalidTokensService tokensService)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _tokensService = tokensService;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

        // validate
        if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
            throw new AppException("Username or password is incorrect");

        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtUtils.GenerateToken(user);
        return response;
    }
    public void LogOut(CreateInvalidTokenRequest model)
        {
        _tokensService.InvalidTokenCreate(model);

       
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User GetById(int id)
    {
        return getUser(id);
    }

    public void Register(CreateRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.Username == model.Username))
            throw new AppException("Username '" + model.Username + "' is already taken");

        // map model to new user object
        var user = _mapper.Map<User>(model);

        // hash password
        user.PasswordHash = BCrypt.HashPassword(model.Password);

        // save user
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(int id, UpdateRequest model)
    {
        var user = getUser(id);

        // validate
        if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
            throw new AppException("Username '" + model.Username + "' is already taken");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCrypt.HashPassword(model.Password);

        // copy model to user and save
        _mapper.Map(model, user);
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = getUser(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    // helper methods

    private User getUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public InvalidTokens getToken(int id)
    {
        return _tokensService.getToken(id);
    }

    public void InvalidTokenCreate(CreateInvalidTokenRequest model)
    {
        _tokensService.InvalidTokenCreate(model);
    }
}