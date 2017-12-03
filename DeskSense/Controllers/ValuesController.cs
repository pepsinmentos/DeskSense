using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace DeskSense.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly string astring = "datasource=desksense.cxgsghd2l9yy.eu-west-2.rds.amazonaws.com;port=3306;username=desksense_admin;password=desk-sense-chuckie69";
        // GET api/values
        [HttpGet]
        public IEnumerable<OccupancyRaw> Get()
        {
            var occupancy = new List<OccupancyRaw>();

            using (var conn = new MySqlConnection(astring))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from desksense.OccupancyRaw;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            occupancy.Add(new OccupancyRaw
                            {
                                OccupancyRawId = (int)reader["occupancyRawId"],
                                DeviceId = (string)reader["deviceId"],
                                Timestamp = (DateTime)reader["timestamp"],
                                IsOccupied = (bool)reader["isOccupied"]
                            });                           
                        }
                    }
                    
                }
            }

            return occupancy;
                
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class OccupancyRaw
    {
        public int OccupancyRawId { get; set; }
        public string DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsOccupied { get; set; }
    }
}
