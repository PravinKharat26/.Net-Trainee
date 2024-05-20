using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Assignment_2__IMS_.Program;

namespace Assignment_2__IMS_
{
    internal class Program
    {
        public class Item
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public float Price { get; set; }
            public int Quantity { get; set; }

            public Item(int id, string name, float price, int quantity)
            {
                ID = id;
                Name = name;
                Price = price;
                Quantity = quantity;

            }
            public static void DisplayItems(Item itemObj)
            {
                Console.WriteLine($"ID:{itemObj.ID}\nName:{itemObj.Name}\nPrice:{itemObj.Price}\nQuantity:{itemObj.Quantity}\n\n");
            }
        }

        public class Inventory
        {
            private static List<Item> items = new List<Item>();
            public void AddItems(Item itemObj)
            {
                items.Add(itemObj);
                Console.WriteLine("\nItem Added Succesfully..");
            }

            public void DisplayItems()
            {
                if (items.Count == 0)
                {
                    Console.WriteLine("Inventory is empty...");
                }
                else
                {
                    Console.WriteLine("Inventory contains following items:\n");
                    foreach (var item in items)
                    {
                        Item.DisplayItems(item);
                    }
                }

            }
            public Item FindItemById(int id)
            {
                if (items.Count == 0)
                {
                    Console.WriteLine("\nInventory is empty..");
                    return null;
                }
                Item foundItem = items.Find(item => item.ID == id);
                if (foundItem == null)
                {
                    Console.WriteLine("Item not Found/Enter Valid ID..");

                }
                return foundItem;

            }
            public Item UpdateItem(int id)
            {
                if (items.Count == 0)
                {
                    Console.WriteLine("\nInventory is empty..");
                    return null;
                }
                Item itemIdToUpdate = items.Find(item => item.ID == id);
                if (itemIdToUpdate == null)
                {
                    Console.WriteLine("Item not Found/Enter Valid ID..");


                }
                return itemIdToUpdate;

            }
            public void DeleteItem(int id)
            {
                Item itemIdToDelete = FindItemById(id);
                if (itemIdToDelete == null)
                {
                    Console.WriteLine("\nItem not found..Enter valid ID\n");

                }
                else
                {
                    items.Remove(itemIdToDelete);
                    Console.WriteLine("\nItem Deleted Succesfully..");
                }
            }
        }
    }
    public class Run
    {
        static Inventory inventoryObj = new Inventory();
        static void Main(string[] args)
        {
            Console.WriteLine("~~~Welcome to Inventory Management System~~~");
            MainIMS();
        }
        public static void MainIMS()
        {
            int choice;

            Console.WriteLine("\n~~Please Enter Your Choice:~~");
            Console.WriteLine("\n1-Add a new Item");
            Console.WriteLine("\n2-Display all Items");
            Console.WriteLine("\n3-Find Item by ID");
            Console.WriteLine("\n4-Update an Item");
            Console.WriteLine("\n5-Delete an Item");
            Console.WriteLine("\n6-Exit");
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            choice = Convert.ToInt32(Console.ReadLine());

            if (choice <= 6)
            {
                switch (choice)
                {
                    case 1:
                        AddItem();
                        break;
                    case 2:
                        DisplayItems();
                        break;
                    case 3:
                        FindItemById();
                        break;
                    case 4:
                        UpdateItem();
                        break;
                    case 5:
                        DeleteItem();
                        break;
                    case 6:
                        Exit();
                        break;
                    default:
                        Console.WriteLine("Error/Enter valid choice");
                        break;
                }
                Console.WriteLine("\n-Press Enter For Main Menu-");
                Console.ReadLine();
                Console.Clear();
                MainIMS();
            }
            else
            {
                Console.WriteLine("Please Enter Valid Choice and Press Enter For Main Menu");
                Console.ReadLine();
                Console.Clear();
                MainIMS();
            }
            void AddItem()
            {
                Console.WriteLine("\nYou chose the option 1-Add new task:");
                Console.WriteLine("\nPlease enter Item ID:");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nPlease enter Item Name:");
                string name = Convert.ToString(Console.ReadLine());

                Console.WriteLine("\nPlease enter Item Price (in Rs):");
                float price = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nPlease enter Item Quantity:");
                int quantity = Convert.ToInt32(Console.ReadLine());

                Item itemsObject = new Item(id, name, price, quantity);
                inventoryObj.AddItems(itemsObject);
            }
            void DisplayItems()
            {
                Console.WriteLine("\nYou chose the option 2-Display all Items:\n");
                inventoryObj.DisplayItems();
            }
            void FindItemById()
            {
                Console.WriteLine("\nYou chose the option 3-Find item by ID:");

                Console.WriteLine("\nEnter Item ID:\n");
                int id = Convert.ToInt32(Console.ReadLine());

                Item foundItem = inventoryObj.FindItemById(id);

                if (foundItem != null)
                {
                    Console.WriteLine("Item Found:");
                    Item.DisplayItems(foundItem);
                }
            }
            void UpdateItem()
            {
                Console.WriteLine("\nYou chose the option 4-Update item by ID:");
                Console.WriteLine("\nEnter Item ID to be Updated:\n");
                int id = Convert.ToInt32(Console.ReadLine());
                Item itemIdToUpdate = inventoryObj.UpdateItem(id);
                if (itemIdToUpdate != null)
                {
                    int Updatechoice;

                    Console.WriteLine("\n~~Please Enter Your Choice:~~");
                    Console.WriteLine("\n1-Name");
                    Console.WriteLine("\n2-Price");
                    Console.WriteLine("\n3-Quantity");
                    Console.WriteLine("\n4-Name,Price");
                    Console.WriteLine("\n5-Name,Quantity");
                    Console.WriteLine("\n6-Price,Quantity");
                    Console.WriteLine("\n7-All attributes");
                    Console.WriteLine("\n~~Please Select Atrribute to Update:~~");

                    Updatechoice = Convert.ToInt32(Console.ReadLine());

                    if (Updatechoice == 1)
                    {
                        Console.WriteLine("Enter new Name:");
                        string name = Console.ReadLine();
                        itemIdToUpdate.Name = name;
                    }
                    else if (Updatechoice == 2)
                    {
                        Console.WriteLine("Enter new Price:");
                        float price = Convert.ToSingle(Console.ReadLine());
                        itemIdToUpdate.Price = price;
                    }
                    else if (Updatechoice == 3)
                    {
                        Console.WriteLine("Enter new Quantity:");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        itemIdToUpdate.Quantity = quantity;
                    }
                    else if (Updatechoice == 4)
                    {
                        Console.WriteLine("Enter new Name:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter new Price:");
                        float price = Convert.ToSingle(Console.ReadLine());

                        itemIdToUpdate.Name = name;
                        itemIdToUpdate.Price = price;
                    }
                    else if (Updatechoice == 5)
                    {
                        Console.WriteLine("Enter new Name:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter new Quantity:");
                        int quantity = Convert.ToInt32(Console.ReadLine());

                        itemIdToUpdate.Name = name;
                        itemIdToUpdate.Quantity = quantity;
                    }
                    else if (Updatechoice == 6)
                    {
                        Console.WriteLine("Enter new Price:");
                        float price = Convert.ToSingle(Console.ReadLine());

                        Console.WriteLine("Enter new Quantity:");
                        int quantity = Convert.ToInt32(Console.ReadLine());

                        itemIdToUpdate.Price = price;
                        itemIdToUpdate.Quantity = quantity;
                    }
                    else if (Updatechoice == 7)
                    {
                        Console.WriteLine("Enter new Name:");
                        string name = Console.ReadLine();
                        itemIdToUpdate.Name = name;

                        Console.WriteLine("Enter new Price:");
                        float price = Convert.ToSingle(Console.ReadLine());
                        itemIdToUpdate.Price = price;

                        Console.WriteLine("Enter new Quantity:");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        itemIdToUpdate.Quantity = quantity;
                    }
                    else
                    {
                        Console.WriteLine("\nEnter Valid Choice...");
                    }

                    Console.WriteLine("\nItem Updated Succesfully..");
                }
            }
            void DeleteItem()
            {
                Console.WriteLine("\nYou chose the option 5-Delete item by ID:");
                Console.WriteLine("\nEnter Item ID to be Deleted:\n");
                int id = Convert.ToInt32(Console.ReadLine());
                inventoryObj.DeleteItem(id);
            }
            void Exit()
            {
                Console.WriteLine("\nYou want to exit..Are you sure?");
                Console.WriteLine("\nPress 0 for exit");
                Console.WriteLine("\nPress 1 for Main Menu");

                int exit = Convert.ToInt32(Console.ReadLine());
                if (exit == 0)
                {
                    Console.WriteLine("Thank you for using Inventory Management System..!");
                    Environment.Exit(0);
                }
                else if (exit == 1)
                {
                    Console.Clear();
                    MainIMS();
                }
                else
                {
                    Console.WriteLine("Enter Valid Choice");
                }
            }
        }
    }
}
