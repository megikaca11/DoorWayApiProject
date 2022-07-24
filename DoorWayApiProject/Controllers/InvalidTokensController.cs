

namespace DoorWayApiProject.Controllers
{
    using AutoMapper;
    using DoorWayApiProject.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using DoorWayApiProject.Authorization;
    using DoorWayApiProject.Helpers;
    using DoorWayApiProject.Models.Tags;
    using DoorWayApiProject.Helpers.NewFolder;
    using DoorWayApiProject.Entities;
    using DoorWayApiProject.Models.InvalidTokens;

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InvalidTokensController : ControllerBase
    {
        private IInvalidTokensService _tokensService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public InvalidTokensController(
            IInvalidTokensService tokensService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _tokensService = tokensService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        [HttpPost("create/{id}")]
        public IActionResult InvalidTokenCreate(CreateInvalidTokenRequest model)
        {
            _tokensService.InvalidTokenCreate(model);
            return Ok(new { message = "Creation successful" });
        }
    }
}