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
    class ProductRepository : IProductRepository
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

        // CRUD Product

        /// <summary>
        /// Returns the list of all Products in the database.
        /// </summary>
        /// <returns> List<Product> list </returns>
        public List<Domain.Product> GetAllProducts()
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var x = _context.Set<Product>().AsEnumerable();
            List<Domain.Product> list = new List<Domain.Product>();

            foreach (var i in x)
            {
                var p = new Domain.Product(i.Name, (decimal)i.Price) { ID = i.Id };
                list.Add(p);
            }

            return list;
        }

        /// <summary>
        /// Returns the specific Product that has the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Product x </returns>
        public Domain.Product GetProductByID(int id)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var c = _context.Set<Product>().Find(id);
            var x = new Domain.Product(c.Name, (decimal)c.Price);

            return x;
        }

        /// <summary>
        /// Returns the list of products that match the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns> List<Product> toReturn </returns>
        public List<Domain.Product> GetProductByName(string name)
        {
            using var logStream = new StreamWriter("bkdb-logs.txt", append: false) { AutoFlush = true };
            using var _context = GenerateDBContext(logStream);

            var products = _context.Set<Product>().Where(x => name == x.Name).ToList();
            List<Domain.Product> toReturn = new List<Domain.Product>();

            foreach (var p in products)
            {
                var x = new Domain.Product(p.Name, (decimal)p.Price) { ID = p.Id };
                toReturn.Add(x);
            }

            return toReturn;
        }

        /// <summary>
        /// Adds the given Product to the database.
        /// </summary>
        /// <param name="p"></param>
        public void AddProduct(Domain.Product p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the given Product in the database.
        /// </summary>
        /// <param name="p"></param>
        public void UpdateProduct(Domain.Product p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the given Product from the database.
        /// </summary>
        /// <param name="p"></param>
        public void DeleteProduct(Domain.Product p)
        {
            throw new NotImplementedException();
        }
    }
}
