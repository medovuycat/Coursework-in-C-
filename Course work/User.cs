using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class User
{
    public int UserID { get; set; }
    public string Name { get; set; }
    public List<Transaction> Transactions { get; set; }

    public User(int userID, string name)
    {
        UserID = userID;
        Name = name;
        Transactions = new List<Transaction>();
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
    }

    public decimal GetTotalExpenses()
    {
        return Transactions.Where(t => t.Type == "Витрати").Sum(t => t.Amount);
    }

    public decimal GetTotalIncome()
    {
        return Transactions.Where(t => t.Type == "Надходження").Sum(t => t.Amount);
    }

    public decimal GetNetBalance()
    {
        return GetTotalIncome() - GetTotalExpenses();
    }

    public List<Transaction> GetTransactionsByMonth(int month, int year)
    {
        return Transactions.Where(t => t.Date.Month == month && t.Date.Year == year).ToList();
    }

    public void SaveToFile(string filePath)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(UserID);
            writer.WriteLine(Name);
            writer.WriteLine(Transactions.Count);
            foreach (var transaction in Transactions)
            {
                writer.WriteLine(transaction.TransactionID);
                writer.WriteLine(transaction.Amount);
                writer.WriteLine(transaction.Date);
                writer.WriteLine(transaction.Category.CategoryID);
                writer.WriteLine(transaction.Category.Name);
                writer.WriteLine(transaction.Category.Description);
                writer.WriteLine(transaction.Type);
            }
        }
    }

    public void LoadFromFile(string filePath)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Файл не знайден");

        using (StreamReader reader = new StreamReader(filePath))
        {
            UserID = int.Parse(reader.ReadLine());
            Name = reader.ReadLine();
            int transactionCount = int.Parse(reader.ReadLine());

            Transactions.Clear();
            for (int i = 0; i < transactionCount; i++)
            {
                int transactionID = int.Parse(reader.ReadLine());
                decimal amount = decimal.Parse(reader.ReadLine());
                DateTime date = DateTime.Parse(reader.ReadLine());
                int categoryID = int.Parse(reader.ReadLine());
                string categoryName = reader.ReadLine();
                string categoryDescription = reader.ReadLine();
                string type = reader.ReadLine();

                Category category = new Category(categoryID, categoryName, categoryDescription);
                Transaction transaction = new Transaction(transactionID, amount, date, category, type);
                Transactions.Add(transaction);
            }
        }
    }
}
