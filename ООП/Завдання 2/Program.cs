using System.Text;

/*
    Варіант 8. Садиков Єгор
    Користувач може додавати нові номери, реєструвати клієнтів в кімнати, 
    переглядати всі вільні кімнати та отримувати інформацію про вартість проживання клієнтів. 
    Система складається з трьох основних класів: один для представлення кімнат, другий для 
    збереження інформації про клієнтів, і третій, який управляє додаванням кімнат, реєстрацією клієнтів, 
    а також виведенням інформації про наявність вільних номерів та ціни для конкретних клієнтів. 
    Програма працює в консолі та дозволяє взаємодіяти з користувачем через меню. 
 */

public class Room
{
    // номер кімнати, ціна та стан зайнятості
    public int Number { get; set; }
    public double Price { get; set; }
    public bool IsOccupied { get; set; } // чи зайнята кімната

    public Room(int number, double price)
    {
        Number = number; // ініціалізуємо номер кімнати
        Price = price; // ініціалізуємо ціну кімнати
        IsOccupied = false; // спочатку кімната не зайнята
    }

    // перевизначення методу ToString для виведення інформації про кімнату
    public override string ToString()
    {
        return $"Номер: {Number}, Ціна: {Price}, Зайнятий: {(IsOccupied ? "Так" : "Ні")}";
    }
}

public class Client
{
    // прізвище клієнта та номер заброньованої кімнати
    public string LastName { get; set; }
    public int BookedRoomNumber { get; set; }

    public Client(string lastName, int bookedRoomNumber)
    {
        LastName = lastName; // ініціалізація прізвища
        BookedRoomNumber = bookedRoomNumber; // ініціалізація номеру заброньованої кімнати
    }

    // перевизначення методу ToString для виведення інформації про клієнта
    public override string ToString()
    {
        return $"Клієнт: {LastName}, Замовлений номер: {BookedRoomNumber}";
    }
}

public class HotelSystem
{
    // список кімнат та словник клієнтів, де ключ — прізвище клієнта
    private List<Room> rooms = new List<Room>(); // для зберігання всіх кімнат
    private Dictionary<string, Client> clients = new Dictionary<string, Client>(); // для зберігання клієнтів по прізвищу

    // метод для додавання нового номера до системи
    public void AddRoom(int number, double price)
    {
        rooms.Add(new Room(number, price)); // створюємо нову кімнату та додаємо до списку
        Console.WriteLine($"Додано номер {number} з ціною {price} грн.");
    }

    // метод для реєстрації клієнта в кімнату
    public void RegisterClient(string lastName, int roomNumber)
    {
        var room = rooms.FirstOrDefault(r => r.Number == roomNumber); // шукаємо кімнату по номеру
        if (room == null)
        {
            Console.WriteLine("Номер не знайдено."); // якщо кімната не знайдена
            return;
        }

        if (room.IsOccupied)
        {
            Console.WriteLine("Номер уже зайнятий."); // якщо кімната вже зайнята
            return;
        }

        room.IsOccupied = true; // позначаємо номер як зайнятий
        clients[lastName] = new Client(lastName, roomNumber); // реєструємо клієнта в системі
        Console.WriteLine($"Клієнт, {lastName} зареєстрований у номері {roomNumber}.");
    }

    // метод для показу всіх вільних кімнат
    public void ShowAvailableRooms()
    {
        var availableRooms = rooms.Where(r => !r.IsOccupied); // фільтруємо кімнати, які не зайняті
        Console.WriteLine("Вільні номери:");
        foreach (var room in availableRooms)
        {
            Console.WriteLine(room); // виводимо інформацію про кожну вільну кімнату
        }
    }

    // метод для отримання ціни проживання для конкретного клієнта
    public void GetClientRoomPrice(string lastName)
    {
        if (!clients.TryGetValue(lastName, out var client)) // шукаємо клієнта в словнику
        {
            Console.WriteLine("Клієнта не знайдено."); // якщо клієнт не знайдений
            return;
        }

        var room = rooms.FirstOrDefault(r => r.Number == client.BookedRoomNumber); // шукаємо номер, який забронював клієнт
        if (room != null)
        {
            Console.WriteLine($"Клієнт, {lastName} проживає у номері {room.Number}. Ціна: {room.Price} грн.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8; // встановлюємо коректну кодування для виведення українських символів

        var hotelSystem = new HotelSystem(); // створюємо нову систему готелю

        while (true)
        {
            // основне меню
            Console.WriteLine("");
            Console.WriteLine("1. Додати номер");
            Console.WriteLine("2. Зареєструвати клієнта");
            Console.WriteLine("3. Показати вільні номери");
            Console.WriteLine("4. Отримати вартість проживання клієнта");
            Console.WriteLine("5. Вийти");
            Console.Write("Оберіть дію: ");

            string choice = Console.ReadLine(); // отримуємо вибір користувача

            switch (choice)
            {
                case "1":
                    Console.Write("Введіть номер кімнати: ");
                    int number = int.Parse(Console.ReadLine()); // отримуємо номер кімнати
                    Console.Write("Введіть ціну за номер: ");
                    double price = double.Parse(Console.ReadLine()); // отримуємо ціну за номер
                    hotelSystem.AddRoom(number, price); // додаємо новий номер
                    break;
                case "2":
                    Console.Write("Введіть прізвище клієнта: ");
                    string lastName = Console.ReadLine(); // отримуємо прізвище клієнта
                    Console.Write("Введіть номер кімнати: ");
                    int roomNumber = int.Parse(Console.ReadLine()); // отримуємо номер кімнати
                    hotelSystem.RegisterClient(lastName, roomNumber); // реєструємо клієнта
                    break;
                case "3":
                    hotelSystem.ShowAvailableRooms(); // показуємо всі вільні кімнати
                    break;
                case "4":
                    Console.Write("Введіть прізвище клієнта: ");
                    string clientName = Console.ReadLine(); // отримуємо прізвище клієнта
                    hotelSystem.GetClientRoomPrice(clientName); // отримуємо ціну проживання клієнта
                    break;
                case "5":
                    Console.WriteLine("Вихід з програми");
                    return; // виходимо з програми
                default:
                    Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                    break;
            }
        }
    }
}