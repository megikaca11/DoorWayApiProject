namespace DoorWayApiProject.Controllers
{
    using AutoMapper;
    using DoorWayApiProject.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using DoorWayApiProject.Authorization;
    using DoorWayApiProject.Helpers;
    using DoorWayApiProject.Models.CheckIn;
    using DoorWayApiProject.Entities;
    using Microsoft.Extensions.Logging;
    using NPOI.Util;

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CheckInController : ControllerBase
    {
        private ICheckInService _checkInService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CheckInController(
            ICheckInService checkInService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _checkInService = checkInService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        [HttpPost("create")]
        public IActionResult CheckInCreate(CreateCheckInRequest model)
        {

            var checkin = _checkInService.allowCheckIn(model);
            if (checkin)
            {

                _checkInService.CheckInCreate(model);
                return Ok(new { message = "Creation successful" });
            }

            else
            {
                return Unauthorized(new { message = "Token Expired" });
            }
        }






[HttpPut("update/{id}")]
        public IActionResult CheckOutCreate(int id, UpdateCheckInRequest model)
        {
            _checkInService.Update(id, model);
            return Ok(new { message = "Checkin updated successfully" });
        }

        [HttpGet("getCheckIn/{userId}")]
        public IEnumerable<CheckIn> FilterCheckInByUserId(int userId)
        {
            var checkInFiltered = _checkInService.FilterCheckInByUserId(userId);
            // return Ok(new { message = "Tag updated successfully" })
            // ;
            // var a = tagsFiltered;
            return checkInFiltered;
        }
    }
}
