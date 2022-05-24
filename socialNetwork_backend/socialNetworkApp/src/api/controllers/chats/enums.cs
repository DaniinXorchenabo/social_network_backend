namespace socialNetworkApp.api.controllers.chat;

public enum ChatTypeEnum
{
    Simple,
    Secret,
    Fantom
}

public enum ChatCreatorTypeEnum
{
    User,
    Group
}

public enum UserOperationClassEnum
{
    UsersAdd,
    UsersRemove,
    AdminsAdd,
    AdminsRemove,
    BlackListAdd,
    BlackListRemove
}

public enum ChatToUserRoleEnum
{
    User,
    Admin,
    Creator,
    BlackList
}