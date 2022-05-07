﻿using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.responses;

namespace socialNetworkApp.api.controllers.posts;


[ApiController]
[Route("api/posts")]
[Produces("application/json")]
public class PostController : Controller
{
    [HttpGet("get/{post_id:guid}")]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public Resp  GetMessagesFromChat(Guid postId)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(postId, "sf", Guid.NewGuid(), DateTime.Now)
        ));
    }

    [HttpGet("get/all")]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public Resp GetMessagesFromChatThroughData(DateTime dateTime, int limit = 100, int offset = 0)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), "sf", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "serff", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), DateTime.Now)
        ));
    }

    [HttpGet("get/author/{author_id:guid}")]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public Resp SearchMessagesFromChat(Guid authorId, int limit = 100, int offset = 0)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), "sf", authorId, DateTime.Now),
            new PostDto(Guid.NewGuid(), "serff", authorId, DateTime.Now),
            new PostDto(Guid.NewGuid(), "sdfhf", authorId, DateTime.Now)
        ));
    }

    [HttpGet("get/authors")]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public Resp SearchMessagesFromChat(Guid[] authorIds, int limit = 100, int offset = 0)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), "sf", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "serff", Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), DateTime.Now)
        ));
    }

    [HttpGet("search/by_text")]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public Resp SearchMessagesFromChat(string foundText, Guid[]? authorIds = null, int limit = 100,
        int offset = 0)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now),
            new PostDto(Guid.NewGuid(), foundText, Guid.NewGuid(), DateTime.Now)
        ));
    }


    [HttpPost("new")]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public Resp SendMessage(CreatePostDto newPostDto)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), newPostDto.Text, Guid.NewGuid(), DateTime.Now)));
    }

    [HttpPut("edit/{post_id:guid}")]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public Resp EditMessage(Guid postId, UpdatePostDto updatedPostDto)
    {
        return new Resp(200, new PostAnswer(
            new PostDto(Guid.NewGuid(), updatedPostDto.Text, Guid.NewGuid(), DateTime.Now)));
    }

    [HttpDelete("delete/{post_id:guid}")]
    [ProducesResponseType(typeof(PostAnswer), StatusCodes.Status200OK)]
    public Resp DeleteMessage(Guid postId)
    {
        return new Resp(200, new PostAnswer());
    }
}