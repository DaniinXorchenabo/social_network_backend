namespace socialNetworkApp.api.controllers.messages;

public enum MessageType
{
    text,
    systemMassage
}

public static class SystemMessages
{
    public static string createChat { get; set; } = "Чат был создан";
}