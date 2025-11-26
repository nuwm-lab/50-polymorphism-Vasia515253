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
        // Поле для зберігання вершин (3 для трикутника)
        protected Point[] vertices; 
        protected int vertexCount = 3;

        // Конструктор
        public Triangle(Point p1, Point p2, Point p3)
        {
            vertices = new Point[vertexCount];
            SetVertices(p1, p2, p3);
        }

        /// <summary>
        /// Віртуальний метод для задання координат вершин.
        /// </summary>
        public virtual void SetVertices(params Point[] newVertices)
        {
            if (newVertices.Length >= vertexCount)
            {
                for (int i = 0; i < vertexCount; i++)
                {
                    vertices[i] = newVertices[i];
                }
            }
        }

        /// <summary>
        /// Віртуальний метод для виведення координат вершин на екран.
        /// </summary>
        public virtual void DisplayVertices()
        {
            Console.WriteLine($"--- Фігура: Трикутник ({vertexCount} вершин) ---");
            for (int i = 0; i < vertexCount; i++)
            {
                Console.WriteLine($"Вершина {i + 1}: ({vertices[i].X}, {vertices[i].Y})");
            }
        }

        /// <summary>
        /// Віртуальний метод для обчислення площі.
        /// Використовується для демонстрації поліморфізму.
        /// </summary>
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
        // Чотирикутник має 4 вершини
        private const int QUAD_COUNT = 4;

        // Перевизначаємо конструктор, щоб приймати 4 точки
        public ConvexQuadrilateral(Point p1, Point p2, Point p3, Point p4) 
            : base(p1, p2, p3) // Викликаємо конструктор базового класу
        {
            // Переініціалізуємо поля для 4 вершин
            vertices = new Point[QUAD_COUNT];
            vertexCount = QUAD_COUNT;
            SetVertices(p1, p2, p3, p4);
        }

        /// <summary>
        /// Перевизначений метод для задання координат 4 вершин.
        /// </summary>
        public override void SetVertices(params Point[] newVertices)
        {
            if (newVertices.Length >= QUAD_COUNT)
            {
                for (int i = 0; i < QUAD_COUNT; i++)
                {
                    vertices[i] = newVertices[i];
                }
            }
        }

        /// <summary>
        /// Перевизначений метод для виведення координат 4 вершин на екран.
        /// </summary>
        public override void DisplayVertices()
        {
            Console.WriteLine($"--- Фігура: Опуклий чотирикутник ({vertexCount} вершин) ---");
            for (int i = 0; i < vertexCount; i++)
            {
                Console.WriteLine($"Вершина {i + 1}: ({vertices[i].X}, {vertices[i].Y})");
            }
        }

        /// <summary>
        /// Перевизначений метод для обчислення площі чотирикутника 
        /// (як сума площ двох трикутників: 1-2-3 та 1-3-4).
        /// </summary>
        public override double CalculateArea()
        {
            // Площа трикутника 1-2-3 (використовуємо перші 3 точки: vertices[0], vertices[1], vertices[2])
            double area123 = 0.5 * Math.Abs(
                vertices[0].X * (vertices[1].Y - vertices[2].Y) +
                vertices[1].X * (vertices[2].Y - vertices[0].Y) +
                vertices[2].X * (vertices[0].Y - vertices[1].Y)
            );

            // Площа трикутника 1-3-4 (використовуємо точки: vertices[0], vertices[2], vertices[3])
            double area134 = 0.5 * Math.Abs(
                vertices[0].X * (vertices[2].Y - vertices[3].Y) +
                vertices[2].X * (vertices[3].Y - vertices[0].Y) +
                vertices[3].X * (vertices[0].Y - vertices[2].Y)
            );

            return area123 + area134;
        }
    }

    // ----------------------------------------------------
    // 4. Головна програма та демонстрація поліморфізму
    // ----------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("## ✍️ Демонстрація поліморфізму");
            Console.WriteLine("Оберіть тип фігури, яку бажаєте створити:");
            Console.WriteLine("1 - Працювати з Трикутником");
            Console.WriteLine("2 - Працювати з Опуклим чотирикутником");
            Console.Write("Ваш вибір (1 або 2): ");

            string userChoose = Console.ReadLine();
            
            // Покажчик/посилання на екземпляр базового класу (Triangle)
            // Це дозволяє динамічно створити будь-яку похідну фігуру.
            Triangle figure; 

            if (userChoose == "1")
            {
                // Створення об'єкта Трикутник
                Point t1 = new Point(1, 1);
                Point t2 = new Point(4, 5);
                Point t3 = new Point(1, 5); // Прямокутний трикутник, площа 6
                figure = new Triangle(t1, t2, t3);
                Console.WriteLine("\nСтворено об'єкт: Трикутник.");
            }
            else if (userChoose == "2")
            {
                // Створення об'єкта Опуклий чотирикутник
                Point q1 = new Point(0, 0);
                Point q2 = new Point(6, 0);
                Point q3 = new Point(7, 3);
                Point q4 = new Point(1, 4); // Трапеція + трикутник, площа 20.5
                figure = new ConvexQuadrilateral(q1, q2, q3, q4);
                Console.WriteLine("\nСтворено об'єкт: Опуклий чотирикутник.");
            }
            else
            {
                Console.WriteLine("\nНекоректний вибір. Створено об'єкт за замовчуванням: Трикутник (0,0), (1,0), (0,1).");
                figure = new Triangle(new Point(0, 0), new Point(1, 0), new Point(0, 1));
            }

            Console.WriteLine("\n" + new string('-', 45));
            Console.WriteLine("Виклик віртуальних методів через посилання на базовий клас:");
            Console.WriteLine(new string('-', 45));

            // 1. Виклик DisplayVertices() - викликається відповідний override-метод
            figure.DisplayVertices();

            // 2. Виклик CalculateArea() - викликається відповідний override-метод
            double area = figure.CalculateArea();
            Console.WriteLine($"\n✅ Обчислена площа фігури: {area:F2}");
            
            Console.WriteLine(new string('-', 45));
            // Console.ReadKey(); // Залиште, якщо запускаєте без середовища
        }
    }
}
