using Microsoft.CodeAnalysis.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class DAO
    {
        public DAO() { }
        public string GetConnectionString()
        {
            IConfiguration config=new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",true,true)
                .Build();
            var strConnection = config["ConnectionStrings:MyDB"];
            return strConnection;
        }
       public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            DbProviderFactory factory = SqlClientFactory.Instance;
            using DbConnection connection = factory.CreateConnection();
            if(connection == null )
            {
                return null;
            }
            connection.ConnectionString = GetConnectionString();
            connection.Open();
            DbCommand command = connection.CreateCommand();
            if( command == null)
            {
                return null;
            }
            command.Connection = connection;
            command.CommandText = "select * from [order]";
            using DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int orderId = reader.GetInt32(0);
                string orderType = reader.GetString(1);
                DateTime orderDate = reader.GetDateTime(2);
                string createdBy = reader.GetString(3);
                Order order = new Order();
                order.OrderType = orderType;
                order.OrderDate = orderDate;
                order.CreatedBy = createdBy;
                order.Id = orderId;
                orders.Add(order);
            }
            return orders;
        }
        public List<Order> GetOrdersProcedure(int pageNumber, int pageSize)
        {
            List<Order> orders = new List<Order>();
            DbProviderFactory factory = SqlClientFactory.Instance;
            using DbConnection connection = factory.CreateConnection();
            if (connection == null)
            {
                return null;
            }
            connection.ConnectionString = GetConnectionString();
            connection.Open();
            DbCommand command = connection.CreateCommand();
            if (command == null)
            {
                return null;
            }
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetOrdersByPage";
            command.Parameters.Add(new SqlParameter("@page_number", SqlDbType.Int));
            command.Parameters["@page_number"].Value = pageNumber;
            command.Parameters.Add(new SqlParameter("@page_size", SqlDbType.Int));
            command.Parameters["@page_size"].Value = pageSize;
            using DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int orderId = reader.GetInt32(0);
                string orderType = reader.GetString(1);
                DateTime orderDate = reader.GetDateTime(2);
                string createdBy = reader.GetString(3);
                Order order = new Order();
                order.OrderType = orderType;
                order.OrderDate = orderDate;
                order.CreatedBy = createdBy;
                order.Id = orderId;
                orders.Add(order);
            }
            return orders;
        }
    }
}
