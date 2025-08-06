from animals import Animal

class Food:
    def __init__(self, name: str, quantity_kg: float):
        self.name = name
        self.quantity_kg = quantity_kg

    def feed(self, animal: Animal):
        print(f"{animal.name} має їжу - {self.name}")