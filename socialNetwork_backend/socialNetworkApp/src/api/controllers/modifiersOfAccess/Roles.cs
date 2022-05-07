namespace socialNetworkApp.api.controllers.modifiersOfAccess;

public class RolesFromMods
{
    public static EnumModList User { get; } = new EnumModList()
    {
        AllModsEnum.msgReader, AllModsEnum.msgCreator, AllModsEnum.msgUpdater, AllModsEnum.msgDeleter,
        AllModsEnum.chatReader, AllModsEnum.chatCreator, AllModsEnum.chatUpdater, AllModsEnum.chatDeleter,
        AllModsEnum.userReader,  AllModsEnum.userUpdater, AllModsEnum.userDeleter,
        AllModsEnum.postReader, AllModsEnum.postCreator, AllModsEnum.postUpdater, AllModsEnum.postDeleter
    };
}