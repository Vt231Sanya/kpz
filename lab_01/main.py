from animals import Lion, Cat, Penguin
from enclosure import Enclosure
from food import Food
from workers import ZooWorker
from inventory import Inventory

def main():
    lion = Lion("Сімба", 5)
    cat = Cat("Шавєрмік", 10)
    cat2 = Cat("Фубля", 5)
    penguin = Penguin("Лоло", 3)

    dry_enclosure = Enclosure("Саванна1", "Саванна", 2)
    aquatic_enclosure = Enclosure("Басейн1", "Басейн", 3)
    home_enclosure = Enclosure("Кімната1", "Кімната", 3)

    dry_enclosure.add_animal(lion)
    home_enclosure.add_animal(cat)
    home_enclosure.add_animal(cat2)
    aquatic_enclosure.add_animal(penguin)

    worker1 = ZooWorker("Олександр", "Доглядач")
    worker2 = ZooWorker("Кіллуа", "Годувач")

    meat = Food("М'ясо", 20.0)
    fish = Food("Риба", 10.0)

    worker2.feed_animals(dry_enclosure, meat)
    worker2.feed_animals(aquatic_enclosure, fish)

    inventory = Inventory([dry_enclosure, aquatic_enclosure, home_enclosure], [worker1, worker2])
    inventory.generate_report()

if __name__ == "__main__":
    main()