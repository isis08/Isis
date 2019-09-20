using System.Reflection;
using Isis.Tools.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Isis.Tools.Serialization.NewtonsoftJson
{
    public class PrivateSetterCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
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
