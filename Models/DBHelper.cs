using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace smartAgriculture.Models
{
    public class DBHelper
    {
        //static string _connectionString = "server=localhost;port=3306;database=soildata;uid=myusername;pwd=mypassword";
        private string _connectionString;

        public DBHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int ExecuteNonQuery(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error executing non-query: " + ex.Message);
                }
            }
        }
        public DataTable ExecuteQuery(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error executing query: " + ex.Message);
                }
            }
        }
        public object ExecuteScalar(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        return cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error executing scalar: " + ex.Message);
                }
            }
        }
        public void InsertMultipleRecords(List<SensorData> records)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO tb_historydata (Date,Temperature,WaterContent,PhContent,Nitrogen,Phosphorus,Potassium,insert_time)" +
                        " VALUES (@Value1,@Value2,@Value3,@Value4,@Value5,@Value6,@Value7,@Value9)";

                    foreach (var record in records)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Value1", record.GatherTime);
                        cmd.Parameters.AddWithValue("@Value2", record.Temperatrue);
                        cmd.Parameters.AddWithValue("@Value3", record.WaterContent);
                        cmd.Parameters.AddWithValue("@Value4", record.PhContent);
                        cmd.Parameters.AddWithValue("@Value5", record.Nitrogen);
                        cmd.Parameters.AddWithValue("@Value6", record.Phosphorus);
                        cmd.Parameters.AddWithValue("@Value7", record.Potassium);
                        //cmd.Parameters.AddWithValue("@Value8", record.room_temperature);
                        cmd.Parameters.AddWithValue("@Value9", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        break;
                    }
                    connection.Close();
                }
            }
        }
        public void UpdatePage(float TemperatrueUpperLimit,float TemperatrueLowerLimit,
            float WaterContentUpperLimit, float WaterContentLowerLimit,
            float PhUpperLimit,float PhLowerLimit,
            float NitrogenUpperLimit,float NitrogenLowerLimit,
            float PhosphorusUpperLimit,float PhosphorusLowerLimit,
            float PotassiumUpperLimit,float PotassiumLowerLimit)
            //float room_temperatureUpperLimit,float room_temperatureLowerLimit)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                
                MySqlCommand mySqlCommand = new MySqlCommand("update tb_Page set TemperatrueUpper=@value1,TemperatrueLower=@value2" +
                    ",WaterContentUpper=@value3,WaterContentLower=@value4," +
                    "PhUpper=@value5,PhLower=@value6," +
                    "NitrogenUpper=@value7,NitrogenLower=@value8," +
                    "PhosphorusUpper=@value9,PhosphorusLower=@value10," +
                    "PotassiumUpper=@value11,PotassiumLower=@value12" 
                    , connection);
                connection.Open();
                mySqlCommand.Parameters.AddWithValue("@value1",TemperatrueUpperLimit);
                mySqlCommand.Parameters.AddWithValue("@value2",TemperatrueLowerLimit);

                mySqlCommand.Parameters.AddWithValue("@value3",WaterContentUpperLimit);
                mySqlCommand.Parameters.AddWithValue("@value4",WaterContentLowerLimit);

                mySqlCommand.Parameters.AddWithValue("@value5",PhUpperLimit);
                mySqlCommand.Parameters.AddWithValue("@value6",PhLowerLimit);

                mySqlCommand.Parameters.AddWithValue("@value7",NitrogenUpperLimit);
                mySqlCommand.Parameters.AddWithValue("@value8",NitrogenLowerLimit);

                mySqlCommand.Parameters.AddWithValue("@value9",PhosphorusUpperLimit);
                mySqlCommand.Parameters.AddWithValue("@value10",PhosphorusLowerLimit);

                mySqlCommand.Parameters.AddWithValue("@value11",PotassiumUpperLimit);
                mySqlCommand.Parameters.AddWithValue("@value12",PotassiumLowerLimit);

                //mySqlCommand.Parameters.AddWithValue("@value13",room_temperatureUpperLimit);
                //mySqlCommand.Parameters.AddWithValue("@value14",room_temperatureLowerLimit);
                mySqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void LoadPage(SetAlarmData setAlarm)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try{
                    connection.Open();
                    MySqlCommand sqlCommand = new MySqlCommand("select * from tb_Page limit 1", connection);
                    MySqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        float a = (float)reader["TemperatrueUpper"];
                        setAlarm.TemperatrueUpperLimit = (float)reader["TemperatrueUpper"];
                        setAlarm.TemperatrueLowerLimit = (float)reader["TemperatrueLower"];

                        setAlarm.WaterContentUpperLimit = (float)reader["WaterContentUpper"];
                        setAlarm.WaterContentLowerLimit = (float)reader["WaterContentLower"];

                        setAlarm.PhUpperLimit = (float)reader["PhUpper"];
                        setAlarm.PhLowerLimit = (float)reader["PhLower"];

                        setAlarm.NitrogenUpperLimit = (float)reader["NitrogenUpper"];
                        setAlarm.NitrogenLowerLimit = (float)reader["NitrogenLower"];

                        setAlarm.PhosphorusUpperLimit = (float)reader["PhosphorusUpper"];
                        setAlarm.PhosphorusLowerLimit = (float)reader["PhosphorusLower"];

                        setAlarm.PotassiumUpperLimit = (float)reader["PotassiumUpper"];
                        setAlarm.PotassiumLowerLimit = (float)reader["PotassiumLower"];

                        setAlarm.room_temperatureUpperLimit = (float)reader["room_temperatureUpper"];
                        setAlarm.room_temperatureLowerLimit = (float)reader["room_temperatureLower"];
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void InsertAlarm(string alarm,DateTime time)
        {
            using(MySqlConnection conn=new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand("insert into tb_Alarm(time,alarm) values(@time,@alarm)",conn);
                    mySqlCommand.Parameters.Clear();
                    mySqlCommand.Parameters.AddWithValue("@time",time);
                    mySqlCommand.Parameters.AddWithValue("@alarm",alarm);
                    mySqlCommand.ExecuteNonQuery();
                    conn.Clone();
                }catch(MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public string GetConnectionString()
        {
            return _connectionString;
        }
    }


}

