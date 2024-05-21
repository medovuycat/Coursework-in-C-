using System;

public class Transaction
{
    
    public int TransactionID { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Category Category { get; set; }
    public string Type { get; set; } 

    public Transaction(int transactionID, decimal amount, DateTime date, Category category, string type)
    {
        if (amount <= 0)
            throw new ArgumentException("Сума має бути більше нуля.");
        if (category == null)
            throw new ArgumentNullException(nameof(category));
        if (type != "Витрати" && type != "Надходження")
            throw new ArgumentException("Тип має бути 'Витрати' або 'Надходження'.");

        TransactionID = transactionID;
        Amount = amount;
        Date = date;
        Category = category;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} - {Category.Name}: {Amount} ({Type})";
    }
}
