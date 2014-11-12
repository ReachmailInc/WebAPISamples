using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Reachmail
{
    internal class JsonConverter : JavaScriptConverter
    {
        private readonly Dictionary<Type, Func<object, string>> _converters = 
            new Dictionary<Type, Func<object, string>>();

        public JsonConverter Add<T>(Func<T, string> converter)
        {
            if (typeof (T) == typeof (Enum))
                Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsEnum).ToList().ForEach(x => AddConverter(x, converter));
            else AddConverter(typeof(T), converter);
            return this;
        }

        private void AddConverter<T>(Type type, Func<T, string> converter)
        {
            _converters[type] = x => converter((T)x);
            if (type.IsValueType) _converters[typeof(Nullable<>).MakeGenericType(type)] = x => x != null ? converter((T)x) : null;
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return new JavaScriptSerializer().ConvertToType(dictionary, type);
        }

        public override IDictionary<string, object> Serialize(object value, JavaScriptSerializer serializer)
        {
            return _converters.ContainsKey(value.GetType()) ? new CustomString(_converters[value.GetType()](value)) : null;
        }

        public override IEnumerable<Type> SupportedTypes { get { return _converters.Keys; } }

        private class CustomString : Uri, IDictionary<string, object>
        {
            public CustomString(string str) : base(str, UriKind.Relative) { }

            void IDictionary<string, object>.Add(string key, object value) { throw new NotImplementedException(); }
            bool IDictionary<string, object>.ContainsKey(string key) { throw new NotImplementedException(); }
            ICollection<string> IDictionary<string, object>.Keys { get { throw new NotImplementedException(); } }
            bool IDictionary<string, object>.Remove(string key) { throw new NotImplementedException(); }
            bool IDictionary<string, object>.TryGetValue(string key, out object value) { throw new NotImplementedException(); }
            ICollection<object> IDictionary<string, object>.Values { get { throw new NotImplementedException(); } }
            object IDictionary<string, object>.this[string key] { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
            void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item) { throw new NotImplementedException(); }
            void ICollection<KeyValuePair<string, object>>.Clear() { throw new NotImplementedException(); }
            bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item) { throw new NotImplementedException(); }
            void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) { throw new NotImplementedException(); }
            int ICollection<KeyValuePair<string, object>>.Count { get { throw new NotImplementedException(); } }
            bool ICollection<KeyValuePair<string, object>>.IsReadOnly { get { throw new NotImplementedException(); } }
            bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item) { throw new NotImplementedException(); }
            IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() { throw new NotImplementedException(); }
            IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
        } 
    }
}