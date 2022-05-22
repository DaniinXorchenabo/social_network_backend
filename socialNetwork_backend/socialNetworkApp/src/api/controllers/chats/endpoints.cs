using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.controllers.auth;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.responses;

namespace socialNetworkApp.api.controllers.chat;

[ApiController]
[Route("api/chat")]
[Produces("application/json")]
public class ChatController : Controller
{
    [HttpGet("get/last")]
    [Authorize]
    [ValidationActionFilter]
    [ProducesResponseType(typeof(ChatAnswer), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MyProducesResponseType(typeof(ChatAnswer<Pagination>), 422)]
    public async Task<IActionResult> GelLastChats([FromQuery] Pagination pagination)
    {
        Guid a;
        return new Resp(200, new ChatAnswer(
            new ChatDto(Guid.NewGuid(), "n", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a),
            new ChatDto(Guid.NewGuid(), "ndfgh", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a),
            new ChatDto(Guid.NewGuid(), "sdfn", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a),
            new ChatDto(Guid.NewGuid(), "dhdhgn", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a))
        );
    }

    [HttpGet("get/with_message/last")]
    [ValidationActionFilter]
    [AuthorizeWithMods(AllModsEnum.chatReader)]
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
    [AuthorizeWithMods(AllModsEnum.chatCreator, AllModsEnum.chatReader)]
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
    [ProducesResponseType(typeof(ChatAnswerWithMessage<CreateChatDto>), StatusCodes.Status201Created)]
    [MyProducesResponseType(typeof(ChatAnswerWithMessage<CreateChatDto>), 422)]
    public async Task<IActionResult> CreateChat(CreateChatDto newChatDto)
    {
        Guid a;
        return new Resp(201, new ChatAnswerWithMessage(new ChatWithMessageDto(
            a = Guid.NewGuid(),
            newChatDto.Name,
            DateTime.Now,
            newChatDto.Users,
            Guid.NewGuid(),
            new MessageDto(new Guid(), SystemMessages.CreateChat, null, a, DateTime.Now, null,
                MessageType.SystemMassage),
            null,
            newChatDto.ChatCreatorType,
            newChatDto.ChatType,
            null,
            null,
            newChatDto.Photo
        )));
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