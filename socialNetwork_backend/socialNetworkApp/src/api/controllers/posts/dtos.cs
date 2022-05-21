using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.posts;

[AddAnswerType(AnswerType.Post)]
public class PostDto : AbstractDto
{
    public Guid Id { get; set; }
    public string Text{ get; set; }
    public Guid? Author{ get; set; }
    public DateTime CreatedAt{ get; set; }
    public DateTime? UpdatedAt { get; set; } = null;
    public int ViewedCount{ get; set; } = 0;
    public bool IsDeleted{ get; set; } = false;

    public PostDto(Guid id = default, string text = null, Guid? author = default, DateTime createdAt = default,
        DateTime? updatedAt = default, int viewedCount = default, bool isDeleted = default)
    {
        Id = id;
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Author = author;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        ViewedCount = viewedCount;
        IsDeleted = isDeleted;
    }
    public PostDto(object obj) : base(obj){}
    
    public PostDto(){}
}

[AddAnswerType(AnswerType.Post)]
public class CreatePostDto : AbstractDto
{
    public string Text{ get; set; }

    public CreatePostDto(string text = null)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }
    public CreatePostDto(object obj) : base(obj){}
    
    public CreatePostDto(){}
}

[AddAnswerType(AnswerType.Post)]
public class UpdatePostDto : AbstractDto
{
    public string Text{ get; set; }

    public UpdatePostDto(string text = null)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }
    public UpdatePostDto(object obj) : base(obj){}
    
    public UpdatePostDto(){}
}