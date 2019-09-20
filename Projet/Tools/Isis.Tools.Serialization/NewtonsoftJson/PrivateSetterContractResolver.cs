using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Isis.Tools.Reflection;

namespace Isis.Tools.Serialization.NewtonsoftJson
{
    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.Writable)
                return jProperty;

            jProperty.Writable = member.IsPropertyWithSetter();

            return jProperty;
        }
    }
}
