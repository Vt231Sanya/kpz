using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_04
{
    public class Virus
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public List<Virus> Children { get; set; } = new List<Virus>();

        public Virus(string name, string species, double weight, int age)
        {
            Name = name;
            Species = species;
            Weight = weight;
            Age = age;
        }

        public Virus Clone()
        {
            var clone = new Virus(Name, Species, Weight, Age);
            foreach (var child in Children)
            {
                clone.Children.Add(child.Clone());
            }
            return clone;
        }

        public void Display(string indent = "")
        {
            Console.WriteLine($"{indent} {Name} ({Species}) - {Weight}g, {Age} days");
            foreach (var child in Children)
            {
                child.Display(indent + "  ");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Virus parent = new Virus("Alpha", "Corona", 1.2, 30);

            Virus child1 = new Virus("Beta", "Corona", 0.9, 10);
            Virus child2 = new Virus("Gamma", "Corona", 0.8, 8);

            Virus grandChild = new Virus("Delta", "Corona", 0.6, 4);
            child1.Children.Add(grandChild);

            parent.Children.Add(child1);
            parent.Children.Add(child2);

            Console.WriteLine("Оригінал:");
            parent.Display();

            Virus clone = parent.Clone();

            Console.WriteLine("\nКлон:");
            clone.Display();

            Console.WriteLine("\nПеревірка: чи це різні об'єкти?");
            Console.WriteLine("parent == clone: " + (parent == clone));
            Console.WriteLine("parent.Children[0] == clone.Children[0]: " + (parent.Children[0] == clone.Children[0]));
        }
    }
}
