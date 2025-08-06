using System;
using System.Text;

interface IRenderer
{
    void Render(string shapeName);
}

class VectorRenderer : IRenderer
{
    public void Render(string shapeName)
    {
        Console.WriteLine($"Drawing {shapeName} as vector graphics.");
    }
}

class RasterRenderer : IRenderer
{
    public void Render(string shapeName)
    {
        Console.WriteLine($"Drawing {shapeName} as pixels.");
    }
}

abstract class Shape
{
    protected IRenderer renderer;

    public Shape(IRenderer renderer)
    {
        this.renderer = renderer;
    }

    public abstract void Draw();
}

class Circle : Shape
{
    public Circle(IRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        renderer.Render("Circle");
    }
}

class Square : Shape
{
    public Square(IRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        renderer.Render("Square");
    }
}

class Triangle : Shape
{
    public Triangle(IRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        renderer.Render("Triangle");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;

        IRenderer vector = new VectorRenderer();
        IRenderer raster = new RasterRenderer();

        Shape circleVector = new Circle(vector);
        Shape squareRaster = new Square(raster);
        Shape triangleVector = new Triangle(vector);
        Shape circleRaster = new Circle(raster);

        Console.WriteLine("Займаємось малюванням\n");

        circleVector.Draw();  
        squareRaster.Draw();   
        triangleVector.Draw(); 
        circleRaster.Draw();  
    }
}
