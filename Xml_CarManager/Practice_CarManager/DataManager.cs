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

        public static void Load()   //xml요소들 채우기
        {
            try
            {
                string CarsOutput = File.ReadAllText(@"./Cars.xml");
                XElement carsXElement = XElement.Parse(CarsOutput);
                Cars = (from item in carsXElement.Descendants("car")
                         select new Car()
                         {
                             parkingSpot = item.Element("parkingSpot").Value,
                             carNumber = item.Element("carNumber").Value,
                             driverName = item.Element("driverName").Value,
                             phoneNumber = item.Element("phoneNumber").Value,
                             parkingTime = DateTime.Parse(item.Element("parkingTime").Value)
                         }).ToList<Car>();
            }
            catch (Exception)
            {
                using (StreamWriter writer = new StreamWriter(@"./Cars.xml", true))
                {
                    writer.WriteLine("<cars>");

                    for(int i=1; i<6; i++)
                    {
                        writer.WriteLine("<car>");
                        writer.WriteLine("  <parkingSpot>"+i+"</parkingSpot>");
                        writer.WriteLine("  <carNumber></carNumber>");
                        writer.WriteLine("  <driverName></driverName>");
                        writer.WriteLine("  <phoneNumber></phoneNumber>");
                        writer.WriteLine("  <parkingTime>0001년 1월 1일 월요일</parkingTime>");
                        writer.WriteLine("</car>");

                    }

                    writer.WriteLine("</cars>");
                    
                }
                Load();
            }
        }

        public static void Save()       //xml파일에 저장시킬 내용(노드들)
        {
            string CarsOutput = "";
            CarsOutput += "<cars>\n";
            foreach (var item in Cars)
            {
                CarsOutput += "<car>\n";
                CarsOutput += "  <parkingSpot>" + item.parkingSpot + "</parkingSpot>\n";
                CarsOutput += "  <carNumber>" + item.carNumber + "</carNumber>\n";
                CarsOutput += "  <driverName>" + item.driverName + "</driverName>\n";
                CarsOutput += "  <phoneNumber>" + item.phoneNumber + "</phoneNumber>\n";
                CarsOutput += "  <parkingTime>" + item.parkingTime + "</parkingTime>\n";
                CarsOutput += "</car>";
            }
            CarsOutput += "</cars>";

            File.WriteAllText(@"./Cars.xml", CarsOutput);
        }
    }
}
