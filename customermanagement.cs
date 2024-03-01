/*
 * Jackson Keithley (JK8GT)
 * 3/1/24
 * IT 4400
 * M7 Challenge
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Customer_Management
{
    class Customer
    {
        //Private Properties
        private string firstName;
        private string lastName;
        private string email;
        private string id;

        //Constructor
        public Customer(string firstName, string lastName, string email, string id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.id = id;
        }

        //Properties
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        //Method
        public string GetDisplayText()
        {
            return $" Customer ID: {id} - Name: {firstName} {lastName}, Email: {email}";
        }
    }

    class CustomerList
    {
        private List<Customer> customers;

        //Constructor
        public CustomerList()
        {
            customers = new List<Customer>();
        }

        //Read-Only Property
        public int Count => customers.Count;

        //Indexer
        public Customer this[int index]
        {
            get { return customers[index]; }
        }

        //Methods
        public void Add(Customer customer)
        {
            customers.Add(customer);
            OnChanged();
        }

        public void Remove(Customer customer)
        {
            customers.Remove(customer);
            OnChanged();
        }

        //Delegate and events
        public delegate void ChangeHandler(CustomerList customer);
        public event ChangeHandler Changed;

        private void OnChanged()
        {
            Changed?.Invoke(this);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerList customerList = new CustomerList();
            customerList.Changed += DisplayCustomerList; //Subscribed to List.Changed


            //UI
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a new customer");
                Console.WriteLine("2. Delete a customer");
                Console.WriteLine("3. Exit the program");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCustomer(customerList);
                        break;
                    case "2":
                        DeleteCustomer(customerList);
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        //Functions
        static void AddCustomer(CustomerList customerList)
        {
            Console.WriteLine("Enter first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter ID:");
            string id = Console.ReadLine();

            Customer newCustomer = new Customer(firstName, lastName, email, id);
            customerList.Add(newCustomer);

            Console.WriteLine("Customer added successfully.");
        }

        static void DeleteCustomer(CustomerList customerList)
        {
            Console.WriteLine("Enter the ID of the customer to delete:");
            string id = Console.ReadLine();

            Customer customerToRemove = null;

            for (int i = 0; i < customerList.Count; i++)
            {
                if (customerList[i].ID == id)
                {
                    customerToRemove = customerList[i];
                    break;
                }
            }

            if (customerToRemove != null)
            {
                customerList.Remove(customerToRemove);
                Console.WriteLine("Customer deleted successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
        
        static void DisplayCustomerList(CustomerList customerList)
        {
            Console.WriteLine("Customer List:");
            for (int i = 0; i < customerList.Count; i++)
            {
                Console.WriteLine(customerList[i].GetDisplayText());
            }
            Console.WriteLine();
        }
    }
}