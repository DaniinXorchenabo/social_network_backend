using System.Web.Http.Filters;
using EntityFramework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.api.controllers.auth;
using socialNetworkApp.api.controllers.chat;
using socialNetworkApp.api.controllers.modifiersOfAccess;
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
    [AuthorizeWithMods(AllModsEnum.msgReader)]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(MessageAnswer<Pagination>), 422)]
    public async Task<IActionResult> GetMessagesFromChat(Guid chat_id, [FromQuery] Pagination pagination)
    {
        await using (var db = Db)
        {
            var current_user = Guid.Parse(this.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            var q = (from msg in db.Messages
                where chat_id == msg.ChatId & msg.IsDeleted == false
                select msg);
            var d = await QBuilder.Select(
                q,
                pagination,
                x => x.OrderByDescending(x => x.CreatedAt)
            );


            return new Resp(200, new MessageAnswer(d));
        }
    }

    [Obsolete]
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

    [Obsolete]
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
    [AuthorizeWithMods(AllModsEnum.msgCreator)]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status201Created)]
    [MyProducesResponseType(typeof(MessageAnswer<CreateMessageDto>), 422)]
    public async Task<IActionResult> SendMessage(Guid chat_id, CreateMessageDto new_msg)
    {
        var userId = Guid.Parse(this.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        await using (var db = Db)
        {
            var d = await db.ChatsToUsers
                .Where(x => x.ChatId == chat_id & x.UserId == userId & !x.Roles.Contains(ChatToUserRoleEnum.BlackList))
                .FirstOrDefaultAsync();
            if (d != null)
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
            return new Resp(404, new MessageAnswer());
            
        }
    }

    [HttpPut("chat/{chat_id:guid}/edit/{message_id:guid}")]
    [AuthorizeWithMods(AllModsEnum.msgUpdater)]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(MessageAnswer<UpdateMessageDto>), 422)]
    public async Task<IActionResult> EditMessage(Guid chat_id, Guid message_id, UpdateMessageDto updateMsg)
    {
        var userId = Guid.Parse(this.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        await using (var db = Db)
        {
            var d = await db.Messages
                .Where(
                    x => x.Id == message_id 
                        & x.ChatId == chat_id 
                        & x.AuthorId == userId 
                        & x.IsDeleted == false 
                        & !x.ChatsAndUsersEntity.Roles.Contains(ChatToUserRoleEnum.BlackList)
                        )
                .FirstOrDefaultAsync();
            if (d != null)
            {
                d!.Update(updateMsg);
                Console.WriteLine($"==> {d}");
                await db.SaveChangesAsync();
                return new Resp(200, new MessageAnswer(d));
            }

            return new Resp(404, new MessageAnswer(d));
        }
    }

    [HttpDelete("chat/{chat_id:guid}/delete/{message_id:guid}")]
    [AuthorizeWithMods(AllModsEnum.msgDeleter)]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(MessageAnswer), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMessage(Guid chat_id, Guid message_id)
    {
        var userId = Guid.Parse(this.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);

        await using (var db = Db)
        {
            var d = await db.Messages
                .Where(
                    x => x.Id == message_id
                         & x.ChatId == chat_id
                         & (x.AuthorId == userId | x.ChatsAndUsersEntity.Roles.Contains(ChatToUserRoleEnum.Admin))
                         & !x.ChatsAndUsersEntity.Roles.Contains(ChatToUserRoleEnum.BlackList))
                .FirstOrDefaultAsync();
            if (d != null)
            {
                d.IsDeleted = true;
                Console.WriteLine($"==> {d}");
                await db.SaveChangesAsync();
                return new Resp(200, new MessageAnswer(d));
            }

            return new Resp(404, new MessageAnswer(d));
        }
    }
}