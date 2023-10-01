using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace BAL.Common
{
    public static class EntityToXML
    {
        public static string ToXML<T>(this List<T> list, string rootName) where T : class
        {
            if (null == list) return null;
            var xmlBuilder = new StringBuilder();
            xmlBuilder.AppendLine($"<{rootName}>");
            foreach (var obj in list)
            {
                var clsName = obj.GetType().Name;
                xmlBuilder.AppendLine($"<{clsName}>");
                foreach (
                    var e in
                        obj.GetType().GetProperties().Select(pInfo => new XElement(pInfo.Name, pInfo.GetValue(obj))))
                {
                    xmlBuilder.AppendLine($"{e}");
                }
                xmlBuilder.AppendLine($"</{clsName}>");
            }
            xmlBuilder.AppendLine($"</{rootName}>");
            return xmlBuilder.ToString();
        }
        public static string ObjectToXml<T>(this T obj) where T : class
        {
            if (null == obj) return null;
            var xmlBuilder = new StringBuilder();
            var clsName = obj.GetType().Name;
            xmlBuilder.AppendLine($"<{clsName}>");
            foreach (
                var e in obj.GetType().GetProperties().Select(pInfo => new XElement(pInfo.Name, pInfo.GetValue(obj))))
            {
                xmlBuilder.AppendLine($"{e}");
            }
            xmlBuilder.AppendLine($"</{clsName}>");
            return xmlBuilder.ToString();
        }
    }
}