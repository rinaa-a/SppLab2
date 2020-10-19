using Lab2Spp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lab4Spp
{
    public class Faker
    {
        private Stack<Type> progressStack = new Stack<Type>();
        private Generator generator = new Generator();
        public object Generate(Type type)
        {
            if (generator.CanGenerate(type))
            {
                return generator.Generate(type);
            }

            if (isDTO(type) && !progressStack.Contains(type))
            {
                MethodInfo create = typeof(Faker).GetMethod("Create").MakeGenericMethod(type);
                return create.Invoke(this, null);
            }
            if (isDTO(type) && progressStack.Contains(type))
            {
                return null;
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var listType = type.GetGenericTypeDefinition();
                    var genericType = type.GetGenericArguments()[0];
                    var constructedList = listType.MakeGenericType(genericType);
                    var random = new Random();
                    byte length = (byte)random.Next(1, 16);
                    object[] parameters = { length };
                    var instance = Activator.CreateInstance(constructedList, parameters);
                    for (int i = 0; i < length; i++)
                    {
                        instance.GetType().GetMethod("Add")
                            .Invoke(instance, new[] { Generate(genericType) });
                    }

                    return instance;
                }
            }

            return null;
        }
        private bool isDTO(Type type)
        {
            return type.GetCustomAttribute(typeof(DTOAttribute), false) != null;
        }
        public T Create<T>()
        {
            Type type = typeof(T);

            if (!isDTO(type))
            {
                return default(T);
            }

            progressStack.Push(type);

            Type[] fieldsTypes = type.GetFields().Select(field => field.GetType()).ToArray();
            ConstructorInfo constructor = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, fieldsTypes, null)
                ?? type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)[0];

            ParameterInfo[] parameters = constructor.GetParameters();
            List<object> paramList = new List<object>(parameters.Length);
            parameters.ToList().Select(param => param.GetType()).ToList().ForEach(type => paramList.Add(Generate(type)));
            var obj = constructor.Invoke(paramList.ToArray());
            obj.GetType().GetFields().ToList().ForEach(field => field.SetValue(obj, Generate(field.FieldType)));
            obj.GetType().GetProperties().ToList().ForEach(property => property.SetValue(obj, Generate(property.PropertyType)));

            progressStack.Pop();
            return (T) obj;
        }
    }
}
