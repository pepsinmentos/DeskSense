using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DeskSense.LoadGenerator
{
    class Program
    {
        static int SLEEP_SECONDS = 10;
        static HttpClient client = new HttpClient();

        static async Task PostOccupancy(List<OccupancyData> data)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/area/postoccupancy", data);
            response.EnsureSuccessStatusCode();
        }


        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                PostOccupancy(TestSet()).GetAwaiter().GetResult();
                
                System.Threading.Thread.Sleep(SLEEP_SECONDS * 1000);
            }
        }

        public static List<OccupancyData> TestSet()
        {

            return new List<OccupancyData>()
            {
                new OccupancyData() { DeviceId = "abc", Occupancy = CalculateOccupancy(100) },
                new OccupancyData() { DeviceId = "xyz", Occupancy = CalculateOccupancy(10) },
                new OccupancyData() { DeviceId = "def", Occupancy = CalculateOccupancy(50) }

            };
        }

        public static int CalculateOccupancy(int occupancyChance)
        {
            var rand = new Random().Next(100);
            return rand <= occupancyChance ? 1 : 0;
        }
    }


    

    public class OccupancyData
    {
        public string DeviceId { get; set; }
        public int Occupancy { get; set; }
    }
}
