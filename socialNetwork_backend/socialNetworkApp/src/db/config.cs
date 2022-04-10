using socialNetworkApp.api.controllers.users;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.config;

namespace socialNetworkApp.db;



public class BaseBdConnection : DbContext
{
    private readonly Env _env;
    public DbSet<UserDb> Users { get; set; }

    public BaseBdConnection(DbContextOptions<BaseBdConnection> options): base(options)
    {
        this._env = new Env();
        Database.EnsureCreated();
        Console.WriteLine(GetConnectionString(_env));
    }

    public BaseBdConnection(DbContextOptionsBuilder optionsBuilder)
    {
        this._env = new Env();
        OnConfiguring(optionsBuilder);
        
        Console.WriteLine(GetConnectionString(_env));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine(GetConnectionString(_env));
        optionsBuilder.UseNpgsql( GetConnectionString(_env) );
    }

    public string GetConnectionString(Env env)
    {
        return  $"Host={env.Db.DbHost};Port={env.Db.DbPort};Database={env.Db.DbName};Username={env.Db.DbUsername};Password={env.Db.DbPassword}";
    }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=usersdb2;Username=postgres;Password=password");
    // }
}