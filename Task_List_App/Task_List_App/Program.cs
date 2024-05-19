using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Task_List_App
{
    internal class Program
    {
        static List<string> list = new List<string>();
        static void Main(string[] args)
        {
            Console.WriteLine("~~WELCOME TO TASK LIST APPLICATION~~");
            Tasks();
        }
        public static void Tasks()
        {

            int choice, exit, taskIndex;
            string title, desc, modifiedTitle, modifiedDesc;
            bool validIndex = false;

            Console.WriteLine("\n~~Please Enter Your Choice:~~");
            Console.WriteLine("\n1-Add a new task");
            Console.WriteLine("\n2-Display tasks");
            Console.WriteLine("\n3-Modify/Edit task");
            Console.WriteLine("\n4-Deleta a task");
            Console.WriteLine("\n5-Exit");

            choice = Convert.ToInt32(Console.ReadLine());

            if (choice <= 5)
            {
                switch (choice)
                {
                    case 1:

                        Console.WriteLine("\nYou chose the option 1-Add new task:");
                        Console.WriteLine("\nPlease enter Task title:");
                        title = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("\nPlease enter Task description:");
                        desc = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("\nTask Added Succesfully");
                        list.Add($"Task Title:\n{title}\nDescription of task:\n{desc}\n");
                        break;

                    case 2:

                        Console.WriteLine("\nYou chose the option 2-Display tasks:");
                        if (list.Count == 0)
                        {
                            Console.WriteLine("\nNo tasks added");
                        }
                        else
                        {
                            Console.WriteLine("\n~~~Your Tasks are:~~~\n");
                            foreach (string tasks in list)
                            {
                                Console.WriteLine(tasks);

                            }
                        }
                        break;

                    case 3:

                        Console.WriteLine("\nYou chose the option 3-Modify/Edit task:");

                        if (list.Count == 0)
                        {
                            Console.WriteLine("\nCan't Modify,Task List is empty");
                        }

                        else
                        {
                            foreach (string tasks in list)
                            {
                                Console.WriteLine("\n~~~Your Tasks are:~~~\n");
                                Console.WriteLine(tasks);
                            }

                            while (validIndex = true)
                            {
                                Console.WriteLine("\nPlease Enter Index of Task to be Modified:");
                                taskIndex = Convert.ToInt32(Console.ReadLine());

                                if (taskIndex < 0 || taskIndex >= list.Count)
                                {
                                    Console.WriteLine("\nPlease Enter valid index...");
                                }

                                else
                                {
                                    validIndex = true;
                                    Console.WriteLine("\nPlease Enter:\n ~~0 to Modify Only Title \n ~~1 to Modify Only Description \n ~~2 to Modify Both\n");
                                    int modifyChoice = Convert.ToInt32(Console.ReadLine());

                                    if (modifyChoice == 0)
                                    {
                                        Console.WriteLine("\nPlease enter Task title:");
                                        modifiedTitle = Convert.ToString(Console.ReadLine());

                                        string originalDesc = list[taskIndex].Substring(list[taskIndex].IndexOf("Description of task:"));

                                        list[taskIndex] = ($"Task Title:\n{modifiedTitle}\n{originalDesc}\n");
                                        Console.WriteLine("\nTask Modified Succesfully");


                                    }
                                    else if (modifyChoice == 1)
                                    {
                                        Console.WriteLine("\nPlease enter Task description:");
                                        modifiedDesc = Convert.ToString(Console.ReadLine());

                                        int descIndex = list[taskIndex].IndexOf("Description of task:");
                                        string originalTitle = list[taskIndex].Substring(0, descIndex);

                                        list[taskIndex] = ($"{originalTitle}\nDescription of task:\n{modifiedDesc}\n");

                                        Console.WriteLine("\nTask Modified Succesfully");
                                    }

                                    else if (modifyChoice == 2)
                                    {
                                        Console.WriteLine("\nPlease enter Task title:");
                                        modifiedTitle = Convert.ToString(Console.ReadLine());


                                        Console.WriteLine("\nPlease enter Task description:");
                                        modifiedDesc = Convert.ToString(Console.ReadLine());

                                        list[taskIndex] = ($"Task Title:\n{modifiedTitle}\nDescription of task:\n{modifiedDesc}\n");
                                        Console.WriteLine("\nTask Modified Succesfully");

                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter valid choice..");
                                    }

                                    break;

                                }
                            }
                        }
                        break;

                    case 4:

                        Console.WriteLine("\nYou chose the option 4-Delete a task:");

                        if (list.Count == 0)
                        {
                            Console.WriteLine("\nCan't Delete,Task List is empty");
                        }

                        else
                        {
                            foreach (string tasks in list)
                            {
                                Console.WriteLine("\n~~~Your Tasks are:~~~\n");
                                Console.WriteLine(tasks);
                            }

                            while (validIndex = true)
                            {
                                Console.WriteLine("\nPlease Enter Index of Task to be Deleted:");
                                taskIndex = Convert.ToInt32(Console.ReadLine());

                                if (taskIndex < 0 || taskIndex >= list.Count)
                                {
                                    Console.WriteLine("\nPlease Enter valid index...");
                                }

                                else
                                {
                                    validIndex = true;

                                    list.RemoveAt(taskIndex);
                                    Console.WriteLine("\nTask Deleted Succesfully");

                                    break;
                                }
                            }
                        }
                        break;

                    case 5:

                        Console.WriteLine("\nYou want to exit..Are you sure?");
                        Console.WriteLine("\nPress 0 for exit");
                        Console.WriteLine("\nPress 1 for Main Menu");

                        exit = Convert.ToInt32(Console.ReadLine());
                        if (exit == 0)
                        {
                            Environment.Exit(0);

                        }
                        else if (exit == 1)
                        {
                            Console.Clear();
                            Tasks();
                        }
                        else
                        {
                            Console.WriteLine("Enter Valid Choice");

                        }
                        break;

                    default:

                        Console.WriteLine("Error/Enter valid choice");
                        break;
                }

                Console.WriteLine("\n-Press Enter For Main Menu-");
                Console.ReadLine();
                Console.Clear();
                Tasks();
            }
            else
            {
                Console.WriteLine("Please Enter Valid Choice and Press Enter For Main Menu");
                Console.ReadLine();
                Console.Clear();
                Tasks();
            }
        }
    }
}



