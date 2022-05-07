using System.Collections;
using Microsoft.OpenApi.Extensions;

namespace socialNetworkApp.api.controllers.modifiersOfAccess;

public class Mod
{
    public string Name { get; private set; } = null!;

    public Mod(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
    public static ModList operator +(Mod mod1, Mod mod2)
    {
        return new ModList() {mod1, mod2};
    }
}

public static class AllMods
{
    public static readonly Mod msgReader = new Mod("msgReader");
    public static readonly Mod msgCreator = new Mod("msgCreator");
    public static readonly Mod msgUpdater = new Mod("msgUpdater");
    public static readonly Mod msgDeleter = new Mod("msgDeleter");

    public static readonly Mod chatReader = new Mod("chatReader");
    public static readonly Mod chatCreator = new Mod("chatCreator");
    public static readonly Mod chatUpdater = new Mod("chatUpdater");
    public static readonly Mod chatDeleter = new Mod("chatDeleter");

    public static readonly Mod userReader = new Mod("userReader");
    public static readonly Mod userCreator = new Mod("userCreator");
    public static readonly Mod userUpdater = new Mod("userUpdater");
    public static readonly Mod userDeleter = new Mod("userDeleter");

    public static readonly Mod postReader = new Mod("postReader");
    public static readonly Mod postCreator = new Mod("postCreator");
    public static readonly Mod postUpdater = new Mod("postUpdater");
    public static readonly Mod postDeleter = new Mod("postDeleter");

    private static readonly IEnumerable<Mod> _AllNods;

    static AllMods()
    {
        _AllNods = typeof(AllMods)
            .GetProperties()
            .Where(x => x.GetMethod != null && x.GetMethod.IsPublic && x.GetMethod.IsStatic &&
                        x.PropertyType == typeof(Mod))
            .Select(x => x.GetConstantValue() as Mod);
    }

    public static IEnumerable<Mod> GetIEnumerable()
    {
        return _AllNods;
    }

    public static IEnumerator<Mod> GetIEnumerator()
    {
        return _AllNods.GetEnumerator();
    }
}

public enum AllModsEnum
{
    msgReader, msgCreator, msgUpdater, msgDeleter,
    chatReader, chatCreator, chatUpdater, chatDeleter,
    userReader, userCreator, userUpdater, userDeleter,
    postReader, postCreator, postUpdater, postDeleter
    
     // public static ModList operator +(AllModsEnum mod1, AllModsEnum mod2)
     // {
     // return new ModList() {mod1, mod2};
     //  }
}


public class ModList: SortedSet<Mod>
{
    public static ModList operator +(ModList list, Mod mod)
    {
        list.Add(mod);
        return list;
    }
    
    public static ModList operator +( Mod mod, ModList list)
    {
        list.Add(mod);
        return list;
    }
    
    public static ModList operator +( ModList list1, ModList list2)
    {
        list1.UnionWith(list2);
        return list1;
    }

    public override string ToString()
    {
        string? result = "";
        foreach (var mod in this)
        {
            if (result != "")
            {
                result += ",";
            }

            result += mod.Name;
        }

        return result;
    }
}

public class EnumModList: SortedSet<AllModsEnum>
{
    
    public EnumModList(AllModsEnum[] data): base()
        {
            foreach (var item in data)
            {
                this.Add(item);
            }
        }

    public EnumModList(): base()
    {
        
    }
    
    public static EnumModList operator +(EnumModList list, AllModsEnum mod)
    {
        list.Add(mod);
        return list;
    }
    
    public static EnumModList operator +( AllModsEnum mod, EnumModList list)
    {
        list.Add(mod);
        return list;
    }
    
    public static EnumModList operator +( EnumModList list1, EnumModList list2)
    {
        list1.UnionWith(list2);
        return list1;
    }

    public override string ToString()
    {
        string? result = "";
        foreach (var mod in this)
        {
            if (result != "")
            {
                result += ",";
            }

            result += mod;
        }

        return result;
    }
}