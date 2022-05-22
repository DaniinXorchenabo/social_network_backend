using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.responses;

namespace socialNetworkApp.api.controllers.posts;

[Obsolete]
[ApiController]
[Route("api/posts")]
[Produces("application/json")]
public class PostController : Controller
{
    [HttpGet("get/{post_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessagesFromChat(Guid postId)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(postId, "sf", Guid.NewGuid(), DateTime.Now)
        ));
    }

    [HttpGet("get/all")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(PostAnswer<Pagination>), 422)]
    public async Task<IActionResult> GetMessagesFromChatThroughData(DateTime dateTime,
        [FromQuery] Pagination pagination)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), "sf", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "serff", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), DateTime.Now)
        ));
    }

    [HttpGet("get/author/{author_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(PostAnswer<Pagination>), 422)]
    public async Task<IActionResult> SearchMessagesFromChat(Guid authorId, [FromQuery] Pagination pagination)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), "sf", authorId, DateTime.Now),
            new PostDto(Guid.NewGuid(), "serff", authorId, DateTime.Now),
            new PostDto(Guid.NewGuid(), "sdfhf", authorId, DateTime.Now)
        ));
    }

    [HttpGet("get/authors")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(PostAnswer<Pagination>), 422)]
    public async Task<IActionResult> SearchMessagesFromChat(Guid[] authorIds, [FromQuery] Pagination pagination)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), "sf", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "serff", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), DateTime.Now)
        ));
    }

    [HttpGet("search/by_text")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(PostAnswer<Pagination>), 422)]
    public async Task<IActionResult> SearchMessagesFromChat(string foundText, [FromQuery] Pagination pagination,
        Guid[]? authorIds = null)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now)
        ));
    }


    [HttpPost("new")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(PostAnswer<CreatePostDto>), 422)]
    public async Task<IActionResult> SendMessage(CreatePostDto newPostDto)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), newPostDto.Text, Guid.NewGuid(), DateTime.Now)));
    }

    [HttpPut("edit/{post_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(PostAnswer<UpdatePostDto>), 422)]
    public async Task<IActionResult> EditMessage(Guid postId, UpdatePostDto updatedPostDto)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), updatedPostDto.Text, Guid.NewGuid(), DateTime.Now)));
    }

    [HttpDelete("delete/{post_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMessage(Guid postId)
    {
        return new Resp(200, new PostAnswer());
    }
}