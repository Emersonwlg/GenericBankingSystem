using System;
using System.Collections.Generic;

namespace BankingSystemGenerics
{
    // Base entity
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
    }

    // Customer entity
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Document { get; set; } // like CPF/SSN

        public override string ToString() => $"Customer: {Name} - Document: {Document}";
    }

    // Bank account entity
    public class BankAccount : BaseEntity
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; private set; }

        public void Deposit(decimal amount) => Balance += amount;

        public bool Withdraw(decimal amount)
        {
            if (amount > Balance) return false;
            Balance -= amount;
            return true;
        }

        public bool TransferTo(BankAccount destination, decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Transfer amount must be greater than zero.");
                return false;
            }

            if (!Withdraw(amount))
            {
                Console.WriteLine($"Transfer failed: insufficient funds in account {AccountNumber}");
                return false;
            }

            destination.Deposit(amount);
            Console.WriteLine($"Transfer successful: {amount:C} from {AccountNumber} to {destination.AccountNumber}");
            return true;
        }

        public override string ToString() => $"Account {AccountNumber} - Balance: {Balance:C}";
    }

    // Generic repository
    public class Repository<T> where T : BaseEntity
    {
        private readonly List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
            Console.WriteLine($"{item} added to repository!");
        }

        public IEnumerable<T> GetAll() => _items;

        public T GetById(Guid id) => _items.Find(i => i.Id == id);

        public IEnumerable<T> Find(Func<T, bool> predicate) => _items.FindAll(new Predicate<T>(predicate));
    }

    // Utility class with generic method
    public static class Util
    {
        public static void PrintList<T>(IEnumerable<T> list)
        {
            foreach (var item in list)
                Console.WriteLine(item);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var customerRepo = new Repository<Customer>();
            var accountRepo = new Repository<BankAccount>();

            // Creating customers
            var customer1 = new Customer { Name = "John Smith", Document = "111-111-111" };
            var customer2 = new Customer { Name = "Mary Johnson", Document = "222-222-222" };

            customerRepo.Add(customer1);
            customerRepo.Add(customer2);

            // Creating accounts
            var account1 = new BankAccount { AccountNumber = "1001" };
            account1.Deposit(1500);

            var account2 = new BankAccount { AccountNumber = "1002" };
            account2.Deposit(3000);

            accountRepo.Add(account1);
            accountRepo.Add(account2);

            Console.WriteLine("\n--- Customers ---");
            Util.PrintList(customerRepo.GetAll());

            Console.WriteLine("\n--- Accounts ---");
            Util.PrintList(accountRepo.GetAll());

            Console.WriteLine("\n--- Banking Operations ---");
            Console.WriteLine("Withdrawing $500 from account 1001...");
            account1.Withdraw(500);

            Console.WriteLine("Depositing $1000 into account 1002...");
            account2.Deposit(1000);

            Console.WriteLine("Transferring $700 from account 1002 to account 1001...");
            account2.TransferTo(account1, 700);

            Console.WriteLine("\n--- Updated Accounts ---");
            Util.PrintList(accountRepo.GetAll());

            Console.WriteLine("\n--- Find Customer Mary ---");
            var found = customerRepo.Find(c => c.Name.Contains("Mary"));
            Util.PrintList(found);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
