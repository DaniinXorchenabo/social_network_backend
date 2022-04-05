namespace socialNetworkApp.api.controllers.chat;

public enum ChatType
{
    simple,
    secret,
    fantom
}

public enum ChatCreatorType
{
    user,
    group
}

public enum UserOperationClass
{
    usersAdd,
    usersRemove,
    adminsAdd,
    adminsRemove,
    blackListAdd,
    blackListRemove
}