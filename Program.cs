using System;

namespace LabWork
{
// ----------------------------------------------------
// 1. Структура для представлення точки (вершини)
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
// 2. Базовий клас: Triangle (Трикутник)
// ----------------------------------------------------
public class Triangle
{
    protected Point[] vertices; 
    protected int vertexCount = 3;

    public Triangle(Point p1, Point p2, Point p3)
    {
        vertices = new Point[vertexCount];
        SetVertices(p1, p2, p3);
    }

    public virtual void SetVertices(params Point[] newVertices)
    {
        if (newVertices.Length < vertexCount)
        {
            throw new ArgumentException($"Трикутник потребує {vertexCount} точок.");
        }

        for (int i = 0; i < vertexCount; i++)
        {
            vertices[i] = newVertices[i];
        }
    }

    public virtual void DisplayVertices()
    {
        Console.WriteLine($"--- Фігура: Трикутник ({vertexCount} вершин) ---");
        for (int i = 0; i < vertexCount; i++)
        {
            Console.WriteLine($"Вершина {i + 1}: ({vertices[i].X}, {vertices[i].Y})");
        }
    }

    public virtual double CalculateArea()
    {
        // Формула площі трикутника за координатами (формула Гаусса)
        double area = 0.5 * Math.Abs(
            vertices[0].X * (vertices[1].Y - vertices[2].Y) +
            vertices[1].X * (vertices[2].Y - vertices[0].Y) +
            vertices[2].X * (vertices[0].Y - vertices[1].Y)
        );
        return area;
    }
}

// ----------------------------------------------------
// 3. Похідний клас: ConvexQuadrilateral (Опуклий чотирикутник)
// ----------------------------------------------------
public class ConvexQuadrilateral : Triangle
{
    private const int QuadCount = 4;

    public ConvexQuadrilateral(Point p1, Point p2, Point p3, Point p4)
        : base(p1, p2, p3)
    {
        vertices = new Point[QuadCount];
        vertexCount = QuadCount;
        SetVertices(p1, p2, p3, p4);
    }

    public override void SetVertices(params Point[] newVertices)
    {
        if (newVertices.Length < QuadCount)
        {
            throw new ArgumentException($"Опуклий чотирикутник потребує {QuadCount} точок.");
        }

        for (int i = 0; i < QuadCount; i++)
        {
            vertices[i] = newVertices[i];
        }
    }

    public override void DisplayVertices()
    {
        Console.WriteLine($"--- Фігура: Опуклий чотирикутник ({vertexCount} вершин) ---");
        for (int i = 0; i < vertexCount; i++)
        {
            Console.WriteLine($"Вершина {i + 1}: ({vertices[i].X}, {vertices[i].Y})");
        }
    }

    public override double CalculateArea()
    {
        // Сумарна площа двох трикутників: 1-2-3 та 1-3-4
        double area123 = 0.5 * Math.Abs(
            vertices[0].X * (vertices[1].Y - vertices[2].Y) +
            vertices[1].X * (vertices[2].Y - vertices[0].Y) +
            vertices[2].X * (vertices[0].Y - vertices[1].Y)
        );

        double area134 = 0.5 * Math.Abs(
            vertices[0].X * (vertices[2].Y - vertices[3].Y) +
            vertices[2].X * (vertices[3].Y - vertices[0].Y) +
            vertices[3].X * (vertices[0].Y - vertices[2].Y)
        );

        return area123 + area134;
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

        Console.WriteLine("## ✍️ Демонстрація поліморфізму");
        Console.WriteLine("Оберіть тип фігури, яку бажаєте створити:");
        Console.WriteLine("1 - Трикутник");
        Console.WriteLine("2 - Опуклий чотирикутник");
        Console.Write("Ваш вибір (1 або 2): ");

        string userChoice = Console.ReadLine();
        Triangle figure;

        if (userChoice == "1")
        {
            Point t1 = new Point(1, 1);
            Point t2 = new Point(4, 5);
            Point t3 = new Point(1, 5);
            figure = new Triangle(t1, t2, t3);
            Console.WriteLine("\nСтворено об'єкт: Трикутник.");
        }
        else if (userChoice == "2")
        {
            Point q1 = new Point(0, 0);
            Point q2 = new Point(6, 0);
            Point q3 = new Point(7, 3);
            Point q4 = new Point(1, 4);
            figure = new ConvexQuadrilateral(q1, q2, q3, q4);
            Console.WriteLine("\nСтворено об'єкт: Опуклий чотирикутник.");
        }
        else
        {
            figure = new Triangle(new Point(0, 0), new Point(1, 0), new Point(0, 1));
            Console.WriteLine("\nНекоректний вибір. Створено об'єкт за замовчуванням: Трикутник.");
        }

        Console.WriteLine("\n" + new string('-', 45));
        figure.DisplayVertices();
        double area = figure.CalculateArea();
        Console.WriteLine($"\n✅ Площа фігури: {area:F2}");
        Console.WriteLine(new string('-', 45));
    }
}
```

}
