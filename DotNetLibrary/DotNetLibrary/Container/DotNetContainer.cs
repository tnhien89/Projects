using System;
using System.Collections.Generic;
using System.Reflection;

namespace DotnetLibrary.Container
{
    public class DotNetContainer
    {
        private static readonly Dictionary<string, object> _services = new Dictionary<string, object>();

        public static void Register<T>(T obj)
        {
            _services[typeof(T).ToString()] = obj;
        }

        public static void Register<T>(T obj, string name)
        {
            _services[typeof(T) + name] = obj;
        }

        public static void Register<TParent, TChild>() where TChild : TParent
        {
            _services[typeof(TParent).ToString()] = Activator.CreateInstance(typeof(TChild));
        }

        public static void Register<TParent, TChild>(string name)
        {
            _services[typeof(TParent) + name] = Activator.CreateInstance(typeof(TChild));
        }

        public static T Resolve<T>()
        {
            object service;
            if (_services.TryGetValue(typeof(T).ToString(), out service))
            {
                return (T)service;
            }

            ConstructorInfo[] ctors = typeof(T).GetConstructors();
            if (ctors.Length > 0)
            {
                foreach (ConstructorInfo ctor in ctors)
                {
                    ParameterInfo[] paramInfos = ctor.GetParameters();
                    if (paramInfos.Length > 0)
                    {
                        List<object> lstParam = new List<object>();
                        foreach (ParameterInfo param in paramInfos)
                        {
                            object obj = null;
                            if (_services.TryGetValue(param.ParameterType.ToString(), out obj))
                            {
                                lstParam.Add(obj);
                            }
                        }

                        if (lstParam.Count == paramInfos.Length)
                        {
                            return (T)Activator.CreateInstance(typeof(T), lstParam.ToArray());
                        }
                    }
                }
            }

            throw new Exception("service not found.");
        }

        public static T Resolve<T>(string name)
        {
            object service;
            if (_services.TryGetValue(typeof(T).ToString() + name, out service))
            {
                return (T)service;
            }

            ConstructorInfo[] ctors = typeof(T).GetConstructors();
            if (ctors.Length > 0)
            {
                foreach (ConstructorInfo ctor in ctors)
                {
                    ParameterInfo[] paramInfos = ctor.GetParameters();
                    if (paramInfos.Length > 0)
                    {
                        List<object> lstParam = new List<object>();
                        foreach (ParameterInfo param in paramInfos)
                        {
                            object obj = null;
                            if (_services.TryGetValue(param.ParameterType.ToString() + name, out obj))
                            {
                                lstParam.Add(obj);
                            }
                        }

                        if (lstParam.Count == paramInfos.Length)
                        {
                            return (T)Activator.CreateInstance(typeof(T), lstParam.ToArray());
                        }
                    }
                }
            }

            throw new Exception("service not found.");
        }

        public static void Remove<T>()
        {
            _services.Remove(typeof(T).ToString());
        }
    }
}
