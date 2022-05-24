using System.Reflection;
using socialNetworkApp.api.controllers.users;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using socialNetworkApp.api.controllers.chat;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.config;
using ChatToUserDb = socialNetworkApp.api.controllers.chat.ChatToUserDb;

namespace socialNetworkApp.db;

public class BaseBdConnection : DbContext
{
    static BaseBdConnection()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<AllModsEnum>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ChatCreatorTypeEnum>();;
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ChatTypeEnum>();;
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ChatToUserRoleEnum>();;
        NpgsqlConnection.GlobalTypeMapper.MapEnum<MessageTypeEnum>();;
    }

    private readonly Env _env;

    public DbSet<UserDb> Users { get; set; } = null!;
    public DbSet<ChatDb> Chats { get; set; } = null!;
    public DbSet<ChatToUserDb> ChatsToUsers { get; set; } = null!;
    public DbSet<MessageDb> Messages { get; set; } = null!;

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresEnum<AllModsEnum>();
        builder.HasPostgresEnum<ChatCreatorTypeEnum>();
        builder.HasPostgresEnum<ChatTypeEnum>();
        builder.HasPostgresEnum<ChatToUserRoleEnum>();
        builder.HasPostgresEnum<MessageTypeEnum>();
        builder.Entity<ChatToUserDb>()
            .HasKey(u => new { u.UserId, u.ChatId});
        builder.Entity<ChatToUserDb>()
            .HasOne(u => u.UserEntity)
            .WithMany(c => c.ChatUserEntities)
            .HasForeignKey(u => u.UserId);
        builder.Entity<ChatToUserDb>()
            .HasOne(u => u.ChatEntity)
            .WithMany(c => c.ChatUserEntities)
            .HasForeignKey(u => u.ChatId);
        
        builder.Entity<MessageDb>()
            .HasOne(u => u.AuthorEntity)
            .WithMany(c => c.MessageEntities)
            .HasForeignKey(u => u.AuthorId);
        builder.Entity<MessageDb>()
            .HasOne(u => u.ChatEntity)
            .WithMany(c => c.MessageEntities)
            .HasForeignKey(u => u.ChatId);
        builder.Entity<MessageDb>()
            .HasOne(u => u.ChatsAndUsersEntity)
            .WithMany(c => c.MessageEntities)
            .HasForeignKey(u => new {u.AuthorId, u.ChatId});
        
        // builder.Entity<SubscriptionLevel>()
        //     .ToTable("SubscriptionLevels");
        //
        // builder.Entity<Customer>()
        //     .ToTable("Customers");
                
        // builder.Entity<ChatDb>()
        //     .Property(c => c.ChatCreatorType)
        //     .HasConversion<string>();
        // builder.Entity<ChatDb>()
        //     .Property(c => c.ChatType)
        //     .HasConversion<string>();
        //
        // builder.Entity<ChatToUserDb>()
        //     .Property(c => c.Roles)
        //     .HasConversion<List<string>>();
        //
        // builder.Entity<MessageDb>()
        //     .Property(s => s.MessageType)
        //     .HasConversion<string>();
        
        // builder.Entity<UserDb>()
        //     .Property(s => s.Mods)
        //     .HasConversion<List<string>>();

        // base.OnModelCreating(builder);
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine(GetConnectionString(_env));
        optionsBuilder.UseNpgsql(GetConnectionString(_env));
            // .UseCamelCaseNamingConvention();
        
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