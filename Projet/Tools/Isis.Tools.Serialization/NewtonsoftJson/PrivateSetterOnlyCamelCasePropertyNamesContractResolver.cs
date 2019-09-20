using System;
using System.Reflection;
using Isis.Tools.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
//using JsonNet.ContractResolvers.Internal;

namespace Isis.Tools.Serialization.NewtonsoftJson
{
    public class PrivateSetterOnlyCamelCasePropertyNamesContractResolver : DefaultContractResolver
    {

        //protected override JsonObjectContract CreateObjectContract(Type objectType)
        //    => base.CreateObjectContract(objectType).SupportPrivateCTors(objectType, CreateConstructorParameters);

        //protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        //{
        //    //var jProperty = base.CreateProperty(member, memberSerialization);
        //    //if (jProperty.Writable)
        //    //    return jProperty;


        //    if (member.GetType().IsNotPublic)
        //        jProperty.Writable = true;
        //    //jProperty.Writable = member.IsPropertyWithSetter();

        //    return jProperty;
        //}
    }
}
