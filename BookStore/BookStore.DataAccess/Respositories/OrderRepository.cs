using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// Generates the context for accessing the database based on options that are given.
        /// </summary>
        /// <param name="logStream"></param>
        /// <returns> bookstoredbContext _context </returns>
        private bookstoredbContext GenerateDBContext(StreamWriter logStream)
        {
            string connString = File.ReadAllText("C:/revature/bkdb.txt");
            DbContextOptions<bookstoredbContext> options = new DbContextOptionsBuilder<bookstoredbContext>()
                .UseSqlServer(connString)
                .LogTo(logStream.WriteLine, minimumLevel: LogLevel.Information)
                .Options;

            return new bookstoredbContext(options);
        }

        // CRUD Orders

        /// <summary>
        /// Returns the list of all Orders in the database.
        /// </summary>
        /// <returns> List<Order> toReturn </returns>
        public List<Domain.Order> GetAllOrders()
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var dbOrders = _context.Set<Order>().ToList();
            List<Domain.Order> toReturn = new List<Domain.Order>();

            foreach (var o in dbOrders)
            {
                var n = new Domain.Order(o.Id, o.CustomerId, o.LocationId) { Time = (DateTimeOffset)o.Time, Total = (decimal)o.TotalPrice };

                var lines = _context.Set<OrderLine>().Where(i => i.OrderId == o.Id).ToList();
                foreach (var i in lines)
                {
                    var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                    var domProduct = new Domain.Product(dbProduct.Id, dbProduct.Name, (decimal)dbProduct.Price);
                    
                    n.SetItemAmount(domProduct, i.Amount);
                }
                toReturn.Add(n);
            }

            return toReturn;
        }

        /// <summary>
        /// Returns the specific Order matching the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Order ord </returns>
        public Domain.Order GetOrderByID(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var o = _context.Set<Order>().Find(id);
            var ord = new Domain.Order(o.Id, o.CustomerId, o.LocationId) { Time = (DateTimeOffset)o.Time, Total = (decimal)o.TotalPrice };

            var items = _context.Set<OrderLine>().Where(i => i.OrderId == id).ToList();
            foreach (var i in items)
            {
                var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                var domProduct = new Domain.Product(dbProduct.Id, dbProduct.Name, (decimal)dbProduct.Price);

                ord.SetItemAmount(domProduct, i.Amount);
            }

            return ord;
        }

        /// <summary>
        /// Returns the list of Orders placed by the Customer with the given ID.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns> List<Order> toReturn </returns>
        public List<Domain.Order> GetOrdersByCustomerID(int customerID)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var dbOrders = _context.Set<Order>().Where(i => i.CustomerId == customerID).ToList();
            List<Domain.Order> toReturn = new List<Domain.Order>();

            foreach (var o in dbOrders)
            {
                var n = new Domain.Order(o.Id, o.CustomerId, o.LocationId) { Time = (DateTimeOffset)o.Time, Total = (decimal)o.TotalPrice };
                var lines = _context.Set<OrderLine>().Where(i => i.OrderId == o.Id).ToList();
                foreach (var i in lines)
                {
                    var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                    var domProduct = new Domain.Product(dbProduct.Id, dbProduct.Name, (decimal)dbProduct.Price);
                    n.SetItemAmount(domProduct, i.Amount);
                }
                toReturn.Add(n);
            }

            return toReturn;
        }

        /// <summary>
        /// Returns the list of Orders placed at the Location with the given ID.
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns> List<Order> toReturn </returns>
        public List<Domain.Order> GetOrdersByLocationID(int locationID)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var dbOrders = _context.Set<Order>().Where(i => i.LocationId == locationID).ToList();
            List<Domain.Order> toReturn = new List<Domain.Order>();

            foreach (var o in dbOrders)
            {
                var n = new Domain.Order(o.Id, o.CustomerId, o.LocationId) { Time = (DateTimeOffset)o.Time, Total = (decimal)o.TotalPrice };
                var lines = _context.Set<OrderLine>().Where(i => i.OrderId == o.Id).ToList();
                foreach (var i in lines)
                {
                    var dbProduct = _context.Set<Product>().Where(p => p.Id == i.ProductId).FirstOrDefault();
                    var domProduct = new Domain.Product(dbProduct.Id, dbProduct.Name, (decimal)dbProduct.Price);
                    n.SetItemAmount(domProduct, i.Amount);
                }
                toReturn.Add(n);
            }

            return toReturn;
        }

        /// <summary>
        /// Adds the given Order to the database.
        /// </summary>
        /// <param name="o"></param>
        public void AddOrder(Domain.Order o)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            Order entity = new Order() { CustomerId = o.CustomerID, LocationId = o.LocationID, Time = o.Time.UtcDateTime, TotalPrice = o.Total };
            _context.Set<Order>().Add(entity);
            _context.SaveChanges();
            foreach (var kv in o.Items)
            {
                OrderLine ol = new OrderLine() { OrderId = entity.Id, ProductId = kv.Key, Amount = kv.Value };
                _context.Set<OrderLine>().Add(ol);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates the given Order in the database.
        /// </summary>
        /// <param name="o"></param>
        public void UpdateOrder(Domain.Order o)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the given Order from the database.
        /// </summary>
        /// <param name="o"></param>
        public void DeleteOrder(Domain.Order o)
        {
            throw new NotImplementedException();
        }
    }
}
