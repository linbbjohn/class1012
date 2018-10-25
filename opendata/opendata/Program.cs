using opendata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace opendata
{
    class Program
    {


        static void Main(string[] args)
        {
            var nodes = findopendata();
            showopendata(nodes);
            Console.ReadKey();
        }

        private static void showopendata(List<Class1> nodes)
        {
            Console.WriteLine(string.Format("共收到{0}筆的資料", nodes.Count));
            nodes.GroupBy(node => node.服務分類).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupDatas = group.ToList();
                    var message = $"服務分類:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);

                });
        }

        static List<opendata.Models.Class1> findopendata()
        {
            List<opendata.Models.Class1> result = new List<Class1>();
            var xml = XElement.Load(@"C:\Users\xxz31\source\repos\class1005\opendata\datagovtw_dataset_20181006.xml");

            var nodes = xml.Descendants("node").ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                opendata.Models.Class1 item = new opendata.Models.Class1();
                item.id = int.Parse(getvalue(node, "id"));
                item.服務分類 = getvalue(node, "服務分類");
                item.資料集名稱 = getvalue(node, "資料集名稱");
                result.Add(item);
            }
            return result;
        }

        private static string getvalue(XElement node, string v)
        {
            return node.Element(v)?.Value?.Trim();
        }

    }
}