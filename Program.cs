using System;

namespace LabWork
{
// ----------------------------------------------------
// 1. Структура для точки
// ----------------------------------------------------
public struct Point
{
public double X { get; set; }
public double Y { get; set; }

```
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}

// ----------------------------------------------------
// 2. Клас Triangle
// ----------------------------------------------------
public class Triangle
{
    // Приватне поле для зберігання вершин
    private Point[] _vertices;

    // Властивість для доступу
    public Point[] Vertices => _vertices;

    protected int VertexCount => 3;

    // Конструктор
    public Triangle(Point p1, Point p2, Point p3)
    {
        _vertices = new Point[VertexCount];
        SetVertices(p1, p2, p3);
    }

    // Віртуальний метод для задання координат
    public virtual void SetVertices(params Point[] points)
    {
        if (points.Length < VertexCount)
            throw new ArgumentException($"Трикутник потребує {VertexCount} точок.");

        for (int i = 0; i < VertexCount; i++)
            _vertices[i] = points[i];
    }

    // Віртуальний метод для виведення координат
    public virtual void PrintVertices()
    {
        Console.WriteLine($"--- Трикутник ({VertexCount} вершин) ---");
        for (int i = 0; i < VertexCount; i++)
            Console.WriteLine($"Вершина {i + 1}: ({_vertices[i].X}, {_vertices[i].Y})");
    }

    // Віртуальний метод для обчислення площі
    public virtual double CalculateArea()
    {
        double area = 0.5 * Math.Abs(
            _vertices[0].X * (_vertices[1].Y - _vertices[2].Y) +
            _vertices[1].X * (_vertices[2].Y - _vertices[0].Y) +
            _vertices[2].X * (_vertices[0].Y - _vertices[1].Y)
        );
        return area;
    }
}

// ----------------------------------------------------
// 3. Клас ConvexQuadrilateral
// ----------------------------------------------------
public class ConvexQuadrilateral : Triangle
{
    private Point[] _quadVertices;
    private const int QuadCount = 4;

    public ConvexQuadrilateral(Point p1, Point p2, Point p3, Point p4)
        : base(p1, p2, p3)
    {
        _quadVertices = new Point[QuadCount];
        SetVertices(p1, p2, p3, p4);
    }

    // Перевизначений метод для чотирьох вершин
    public void SetVertices(Point p1, Point p2, Point p3, Point p4)
    {
        _quadVertices[0] = p1;
        _quadVertices[1] = p2;
        _quadVertices[2] = p3;
        _quadVertices[3] = p4;
    }

    public override void PrintVertices()
    {
        Console.WriteLine($"--- Опуклий чотирикутник ({QuadCount} вершин) ---");
        for (int i = 0; i < QuadCount; i++)
            Console.WriteLine($"Вершина {i + 1}: ({_quadVertices[i].X}, {_quadVertices[i].Y})");
    }

    public override double CalculateArea()
    {
        // Розбиваємо чотирикутник на два трикутники: 0-1-2 та 0-2-3
        double area1 = 0.5 * Math.Abs(
            _quadVertices[0].X * (_quadVertices[1].Y - _quadVertices[2].Y) +
            _quadVertices[1].X * (_quadVertices[2].Y - _quadVertices[0].Y) +
            _quadVertices[2].X * (_quadVertices[0].Y - _quadVertices[1].Y)
        );

        double area2 = 0.5 * Math.Abs(
            _quadVertices[0].X * (_quadVertices[2].Y - _quadVertices[3].Y) +
            _quadVertices[2].X * (_quadVertices[3].Y - _quadVertices[0].Y) +
            _quadVertices[3].X * (_quadVertices[0].Y - _quadVertices[2].Y)
        );

        return area1 + area2;
    }
}

// ----------------------------------------------------
// 4. Головна програма
// ----------------------------------------------------
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Демонстрація поліморфізму
        Console.WriteLine("Оберіть тип фігури:");
        Console.WriteLine("1 - Трикутник");
        Console.WriteLine("2 - Опуклий чотирикутник");
        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine();

        Triangle figure;

        if (choice == "1")
        {
            figure = new Triangle(
                new Point(0, 0),
                new Point(3, 0),
                new Point(0, 4)
            );
        }
        else if (choice == "2")
        {
            figure = new ConvexQuadrilateral(
                new Point(0, 0),
                new Point(4, 0),
                new Point(5, 3),
                new Point(1, 4)
            );
        }
        else
        {
            Console.WriteLine("Некоректний вибір. Створюємо трикутник за замовчуванням.");
            figure = new Triangle(
                new Point(0, 0),
                new Point(1, 0),
                new Point(0, 1)
            );
        }

        figure.PrintVertices();
        Console.WriteLine($"\nПлоща фігури: {figure.CalculateArea():F2}");
    }
}
```

}
