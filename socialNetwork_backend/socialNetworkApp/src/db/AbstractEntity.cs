using System.Collections;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.api.dtos;

namespace socialNetworkApp.db;

public abstract class AbstractEntity
{
    public AbstractEntity(object obj)
    {
        var myStaticData = EntityStaticData.AllClasses[this.GetType()];
        var ObjFields = DtoStaticData.AllClasses[obj.GetType()].PropertiesAsString;
        var moveFields = new HashSet<string>(ObjFields);
        moveFields.IntersectWith(myStaticData.PropertiesAsString);
        
        foreach (var moveField in moveFields)
        {
            var property = this.GetType().GetProperty(moveField);
            if (property != null)
            {
                Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var value = obj.GetType().GetProperty(moveField).GetValue(obj);
                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                this.GetType().GetProperty(moveField).SetValue(this, safeValue, null);
            }
        }
    }

    public AbstractEntity()
    {
    }

    public virtual async Task OnStart(BaseBdConnection db)
    {
    }

    public override string ToString()
    {
        return string.Join(", ", this.GetType()
            .GetProperties()
            .Select(x => x.GetValue(this))
            .Where(x => x != null)
            .Select(x => x?.ToString()));
    }

    public AbstractEntity Update(object obj)
    {
        var myStaticData = EntityStaticData.AllClasses[this.GetType()];
        var ObjFields = DtoStaticData.AllClasses[obj.GetType()].PropertiesAsString;
        var moveFields = new HashSet<string>(ObjFields);
        moveFields.IntersectWith(myStaticData.PropertiesAsString);
        
        foreach (var moveField in moveFields)
        {
            var property = this.GetType().GetProperty(moveField);
            if (property != null)
            {
                Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var value = obj.GetType().GetProperty(moveField)?.GetValue(obj);
                object? safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                if (safeValue != null)
                {
                    this.GetType().GetProperty(moveField)?.SetValue(this, safeValue, null);
                }
            }
        }

        return this;
    }

    public AbstractEntity Update<TEntity>(object obj) where TEntity: AbstractEntity
    {
        return (TEntity) this.Update(obj);
    }
    
    // public st
}

public class EntityStaticData
{
    public static Dictionary<Type, EntityStaticData> AllClasses = new Dictionary<Type, EntityStaticData>() { };

    static EntityStaticData()
    {
        RecursionInitObjects(typeof(AbstractEntity));
    }

    static void RecursionInitObjects(Type cls)
    {
        var newObj = new EntityStaticData(cls);
        AllClasses[cls] = newObj;
        IEnumerable<Type> list = Assembly.GetAssembly(cls).GetTypes()
            .Where(type => type.IsSubclassOf(cls)); // using System.Linq

        foreach (var itm in list)
        {
            RecursionInitObjects(itm);
        }
    }


    public HashSet<string> PropertiesAsString { get; set; } = new HashSet<string>();

    public EntityStaticData(Type initType)
    {
        _ = initType.GetProperties()
            .Where(x => x.PropertyType.IsPublic)
            .Select(x => PropertiesAsString.Add(x.Name)).ToList();
    }
}


public class QBuilder
{
    public static Task<List<TModelType>> Select<TModelType>(
        IQueryable<TModelType> query,
        Pagination pagination,
        Func<IQueryable<TModelType>, IQueryable<TModelType>> sort
        )
    {
        return sort(query).Skip(pagination.Offset).Take(pagination.Limit).ToListAsync();
    }
    
}