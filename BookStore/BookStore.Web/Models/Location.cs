using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Models
{
    public class Location
    {
        public Location()
        {
            Inventory = new Dictionary<int, int>();
        }

        public Location(int id, string name)
        {
            ID = id;
            Name = name;
            Inventory = new Dictionary<int, int>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public Dictionary<int, int> Inventory;
    }
}
