using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_05
{

    public class GameCharacter
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Physique { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string Clothes { get; set; }
        public List<string> Inventory { get; set; }
        public List<string> Deeds { get; set; }

        public GameCharacter()
        {
            Inventory = new List<string>();
            Deeds = new List<string>();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Ім'я {Name}");
            Console.WriteLine($"Зріст: {Height}");
            Console.WriteLine($"Статура: {Physique}");
            Console.WriteLine($"Колір волосся: {HairColor}");
            Console.WriteLine($"Колір очей: {EyeColor}");
            Console.WriteLine($"Одяг: {Clothes}");
            Console.WriteLine("Інвентар: " + string.Join(", ", Inventory));
            Console.WriteLine("Дії: " + string.Join(",\n", Deeds));
            Console.WriteLine();
        }
    }

    // Інтерфейс білдера
    public interface ICharacterBuilder
    {
        ICharacterBuilder SetName(string name);
        ICharacterBuilder SetHeight(string height);
        ICharacterBuilder SetPhysique(string physique);
        ICharacterBuilder SetHairColor(string color);
        ICharacterBuilder SetEyeColor(string color);
        ICharacterBuilder SetClothes(string clothes);
        ICharacterBuilder AddInventoryItem(string item);
        ICharacterBuilder AddDeed(string deed);
        GameCharacter Build();
    }

    // Будівельник героя
    public class HeroBuilder : ICharacterBuilder
    {
        private GameCharacter _character;

        public HeroBuilder()
        {
            _character = new GameCharacter();
        }

        public ICharacterBuilder SetName(string name)
        {
            _character.Name = name;
            return this;
        }

        public ICharacterBuilder SetHeight(string height)
        {
            _character.Height = height;
            return this;
        }

        public ICharacterBuilder SetPhysique(string physique)
        {
            _character.Physique = physique;
            return this;
        }

        public ICharacterBuilder SetHairColor(string color)
        {
            _character.HairColor = color;
            return this;
        }

        public ICharacterBuilder SetEyeColor(string color)
        {
            _character.EyeColor = color;
            return this;
        }

        public ICharacterBuilder SetClothes(string clothes)
        {
            _character.Clothes = clothes;
            return this;
        }

        public ICharacterBuilder AddInventoryItem(string item)
        {
            _character.Inventory.Add(item);
            return this;
        }

        public ICharacterBuilder AddDeed(string deed)
        {
            _character.Deeds.Add("Добра справа: " + deed);
            return this;
        }

        public GameCharacter Build()
        {
            return _character;
        }
    }

    // Будівельник ворога
    public class EnemyBuilder : ICharacterBuilder
    {
        private GameCharacter _character;

        public EnemyBuilder()
        {
            _character = new GameCharacter();
        }

        public ICharacterBuilder SetName(string name)
        {
            _character.Name = name;
            return this;
        }

        public ICharacterBuilder SetHeight(string height)
        {
            _character.Height = height;
            return this;
        }

        public ICharacterBuilder SetPhysique(string physique)
        {
            _character.Physique = physique;
            return this;
        }

        public ICharacterBuilder SetHairColor(string color)
        {
            _character.HairColor = color;
            return this;
        }

        public ICharacterBuilder SetEyeColor(string color)
        {
            _character.EyeColor = color;
            return this;
        }

        public ICharacterBuilder SetClothes(string clothes)
        {
            _character.Clothes = clothes;
            return this;
        }

        public ICharacterBuilder AddInventoryItem(string item)
        {
            _character.Inventory.Add(item);
            return this;
        }

        public ICharacterBuilder AddDeed(string deed)
        {
            _character.Deeds.Add("Зла справа: " + deed);
            return this;
        }

        public GameCharacter Build()
        {
            return _character;
        }
    }

    // Директор
    public class CharacterDirector
    {
        public GameCharacter CreateHero(ICharacterBuilder builder)
        {
            return builder
                .SetName("Альбедо")
                .SetHeight("172 см")
                .SetPhysique("Стрункий")
                .SetHairColor("Світло-біляве")
                .SetEyeColor("Бірюзові")
                .SetClothes("Алхімічний плащ і рукавички")
                .AddInventoryItem("Книга алхімії")
                .AddInventoryItem("Меч з Гео-енергією")
                .AddDeed("Допоміг мешканцям Мондштадта")
                .AddDeed("Вивів формулу трансмутації")
                .Build();
        }

        public GameCharacter CreateEnemy(ICharacterBuilder builder)
        {
            return builder
                .SetName("Темний Альбедо")
                .SetHeight("172 см")
                .SetPhysique("Стрункий")
                .SetHairColor("Світло-біляве")
                .SetEyeColor("Бірюзові")
                .SetClothes("Алхімічний плащ і рукавички")
                .AddInventoryItem("Темна есенція")
                .AddInventoryItem("Проклята формула")
                .AddDeed("Знищив алхімічну лабораторію")
                .AddDeed("Обдурив мешканців")
                .Build();
        }
    }

    // Точка входу
    internal class Program
    {
        static void Main(string[] args)
        {
            // Забезпечення коректного виводу українських символів
            Console.OutputEncoding = Encoding.UTF8;

            CharacterDirector director = new CharacterDirector();

            GameCharacter hero = director.CreateHero(new HeroBuilder());
            GameCharacter enemy = director.CreateEnemy(new EnemyBuilder());

            hero.ShowInfo();
            enemy.ShowInfo();

            Console.ReadKey();
        }
    }
}

