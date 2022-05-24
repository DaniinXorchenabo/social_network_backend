namespace socialNetworkApp.api.controllers.messages;

public enum MessageTypeEnum
{
    Text,
    SystemMessage
}

public static class SystemMessages
{
    public static string CreateChat { get; set; } = "Чат был создан";
}

public enum SystemChatMessagesEnum
{
    ChatCreated
}