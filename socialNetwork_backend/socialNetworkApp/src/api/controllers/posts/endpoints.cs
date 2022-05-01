using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.controllers.messages;

namespace socialNetworkApp.api.controllers.posts;


[ApiController]
[Route("api/posts")]
[Produces("application/json")]
public class PostController : Controller
{
    [HttpGet("get/{post_id:guid}")]
    public PostAnswer GetMessagesFromChat(Guid postId)
    {
        return new(
            new PostDto(postId, "sf", Guid.NewGuid(), DateTime.Now)
        );
    }

    [HttpGet("get/all")]
    public PostAnswer GetMessagesFromChatThroughData(DateTime dateTime, int limit = 100, int offset = 0)
    {
        return new(
            new PostDto(Guid.NewGuid(), "sf", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "serff", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), DateTime.Now)
        );
    }

    [HttpGet("get/author/{author_id:guid}")]
    public PostAnswer SearchMessagesFromChat(Guid authorId, int limit = 100, int offset = 0)
    {
        return new(
            new PostDto(Guid.NewGuid(), "sf", authorId, DateTime.Now),
            new PostDto(Guid.NewGuid(), "serff", authorId, DateTime.Now),
            new PostDto(Guid.NewGuid(), "sdfhf", authorId, DateTime.Now)
        );
    }

    [HttpGet("get/authors")]
    public PostAnswer SearchMessagesFromChat(Guid[] authorIds, int limit = 100, int offset = 0)
    {
        return new(
            new PostDto(Guid.NewGuid(), "sf", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "serff", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), DateTime.Now)
        );
    }

    [HttpGet("search/by_text")]
    public PostAnswer SearchMessagesFromChat(string foundText, Guid[]? authorIds = null, int limit = 100,
        int offset = 0)
    {
        return new(
            new PostDto(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now)
        );
    }


    [HttpPost("new")]
    public PostAnswer SendMessage(CreatePostDto newPostDto)
    {
        return new(
            new PostDto(Guid.NewGuid(), newPostDto.Text, Guid.NewGuid(), DateTime.Now));
    }

    [HttpPut("edit/{post_id:guid}")]
    public PostAnswer EditMessage(Guid postId, UpdatePostDto updatedPostDto)
    {
        return new(
            new PostDto(Guid.NewGuid(), updatedPostDto.Text, Guid.NewGuid(), DateTime.Now));
    }

    [HttpDelete("delete/{post_id:guid}")]
    public PostAnswer DeleteMessage(Guid postId)
    {
        return new();
    }
}