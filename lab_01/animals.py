from abc import ABC, abstractmethod

class Animal(ABC):
    def __init__(self, name: str, age: int):
        self.name = name
        self.age = age

    @abstractmethod
    def make_sound(self):
        pass

    def __str__(self):
        return f"{self.__class__.__name__} named {self.name}, age {self.age}"

class Lion(Animal):
    def make_sound(self):
        return "Рррррр!"

class Cat(Animal):
    def make_sound(self):
        return "Мяу!"

class Penguin(Animal):
    def make_sound(self):
        return "Квак!"