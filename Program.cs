using System;
using System.Collections.Generic;

class Program
{
    static List<Account> accounts = new List<Account>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1. Open Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. View Account Info");
            Console.WriteLine("5. Edit Account Info");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    OpenAccount();
                    break;
                case 2:
                    Deposit();
                    break;
                case 3:
                    Withdraw();
                    break;
                case 4:
                    ShowAccountInfo();
                    break;
                case 5:
                    EditAccount();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    static void OpenAccount()
    {
        Console.Write("Account Holder Name: ");
        string name = Console.ReadLine();

        Console.Write("Account Type (Savings/Loan): ");
        string type = Console.ReadLine();

        Account account = new Account(name, type);
        accounts.Add(account);
        

        Console.WriteLine($"Account {type} for {name} has been opened with ID {accounts.Count}.");
    }

    static void Deposit()
    {
        Console.Write("Account ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Deposit Amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        Account account = accounts.Find(a => a.Id == id);

        if (account != null)
        {
            account.Deposit(amount);
            Console.WriteLine($"Amount {amount} has been deposited to {account.Name}'s account.");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    static void Withdraw()
    {
        Console.Write("Account ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Withdrawal Amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        Account account = accounts.Find(a => a.Id == id);

        if (account != null)
        {
            if (account.Withdraw(amount))
                Console.WriteLine($"Amount {amount} has been withdrawn from {account.Name}'s account.");
            else
                Console.WriteLine("Insufficient balance.");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    static void ShowAccountInfo()
    {
        Console.Write("Account ID: ");
        int id = int.Parse(Console.ReadLine());

        Account account = accounts.Find(a => a.Id == id);

        if (account != null)
        {
            Console.WriteLine(account);
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    static void EditAccount()
    {
        Console.Write("Account ID: ");
        int id = int.Parse(Console.ReadLine());

        Account account = accounts.Find(a => a.Id == id);

        if (account != null)
        {
            Console.Write("New Name: ");
            account.Name = Console.ReadLine();

            Console.WriteLine("Account information updated.");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }
}

class Account
{
    private static int count = 1;
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Type { get; private set; }
    public decimal Balance { get; private set; }

    public Account(string name, string type)
    {
        Id = count++;
        Name = name;
        Type = type;
        Balance = 0;
    }

    public void Deposit(decimal amount) => Balance += amount;

    public bool Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Account ID: {Id}, Name: {Name}, Type: {Type}, Balance: {Balance}";
    }
}