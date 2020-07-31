using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FakeCMS.DAL
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StatefullEntityAttribute : Attribute
    {
        public string TableName { get; set; }

        /// <summary>
        /// Title template - substring "[[Prop]]" will be replace with entity prop mean
        /// </summary>
        public string TitleTemplate { get; set; }

        /// <summary>
        /// Desc template - substring "[[Prop]]" will be replace with entity prop mean
        /// </summary>
        public string DescriptionTemplate { get; set; }
    }

    public class StatefullManager
    {
        private readonly object _object;
        private readonly Type _objectType;
        private readonly StatefullEntityAttribute _statefullAttr;

        public StatefullManager(object obj)
        {
            _object = obj;
            _objectType = _object.GetType();
            _statefullAttr = _objectType.GetCustomAttributes(typeof(StatefullEntityAttribute), inherit: false)
                .First() as StatefullEntityAttribute;
        }

        public string GetTitle()
        {
            return ParseTemplate(_statefullAttr.TitleTemplate);
        }

        public string GetDescription()
        {
            return ParseTemplate(_statefullAttr.DescriptionTemplate);
        }

        private string ParseTemplate(string template)
        {
            var strBuilder = new StringBuilder(template);
            var removeTemplate = @"\[\[\w+\]\]";
            foreach (Match match in Regex.Matches(template, removeTemplate))
            {
                var propName = match.Value.Trim().TrimStart('[').TrimEnd(']');
                var propVal = _objectType.GetProperty(propName).GetValue(_object)
                    .ToString();
                strBuilder.Replace(match.Value, propVal);
            }
            return strBuilder.ToString();
        }
    }
}
