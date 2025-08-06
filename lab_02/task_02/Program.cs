using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_02
{
    public interface ILaptop
    {
        string GetInfo();
    }

    public interface INetbook
    {
        string GetInfo();
    }

    public interface IEBook
    {
        string GetInfo();
    }

    public interface ISmartphone
    {
        string GetInfo();
    }

    public interface ITechFactory
    {
        ILaptop CreateLaptop();
        INetbook CreateNetbook();
        IEBook CreateEBook();
        ISmartphone CreateSmartphone();
    }
    public class BalaxyLaptop : ILaptop
    {
        public string GetInfo() => "Ноутбук Balaxy NotePro";
    }



    public class BalaxyNetbook : INetbook
    {
        public string GetInfo() => "Нетбук Balaxy Compact";
    }

    public class BalaxyEBook : IEBook
    {
        public string GetInfo() => "Електронна книга Balaxy SRead";
    }

    public class BalaxySmartphone : ISmartphone
    {
        public string GetInfo() => "Смартфон Balaxy S30";
    }

    public class IProneLaptop : ILaptop
    {
        public string GetInfo() => "Ноутбук IProneBook Pro 16";
    }

    public class IProneNetbook : INetbook
    {
        public string GetInfo() => "Нетбук IProne MiniAir";
    }



    public class IProneEBook : IEBook
    {
        public string GetInfo() => "Електронна книга iRead Classic";
    }

    public class IProneSmartphone : ISmartphone
    {
        public string GetInfo() => "Смартфон IProne 15 Ultra";
    }

    public class KiaomiLaptop : ILaptop
    {
        public string GetInfo() => "Ноутбук Kiaomi MiBook 15";
    }

    public class KiaomiNetbook : INetbook
    {
        public string GetInfo() => "Нетбук Kiaomi AirLite";
    }

    public class KiaomiEBook : IEBook
    {
        public string GetInfo() => "Електронна книга Kiaomi Reader X";
    }

    public class KiaomiSmartphone : ISmartphone
    {
        public string GetInfo() => "Смартфон Kiaomi ZoomMax";
    }

    public class IProneFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new IProneLaptop();
        public INetbook CreateNetbook() => new IProneNetbook();
        public IEBook CreateEBook() => new IProneEBook();
        public ISmartphone CreateSmartphone() => new IProneSmartphone();
    }

    public class KiaomiFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new KiaomiLaptop();
        public INetbook CreateNetbook() => new KiaomiNetbook();
        public IEBook CreateEBook() => new KiaomiEBook();
        public ISmartphone CreateSmartphone() => new KiaomiSmartphone();
    }

    public class BalaxyFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new BalaxyLaptop();
        public INetbook CreateNetbook() => new BalaxyNetbook();
        public IEBook CreateEBook() => new BalaxyEBook();
        public ISmartphone CreateSmartphone() => new BalaxySmartphone();
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            List<ITechFactory> factories = new List<ITechFactory>
        {
            new IProneFactory(),
            new KiaomiFactory(),
            new BalaxyFactory()
        };

            foreach (var factory in factories)
            {
                Console.WriteLine(factory.CreateLaptop().GetInfo());
                Console.WriteLine(factory.CreateNetbook().GetInfo());
                Console.WriteLine(factory.CreateEBook().GetInfo());
                Console.WriteLine(factory.CreateSmartphone().GetInfo());
                Console.WriteLine("");
            }
        }
    }
}
