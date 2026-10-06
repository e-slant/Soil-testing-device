using Google.Protobuf.Compiler;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartAgriculture.Models
{
    public class History
    {
        public int id {  get; set; }
        public DateTime Date { get; set; }
        public float Temperature {  get; set; }
        public float WaterContent {  get; set; }
        public float PhContent {  get; set; }
        public float Nitrogen {  get; set; }
        public float Phosphorus {  get; set; }
        public float Potassium { get; set; }
        //public float room_temperature { get; set; }
        public DateTime insert_time {  get; set; }
        public void GetFromDatabase(List<History> people)
        {
            string connectionString = SystemGlobalVariable.mysqlconnstr;
            string query = "SELECT id,Date,Temperature,WaterContent,PhContent,Nitrogen,Phosphorus,Potassium,insert_time FROM tb_historydata"; 

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            History person = new History
                            {
                                id=reader.GetInt32(0),
                                Date = reader.GetDateTime(1),
                                Temperature = reader.GetFloat(2),
                                WaterContent = reader.GetFloat(3),
                                PhContent = reader.GetFloat(4),
                                Nitrogen = reader.GetFloat(5),
                                Phosphorus = reader.GetFloat(6),
                                Potassium = reader.GetFloat(7),
                                //room_temperature = reader.GetFloat(8),
                                insert_time =reader.GetDateTime(8),
                            };
                            people.Add(person);
                        }
                    }
                }
            }
        }
    }
}
