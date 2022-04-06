using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.controllers.messages;

namespace socialNetworkApp.api.controllers.chat;

[ApiController]
[Route("api/chat")]
public class ChatController : Controller
{
    [HttpGet("get/last")]
    public ChatAnswer GelLastChats(int limit = 100, int offset = 0)
    {
        Guid a;
        return new(
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
                a)
        );
    }

    [HttpGet("get/with_message/last")]
    public ChatAnswerWithMessage GelLastChatsWithlastMessage(int limit = 100, int offset = 0)
    {
        Guid a;
        Guid chat_id;
        return new(
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
            ));
    }


    [HttpGet("get/{chat_id:guid}")]
    public ChatAnswer GelChatOnId(Guid chat_id)
    {
        Guid a;
        return new(
            new ChatDto(chat_id, "n", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a));
    }

    [HttpGet("find/by_name")]
    public ChatAnswer SearchChatOnName(string name)
    {
        Guid a;
        return new(
            new ChatDto(Guid.NewGuid(), name, DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a));
    }

    [HttpGet("find/by_message")]
    public ChatAnswerWithMessage SearchChatOnTestMessage(string messageText)
    {
        Guid a;
        Guid chat_id;
        return new(
            new ChatWithMessageDto(chat_id = Guid.NewGuid(), "dfgdfg", DateTime.Today,
                new List<Guid>()
                    {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a,
                new MessageDto(Guid.NewGuid(), messageText, Guid.NewGuid(), chat_id, DateTime.Now, null)
            ));
    }

    [HttpPost("new")]
    public ChatAnswerWithMessage CreateChat(CreateChatDto newChatDto)
    {
        Guid a;
        return new ChatAnswerWithMessage(new ChatWithMessageDto(
            a = Guid.NewGuid(),
            newChatDto.Name,
            DateTime.Now,
            newChatDto.Users,
            newChatDto.UserCreator,
            new MessageDto(new Guid(), SystemMessages.CreateChat, null, a, DateTime.Now, null, MessageType.SystemMassage),
            newChatDto.GroupCreator,
            newChatDto.ChatCreatorType,
            newChatDto.ChatType,
            null,
            null,
            newChatDto.Photo
        ));
    }

    [HttpPut("edit/metainfo/{chat_id:guid}")]
    public ChatAnswer CreateChat(Guid chat_id, UpdateChatDto updatedChatDto)
    {
        Guid a;
        return new ChatAnswer(new ChatDto(Guid.NewGuid(), updatedChatDto.Name, DateTime.Today,
            new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
            a));
    }
    
    [HttpPut("edit/users/{chat_id:guid}")]
    public ChatAnswer CreateChat(Guid chat_id, UserOperationClass op, Guid[] users)
    {
        Guid a;
        return new ChatAnswer(new ChatDto(Guid.NewGuid(), "sdfgs", DateTime.Today,
            new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
            a));
    }
    
    [HttpDelete("dalete/{chat_id:guid}")]
    public ChatAnswer CreateChat(Guid chat_id)
    {
        return new ChatAnswer();
    }

}