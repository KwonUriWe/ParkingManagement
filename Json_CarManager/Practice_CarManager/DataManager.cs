using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Practice_CarManager
{
    class DataManager
    {
        public static List<Car> Cars = new List<Car>();

        static DataManager()
        {
            Load();
        }

        public static void Load()   //json요소들 채우기
        {
            try
            {
                string CarsOutput = File.ReadAllText(@"./Cars.json");
                JObject carsXElement = JObject.Parse(CarsOutput);
                Cars = (from item in carsXElement["cars"]["car"]
                        select new Car()
                         {
                             parkingSpot = item["parkingSpot"].ToString(),
                             carNumber = item["carNumber"].ToString(),
                             driverName = item["driverName"].ToString(),
                             phoneNumber = item["phoneNumber"].ToString(),
                             parkingTime = DateTime.Parse(item["parkingTime"].ToString())
                         }).ToList<Car>();
            }
            catch (Exception e)
            {
                using (StreamWriter writer = new StreamWriter(@"./Cars.json"))
                {
                    writer.WriteLine("{");
                    writer.WriteLine("  \"cars\": {");
                    writer.WriteLine("    \"car\": [");

                    for (int i=1; i < 6; i++)
                    {
                        writer.WriteLine("      {");
                        writer.WriteLine("        \"parkingSpot\": \""+i+"\",");
                        writer.WriteLine("        \"carNumber\": \"\",");
                        writer.WriteLine("        \"driverName\": \"\",");
                        writer.WriteLine("        \"phoneNumber\": \"\",");
                        writer.WriteLine("        \"parkingTime\": \"0001년 1월 1일 월요일\"");
                        writer.WriteLine("      },");
                    }

                    writer.WriteLine("    ]");
                    writer.WriteLine("  }");
                    writer.WriteLine("}");
                }
                Load();
            }
        }

        public static void Save()       //json파일에 저장시킬 내용(노드들)
        {
            var jCarArray = new JArray();
            foreach (var item in Cars)
            {
                var jCarObject = new JObject();    //노드들 채우고
                jCarObject.Add("parkingSpot", item.parkingSpot);
                jCarObject.Add("carNumber", item.carNumber);
                jCarObject.Add("driverName", item.driverName);
                jCarObject.Add("phoneNumber", item.phoneNumber);
                jCarObject.Add("parkingTime", item.parkingTime.ToLongDateString());
                jCarArray.Add(jCarObject);    //미리 생성해 둔 배열에 노드 넣고
            }

            var jCarArrayObject = new JObject();   //book 오브젝트에 배열 넣고
            jCarArrayObject.Add("car", jCarArray);

            var jCarsArrayObject = new JObject();   //books 오브젝트에 book 넣고
            jCarsArrayObject.Add("cars", jCarArrayObject);

            File.WriteAllText(@"./Cars.json", jCarsArrayObject.ToString());
        }
    }
}
