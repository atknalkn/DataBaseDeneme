﻿using System;
using System.Data;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            DataContextDapper dapper = new(config);


            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('" + myComputer.Motherboard
            //         + "','" + myComputer.HasWifi
            //         + "','" + myComputer.HasLTE
            //         + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
            //         + "','" + myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
            //         + "','" + myComputer.VideoCard
            // + "')";
            // File.WriteAllText("log.txt", "\n"+sql+"\n");
            // using StreamWriter openFile = new ("log.txt",append: true);
            // openFile.WriteLine("\n"+sql+"\n");
            // openFile.Close();
            string computersJson = File.ReadAllText("Computers.json");
            // Console.WriteLine(computersJson);
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);
            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
            if (computersNewtonSoft != null)
            {
                foreach (Computer computer in computersNewtonSoft)
                {
                    // Console.WriteLine(computer.Motherboard);
                    string sql = @"INSERT INTO TutorialAppSchema.Computer (
                 Motherboard,
                 HasWifi,
                 HasLTE,
                 ReleaseDate,
                 Price,
                 VideoCard
             ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                                      + "','" + computer.HasWifi
                                      + "','" + computer.HasLTE
                                      + "','" + computer.ReleaseDate?.ToString("yyyy-MM-dd")
                                      + "','" + computer.Price
                                      + "','" + EscapeSingleQuote(computer.VideoCard)
                              + "')";
                    dapper.ExecuteSql(sql);

                }
            }
            JsonSerializerSettings settings = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string computersCopyNewtonSoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

            File.WriteAllText("computersCopyNewtonSoft.txt", computersCopyNewtonSoft);

            string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

            File.WriteAllText("computersCopySystem.txt", computersCopySystem);

        }
        static string EscapeSingleQuote(string input){
        string output= input.Replace("'","''");
        return output;
        }

    }
}