using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Reflection;

namespace Laba1_Example_
{
    public abstract class FileCreator
    {
        public abstract byte[] FileSave(List<CatalogItem> catalog);
        public abstract List<CatalogItem> FileOpen(byte[] data);
    }
    public class BinFileCreator : FileCreator
    {
        public override byte[] FileSave(List<CatalogItem> catalog)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, catalog);
            return ms.ToArray();
        }
        public override List<CatalogItem> FileOpen(byte[] data)
        {
            List<CatalogItem> catalog;
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(data);
            catalog = (List<CatalogItem>)formatter.Deserialize(ms);
            return catalog;
        }
    }

    public class JsonFileCreator : FileCreator
    {
        public override byte[] FileSave(List<CatalogItem> catalog)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string Serialized = JsonConvert.SerializeObject(catalog, settings);
            byte[] ret = System.Text.Encoding.UTF8.GetBytes(Serialized);
            return ret;
        }
        public override List<CatalogItem> FileOpen(byte[] data)
        {
            string Serialized = Encoding.UTF8.GetString(data);
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            List<CatalogItem> catalog = JsonConvert.DeserializeObject<List<CatalogItem>>(Serialized, settings);
            return catalog;
        }
    }

    public class TextFileCreator : FileCreator
    {
        public override byte[] FileSave(List<CatalogItem> catalog)
        {
            string Serialized = null;
            foreach (CatalogItem item in catalog)
            {
                MakeClass(ref Serialized, item);
                Serialized += "};";
            }
            Serialized += "$";
            byte[] ret = System.Text.Encoding.UTF8.GetBytes(Serialized);
            return ret;
        }

        public override List<CatalogItem> FileOpen(byte[] data)
        {
            string Serialized = Encoding.UTF8.GetString(data);
            List<CatalogItem> Localcatalog = new List<CatalogItem>();
            while (Serialized[0] != '$')
            {
                Localcatalog.Add(ParseSerialized(ref Serialized));
            }
            return Localcatalog;
        }


        private static void MakeClass(ref string str, object obj)
        {
            CatalogItem item = (CatalogItem)obj;
            Type type = Type.GetType("Laba1_Example_." + item.Category.ToString(), false, true);
            str += type.ToString() + "{";
            int i = 0;
            foreach (FieldInfo param in type.GetFields())
            {
                if (param.FieldType.ToString().IndexOf("Laba1_Example_.") > -1 && param.FieldType.ToString().IndexOf("+") <= -1)
                {
                    MakeClass(ref str, param.GetValue(item));
                    str += "},";
                }
                else
                {
                    str += param.FieldType.ToString() + ":" + param.GetValue(item) + ",";
                }
            }
        }
        private static Type FieldType(ref string str, int ind)
        {
            Type objtype = Type.GetType(str.Substring(0, ind), false, true);
            str = str.Remove(0, ind + 1);
            return objtype;
        }

        private static string GetStringValue(ref string str)
        {
            int ind = str.IndexOf(',');
            string s = str.Substring(0, ind);
            str = str.Remove(0, ind + 1);
            return s;
        }

        private static CatalogItem ParseSerialized(ref string str)
        {
            object cat = null;
            int index0 = str.IndexOf('{');
            Type objtype = FieldType(ref str, index0);
            FieldInfo[] fields = objtype.GetFields();
            Type[] types = new Type[fields.Length - 1];
            object[] values = new object[fields.Length - 1];
            int i = 0;
            foreach (var param in fields)
            {
                int index1 = str.IndexOf(':');
                int index2 = str.IndexOf('{');
                if (index1 < index2 || index2 == -1)
                {
                    types[i] = FieldType(ref str, index1);
                    string value_str = GetStringValue(ref str);
                    if (types[i].Name == "Int32")
                        values[i] = Convert.ToInt32(value_str);
                    else if (types[i].Name == "String")
                        values[i] = value_str;
                    else if (types[i].Name == "Double")
                        values[i] = Convert.ToDouble(value_str);
                    else if (types[i].Name == "Boolean")
                        values[i] = Convert.ToBoolean(value_str);
                    else
                    {
                        if (types[i].Name != "TCategorys")
                            values[i] = Enum.Parse(types[i], value_str);
                        else
                        {
                            cat = Enum.Parse(types[i], value_str);
                            i--;
                        }
                    }
                }
                else
                {
                    values[i] = ParseSerialized(ref str);
                }
                i++;
            }
            str = str.Remove(0, 2);
            object obj = Activator.CreateInstance(objtype, values);
            CatalogItem Item = (CatalogItem)obj;
            Item.Category = (CatalogItem.TCategorys)cat;
            return Item;
        }
    }
}
