from typing import List
from animals import Animal

class Enclosure:
    def __init__(self, name: str, enclosure_type: str, capacity: int):
        self.name = name
        self.enclosure_type = enclosure_type
        self.capacity = capacity
        self.animals: List[Animal] = []

    def add_animal(self, animal: Animal):
        if len(self.animals) < self.capacity:
            self.animals.append(animal)
            print(f"{animal.name} додано до {self.name}")
        else:
            print(f"{self.name} вже повний!")

    def list_animals(self):
        return [str(animal) for animal in self.animals]