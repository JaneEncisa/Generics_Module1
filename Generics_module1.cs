using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public class DictionaryRepository<TKey, TValue> where TKey : IComparable<TKey>
    {
        private readonly Dictionary<TKey, TValue> items = new Dictionary<TKey, TValue>();

        public void Add(TKey id, TValue item)
        {
            if (items.ContainsKey(id))
                throw new ArgumentException("ID already exists.");
            items.Add(id, item);
        }

        public TValue Get(TKey id)
        {
            if (!items.ContainsKey(id))
                throw new KeyNotFoundException("ID not found.");
            return items[id];
        }

        public void Update(TKey id, TValue newItem)
        {
            if (!items.ContainsKey(id))
                throw new KeyNotFoundException("ID not found.");
            items[id] = newItem;
        }

        public void Delete(TKey id)
        {
            if (!items.ContainsKey(id))
                throw new KeyNotFoundException("ID not found.");
            items.Remove(id);
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetAll()
        {
            return items;
        }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {ProductName}";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Product Management");

            var repository = new DictionaryRepository<int, Product>();

            while (true)
            {
                Console.WriteLine("\n1-Add\n2-Get\n3-Update\n4-Delete\n5-Show All\n6-Exit");
                if (!int.TryParse(Console.ReadLine(), out int command))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (command)
                {
                    case 1:
                        Console.Write("Enter Product ID: ");
                        if (int.TryParse(Console.ReadLine(), out int addid) && addid > 0)
                        {
                            Console.Write("Enter Product Name: ");
                            string addName = Console.ReadLine();
                            try
                            {
                                repository.Add(addid, new Product { ProductId = addid, ProductName = addName });
                                Console.WriteLine("Product added.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID. Please enter a positive integer.");
                        }
                        break;

                    case 2:
                        Console.Write("Enter Product ID to retrieve: ");
                        if (int.TryParse(Console.ReadLine(), out int getId))
                        {
                            try
                            {
                                Console.WriteLine(repository.Get(getId));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;

                    case 3:
                        Console.Write("Enter Product ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            Console.Write("Enter new Product Name: ");
                            string updateName = Console.ReadLine();
                            try
                            {
                                repository.Update(updateId, new Product { ProductId = updateId, ProductName = updateName });
                                Console.WriteLine("Product updated.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;

                    case 4:
                        Console.Write("Enter Product ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            try
                            {
                                repository.Delete(deleteId);
                                Console.WriteLine("Product deleted.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;

                    case 5:
                        Console.WriteLine("All Products:");
                        foreach (var item in repository.GetAll())
                        {
                            Console.WriteLine($"ID: {item.Key}, Product: {item.Value}");
                        }
                        break;

                    case 6:
                        Console.WriteLine("Exiting program...");
                        return;

                    default:
                        Console.WriteLine("Invalid command. Please enter a number between 1 and 6.");
                        break;
                }
            }
        }
    }
}
