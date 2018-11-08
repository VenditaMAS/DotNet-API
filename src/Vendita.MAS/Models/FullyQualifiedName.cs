using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Vendita.MAS.Models
{
    [JsonConverter(typeof(FullyQualifiedNameJsonConverter))]
    public sealed class FullyQualifiedName: IEnumerable<Name>
    {
        private readonly Name[] names;

        public FullyQualifiedName(string fullyQualifiedName)
        {
            names = fullyQualifiedName.Split('.').Select(s => new Name(s)).ToArray();
        }

        public FullyQualifiedName(params Name[] names)
        {
            if (names.Length == 0) throw new ArgumentException("At least one Name instance is required.", "names");
            this.names = names;
        }

        public FullyQualifiedName(IEnumerable<Name> names)
            : this(names.ToArray())
        {

        }

        public Name Name => names.Last();
        public FullyQualifiedName Namespace => names.Length > 1 ? new FullyQualifiedName(names.Take(names.Length - 1)) : null;
        public override string ToString() => String.Join(".", (IEnumerable<Name>)names);
        IEnumerator IEnumerable.GetEnumerator() => names.GetEnumerator();
        public IEnumerator<Name> GetEnumerator() => names.AsEnumerable().GetEnumerator();

        public static implicit operator FullyQualifiedName(string fullyQualifiedName)
        {
            return new FullyQualifiedName(fullyQualifiedName);
        }

        public static implicit operator string(FullyQualifiedName fullyQualifiedName)
        {
            return fullyQualifiedName.ToString();
        }
    }

    public sealed class FullyQualifiedNameJsonConverter : JsonConverter<FullyQualifiedName>
    {
        public override FullyQualifiedName ReadJson(JsonReader reader, Type objectType, FullyQualifiedName existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.String:
                    return JValue.Load(reader).ToObject<string>();
                default:
                    throw new JsonSerializationException();
            }
        }

        public override void WriteJson(JsonWriter writer, FullyQualifiedName value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }

    public sealed class Name
    {
        private static readonly Regex validationRegex = new Regex(@"^\w+$");
        private readonly string name;
        public Name(string name)
        {
            if (String.IsNullOrWhiteSpace(name) || name.Contains(".") || !validationRegex.IsMatch(name))
            {
                throw new ArgumentException($"'{name}' is not a valid Vendita.MAS.Name.", "name");
            }
            this.name = name;
        }

        public override string ToString() => name;
        
        public static implicit operator Name(string name)
        {
            return new Name(name);
        }

        public static implicit operator string(Name name)
        {
            return name.ToString();
        }
    }
}