using Microsoft.CodeAnalysis.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
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
        public List<OrderDetailDTO> GetOrderDetailsByID(int orderId)
        {
            List<OrderDetailDTO> orders = new List<OrderDetailDTO>();
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
            command.CommandText = "GetOrderDetailsByID";
            command.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int));
            command.Parameters["@OrderId"].Value = orderId;
      
            using DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string orderType = reader.GetString(1);
                DateTime orderDate = reader.GetDateTime(2);
                string createdBy = reader.GetString(3);
                string name = reader.GetString(4);
                int quantity = reader.GetInt32(5);
                double price = reader.GetDouble(6);
                OrderDetailDTO order = new OrderDetailDTO();
                order.OrderType = orderType;
                order.OrderDate = orderDate;
                order.OrderID = id;
                order.OrderBy = orderType;
                order.Quantiy = quantity;
                order.Price = price;
                order.ProductName = name;
                orders.Add(order);
            }
            return orders;
        }

        public Account Login(string username, string password)
        {
            Account account = null;
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
            command.CommandText = "Login";
            command.Parameters.Add(new SqlParameter("@username", SqlDbType.NChar));
            command.Parameters["@username"].Value = username;
            command.Parameters.Add(new SqlParameter("@password", SqlDbType.NChar));
            command.Parameters["@password"].Value = password;
            using DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int account_id = reader.GetInt32("account_id");
                int role_id = reader.GetInt32(1);
               string usernam=reader.GetString(2);
               string pass=reader.GetString(3);
               string rolename=reader.GetString(4);
                account = new Account();
               account.AccountId = account_id;
                account.Username = usernam;
                account.Password = pass;      
            }
            return account;
        }


        public List<ReportProductQuantity> ReportProductQuantity(string type)
        {
            List <ReportProductQuantity> list= new List<ReportProductQuantity>();
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
            command.CommandText = "ReportProductQuantity";
            command.Parameters.Add(new SqlParameter("@OrderType", SqlDbType.NChar));
            command.Parameters["@OrderType"].Value = type;
     
            using DbDataReader reader = command.ExecuteReader();
            ReportProductQuantity r = null;
            while (reader.Read())
            {
                int year = reader.GetInt32("year");
                int month = reader.GetInt32("month");
                string productName = reader.GetString("name");
                int total = reader.GetInt32("total_quantity");
                r = new ReportProductQuantity();
                r.Year = year;
                r.ProductName= productName;
                r.Month = month;
                r.TotalQuantity = total;
                list.Add(r);
            }
            return list;
        }

        public List<OrderDetails> GetOrderDetails(int order_id)
        {
            List<OrderDetails> orders = new List<OrderDetails>();
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
            command.CommandText = "select op.order_id,op.product_id,op.price,op.quantity,p.[name] from order_product op join Product p on p.id=op.product_id where order_id= " + order_id;
            using DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                OrderDetails order = new OrderDetails();
                order.productId= reader.GetInt32("product_id");
                order.quantity = reader.GetInt32("quantity");
                order.price = reader.GetDouble("price");
                order.productName = reader.GetString("name");
                orders.Add(order);
            }
            return orders;
        }

        public bool DeleteAllOrder(int order_id)
        {
          
            DbProviderFactory factory = SqlClientFactory.Instance;
            using DbConnection connection = factory.CreateConnection();
            if (connection == null)
            {
                return false;
            }
            connection.ConnectionString = GetConnectionString();
            connection.Open();
            DbCommand command = connection.CreateCommand();
            if (command == null)
            {
                return false;
            }
            command.Connection = connection;
            command.CommandText = "  delete Order_Product where order_id= " + order_id;
            int rowsAffected = command.ExecuteNonQuery();

            // Check if any rows were affected
            if (rowsAffected > 0)
            {
                // Rows were affected, return true to indicate success
                return true;
            }
            else
            {
                // No rows were affected, return false to indicate failure
                return false;
            }
        }
    }
}
