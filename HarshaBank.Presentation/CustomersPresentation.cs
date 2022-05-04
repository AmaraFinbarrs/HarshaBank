using System;
using System.Collections.Generic;
using HarshaBank.Entities;
using HarshaBank.Exceptions;
using HarshaBank.BusinessLogicLayer;
using HarshaBank.BusinessLogicLayer.BLLContracts;

namespace HarshaBank.Presentation
{
    static class CustomersPresentation
    {
        internal static void AddCustomer()
        {
            try
            {
                //create an object of customer
                Customer customer = new Customer();

                //read all details from the user
                Console.WriteLine("\n************ADD CUSTOMER************");
                Console.Write("Customer Name: ");
                customer.CustomerName = Console.ReadLine();
                Console.Write("Address: ");
                customer.Address = Console.ReadLine();
                Console.Write("Landmark: ");
                customer.Landmark = Console.ReadLine();
                Console.Write("City: ");
                customer.City = Console.ReadLine();
                Console.Write("Country: ");
                customer.Country = Console.ReadLine();
                Console.Write("Mobile: ");
                customer.Mobile = Console.ReadLine();

                //Create BLL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();
                Guid newGuid = customersBusinessLogicLayer.AddCustomer(customer);

                List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(item => item.CustomerID == newGuid);

                if (matchingCustomers.Count >= 1)
                {
                    Console.WriteLine("New Customer Code: " + matchingCustomers[0].CustomerCode);
                    Console.WriteLine("Customer Added.\n");
                }
                else
                {
                    Console.WriteLine("Customer Not Added");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }

        }



        internal static void ViewCustomer()
        {
            try
            {
                //Create BLL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                //get a list of existing customers
                List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();
                Console.WriteLine("\n************ALL CUSTOMERS************");
                //read all customers
                foreach (var item in allCustomers)
                {
                    Console.WriteLine("Customer Code: " + item.CustomerCode);
                    Console.WriteLine("Customer Name: " + item.CustomerName);
                    Console.WriteLine("Customer Address: " + item.Address);
                    Console.WriteLine("Customer Landmark: " + item.Landmark);
                    Console.WriteLine("Customer City: " + item.City);
                    Console.WriteLine("Customer Country: " + item.Country);
                    Console.WriteLine("Customer Mobile: " + item.Mobile);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void DeleteCustomer()
        {
            try
            {
                //Create BLL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                //get a list of existing customers
                List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();

                //Retrieve the customer code
                System.Console.Write("\nWhat is the customer's code: ");
                int CustomerCode = Convert.ToInt32(Console.ReadLine());

                //find the customer with the retrieved customer code
                Customer customerToDelete = allCustomers.Find(Person => Person.CustomerCode == CustomerCode);

                if (!(customerToDelete is null))
                {
                    //Deletes the customer using their ID
                    bool isCustomersDeleted = customersBusinessLogicLayer.DeleteCustomer(customerToDelete.CustomerID);

                    //Display message to show that customer has been deleted
                    if (isCustomersDeleted is true)
                    {
                        Console.WriteLine("\nCustomer has been deleted");
                    }
                }
                else
                {
                    Console.WriteLine("\nCustomer does not exist");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void UpdateCustomer()
        {
            try
            {
                //Create BLL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                //get a list of existing customers
                List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();

                //Retrieve the customer code
                System.Console.Write("\nWhat is the customer's code: ");
                int CustomerCode = Convert.ToInt32(Console.ReadLine());

                //find the customer with the customer code
                Customer customerToUpdate = allCustomers.Find(Person => Person.CustomerCode == CustomerCode);

                //check if the customer exists
                if (customerToUpdate != null)
                {
                    //Update the customer's details
                    //read all details from the user
                    Console.WriteLine("\n************UPDATE CUSTOMER************");
                    Console.Write("Customer Name: ");
                    customerToUpdate.CustomerName = Console.ReadLine();
                    Console.Write("Address: ");
                    customerToUpdate.Address = Console.ReadLine();
                    Console.Write("Landmark: ");
                    customerToUpdate.Landmark = Console.ReadLine();
                    Console.Write("City: ");
                    customerToUpdate.City = Console.ReadLine();
                    Console.Write("Country: ");
                    customerToUpdate.Country = Console.ReadLine();
                    Console.Write("Mobile: ");
                    customerToUpdate.Mobile = Console.ReadLine();

                    //update existing customer
                    bool isCustomersUpdated = customersBusinessLogicLayer.UpdateCustomer(customerToUpdate);

                    //Display message to show that customer has been updated
                    if (isCustomersUpdated is true)
                    {
                        Console.WriteLine("Customer has been Updated");
                    }
                }
                else
                {
                    Console.WriteLine("Customer does not exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void SearchCustomer()
        {
            try
            {
                //Create BLL object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                //get a list of all customer
                List <Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();

                //Get allowable min and max values
                var allowedMinValue = allCustomers[0].CustomerCode;
                var allowedMaxValue = allCustomers[allCustomers.Count - 1].CustomerCode;

                //Retrieve the customer code
                System.Console.Write("\nWhat is the minimum customer code: ");
                int minCustomerCode = Convert.ToInt32(Console.ReadLine());
                System.Console.Write("What is the maximum customer code: ");
                int maxCustomerCode = Convert.ToInt32(Console.ReadLine());

                //check if the customer exists
                if (minCustomerCode >= allowedMinValue && maxCustomerCode <= allowedMaxValue)
                {
                    //get a list of customer with code
                    List<Customer> customersInRange = customersBusinessLogicLayer.GetCustomersByCondition(Customer => Customer.CustomerCode >= minCustomerCode && Customer.CustomerCode <= maxCustomerCode);

                    //print the customers to the console
                    Console.WriteLine("\n********CUSTOMERS WITH CODES FROM " + minCustomerCode + " TO "+ maxCustomerCode+"********");
                    foreach (Customer item in customersInRange)
                    {
                        Console.WriteLine("Customer Name: " + item.CustomerName);
                        Console.WriteLine("Address: " + item.Address);
                        Console.WriteLine("City: " + item.City);
                        Console.WriteLine("Landmark: " + item.Landmark);
                        Console.WriteLine("Country: " + item.Country);
                        Console.WriteLine("Mobile: " + item.Mobile);
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("\nCustomer range out of bound.");
                    Console.WriteLine("Try values between" + allowedMinValue + " - "+ allowedMaxValue + ".");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

    }
}
