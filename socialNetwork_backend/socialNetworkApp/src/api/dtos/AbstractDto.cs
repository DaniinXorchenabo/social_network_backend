using System.Reflection;
using socialNetworkApp.db;

namespace socialNetworkApp.api.dtos;

public class AbstractDto
{

    public override string ToString()
    {
        return string.Join(", ", this.GetType()
            .GetProperties()
            .Select(x => x.GetValue(this))
            .Where(x => x != null)
            .Select(x => x.ToString()));
    }
}

public class DtoStaticData
{
    public static Dictionary<Type, DtoStaticData> AllClasses = new Dictionary<Type, DtoStaticData>() { };

    static DtoStaticData()
    {
        RecursionInitObjects(typeof(AbstractDto));
    }

    static void RecursionInitObjects(Type cls)
    {
        var newObj = new DtoStaticData(cls);
        AllClasses[cls] = newObj;
        IEnumerable<Type> list = Assembly.GetAssembly(cls).GetTypes()
            .Where(type => type.IsSubclassOf(cls)); // using System.Linq
        
        foreach (var itm in list)
        {
            RecursionInitObjects(itm);
        }
    }

    public HashSet<string> PropertiesAsString { get; set; } = new HashSet<string>();
    public DtoStaticData(Type initType)
    {
        _ = initType.GetProperties()
            .Where(x => x.PropertyType.IsPublic)
            .Select(x => PropertiesAsString.Add(x.Name)).ToList();
    }
}