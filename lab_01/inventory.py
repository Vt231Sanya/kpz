from typing import List
from enclosure import Enclosure
from workers import ZooWorker

class Inventory:
    def __init__(self, enclosures: List[Enclosure], workers: List[ZooWorker]):
        self.enclosures = enclosures
        self.workers = workers

    def generate_report(self):
        print("\n=== Інвенторизація ===")
        total_animals = sum(len(e.animals) for e in self.enclosures)
        print(f"Кількість тваринок: {total_animals}")
        print(f"Кількісит вальєрів: {len(self.enclosures)}")
        print(f"Кількість працівників: {len(self.workers)}")
        for enclosure in self.enclosures:
            print(f"- {enclosure.name}: {len(enclosure.animals)} тваринки")