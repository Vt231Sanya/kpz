using System;
using System.Collections.Generic;

namespace lab_02
{
    public interface ISubscription
    {
        decimal GetMonthlyFee();
        int GetMinPeriod();
        List<string> GetChannels();
        string GetFeatures();
    }

    public interface ISubscriptionCreator
    {
        ISubscription CreateSubscription(string type);
    }

    public class EducationalSubscription : ISubscription
    {
        public decimal GetMonthlyFee() => 3.99m;
        public int GetMinPeriod() => 6;
        public List<string> GetChannels()
        {
            return new List<string> { "Наука", "Історія", "Документальні" };
        }

        public string GetFeatures()
        {
            return "Освітня: Навчальний контент, 2 пристрої";
        }
    }

    public class DomesticSubscription : ISubscription
    {
        public decimal GetMonthlyFee() => 5.99m;
        public int GetMinPeriod() => 1;
        public List<string> GetChannels()
        {
            return new List<string> { "Новини", "Розваги", "Дитячі" };
        }

        public string GetFeatures()
        {
            return "Домашня: Базові канали, 1 пристрій";
        }
    }

    public class PremiumSubscription : ISubscription
    {
        public decimal GetMonthlyFee() => 12.99m;
        public int GetMinPeriod() => 1;
        public List<string> GetChannels()
        {
            return new List<string> { "Усі", "Підтримка 4K", "Спорт", "Міжнародні" };
        }

        public string GetFeatures()
        {
            return "Преміум: Увесь контент, 4 пристрої, офлайн режим";
        }
    }

    public class WebSite : ISubscriptionCreator
    {
        public ISubscription CreateSubscription(string type)
        {
            if (type == "Domestic") return new DomesticSubscription();
            if (type == "Educational") return new EducationalSubscription();
            if (type == "Premium") return new PremiumSubscription();
            throw new ArgumentException("Невідомий тип підписки");
        }
    }

    public class MobileApp : ISubscriptionCreator
    {
        public ISubscription CreateSubscription(string type)
        {
            Console.WriteLine("Створено через мобільний додаток");
            return new WebSite().CreateSubscription(type);
        }
    }

    public class ManagerCall : ISubscriptionCreator
    {
        public ISubscription CreateSubscription(string type)
        {
            Console.WriteLine("Створено через дзвінок менеджера зі знижкою!");
            return new WebSite().CreateSubscription(type);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ISubscriptionCreator creator;

            creator = new WebSite();
            var sub1 = creator.CreateSubscription("Premium");
            Console.WriteLine(sub1.GetFeatures());

            creator = new MobileApp();
            var sub2 = creator.CreateSubscription("Educational");
            Console.WriteLine(sub2.GetFeatures());

            creator = new ManagerCall();
            var sub3 = creator.CreateSubscription("Domestic");
            Console.WriteLine(sub3.GetFeatures());
        }
    }
}
