using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practical 3
{
    class expense
    {
        int id;
        string category;
        double amount;
        string paymentmode;

        public void addexpense()
        {
            Console.Write("Enter Expense ID: ");
            id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Category: ");
            category = Console.ReadLine();

            Console.Write("Enter Amount: ");
            amount = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Payment Mode (Cash/UPI/Card): ");
            paymentmode = Console.ReadLine();

            
        }

        public void displayexpense()
        {
            Console.WriteLine("\n---------------------");
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Category: " + category);
            Console.WriteLine("Amount: " + amount);
            Console.WriteLine("Payment Mode: " + paymentmode);
        }

        public double getamount()
        {
            return amount;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<expense> expenses = new List<expense>();

            int choice;

            do
            {
                Console.WriteLine("\n===== EXPENSE TRACKING SYSTEM =====");
                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. Display All Expenses");
                Console.WriteLine("3. Display Total Expense");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Create new expense object
                        expense e = new expense();

                        // Add expense details
                        e.addexpense();

                        // Store object inside List
                        expenses.Add(e);

                        Console.WriteLine("Expense Added Successfully!");
                        break;



                    case 2:
                        if (expenses.Count == 0)
                        {
                            Console.WriteLine("No Expenses Available.");
                        }
                        else
                        {
                            Console.WriteLine("\n===== ALL EXPENSES =====");

                            int i = 0;

                            while (i < expenses.Count)
                            {
                                Console.WriteLine("\nExpense " + (i + 1));
                                expenses[i].displayexpense();

                                i++;
                            }
                        }
                        break;



                    case 3:
                        double total = 0;
                        int j = 0;

                        while (j < expenses.Count)
                        {
                            total = total + expenses[j].getamount();

                            j++;
                        }

                        Console.WriteLine("\nTotal Expense = " + total);
                        break;



                    case 4:
                        Console.WriteLine("Program Exit...");
                        break;

                    default:
                        Console.WriteLine("Invalid Choice!");
                        break;
                }
            } while (choice != 4);

            Console.ReadKey();


        }
            
    }
}