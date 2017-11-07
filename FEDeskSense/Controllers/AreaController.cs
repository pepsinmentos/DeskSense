using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEDeskSense.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AreaController : Controller
    {
        private List<Area> areas = new List<Area>()
        {
            new Area() { Id = 1, Name = "ABC", Parent = 2 },
            new Area() { Id = 2, Name = "DEF", Parent = 0 },
            new Area() { Id = 3, Name = "GHI", Parent = 2 },
            new Area() { Id = 4, Name = "JKL", Parent = 2 },
            new Area() { Id = 5, Name = "MNO", Parent = 4 }
        };

        [HttpGet("[action]")]
        public IEnumerable<Area> AllAreas()
        {
            return Enumerable.Range(0, 4).Select(index =>
            {
                return areas[index];
            });
        }

        [HttpGet("[action]")]
        public IEnumerable<ChartData> OccupancyLive()
        {
            var list = TestDataset();
            var totals = TestDataset().GroupBy(x => x.Timestamp.Minute).Select(x =>
            {
                var occupancyRate = x.Aggregate(0f, (accumulate, input) => { return accumulate + (float)input.Occupancy; });
                return new OccupancyData() { DeviceId = "Total", Timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, x.Key, 0), Occupancy = occupancyRate  };
            });

            var average = TestDataset().GroupBy(x => x.Timestamp.Minute).Select(x =>
            {
                var acc = x.Aggregate(0f, (accumulate, input) => { return accumulate + input.Occupancy; });
                var occupancyRate = acc / x.Count();
                Console.WriteLine(String.Format($"Acc: {acc}. Count {x.Count()}. Total {occupancyRate}"));
                
                return new OccupancyData() { DeviceId = "Average", Timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, x.Key, 0), Occupancy = occupancyRate };
            });

            list.AddRange(totals);
            list.AddRange(average);

            return list.OrderBy(x => x.Timestamp).Aggregate<OccupancyData, Dictionary<string, ChartData>>(new Dictionary<string, ChartData>(), (aggregate, occupancy) => 
            {
                if (!aggregate.ContainsKey(occupancy.DeviceId)) aggregate.Add(occupancy.DeviceId, new ChartData() { Label = occupancy.DeviceId, Data = new List<float>() });
                aggregate[occupancy.DeviceId].Data.Add(occupancy.Occupancy);


                return aggregate;
            }).Select(dict => dict.Value);
        }

        [HttpGet("[action]")]
        public IEnumerable<Occupancy> OccupancyLatest()
        {
            return new List<Occupancy>()
            {
                new Occupancy {Id = "a1b2c3", X = 150, Y = 75, Occupied= new Random().Next(2) == 0 },
                new Occupancy {Id = "d5e6f7", X = 210, Y = 90, Occupied= new Random().Next(2) == 0 },
                new Occupancy {Id = "abcdef", X = 200, Y = 190, Occupied= new Random().Next(2) == 0 },
                new Occupancy {Id = "ff424", X = 150, Y = 140, Occupied= new Random().Next(2) == 0 }
            };
        }

        public static List<OccupancyData> TestDataset()
        {
            return new List<OccupancyData>()
            {
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(1)) },
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(2)) },
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(3)) },
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(4)) },
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)) },
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(6)) },
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(7)) },
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(8)) },
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(9)) },
                new OccupancyData() { DeviceId = "a1b2c3", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(10)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(1)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(2)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(3)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(4)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(6)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(7)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(8)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(9)) },
                new OccupancyData() { DeviceId = "d5e6f7", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(10)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(1)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(2)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(3)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(4)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(6)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(7)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(8)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(9)) },
                new OccupancyData() { DeviceId = "abcdef", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 1, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(10)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(1)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(2)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(3)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(4)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(6)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(7)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(8)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(9)) },
                new OccupancyData() { DeviceId = "fff424", Id = Guid.NewGuid().ToString().ToLower(), Occupancy = 0, Timestamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(10)) }
            };
        }

        
    }

    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
    }

    public class OccupancyData
    {
        public string Id { get; set; }
        public string DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public float Occupancy { get; set; }
    }

    public class ChartData
    {
        public List<float> Data { get; set; }
        public string Label { get; set; }
    }

    public class Occupancy
    {
        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Occupied { get; set; }
    }
}