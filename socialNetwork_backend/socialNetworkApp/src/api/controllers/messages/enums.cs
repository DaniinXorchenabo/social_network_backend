namespace socialNetworkApp.api.controllers.messages;

public enum MessageType
{
    Text,
    SystemMassage
}

public static class SystemMessages
{
    public static string CreateChat { get; set; } = "Чат был создан";
}