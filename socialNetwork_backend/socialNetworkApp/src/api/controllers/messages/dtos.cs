using System.ComponentModel.DataAnnotations;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.messages;

[AddAnswerType(AnswerType.Massage)]
public class MessageDto : AbstractDto
{
    [Required] [Display(Name = "id")] public virtual Guid Id { get; set; }
    [Required] [Display(Name = "text")] public virtual string Text { get; set; }
    [Required] [Display(Name = "author")] public virtual Guid? AuthorId { get; set; }
    [Required] [Display(Name = "chat_id")] public virtual Guid ChatId { get; set; }

    [Required]
    [Display(Name = "created_at")]
    public virtual DateTime CreatedAt { get; set; }

    [Display(Name = "updated_at")] public virtual DateTime? UpdatedAt { get; set; } = null;
    [Display(Name = "message_type")] public virtual MessageTypeEnum MessageTypeEnum { get; set; } = MessageTypeEnum.Text;
    [Display(Name = "viewed")] public virtual bool Viewed { get; set; } = false;
    [Display(Name = "is_deleted")] public virtual bool IsDeleted { get; set; } = false;

    public MessageDto(Guid id = default, string text = null, Guid? authorId = default, Guid chatId = default,
        DateTime createdAt = default, DateTime? updatedAt = default, MessageTypeEnum messageTypeEnum = default,
        bool viewed = default, bool isDeleted = default)
    {
        Id = id;
        Text = text ?? throw new ArgumentNullException(nameof(text));
        AuthorId = authorId;
        ChatId = chatId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        MessageTypeEnum = messageTypeEnum;
        Viewed = viewed;
        IsDeleted = isDeleted;
    }

    public MessageDto(object obj) : base(obj)
    {
    }

    public MessageDto()
    {
    }
}

[AddAnswerType(AnswerType.Massage)]
public class CreateMessageDto : AbstractDto
{
    [Required] [Display(Name = "text")] public virtual string Text { get; set; }

    public CreateMessageDto(string text = null)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }

    public CreateMessageDto(object obj) : base(obj)
    {
    }

    public CreateMessageDto()
    {
    }
}

public class UpdateMessageDto : AbstractDto
{
    [Display(Name = "new_text")] public virtual string? NewText { get; set; }
}