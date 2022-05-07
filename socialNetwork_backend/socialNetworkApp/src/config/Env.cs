using dotenv.net;
using SuccincT;
using SuccincT.Functional;

namespace socialNetworkApp.config;

public abstract class IEnv
{
    public abstract DbEnv Db{ get; protected set; }
    public abstract BackendEnv Backend { get; protected set; }
}

public  class Env: IEnv
{
    public static Env FirstEnv { get; private set; } = null!;
    public override DbEnv Db { get; protected set; }
    public override BackendEnv Backend { get; protected set; }
    
    public Env(string envFileName = ".env")
    {
        if (FirstEnv == null)
        {
            Db = new DbEnv();
            IEnumerator<string?>[] valuesDb =
            {
                Db.EnvInit(
                        "DB_HOST",
                        "DB_HOST_PORT",
                        "DB_SUPERUSER_NAME",
                        "DB_SUPERUSER_PASSWORD",
                        "DB_DATABASE_NAME"
                        )
                    .GetEnumerator()
            };
            
            Backend = new BackendEnv();
            IEnumerator<string?>[] valuesBackend =
            {
                Backend.EnvInit(
                        "BACKEND_PROTOCOL",
                        "BACKEND_HOST_RUNNABLE", 
                        "BACKEND_PORT_INTERNAL",
                        "BACKEND_AUTH_SECRET_KEY"
                        )
                    .GetEnumerator()
            };
            
            _ = valuesDb.Select(x => x.MoveNext()).ToList();
            _ = valuesBackend.Select(x => x.MoveNext()).ToList();
            LoadEnv(envFileName);
            _ = valuesDb.Select(x => x.MoveNext()).ToList();
            _ = valuesBackend.Select(x => x.MoveNext()).ToList();

            FirstEnv = this;
        }
        else
        {
            Db = FirstEnv.Db;
            Backend = FirstEnv.Backend;
        }
    }

    protected static void LoadEnv(string envFileName)
    {
        string? baseDirectory;
        if (System.Environment.GetEnvironmentVariable("FROM_DOCKER") == "true")
        {
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
        else
        {
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(baseDirectory);
            while ((baseDirectory = Path.GetDirectoryName(baseDirectory))?.EndsWith("socialNetwork_backend") == false)
            {
                Console.WriteLine(baseDirectory);
            }
        }

        if (baseDirectory == null)
        {
            throw new Exception(
                $"You can create a socialNetwork_backend/.env file." +
                $" For example, see https://github.com/DaniinXorchenabo/social_network_backend/blob/master/socialNetwork_backend/example.env");
        }
        Console.WriteLine(Path.Combine(baseDirectory!, envFileName));
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {Path.Combine(baseDirectory!, envFileName)}));
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

public record class BackendEnv : BaseLoader
{
    public string BackendProtocol { get; protected set; } = null!;
    public string BackendHostRunnable { get; protected set; } = null!;
    public string BackendPortInternal { get; protected set; } = null!;
    public string SecretKey { get; protected set; } = null!;
    
    public string Address => $"{BackendProtocol}://{BackendHostRunnable}:{BackendPortInternal}";

    public IEnumerable<string?> EnvInit(
        string backendProtocol,
        string backendHostRunnable,
        string backendPortInternal,
        string secretKey,
        params string[] data)
    {
        var enumerators = (this as IBaseLoader).PreloadEnv(backendProtocol, backendHostRunnable, backendPortInternal, secretKey);
        yield return null;
        (this.BackendProtocol,
            (this.BackendHostRunnable,
                (this.BackendPortInternal,(
                    this.SecretKey ,_)))) = enumerators
            .Where(x => x.MoveNext() || true)
            .Select(x => x.Current ?? "")
            .ToList();
        yield return null;
    }

    public void Deconstruct(out string BackendProtocol, out string BackendHostRunnable, out string BackendPortInternal , out string SecretKey)
    {
        BackendProtocol = this.BackendProtocol;
        BackendHostRunnable = this.BackendHostRunnable;
        BackendPortInternal = this.BackendPortInternal;
        SecretKey = this.SecretKey;
    }

    public override IEnumerable<string?> EnvInit(params string[] data)
    {
        return EnvInit(data[0], data[1], data[2], data[3]);
    }
}