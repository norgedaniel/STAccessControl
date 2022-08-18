using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace STCA_ServiceLayer
{
    /// <summary>
    /// This attribute is used to represent a string value for a value in an enum.
    /// </summary>
    public class StringValueAttribute : Attribute
    {

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

    }

    /// <summary>
    /// Class for Enum extension methods
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Will get the string value for a given enums value, 
        /// this will only work if you assign the StringValue attribute to the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ? GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString()) ?? throw new ArgumentOutOfRangeException(nameof(type), type, null);

            if (fieldInfo == null)
                return String.Empty;

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[] ?? throw new ArgumentOutOfRangeException(nameof(fieldInfo), fieldInfo, null);

            if (attribs == null)
                return null;

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;

        }

    }


}
