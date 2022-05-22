using System.ComponentModel.DataAnnotations;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.posts;

[AddAnswerType(AnswerType.Post)]
public class PostDto : AbstractDto
{
    [Required] [Display(Name = "id")] public virtual Guid Id { get; set; }
    [Required] [Display(Name = "text")] public virtual string Text { get; set; }
    [Required] [Display(Name = "author")] public virtual Guid? Author { get; set; }

    [Required]
    [Display(Name = "created_at")]
    public virtual DateTime CreatedAt { get; set; }

    [Display(Name = "updated_at")] public virtual DateTime? UpdatedAt { get; set; } = null;
    [Display(Name = "viewed_count")] public virtual int ViewedCount { get; set; } = 0;
    [Display(Name = "is_deleted")] public virtual bool IsDeleted { get; set; } = false;

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

    public PostDto(object obj) : base(obj)
    {
    }

    public PostDto()
    {
    }
}

[AddAnswerType(AnswerType.Post)]
public class CreatePostDto : AbstractDto
{
    [Required] [Display(Name = "text")] public virtual string Text { get; set; }

    public CreatePostDto(string text = null)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }

    public CreatePostDto(object obj) : base(obj)
    {
    }

    public CreatePostDto()
    {
    }
}

[AddAnswerType(AnswerType.Post)]
public class UpdatePostDto : AbstractDto
{
    [Display(Name = "text")] public virtual string? Text { get; set; }

    public UpdatePostDto(string text = null)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }

    public UpdatePostDto(object obj) : base(obj)
    {
    }

    public UpdatePostDto()
    {
    }
}