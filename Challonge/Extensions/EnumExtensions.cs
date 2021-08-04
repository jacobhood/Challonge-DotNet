using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Challonge.Extensions.Internal
{
    internal static class EnumExtensions
    {
        internal static string GetEnumMemberValue(this Enum e)
        {
            string value = e.ToString();

            object attribute = e.GetType()
                .GetMember(e.ToString())
                .FirstOrDefault()?
                .GetCustomAttributes(typeof(EnumMemberAttribute), true)
                .FirstOrDefault();

            if (attribute != null)
            {
                value = ((EnumMemberAttribute)attribute).Value;
            }

            return value;
        }
    }
}
