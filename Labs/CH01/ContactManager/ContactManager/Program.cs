using System;


namespace ContactManager
{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            bool exit = false;

            Dictionary<string, string> phonebook = new();

            phonebook.Add("Alice", "123-456-7890");
            phonebook.Add("Bob", "987-654-3210");
            phonebook.Add("Charlie", "555-555-5555");

            do
            {
                Console.WriteLine("\n 1. Add New Contact \n 2. View Contact \n 3. Update Contact \n 4. Delete Contact \n 5. List All Contacts \n 6. Exit \n");
                Console.Write("Select an option (1-6): ");
                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter Name: ");
                    string? name = Console.ReadLine();

                    Console.Write("Enter Phone Number: ");
                    string? phoneNumber = Console.ReadLine();

                    phonebook.Add(name, phoneNumber);
                }
                else if (choice == "2")
                {
                    Console.Write("Enter Name to View: ");
                    string? name = Console.ReadLine();
                    if (phonebook.ContainsKey(name))
                    {
                        Console.WriteLine($"---------------------------------------------\n Name: {name}\n Phone Number: {phonebook[name]}");
                        Console.WriteLine("---------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("Contact Not Found.");
                    }
                }
                else if (choice == "3")
                {
                    Console.Write("Enter Name To Update: ");
                    string? name = Console.ReadLine();
                    if (phonebook.ContainsKey(name))
                    {
                        Console.Write("Enter New Phone Number: ");
                        string? newPhoneNumber = Console.ReadLine();
                        phonebook[name] = newPhoneNumber;
                    }
                    else
                    {
                        Console.WriteLine("Contact not found.");
                    }
                }
                else if (choice == "4")
                {
                    Console.Write("Enter Name to Delete: ");
                    string? name = Console.ReadLine();
                    if (phonebook.ContainsKey(name))
                    {
                        phonebook.Remove(name);
                        Console.WriteLine("Contact deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Contact not found.");
                    }
                }
                else if (choice == "5")
                {
                    Console.WriteLine("\nAll Contacts:");
                    foreach (KeyValuePair<string, string> contact in phonebook)
                    {
                        Console.WriteLine($"---------------------------------------------\n Name: {contact.Key}\n Phone Number: {contact.Value}");
                        Console.WriteLine("---------------------------------------------");
                    }
                }
                else if (choice == "6")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
            while (exit == false);
        }
    }
}
