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
    // Поле для вершин зроблено protected для повторного використання в похідних класах  
    protected Point[] _vertices;  
    protected int VertexCount => 3;  

    public Triangle(Point p1, Point p2, Point p3)  
    {  
        _vertices = new Point[VertexCount];  
        SetVertices(p1, p2, p3);  
    }  

    public virtual void SetVertices(params Point[] points)  
    {  
        if (points.Length < VertexCount)  
            throw new ArgumentException($"Трикутник потребує {VertexCount} точок.");  

        for (int i = 0; i < VertexCount; i++)  
            _vertices[i] = points[i];  
    }  

    public virtual void PrintVertices()  
    {  
        Console.WriteLine($"--- Трикутник ({VertexCount} вершин) ---");  
        for (int i = 0; i < VertexCount; i++)  
            Console.WriteLine($"Вершина {i + 1}: ({_vertices[i].X}, {_vertices[i].Y})");  
    }  

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
    private const int QuadCount = 4;  

    public ConvexQuadrilateral(Point p1, Point p2, Point p3, Point p4)  
        : base(p1, p2, p3)  
    {  
        // Використовуємо базовий масив _vertices для 4 точок  
        Array.Resize(ref _vertices, QuadCount);  
        SetVertices(p1, p2, p3, p4);  
    }  

    // Override для демонстрації поліморфізму і перевірки опуклості  
    public override void SetVertices(params Point[] points)  
    {  
        if (points.Length < QuadCount)  
            throw new ArgumentException($"Опуклий чотирикутник потребує {QuadCount} точок.");  

        if (!IsConvex(points))  
            throw new ArgumentException("Чотирикутник не є опуклим.");  

        for (int i = 0; i < QuadCount; i++)  
            _vertices[i] = points[i];  
    }  

    public override void PrintVertices()  
    {  
        Console.WriteLine($"--- Опуклий чотирикутник ({QuadCount} вершин) ---");  
        for (int i = 0; i < QuadCount; i++)  
            Console.WriteLine($"Вершина {i + 1}: ({_vertices[i].X}, {_vertices[i].Y})");  
    }  

    public override double CalculateArea()  
    {  
        // Розбиваємо на два трикутники: 0-1-2 та 0-2-3  
        double area1 = 0.5 * Math.Abs(  
            _vertices[0].X * (_vertices[1].Y - _vertices[2].Y) +  
            _vertices[1].X * (_vertices[2].Y - _vertices[0].Y) +  
            _vertices[2].X * (_vertices[0].Y - _vertices[1].Y)  
        );  

        double area2 = 0.5 * Math.Abs(  
            _vertices[0].X * (_vertices[2].Y - _vertices[3].Y) +  
            _vertices[2].X * (_vertices[3].Y - _vertices[0].Y) +  
            _vertices[3].X * (_vertices[0].Y - _vertices[2].Y)  
        );  

        return area1 + area2;  
    }  

    private bool IsConvex(Point[] points)  
    {  
        bool? sign = null;  
        int n = points.Length;  
        for (int i = 0; i < n; i++)  
        {  
            double dx1 = points[(i + 1) % n].X - points[i].X;  
            double dy1 = points[(i + 1) % n].Y - points[i].Y;  
            double dx2 = points[(i + 2) % n].X - points[(i + 1) % n].X;  
            double dy2 = points[(i + 2) % n].Y - points[(i + 1) % n].Y;  
            double cross = dx1 * dy2 - dy1 * dx2;  
            if (cross != 0)  
            {  
                if (!sign.HasValue)  
                    sign = cross > 0;  
                else if (sign.Value != (cross > 0))  
                    return false;  
            }  
        }  
        return true;  
    }  
}  

// ----------------------------------------------------  
// 4. Головна програма з демонстрацією поліморфізму  
// ----------------------------------------------------  
class Program  
{  
    static void Main(string[] args)  
    {  
        Console.OutputEncoding = System.Text.Encoding.UTF8;  

        var figures = new List<Triangle>  
        {  
            new Triangle(new Point(0,0), new Point(3,0), new Point(0,4)),  
            new ConvexQuadrilateral(new Point(0,0), new Point(4,0), new Point(5,3), new Point(1,4))  
        };  

        foreach (var figure in figures)  
        {  
            figure.PrintVertices();  
            Console.WriteLine($"Площа: {figure.CalculateArea():F2}\n");  
        }  
    }  
}  
