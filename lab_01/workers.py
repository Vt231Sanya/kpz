from enclosure import Enclosure
from food import Food

class ZooWorker:
    def __init__(self, name: str, role: str):
        self.name = name
        self.role = role

    def feed_animals(self, enclosure: Enclosure, food: Food):
        for animal in enclosure.animals:
            food.feed(animal)

    def __str__(self):
        return f"{self.role}: {self.name}"
