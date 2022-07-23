
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

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TagsController :  ControllerBase
{
    private ITagsService _tagsService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public TagsController(
        ITagsService tagsService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _tagsService = tagsService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

        
        [HttpPost("create")]
        public IActionResult TagCreate(CreateTagRequest model)
        {
            _tagsService.TagCreate(model);
            return Ok(new { message = "Creation successful" });
        }
        //deactivate or activate  tags for users only performed by admin 
        // [Authorize(RoleEnum.Admin)]
        [HttpPatch("update/{id}")]
        public IActionResult Update(int id, UpdateTagRequest model)
        {
            _tagsService.Update(id, model);
            return Ok(new { message = "Tag updated successfully" });
        }

        
        //[Authorize(RoleEnum.Admin)]
        [HttpGet("filter/{statusId}")]
        public List<Tags> FilterTagsByStatus(int statusId)
        {
           var tagsFiltered= _tagsService.FilterTagsByStatus(statusId);
            // return Ok(new { message = "Tag updated successfully" })
            // ;
           // var a = tagsFiltered;
            return tagsFiltered;
        }
    }
}
