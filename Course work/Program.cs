using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string filePath = "user_nikita.txt";
        List<Category> categories = new List<Category>
        {
            new Category(1, "Продукти", "Витрати на продукти"),
            new Category(2, "Зарплата", "Місячна зарплата"),
            new Category(3, "Розваги та спорт", "Витрати на ігри, спортивні товари, перегляд фільмів, тощо"),
            new Category(4, "Краса та здоров'я", "Витрати в аптеках та салонах краси"),
            new Category(5, "Тварини", "Витрати на зоотовари"),
            new Category(6, "Кафе та ресторани", "Витрати на кафе та ресторани"),
            new Category(7, "Перекази на картку", "Перекази на інші картки")
        };

        User user;
        if (File.Exists(filePath))
        {
            user = new User(1, "Нікіта");
            user.LoadFromFile(filePath);
        }
        else
        {
            user = new User(1, "Нікіта");
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Трекер особистих фінансів");
            Console.WriteLine("1. Додати транзакцію");
            Console.WriteLine("2. Перегляд транзакцій");
            Console.WriteLine("3. Перегляд транзакцій за датою");
            Console.WriteLine("4. Керування категоріями");
            Console.WriteLine("5. Переглянути баланс");
            Console.WriteLine("6. Зберегти та вийти");
            Console.Write("Виберіть опцію: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTransaction(user, categories);
                    break;
                case "2":
                    ViewTransactions(user);
                    break;
                case "3":
                    ViewTransactionsByDate(user);
                    break;
                case "4":
                    ManageCategories(categories);
                    break;
                case "5":
                    ViewBalance(user);
                    break;
                case "6":
                    user.SaveToFile(filePath);
                    return;
                default:
                    Console.WriteLine("Недійсний варіант. Будь ласка спробуйте ще раз..");
                    break;
            }

            Console.WriteLine("\nНатисніть будь-яку кнопку, щоб продовжити...");
            Console.ReadKey();
        }
    }

    static void AddTransaction(User user, List<Category> categories)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        try
        {
            Console.Clear();
            Console.WriteLine("Додати транзакцію");

            Console.Write("Сума: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Дата (YYYY-MM-DD): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Виберіть категорію:");
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }
            int categoryChoice = int.Parse(Console.ReadLine());
            Category category = categories[categoryChoice - 1];

            Console.Write("Тип (Витрати/Надходження): ");
            string type = Console.ReadLine();

            int transactionID = user.Transactions.Count + 1;
            Transaction transaction = new Transaction(transactionID, amount, date, category, type);
            user.AddTransaction(transaction);

            Console.WriteLine("Трансакцію додано успішно");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void ViewTransactions(User user)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Транзакції:");
        foreach (var transaction in user.Transactions)
        {
            Console.WriteLine(transaction);
        }
    }

    static void ViewTransactionsByDate(User user)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Перегляд транзакцій за датою");

        Console.Write("Введіть місяць: ");
        int month = int.Parse(Console.ReadLine());

        Console.Write("Введіть рік: ");
        int year = int.Parse(Console.ReadLine());

        var transactions = user.GetTransactionsByMonth(month, year);

        if (transactions.Count == 0)
        {
            Console.WriteLine("Не знайдено транзакцій за вказаний період.");
        }
        else
        {
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }
    }

    static void ViewBalance(User user)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Підсумок балансу:");
        Console.WriteLine($"Загальні витрати: {user.GetTotalExpenses()}");
        Console.WriteLine($"Загальний дохід: {user.GetTotalIncome()}");
        Console.WriteLine($"Чистий баланс: {user.GetNetBalance()}");
    }

    static void ManageCategories(List<Category> categories)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Керування категоріями");
            Console.WriteLine("1. Додати категорію");
            Console.WriteLine("2. Видалити категорію");
            Console.WriteLine("3. Редагувати категорію");
            Console.WriteLine("4. Назад до головного меню");
            Console.Write("Виберіть опцію: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddCategory(categories);
                    break;
                case "2":
                    DeleteCategory(categories);
                    break;
                case "3":
                    EditCategory(categories);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Недійсний варіант. Будь ласка спробуйте ще раз.");
                    break;
            }

            Console.WriteLine("\nНатисніть будь-яку кнопку, щоб продовжити...");
            Console.ReadKey();
        }
    }

    static void AddCategory(List<Category> categories)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Додати категорію");

        Console.Write("Категорія ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Назва: ");
        string name = Console.ReadLine();

        Console.Write("Опис: ");
        string description = Console.ReadLine();

        categories.Add(new Category(id, name, description));
        Console.WriteLine("Категорію успішно додано!");
    }

    static void DeleteCategory(List<Category> categories)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Видалити категорію");

        Console.Write("Категорія ID: ");
        int id = int.Parse(Console.ReadLine());

        var category = categories.FirstOrDefault(c => c.CategoryID == id);
        if (category != null)
        {
            categories.Remove(category);
            Console.WriteLine("Категорію успішно видалено!");
        }
        else
        {
            Console.WriteLine("Категорія не знайдена.");
        }
    }

    static void EditCategory(List<Category> categories)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Редагувати категорію");

        Console.Write("Категорія ID: ");
        int id = int.Parse(Console.ReadLine());

        var category = categories.FirstOrDefault(c => c.CategoryID == id);
        if (category != null)
        {
            Console.Write("Нова назва: ");
            string name = Console.ReadLine();

            Console.Write("Новий опис: ");
            string description = Console.ReadLine();

            category.Name = name;
            category.Description = description;
            Console.WriteLine("Категорію успішно відредаговано!");
        }
        else
        {
            Console.WriteLine("Категорія не знайдена.");
        }
    }
}
