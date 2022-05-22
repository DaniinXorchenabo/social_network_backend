using System.Reflection;
using socialNetworkApp.api.controllers.users;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.config;

namespace socialNetworkApp.db;

public class BaseBdConnection : DbContext
{
    static BaseBdConnection()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<AllModsEnum>();
    }

    private readonly Env _env;

    public DbSet<UserDb> Users { get; set; } = null!;

    public BaseBdConnection(DbContextOptions<BaseBdConnection> options) : base(options)
    {
        this._env = new Env();
        Console.WriteLine(GetConnectionString(_env));
    }

    public BaseBdConnection(DbContextOptionsBuilder optionsBuilder)
    {
        this._env = new Env();
        OnConfiguring(optionsBuilder);

        Console.WriteLine(GetConnectionString(_env));
    }

    public BaseBdConnection()
    {
        this._env = new Env();
        OnConfiguring(Conn.Superuser);

        Console.WriteLine(GetConnectionString(_env));
    }

    protected override void OnModelCreating(ModelBuilder builder) => builder.HasPostgresEnum<AllModsEnum>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine(GetConnectionString(_env));
        optionsBuilder.UseNpgsql(GetConnectionString(_env));
    }

    public string GetConnectionString(Env env)
    {
        return
            $"Host={env.Db.DbHost};Port={env.Db.DbPort};Database={env.Db.DbName};Username={env.Db.DbUsername};Password={env.Db.DbPassword}";
    }

    public async Task OnSrart(Type? parentType = null)
    {
        parentType ??= typeof(AbstractEntity);
        IEnumerable<Type> list = Assembly.GetAssembly(parentType).GetTypes()
            .Where(type => type.IsSubclassOf(parentType));

        foreach (var itm in list)
        {
            await OnSrart(itm);

            if (itm.GetMethod("OnSrart")?
                    .Invoke(Activator.CreateInstance(itm) as AbstractEntity, new object[] {this}) is Task f)
            {
                await f;
            }
        }
    }
}