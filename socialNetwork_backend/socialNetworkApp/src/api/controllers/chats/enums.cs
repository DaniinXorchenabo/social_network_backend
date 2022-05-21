namespace socialNetworkApp.api.controllers.chat;

public enum ChatType
{
    Simple,
    Secret,
    Fantom
}

public enum ChatCreatorType
{
    User,
    Group
}

public enum UserOperationClass
{
    UsersAdd,
    UsersRemove,
    AdminsAdd,
    AdminsRemove,
    BlackListAdd,
    BlackListRemove
}

public enum ChatToUserRole
{
    User,
    Admin,
    Creator,
    BlackList
}