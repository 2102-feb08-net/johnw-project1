using System;
using System.Collections.Generic;

namespace BookStore.Domain
{
    public class Order
    {
        public Order()
        {
            Items = new Dictionary<int, int>();
        }

        public Order(int customerID, int locationID)
        {
            CustomerID = customerID;
            LocationID = locationID;
            Items = new Dictionary<int, int>();
        }

        public Order(int id, int customerID, int locationID)
        {
            ID = id;
            CustomerID = customerID;
            LocationID = locationID;
            Items = new Dictionary<int, int>();
        }

        /// <summary>
        /// An identifier for the Order.
        /// </summary>
        public int ID {get;}

        /// <summary>
        /// An identifier for the Customer who placed the Order.
        /// </summary>
        public int CustomerID {get; set;}

        /// <summary>
        /// An identifier for the Location where the Order was placed.
        /// </summary>
        public int LocationID {get; set;}

        /// <summary>
        /// The mapping of products in the Order, where the key is the Product and the value is the amount of that Product that was ordered.
        /// </summary>
        public Dictionary<int, int> Items { get; set; }

        /// <summary>
        /// The total price of the Order based on the items that were ordered.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// The date and time that the Order was placed.
        /// </summary>
        public DateTimeOffset Time {get; set;}

        /// <summary>
        /// Sets the amount of the given Product that was ordered.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="amount"></param>
        /// <returns> bool indicating success or failure </returns>
        public bool SetItemAmount(Product p, int amount)
        {
            if (p == null || amount < 0) 
            {
                return false;
            }
            Items[p.ID] = amount;
            return true;
        }

        /// <summary>
        /// Sets the amount of the Product matching the given id that was ordered.
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="amount"></param>
        /// <returns> bool indicating success or failure </returns>
        public bool SetItemAmount(int productID, int amount)
        {
            if (productID < 1 || amount < 0) 
            {
                return false;
            }
            Items[productID] = amount;
            return true;
        }

        public bool AddItemAmount(Product p, int amount)
        {
            if (p == null || amount < 1)
            {
                return false;
            }
            if (!Items.ContainsKey(p.ID))
            {
                Items.Add(p.ID, 0);
            }
            Items[p.ID] += amount;
            Total += (amount * p.Price);
            return true;
        }

        public bool SubtractItemAmount(Product p, int amount)
        {
            if (p == null || amount < 1 || !Items.ContainsKey(p.ID))
            {
                return false;
            }

            Items[p.ID] -= amount;
            Total -= (amount * p.Price);
            return true;
        }

        /// <summary>
        /// Returns the amount of the given Product that was ordered
        /// </summary>
        /// <param name="p"></param>
        /// <returns> int </returns>
        public int GetItemAmount(Product p)
        {
            if (p == null || !Items.ContainsKey(p.ID)) 
            {
                return -1;
            }
            return Items[p.ID];
        }

        /// <summary>
        /// Returns the amount of the Product matching the given ID that was ordered.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns> int </returns>
        public int GetItemAmount(int productID)
        {
            if (productID < 1 || !Items.ContainsKey(productID))
            {
                return -1;
            }
            return Items[productID];
        }

        /// <summary>
        /// Removes the given Product from the Order.
        /// </summary>
        /// <param name="p"></param>
        /// <returns> bool indicating success or failure </returns>
        public bool RemoveItem(Product p)
        {
            if (p == null || !Items.ContainsKey(p.ID))
            {
                return false;
            }
            return Items.Remove(p.ID);
        }

        /// <summary>
        /// Removes the Product matching the given ID from the Order.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns> bool indicating success or failure </returns>
        public bool RemoveItem(int productID)
        {
            if (productID < 1 || !Items.ContainsKey(productID))
            {
                return false;
            }
            return Items.Remove(productID);
        }
    }
}