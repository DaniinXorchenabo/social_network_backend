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
            new Chat(Guid.NewGuid(), "n", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a),
            new Chat(Guid.NewGuid(), "ndfgh", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a),
            new Chat(Guid.NewGuid(), "sdfn", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a),
            new Chat(Guid.NewGuid(), "dhdhgn", DateTime.Today,
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
            new ChatWithMessage(chat_id = Guid.NewGuid(), "dfgdfg", DateTime.Today,
                new List<Guid>()
                    {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a,
                new Message(Guid.NewGuid(), "sdfgsdfzsdgsdfgs", Guid.NewGuid(), chat_id, DateTime.Now, null)
            ),
            new ChatWithMessage(chat_id = Guid.NewGuid(), "23tg45g", DateTime.Today,
                new List<Guid>()
                    {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a,
                new Message(Guid.NewGuid(), "drfgsfdg", Guid.NewGuid(), chat_id, DateTime.Now, null)
            ),
            new ChatWithMessage(chat_id = Guid.NewGuid(), "dg", DateTime.Today,
                new List<Guid>()
                    {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a,
                new Message(Guid.NewGuid(), "768edyjhr678r467yuj", Guid.NewGuid(), chat_id, DateTime.Now, null)
            ));
    }


    [HttpGet("get/{chat_id:guid}")]
    public ChatAnswer GelChatOnId(Guid chat_id)
    {
        Guid a;
        return new(
            new Chat(chat_id, "n", DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a));
    }

    [HttpGet("find/by_name")]
    public ChatAnswer SearchChatOnName(string name)
    {
        Guid a;
        return new(
            new Chat(Guid.NewGuid(), name, DateTime.Today,
                new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a));
    }

    [HttpGet("find/by_message")]
    public ChatAnswerWithMessage SearchChatOnTestMessage(string messageText)
    {
        Guid a;
        Guid chat_id;
        return new(
            new ChatWithMessage(chat_id = Guid.NewGuid(), "dfgdfg", DateTime.Today,
                new List<Guid>()
                    {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
                a,
                new Message(Guid.NewGuid(), messageText, Guid.NewGuid(), chat_id, DateTime.Now, null)
            ));
    }

    [HttpPost("new")]
    public ChatAnswerWithMessage CreateChat(CreateChat newChat)
    {
        Guid a;
        return new ChatAnswerWithMessage(new ChatWithMessage(
            a = Guid.NewGuid(),
            newChat.name,
            DateTime.Now,
            newChat.users,
            newChat.userCreator,
            new Message(new Guid(), SystemMessages.createChat, null, a, DateTime.Now, null, MessageType.systemMassage),
            newChat.groupCreator,
            newChat.chatCreatorType,
            newChat.chatType,
            null,
            null,
            newChat.photo
        ));
    }

    [HttpPut("edit/metainfo/{chat_id:guid}")]
    public ChatAnswer CreateChat(Guid chat_id, UpdateChat updatedChat)
    {
        Guid a;
        return new ChatAnswer(new Chat(Guid.NewGuid(), updatedChat.name, DateTime.Today,
            new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
            a));
    }
    
    [HttpPut("edit/users/{chat_id:guid}")]
    public ChatAnswer CreateChat(Guid chat_id, UserOperationClass op, Guid[] users)
    {
        Guid a;
        return new ChatAnswer(new Chat(Guid.NewGuid(), "sdfgs", DateTime.Today,
            new List<Guid>() {(a = Guid.NewGuid()), Guid.NewGuid(), Guid.NewGuid()},
            a));
    }
    
    [HttpDelete("dalete/{chat_id:guid}")]
    public ChatAnswer CreateChat(Guid chat_id)
    {
        return new ChatAnswer();
    }

}