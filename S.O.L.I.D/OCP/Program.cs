using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  static  System.Console;
namespace OCP
{
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large, Luge
    }
    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public  IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size)
                    yield return p;
            }
        }
        public  IEnumerable<Product> FilterByColour(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                    yield return p;
            }
        }
        public IEnumerable<Product> FilterBySizeAndColour(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color && p.Size == size)
                    yield return p;
            }
        }
    }
    
    public class SizeSpeficiation: ISpecification<Product>
    {
        private Size size;
        public SizeSpeficiation(Size size)
        {
            this.size = size;   
        }
        public bool IsSatisfied(Product t)
        {
            return this.size == t.Size;
        }
    }
    public class ColorSpeficiation: ISpecification<Product>
    {
        private Color color;

        public ColorSpeficiation(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product p)
        {
            return this.color == p.Color;
        }
    }

    public  class IAndSpecification<T>:ISpecification<T>
    {
        private ISpecification<T> first, second;

        public IAndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }
        public bool IsSatisfied(T t)
        {
            return this.first.IsSatisfied(t) || this.first.IsSatisfied(t);
        }
    }
    class BetterFilter: IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (var i in items)
            {
                if (specification.IsSatisfied(i))
                    yield return i;
            }
        }
    }
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
    }
    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);
            Product[] products = {apple, tree, house};
            var bf = new BetterFilter();
            WriteLine("Green Products: ");
            foreach (var product in bf.Filter(products, new ColorSpeficiation(Color.Green)))
            {
                WriteLine($"- {product.Name} is Green");
            }
            WriteLine("Large Products: ");
            foreach (var product in bf.Filter(products, new IAndSpecification<Product>(new ColorSpeficiation(Color.Blue), new SizeSpeficiation(Size.Large))))
            {
                WriteLine($"- {product.Name} is Blue and Big");
            }
            ReadLine();
        }
    }
}
