public class Category
{
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Category(int categoryID, string name, string description)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ім’я не може бути нулем або пробілом.");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Опис не може бути нульовим або пробільним.");

        CategoryID = categoryID;
        Name = name;
        Description = description;
    }

    public override string ToString()
    {
        return Name;
    }
}
