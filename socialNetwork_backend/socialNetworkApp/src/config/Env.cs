using dotenv.net;
using SuccincT;
using SuccincT.Functional;

namespace socialNetworkApp.config;

public record class Env
{
    public DbEnv Db { get; protected set; }

    public Env(string envFileName = ".env")
    {
        Db = new DbEnv();
        IEnumerator<string?>[] values =
        {
            Db.EnvInit("DB_HOST", "DB_PORT", "DB_SUPERUSER_NAME", "DB_SUPERUSER_PASSWORD", "DB_DATABASE_NAME")
                .GetEnumerator()
        };
        _ = values.Select(x => x.MoveNext()).ToList();
        LoadEnv(envFileName);
        _ = values.Select(x => x.MoveNext()).ToList();
    }

    protected static void LoadEnv(string envFileName)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        while ((baseDirectory = Path.GetDirectoryName(baseDirectory))?.EndsWith("socialNetwork_backend") == false)
        {
        }
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {Path.Combine(baseDirectory, envFileName)}));
    }
}

public interface IBaseLoader
{
    public IEnumerable<string?> EnvInit(params string[] data);

    public virtual IEnumerator<string?>[] PreloadEnv(params string[] data)
    {
        var variables = data.Select(x => LoadValue(x, "")).ToArray();
        _ = variables.Select(x => x.MoveNext()).ToList();
        return variables;
    }

    public static IEnumerator<string?> LoadValue(string nameToValue, string defaultValue)
    {
        var someValue = System.Environment.GetEnvironmentVariable(nameToValue);
        yield return null;
        string newSomeValue = someValue ?? System.Environment.GetEnvironmentVariable(nameToValue) ?? defaultValue;

        yield return newSomeValue;
    }
}

public abstract record class BaseLoader : IBaseLoader
{
    public abstract IEnumerable<string?> EnvInit(params string[] data);
}

public record class DbEnv : BaseLoader
{
    public string DbHost { get; protected set; } = null!;
    public string DbPort { get; protected set; } = null!;
    public string DbUsername { get; protected set; } = null!;
    public string DbPassword { get; protected set; } = null!;
    public string DbName { get; protected set; } = null!;

    public IEnumerable<string?> EnvInit(
        string dbHost,
        string dbPort,
        string dbPassword,
        string dbUsername,
        string dbName,
        params string[] data)
    {
        var enumerators = (this as IBaseLoader).PreloadEnv(dbHost, dbPort, dbPassword, dbUsername, dbName);
        yield return null;
        (this.DbHost,
            (this.DbPort,
                (this.DbUsername,
                    (this.DbPassword,
                        (this.DbName, _))))) = enumerators
            .Where(x => x.MoveNext() || true)
            .Select(x => x.Current ?? "")
            .ToList();
        yield return null;
    }

    public void Deconstruct(out string DbHost, out string DbPort, out string DbUsername, out string DbPassword,
        out string DbName)
    {
        DbHost = this.DbHost;
        DbPort = this.DbPort;
        DbUsername = this.DbUsername;
        DbPassword = this.DbPassword;
        DbName = this.DbName;
    }

    public override IEnumerable<string?> EnvInit(params string[] data)
    {
        return EnvInit(data[0], data[1], data[2], data[3], data[4]);
    }
}