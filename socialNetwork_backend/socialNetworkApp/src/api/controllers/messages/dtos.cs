using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.messages;

[AddAnswerType(AnswerType.Massage)]
public  class MessageDto : AbstractDto
{
    public Guid Id{ get; set; }
    public string Text{ get; set; }
    public Guid? Autor{ get; set; }
    public Guid ChatId{ get; set; }
    public DateTime CreatedAt{ get; set; }
    public DateTime? UpdatedAt{ get; set; } = null;
    public MessageType MessageType{ get; set; } = MessageType.Text;
    public bool Viewed{ get; set; } = false;
    public bool IsDeleted{ get; set; } = false;

    public MessageDto(Guid id = default, string text = null, Guid? autor = default, Guid chatId = default,
        DateTime createdAt = default, DateTime? updatedAt = default, MessageType messageType = default,
        bool viewed = default, bool isDeleted = default)
    {
        Id = id;
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Autor = autor;
        ChatId = chatId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        MessageType = messageType;
        Viewed = viewed;
        IsDeleted = isDeleted;
    }
    public MessageDto(object obj) : base(obj){}
    
    public MessageDto(){}
}
[AddAnswerType(AnswerType.Massage)]
public class CreateMessageDto : AbstractDto
{
    public string Text{ get; set; }
    public Guid Author{ get; set; }
    public MessageType MessageType{ get; set; } = MessageType.Text;

    public CreateMessageDto(string text = null, Guid author = default, MessageType messageType = default)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Author = author;
        MessageType = messageType;
    }

    public CreateMessageDto(object obj) : base(obj){}
    
    public CreateMessageDto(){}
}


public class UpdateMessageDto : AbstractDto
{
    public string new_text { get; set; }
}