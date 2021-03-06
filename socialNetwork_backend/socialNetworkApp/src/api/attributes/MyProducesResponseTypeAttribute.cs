using System.Drawing;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.error;

namespace Microsoft.AspNetCore.Mvc
{
    public class MyProducesResponseTypeAttribute : ProducesResponseTypeAttribute
    {
        public static Dictionary<Type, Type> ValidateErrorClasses { get; set; } = new Dictionary<Type, Type>() { };
        public static Dictionary<Type, Type> ValidateErrorClassEnums { get; set; } = new Dictionary<Type, Type>() { };

        public MyProducesResponseTypeAttribute(int statusCode) : base(statusCode)
        {
        }

        public MyProducesResponseTypeAttribute(Type type, int statusCode) : base(type, statusCode)
        {
            if (type.IsSubclassOf(typeof(BaseResponse)))
            {
                if (ValidateErrorClasses.ContainsKey(type))
                {
                    Type = ValidateErrorClasses[type];
                }
                else
                {
                    var validateType =
                        type.GenericTypeArguments.FirstOrDefault(x => x.IsSubclassOf(typeof(AbstractDto)));
                    if (validateType != null)
                    {
                        Int32 fieldOffset = 0;

                        // get the public fields from the source object
                        FieldInfo[] sourceFields = validateType.GetFields();

                        // get a dynamic TypeBuilder and inherit from the base type

                        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                            new AssemblyName(Guid.NewGuid().ToString()),
                            AssemblyBuilderAccess.Run);
                        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MyDynamicModule");

                        TypeBuilder typeBuilder = moduleBuilder.DefineType(
                            $"{validateType.Name}ValidateError{statusCode}",
                            TypeAttributes.Public
                            | TypeAttributes.Class
                            | TypeAttributes.AutoClass
                            | TypeAttributes.AnsiClass
                        );
                        var newEnumType = GenerateEnum(
                            $"{typeBuilder.Name}Enum",
                            sourceFields
                                .Where(x => x.IsPublic && !x.IsStatic)
                                .Select(x => x.Name)
                                .ToArray()
                        );
                        ValidateErrorClassEnums[type] = newEnumType;

                        var parentWithGeneric = typeof(ValidateError<>).MakeGenericType(newEnumType);
                        typeBuilder.SetParent(parentWithGeneric);

                        Type dynamicType = typeBuilder.CreateType();

                        Type[] GetAllGenerics(Type currentType)
                        {
                            if (currentType.GenericTypeArguments.Length < 3)
                            {
                                return GetAllGenerics(currentType.BaseType);
                            }

                            return currentType.GenericTypeArguments;
                        }


                        var newRespType = typeof(BaseResponse<,,>);
                        var generics = GetAllGenerics(type);
                        newRespType = newRespType.MakeGenericType(generics[0], generics[1], dynamicType!);

                        ValidateErrorClasses[type] = newRespType;
                        Type = newRespType ?? type ?? throw new ArgumentNullException(nameof(dynamicType));

                        object destObject = Activator.CreateInstance(newRespType);
                        Console.WriteLine("");
                    }
                }
            }
        }

        public MyProducesResponseTypeAttribute(Type type, int statusCode, string contentType,
            params string[] additionalContentTypes) : base(type, statusCode, contentType, additionalContentTypes)
        {
        }

        public Type GenerateEnum(string enumName, string[] fieldNames)
        {
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName(Guid.NewGuid().ToString().Replace("_", "") + ".dll"),
                AssemblyBuilderAccess.Run);

            // Define a dynamic module in "MyEnums" assembly.
            // For a single-module assembly, the module has the same name as the assembly.
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MyDynamicModule");

            // Define a public enumeration with the name "MyEnum" and an underlying type of Integer.
            EnumBuilder myEnum = moduleBuilder.DefineEnum($"EnumeratedTypes.{enumName}",
                TypeAttributes.Public, typeof(int));

            // Get data from database

            int counter = 1;
            foreach (var name in fieldNames)
            {
                myEnum.DefineLiteral(name, counter);
                counter++;
            }

            // Create the enum
            myEnum.CreateType();
            // Finally, save the assembly
            // assemblyBuilder

            // Finally, save the assembly
            return myEnum;
        }
    }
}