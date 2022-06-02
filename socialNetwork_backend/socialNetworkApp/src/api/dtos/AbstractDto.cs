using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;
using socialNetworkApp.db;
using SuccincT.PatternMatchers;

namespace socialNetworkApp.api.dtos;

public class AbstractDto : EmptyAnswer
{
    public AbstractDto(object obj)
    {
        var myStaticData = DtoStaticData.AllClasses[this.GetType()];
        var ObjFields = EntityStaticData.AllClasses[obj.GetType()].PropertiesAsString;
        var moveFields = new HashSet<string>(ObjFields);
        moveFields.IntersectWith(myStaticData.PropertiesAsString);

        Console.WriteLine($"-------------- {this.GetType().Name}");
        Console.WriteLine(string.Join(", ", ObjFields));
        Console.WriteLine(string.Join(", ", myStaticData.PropertiesAsString));
        Console.WriteLine(string.Join(", ", moveFields));
        Console.WriteLine("--------------");

        RecursionCreateObject(obj, moveFields, ObjFields, myStaticData);
    }


    public void RecursionCreateObject(object obj, HashSet<string> moveFields, HashSet<string> ObjFields, DtoStaticData myStaticData)
    {



        foreach (var moveField in moveFields)
        {
            var property = this.GetType().GetProperty(moveField);
            if (property != null)
            {
                Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var value = obj.GetType().GetProperty(moveField)?.GetValue(obj);
                var safeValue = ConvertFieldType(value, t);

               

                property.SetValue(this, safeValue, null);
            }
        }
    }

    public object? ConvertFieldType(dynamic? value, Type t)
    {
        object safeValue = null;
        if (value == null)
        {
            safeValue = value;
        }
        else if (value.GetType() == t)
        {
            safeValue = value;
        } 
        else if (value.GetType().IsGenericType & value is IEnumerable )
        {
            dynamic safeValueEnumerable = Activator.CreateInstance(t);

            foreach (var o in value)
            {
                safeValueEnumerable.Add(ConvertFieldType(o, t.GenericTypeArguments[0]));
            }

            safeValue = safeValueEnumerable;
        }
        else if (value is AbstractEntity)
        {
            safeValue = Activator.CreateInstance(t, value);
        }
        else
        {
            safeValue = (value == null) ? null : Convert.ChangeType(value, t);
        }

        return safeValue;
    }

    public AbstractDto()
    {
    }

    public override string ToString()
    {
        return string.Join(", ", this.GetType()
            .GetProperties()
            .Select(x => x.GetValue(this))
            .Where(x => x != null)
            .Select(x => x.ToString()));
    }
}

[AddAnswerType(AnswerType.UserAnswer)]
public class Pagination : AbstractDto
{
    [Required]
    [Range(1, 1000)]
    [Display(Name = "limit")]
    public int Limit { get; set; } = 100;

    [Required]
    [Range(0.0, long.MaxValue)]
    [Display(Name = "offset")]
    public int Offset { get; set; } = 0;

    public Pagination(object obj) : base(obj)
    {
    }

    public Pagination()
    {
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
            .Where(type => type.IsSubclassOf(cls));

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