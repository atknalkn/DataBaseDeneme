﻿using System;
using System.Data;
using Microsoft.Data.SqlClient;
using HelloWorld.Models;
using System.Data.Common;
using Dapper;
using HelloWorld.data;
namespace HelloWorld

{
  internal class Program
  {
    public static void Main(string[] args)
    {
      DataContextDapper dapper =new ();

      DateTime rightNow= dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

      // Console.WriteLine(rightNow.ToString());

      Computer myComputer = new()
      {
        Motherboard = "Z690",
        HasWifi = true,
        HasLTE = false,
        RelaseDate = DateTime.Now,
        Price = 942.25f,
        VideoCard = "RTX2060"
      };
      
;
      string sql = @"INSERT INTO TutorialAppSchema.Computer(
      Motherboard ,
        HasWifi,
        HasLTE ,
        RelaseDate ,
        Price,
        VideoCard 
        )VALUES (@Motherboard,
        @HasWifi,
        @HasLTE,
        @RelaseDate,
        @Price,
        @VideoCard)";

        // Console.WriteLine(sql);
        bool result = dapper.ExecuteSql(sql,new {
          myComputer.Motherboard,
          myComputer.HasWifi,
          myComputer.HasLTE,
          myComputer.RelaseDate,
          myComputer.Price,
          myComputer.VideoCard
        });
        // Console.WriteLine(result);
        string sqlSelect = @"
        SELECT 
          Motherboard ,
          HasWifi,
          HasLTE ,
          RelaseDate ,
          Price,
          VideoCard 
        FROM TutorialAppSchema.Computer";
        IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
        Console.WriteLine("'Motherboard','HasWifi','HasLTE','RelaseDate'"+"'Price','VideoCard'");

        foreach(Computer singleComputer in computers){
          Console.WriteLine("'"+ myComputer.Motherboard 
        + "','" + singleComputer.HasWifi
        + "','" + singleComputer.HasLTE
        + "','" + singleComputer.RelaseDate
        + "','" + singleComputer.Price
        + "','" + singleComputer.VideoCard
        +"'");
        }
      // Console.WriteLine(myComputer.Price);
    }

  }
}