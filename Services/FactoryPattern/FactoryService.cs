using System.Collections.Generic;

namespace Service
{
  public class FactoryService
  {
    private static Dictionary<string, IService> _instance = new Dictionary<string, IService>();

    public static T getInstance<T>() where T : new()
    {
      IService instance;
      bool exist = _instance.TryGetValue(typeof(T).FullName, out instance);

      if (!exist)
      {
        instance = (IService)new T();
        _instance.Add(typeof(T).FullName, instance);
      }

      return (T)instance;
    }
  }
}