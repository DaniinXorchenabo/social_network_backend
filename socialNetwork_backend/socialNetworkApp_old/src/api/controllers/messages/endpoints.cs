using Microsoft.AspNetCore.Mvc;

namespace socialNetworkApp.api.controllers.messages;

[ApiController]
[Route("api/messages")]
public class MessagesController: Controller
{
    [HttpGet("chat/{chat_id:guid}")]
    public MessageAnswer GetMessagesFromChat(Guid chat_id, int limit = 100, int offset = 0)
    {
        return new (
            new Message(Guid.NewGuid(), "sf", Guid.NewGuid(), chat_id, DateTime.Now),
            new Message(Guid.NewGuid(), "serff", Guid.NewGuid(), chat_id, DateTime.Now),
            new Message(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), chat_id, DateTime.Now)
            );
    }
    [HttpGet("chat/{chat_id:guid}/datatime")]
    public MessageAnswer GetMessagesFromChatThroughData(Guid chat_id, DateTime dateTime, int limit = 100)
    {
        return new (
            new Message(Guid.NewGuid(), "sf", Guid.NewGuid(), chat_id, DateTime.Now),
            new Message(Guid.NewGuid(), "serff", Guid.NewGuid(), chat_id, DateTime.Now),
            new Message(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), chat_id, DateTime.Now)
        );
    }
    [HttpGet("chat/{chat_id:guid}/search/text")]
    public MessageAnswer SearchMessagesFromChat(Guid chat_id, string foundText, int limit = 100, int offset = 0)
    {
        return new (
            new Message(Guid.NewGuid(), "sf", Guid.NewGuid(), chat_id, DateTime.Now),
            new Message(Guid.NewGuid(), "serff", Guid.NewGuid(), chat_id, DateTime.Now),
            new Message(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), chat_id, DateTime.Now)
        );
    }
    [HttpPost("chat/{chat_id:guid}/new")]
    public MessageAnswer SendMessage(Guid chat_id, CreateMessage new_msg)
    {
        return new (
            new Message(Guid.NewGuid(), new_msg.text, new_msg.autor,
                chat_id, DateTime.Now, null, new_msg.messageType));
    }    
    [HttpPut("chat/{chat_id:guid}/edit/{message_id:guid}")]
    public MessageAnswer EditMessage(Guid chat_id, Guid message_id, string new_text)
    {
        return new (
            new Message(message_id, new_text, Guid.NewGuid(), chat_id, DateTime.Now, updatedAt: DateTime.Now));
    }
    [HttpDelete("chat/{chat_id:guid}/delete/{message_id:guid}")]
    public MessageAnswer DeleteMessage(Guid chat_id, Guid message_id)
    {
        return new ();
    }
    
}