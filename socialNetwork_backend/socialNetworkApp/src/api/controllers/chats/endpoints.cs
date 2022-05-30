using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.api.controllers.auth;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.responses;
using socialNetworkApp.config;
using socialNetworkApp.db;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using Npgsql;
using SuccincT.Functional;

namespace socialNetworkApp.api.controllers.chat;

[ApiController]
[Route("api/chat")]
[Produces("application/json")]
public class ChatController : Controller
{
    protected readonly IEnv Env_;
    protected readonly BaseBdConnection Db;
    protected readonly IHttpContextAccessor HttpContext;

    public ChatController(IEnv env, BaseBdConnection context)
    {
        Env_ = env;
        Db = context;
        // HttpContext = httpContext;
    }

    [HttpGet("get/last")]
    [Authorize]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(ChatAnswer), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MyProducesResponseType(typeof(ChatAnswerWithMessage<Pagination>), 422)]
    public async Task<IActionResult> GelLastChats([FromQuery] Pagination pagination)
    {
        await using (var db = Db)
        {
            var current_user = Guid.Parse(this.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            var s = from chat in db.Chats select chat;
            var d = s;
            
            var lastChatsLinq2 = await QBuilder.Select(
                (from ctu in db.ChatsToUsers
                    join c in db.Chats on ctu.ChatId equals c.Id into joined_c
                    from j_c in joined_c.DefaultIfEmpty()
                    join m in (
                        from mm_ in db.Messages
                        group mm_ by mm_.ChatId
                        into g
                        select new
                        {
                            _chat_id = g.Key,
                            created_at = g.Max(messageDb => messageDb.CreatedAt),
                        }
                    ) on ctu.ChatId equals m._chat_id
                    join lastM in db.Messages on new {A = m._chat_id, B = m.created_at} equals new
                        {A = lastM.ChatId, B = lastM.CreatedAt}
                    where ctu.UserId == current_user
                    select new {currentChatToUser = ctu, chatEntity = j_c, lastMessage = lastM}),
                pagination,
                x => x.OrderByDescending(x => x.lastMessage.CreatedAt)
                );
            Console.WriteLine(lastChatsLinq2);
            Console.WriteLine("");

            var da = new List<ChatWithMessageDto>() { };
            foreach (var x1 in lastChatsLinq2)
            {
                da.Add(new ChatWithMessageDto(x1.chatEntity)
                {
                    Message = new MessageDto(x1.lastMessage)
                });
            }

            return new Resp(
                200,
                new ChatAnswerWithMessage(da)
            );
        }
    }

    [HttpGet("get/with_message/last")]
    [ValidationActionFilter]
    // [AuthorizeWithMods(AllModsEnum.chatReader)]
    [ProducesResponseType(typeof(ChatAnswerWithMessage), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(ChatAnswerWithMessage<Pagination>), 422)]
    public async Task<IActionResult> GelLastChatsWithlastMessage([FromQuery] Pagination pagination)
    {
        Guid a;
        Guid chat_id;
        return new Resp(200, new ChatAnswerWithMessage(
            new ChatWithMessageDto(chat_id = Guid.NewGuid(), "dfgdfg", DateTime.Today,
                new List<Guid>()
                    {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a,
                new MessageDto(Guid.NewGuid(), "sdfgsdfzsdgsdfgs", Guid.NewGuid(), chat_id, DateTime.Now, null)
            ),
            new ChatWithMessageDto(chat_id = Guid.NewGuid(), "23tg45g", DateTime.Today,
                new List<Guid>()
                    {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a,
                new MessageDto(Guid.NewGuid(), "drfgsfdg", Guid.NewGuid(), chat_id, DateTime.Now, null)
            ),
            new ChatWithMessageDto(chat_id = Guid.NewGuid(), "dg", DateTime.Today,
                new List<Guid>()
                    {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a,
                new MessageDto(Guid.NewGuid(), "768edyjhr678r467yuj", Guid.NewGuid(), chat_id, DateTime.Now, null)
            )));
    }


    [HttpGet("get/{chat_id:guid}")]
    [ValidationActionFilter]
    // [AuthorizeWithMods(AllModsEnum.chatCreator, AllModsEnum.chatReader)]
    [ProducesResponseType(typeof(ChatAnswer), StatusCodes.Status200OK)]
    public async Task<IActionResult> GelChatOnId(Guid chat_id)
    {
        Guid a;
        return new Resp(200, new ChatAnswer(
            new ChatDto(chat_id, "n", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a)));
    }

    [HttpGet("find/by_name")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(ChatAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(ChatAnswer<Pagination>), 422)]
    public async Task<IActionResult> SearchChatOnName(string name, [FromQuery] Pagination pagination)
    {
        Guid a;
        return new Resp(200, new ChatAnswer(
            new ChatDto(Guid.NewGuid(), name, DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a)));
    }

    [HttpGet("find/by_message")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(ChatAnswerWithMessage), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(ChatAnswerWithMessage<Pagination>), 422)]
    public async Task<IActionResult> SearchChatOnTestMessage(string messageText, [FromQuery] Pagination pagination)
    {
        Guid a;
        Guid chat_id;
        return new Resp(200, new ChatAnswerWithMessage(
            new ChatWithMessageDto(chat_id = Guid.NewGuid(), "dfgdfg", DateTime.Today,
                new List<Guid>()
                    {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a,
                new MessageDto(Guid.NewGuid(), messageText, Guid.NewGuid(), chat_id, DateTime.Now, null)
            )));
    }

    [HttpPost("new")]
    [ValidationActionFilter]
    [Authorize]
    [ProducesResponseType(typeof(ChatAnswerWithMessage<CreateChatDto>), StatusCodes.Status201Created)]
    [MyProducesResponseType(typeof(ChatAnswerWithMessage<CreateChatDto>), 422)]
    public async Task<IActionResult> CreateChat(CreateChatDto newChatDto)
    {
        // var chatDb = new ChatDb(newChatDto);
        // var creator = context.User.FindFirst(ClaimsIdentity.DefaultNameClaimType);
        // creator.
        // var httpC = HttpContext.HttpContext;
        var userId = Guid.Parse(this.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
        newChatDto.Users.Add(userId);
        var chatId = Guid.NewGuid();
        var chatToUserList = newChatDto.Users
            .Select(x => new ChatToUserDb {ChatId = chatId, UserId = x}).ToList();
        chatToUserList.FirstOrDefault(x => x.UserId == userId).Roles.Add(ChatToUserRoleEnum.Creator);
        // var startMsg = new MessageDb
        // {
        //     CreatedAt = DateTime.UtcNow,
        //     Id = Guid.NewGuid(),
        //     MessageTypeEnum = MessageTypeEnum.SystemMessage,
        //     Text = Enum.GetName(typeof(SystemChatMessagesEnum), SystemChatMessagesEnum.ChatCreated)
        // };
        await using (var db = Db)
        {
            var chat = new ChatDb(newChatDto)
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ChatUserEntities = chatToUserList,
                MessageEntities = new List<MessageDb>
                {
                    new MessageDb
                    {
                        CreatedAt = DateTime.UtcNow,
                        Id = Guid.NewGuid(),
                        MessageTypeEnum = MessageTypeEnum.SystemMessage,
                        Text = Enum.GetName(typeof(SystemChatMessagesEnum), SystemChatMessagesEnum.ChatCreated)
                    }
                }
            };

            Console.WriteLine(chat.ToString());

            await db.Chats.AddAsync(chat);
            // await db.ChatsToUsers.AddRangeAsync(chatToUserList);
            // await db.Messages.AddAsync(startMsg);
            // db.ChatsToUsers.
            await db.SaveChangesAsync();
            return new Resp(201, new ChatAnswerWithMessage(chat));
        }
    }

    [HttpPut("edit/metainfo/{chat_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(ChatAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(ChatAnswer<UpdateChatDto>), 422)]
    public async Task<IActionResult> EditMetainfoChat(Guid chat_id, UpdateChatDto updatedChatDto)
    {
        Guid a;
        return new Resp(200, new ChatAnswer(new ChatDto(Guid.NewGuid(), updatedChatDto.Name, DateTime.Today,
            new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
            a)));
    }

    [HttpPut("edit/users/{chat_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(ChatAnswer), StatusCodes.Status200OK)]
    [MyProducesResponseType(typeof(ChatAnswer<UpdateChatUsersDto>), 422)]
    public async Task<IActionResult> EditUsersChat(Guid chat_id, UpdateChatUsersDto updateUsers)
    {
        Guid a;
        return new Resp(200, new ChatAnswer(new ChatDto(Guid.NewGuid(), "sdfgs", DateTime.Today,
            new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
            a)));
    }

    [HttpDelete("dalete/{chat_id:guid}")]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(ChatAnswer), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteChat(Guid chat_id)
    {
        return new Resp(200, new ChatAnswer());
    }
}