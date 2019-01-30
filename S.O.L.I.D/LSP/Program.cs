using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace LSP
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }
        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
    class Program
    {
        static public int Area(Rectangle r) => r.Height * r.Width;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle();
            rc.Height = 9;
            rc.Width = 10;
            WriteLine($"{rc} has area {Area(rc)}");

            Rectangle square = new Square();
            square.Width = 4;
            square.Height = 9;
            WriteLine($"{square} has area {Area(square)}");
            ReadLine();
        }
    }
}
