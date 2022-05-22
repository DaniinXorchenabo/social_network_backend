using Microsoft.EntityFrameworkCore;

namespace socialNetworkApp.db;

public static class Conn
{
    public static DbContextOptionsBuilder<BaseBdConnection> Superuser;

    static Conn()
    {
        Superuser = new DbContextOptionsBuilder<BaseBdConnection>();
    }
}