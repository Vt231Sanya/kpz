using System;
using System.Text;

interface IHero
{
    string GetStats();
}

class Warrior : IHero
{
    public string GetStats() => "Warrior with base stats";
}

class Mage : IHero
{
    public string GetStats() => "Mage with base stats";
}

class Palladin : IHero
{
    public string GetStats() => "Palladin with base stats";
}

abstract class HeroDecorator : IHero
{
    protected IHero _hero;

    public HeroDecorator(IHero hero)
    {
        _hero = hero;
    }

    public virtual string GetStats()
    {
        return _hero.GetStats();
    }
}

class Armor : HeroDecorator
{
    public Armor(IHero hero) : base(hero) { }

    public override string GetStats()
    {
        return base.GetStats() + " + Armor (+52 defense)";
    }
}

class Weapon : HeroDecorator
{
    public Weapon(IHero hero) : base(hero) { }

    public override string GetStats()
    {
        return base.GetStats() + " + Weapon (+69 attack)";
    }
}

class Artifact : HeroDecorator
{
    public Artifact(IHero hero) : base(hero) { }

    public override string GetStats()
    {
        return base.GetStats() + " + Artifact (+666 magic)";
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;

        IHero warrior = new Warrior();
        IHero warriorWithItems = new Armor(new Weapon(new Artifact(warrior)));

        IHero mage = new Mage();
        IHero mageWithItems = new Artifact(new Artifact(new Weapon(mage)));

        IHero palladin = new Palladin();
        IHero PalladinWithItem = new Weapon(new Armor(new Artifact(new Artifact(palladin))));

        Console.WriteLine("Герої ->");
        Console.WriteLine("\nWarrior:");
        Console.WriteLine(warriorWithItems.GetStats());

        Console.WriteLine("\nMage:");
        Console.WriteLine(mageWithItems.GetStats());

        Console.WriteLine("\nPalladin:");
        Console.WriteLine(PalladinWithItem.GetStats());
    }
}
