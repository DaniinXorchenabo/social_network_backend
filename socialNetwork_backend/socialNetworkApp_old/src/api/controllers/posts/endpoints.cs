using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.controllers.messages;

namespace socialNetworkApp.api.controllers.posts;


[ApiController]
[Route("api/posts")]
public class PostController : Controller
{
    [HttpGet("get/{post_id:guid}")]
    public PostAnswer GetMessagesFromChat(Guid postId)
    {
        return new(
            new Post(postId, "sf", Guid.NewGuid(), DateTime.Now)
        );
    }

    [HttpGet("get/all")]
    public PostAnswer GetMessagesFromChatThroughData(DateTime dateTime, int limit = 100, int offset = 0)
    {
        return new(
            new Post(Guid.NewGuid(), "sf", Guid.NewGuid(), DateTime.Now),
            new Post(Guid.NewGuid(), "serff", Guid.NewGuid(), DateTime.Now),
            new Post(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), DateTime.Now)
        );
    }

    [HttpGet("get/author/{author_id:guid}")]
    public PostAnswer SearchMessagesFromChat(Guid authorId, int limit = 100, int offset = 0)
    {
        return new(
            new Post(Guid.NewGuid(), "sf", authorId, DateTime.Now),
            new Post(Guid.NewGuid(), "serff", authorId, DateTime.Now),
            new Post(Guid.NewGuid(), "sdfhf", authorId, DateTime.Now)
        );
    }

    [HttpGet("get/authors")]
    public PostAnswer SearchMessagesFromChat(Guid[] authorIds, int limit = 100, int offset = 0)
    {
        return new(
            new Post(Guid.NewGuid(), "sf", Guid.NewGuid(), DateTime.Now),
            new Post(Guid.NewGuid(), "serff", Guid.NewGuid(), DateTime.Now),
            new Post(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), DateTime.Now)
        );
    }

    [HttpGet("search/by_text")]
    public PostAnswer SearchMessagesFromChat(string foundText, Guid?[] authorIds = null, int limit = 100,
        int offset = 0)
    {
        return new(
            new Post(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now),
            new Post(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now),
            new Post(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now)
        );
    }


    [HttpPost("new")]
    public PostAnswer SendMessage(CreatePost newPost)
    {
        return new(
            new Post(Guid.NewGuid(), newPost.text, Guid.NewGuid(), DateTime.Now));
    }

    [HttpPut("edit/{post_id:guid}")]
    public PostAnswer EditMessage(Guid postId, UpdatePost updatedPost)
    {
        return new(
            new Post(Guid.NewGuid(), updatedPost.text, Guid.NewGuid(), DateTime.Now));
    }

    [HttpDelete("delete/{post_id:guid}")]
    public PostAnswer DeleteMessage(Guid postId)
    {
        return new();
    }
}