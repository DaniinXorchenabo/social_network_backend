using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.controllers.chat;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.responses;
using socialNetworkApp.config;
using socialNetworkApp.db;

namespace socialNetworkApp.api.controllers.messages;

[ApiController]
[Route("api/messages")]
[Produces("application/json")]
public class MessagesController : Controller
{
    protected readonly IEnv Env_;
    protected readonly BaseBdConnection Db;
    protected readonly IHttpContextAccessor HttpContext;

    public MessagesController(IEnv env, BaseBdConnection context)
    {
        Env_ = env;
        Db = context;
        // HttpContext = httpContext;
    }
    
    
    [HttpGet("chat/{chat_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(MessageAnswer<Pagination>), 422)]
    public async Task<IActionResult> GetMessagesFromChat(Guid chat_id, [FromQuery] Pagination pagination)
    {
        return new Resp(200, new MessageAnswer(
            new MessageDto(Guid.NewGuid(), "sf", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "serff", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), chat_id, DateTime.Now)
        ));
    }

    [HttpGet("chat/{chat_id:guid}/datatime")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(MessageAnswer<Pagination>), 422)]
    public async Task<IActionResult> GetMessagesFromChatThroughData(Guid chat_id, DateTime dateTime,
        [FromQuery] Pagination pagination)
    {
        return new Resp(200, new MessageAnswer(
            new MessageDto(Guid.NewGuid(), "sf", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "serff", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), chat_id, DateTime.Now)
        ));
    }

    [HttpGet("chat/{chat_id:guid}/search/text")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(MessageAnswer<Pagination>), 422)]
    public async Task<IActionResult> SearchMessagesFromChat(Guid chat_id, string foundText,
        [FromQuery] Pagination pagination)
    {
        return new Resp(200, new MessageAnswer(
            new MessageDto(Guid.NewGuid(), "sf", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "serff", Guid.NewGuid(), chat_id, DateTime.Now),
            new MessageDto(Guid.NewGuid(), "sdfhf", Guid.NewGuid(), chat_id, DateTime.Now)
        ));
    }

    [HttpPost("chat/{chat_id:guid}/new")]
    [Authorize]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status201Created)]
    [MyProducesResponseType(typeof(MessageAnswer<CreateMessageDto>), 422)]
    public async Task<IActionResult> SendMessage(Guid chat_id, CreateMessageDto new_msg)
    {
        var userId = Guid.Parse(this.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        await using (var db = Db)
        {
            var newMsgFb = new MessageDb(new_msg)
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                MessageTypeEnum = MessageTypeEnum.Text,
                AuthorId = userId,
                ChatId = chat_id,

            };
            await db.Messages.AddAsync(newMsgFb);
            // await db.ChatsToUsers.AddRangeAsync(chatToUserList);
            // await db.Messages.AddAsync(startMsg);
            // db.ChatsToUsers.
            await db.SaveChangesAsync();
            return new Resp(201, new MessageAnswer(newMsgFb));
        }
    }

    [HttpPut("chat/{chat_id:guid}/edit/{message_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(MessageAnswer<UpdateMessageDto>), 422)]
    public async Task<IActionResult> EditMessage(Guid chat_id, Guid message_id, UpdateMessageDto updateMsg)
    {
        return new Resp(200, new MessageAnswer(
            new MessageDto(message_id, updateMsg.NewText, Guid.NewGuid(), chat_id, DateTime.Now,
                updatedAt: DateTime.Now)));
    }

    [HttpDelete("chat/{chat_id:guid}/delete/{message_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMessage(Guid chat_id, Guid message_id)
    {
        return new Resp(200, new MessageAnswer());
    }
}