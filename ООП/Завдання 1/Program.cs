/* 
    Цей код створює систему для побудови таблиці значень різних функцій, використовуючи інтерфейси та поліморфізм. 
    Інтерфейс Funkcia визначає метод Value, який обчислює значення функції в заданій точці. 
    Класи Funkcia1, Funkcia2 і Funkcia3 реалізують цей інтерфейс для функцій x^2, sin x і cos x. 
    Клас Tablica приймає функцію, і створює таблицю її значень на зазначеному проміжку. 
*/

public interface Funkcia
{
    // обчислення значення функції для заданого x
    double Value(double x);
}

// реалізує функцію f(x) = x^2
public class Funkcia1 : Funkcia
{
    public double Value(double x)
    {
        return x * x;
    }
}

// реалізує функцію f(x) = sin(x)
public class Funkcia2 : Funkcia
{
    public double Value(double x)
    {
        return Math.Sin(x);
    }
}

// реалізує функцію f(x) = cos(x)
public class Funkcia3 : Funkcia
{
    public double Value(double x)
    {
        return Math.Cos(x);
    }
}

// клас для виведення таблиці значень функції
public class Tablica
{
    // для зберігання об'єкта функції, що реалізує інтерфейс IFunkcia
    private Funkcia _funkcia;

    // конструктор класу Tablica, який приймає об'єкт функції
    public Tablica(Funkcia funkcia)
    {
        _funkcia = funkcia; // зберігаємо передану функцію
    }

    // для виведення таблиці значень функції на інтервалі xmin, xmax з n значеннями
    public void ShowTable(double xmin, double xmax, int N)
    {
        // обчислення кроку між значеннями x
        double step = (xmax - xmin) / (N - 1);

        // заголовок таблиці
        Console.WriteLine("x\tf(x)");

        // для обчислення і виведення значень функції для кожного x
        for (int i = 0; i < N; i++)
        {
            double x = xmin + i * step; // обчислення x для поточної ітерації
            double fx = _funkcia.Value(x); // обчислення f(x) для цього x
            Console.WriteLine($"{x:F2}\t{fx:F2}"); // виведення значень x та f(x) з точністю до двох знаків
        }
    }
}

class Program
{
    static void Main()
    {
        Tablica tablica1 = new Tablica(new Funkcia1());
        Console.WriteLine("Таблиця для f(x) = x^2:");
        tablica1.ShowTable(0, 10, 5); 
        Console.WriteLine();

        Tablica tablica2 = new Tablica(new Funkcia2());
        Console.WriteLine("Таблиця для f(x) = sin(x):");
        tablica2.ShowTable(0, Math.PI, 5);
        Console.WriteLine();

        Tablica tablica3 = new Tablica(new Funkcia3());
        Console.WriteLine("Таблиця для f(x) = cos(x):");
        tablica3.ShowTable(0, Math.PI, 5); 
    }
}