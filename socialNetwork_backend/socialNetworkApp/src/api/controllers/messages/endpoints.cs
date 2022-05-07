using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.responses;

namespace socialNetworkApp.api.controllers.messages;

[ApiController]
[Route("api/messages")]
[Produces("application/json")]
public class MessagesController : Controller
{
    [HttpGet("chat/{chat_id:guid}")]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    public Resp GetMessagesFromChat(Guid chat_id, int limit = 100, int offset = 0)
    {
        return new Resp(200, new MessageAnswer(
            new MessageDto(Guid.NewGuid(), "sf", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "serff", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), chat_id, DateTime.Now)
        ));
    }

    [HttpGet("chat/{chat_id:guid}/datatime")]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    public Resp GetMessagesFromChatThroughData(Guid chat_id, DateTime dateTime, int limit = 100)
    {
        return new Resp(200, new MessageAnswer(
            new MessageDto(Guid.NewGuid(), "sf", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "serff", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), chat_id, DateTime.Now)
        ));
    }

    [HttpGet("chat/{chat_id:guid}/search/text")]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    public Resp SearchMessagesFromChat(Guid chat_id, string foundText, int limit = 100, int offset = 0)
    {
        return new Resp(200, new MessageAnswer(
            new MessageDto(Guid.NewGuid(), "sf", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "serff", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), chat_id, DateTime.Now)
        ));
    }

    [HttpPost("chat/{chat_id:guid}/new")]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    public Resp SendMessage(Guid chat_id, CreateMessageDto new_msg)
    {
        return new Resp(200, new MessageAnswer(
            new MessageDto(Guid.NewGuid(), new_msg.Text, new_msg.Author,
                chat_id, DateTime.Now, null, new_msg.MessageType)));
    }

    [HttpPut("chat/{chat_id:guid}/edit/{message_id:guid}")]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    public Resp  EditMessage(Guid chat_id, Guid message_id, string new_text)
    {
        return new Resp(200, new MessageAnswer(
            new MessageDto(message_id, new_text, Guid.NewGuid(), chat_id, DateTime.Now, UpdatedAt: DateTime.Now)));
    }

    [HttpDelete("chat/{chat_id:guid}/delete/{message_id:guid}")]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    public Resp DeleteMessage(Guid chat_id, Guid message_id)
    {
        return new Resp(200, new MessageAnswer());
    }
}