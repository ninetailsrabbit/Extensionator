using System.Reflection;

namespace Extensionator {

    /// <summary>
    /// Example of use: public sealed class Preloader : SingletonBase&lt;Preloader&gt;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonBase<T> where T : class {
        static SingletonBase() { }

        public static readonly T Instance =
            typeof(T).InvokeMember(typeof(T).Name,
                        BindingFlags.CreateInstance |
                        BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.NonPublic,
                        null, null, null) as T;
    }

}