using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns
{
    internal class BuilderDemo
    {
        public void Show()
        {
            DrinkBuilder builder = new CoffeeBuilder();
            builder
                .SetMilk()
                .SetSugar()
                .SetSyrup();

            Drink drink = builder.Build();
            Console.WriteLine(drink.Description);

            DrinkDirector drinkDirector = new DrinkDirector(builder);
            drink = drinkDirector.MakeAmericano();
            Console.WriteLine(drink.Description);
            drink = drinkDirector.MakeEspresso();
            Console.WriteLine(drink.Description);
            Console.WriteLine("------------------------");

            Drink drink1 = 
                new CoffeeBuilder()
                    .SetCinnamon()
                    .SetCream()
                    .SetIce()
                    .Build();
            Console.WriteLine(drink1.Description);

            drink1 =
                new CocoaBuilder()
                    .SetSugar()
                    .SetChocko()
                    .Build();
            Console.WriteLine(drink1.Description);

            // без паттерна
            drink = new Coffee();
            drink.HasMilk = true;
            drink.HasSugar = true;
            Console.WriteLine(drink.Description);

            // без паттерна C# style (список инициализации)
            drink = new Cocoa
            {
                HasSugar = true,
                HasIce = true
            };
            Console.WriteLine(drink.Description);
        }
    }

    abstract class Drink
    {
        public string Name { get; private set; }
        public bool HasMilk { get; set; }      = false;
        public bool HasSugar { get; set; }     = false;
        public bool HasSyrup { get; set; }     = false;
        public bool HasCream { get; set; }     = false;
        public bool HasChocko { get; set; }    = false;
        public bool HasCinnamon { get; set; }  = false;
        public bool HasIce { get; set; }       = false;
        public String Feature { get; set; }    = String.Empty;
        public string Description { 
            get
            {
                StringBuilder sb = new StringBuilder(Name);
                if (HasMilk)     sb.Append(" with milk");
                if (HasSugar)    sb.Append(" with sugar");
                if (HasSyrup)    sb.Append(" with syrup");
                if (HasCream)    sb.Append(" with cream");
                if (HasChocko)   sb.Append(" with chockolat");
                if (HasCinnamon) sb.Append(" with cinnamon");
                if (HasIce)      sb.Append(" with ice");
                if (Feature != String.Empty) sb.Append(Feature);

                return sb.ToString();
            } 
        }
        public Drink(string Name)
        {
            this.Name = Name;
        }

    }
    class Coffee : Drink
    {
        public Coffee() : base("Coffee") { }
    }
    class Cocoa : Drink
    {
        public Cocoa() : base("Cocoa") { }
    }

    abstract class DrinkBuilder
    {
        private Drink drink;  // Объект, который будет построен

        public DrinkBuilder(Drink drink)
        {
            this.drink = drink;
        }

        public DrinkBuilder SetMilk() { drink.HasMilk = true; return this; }
        public DrinkBuilder SetSugar() { drink.HasSugar = true; return this; }
        public DrinkBuilder SetSyrup() { drink.HasSyrup = true; return this; }
        public DrinkBuilder SetCream() { drink.HasCream = true; return this; }
        public DrinkBuilder SetChocko() { drink.HasChocko = true; return this; }
        public DrinkBuilder SetCinnamon() { drink.HasCinnamon = true; return this; }
        public DrinkBuilder SetIce() { drink.HasIce = true; return this; }

        public Drink Build() { return drink; }
    }
    class CoffeeBuilder : DrinkBuilder
    {
        public CoffeeBuilder() : base(new Coffee()) { }
    }
    class CocoaBuilder : DrinkBuilder
    {
        public CocoaBuilder() : base(new Cocoa()) { }
    }

    class DrinkDirector
    {
        private readonly DrinkBuilder drinkBuilder;
        public DrinkDirector(DrinkBuilder drinkBuilder)
        {
            this.drinkBuilder = drinkBuilder;
        }

        public Drink MakeEspresso()
        {
            Drink drink = drinkBuilder.Build();
            drink.Feature = " Espresso";
            return drink;
        }

        public Drink MakeAmericano()
        {
            Drink drink = drinkBuilder.Build();
            drink.Feature = " Americano";
            return drink;
        }
    }
}
/* Строитель (Builder)
Порождающий паттерн - производящий объекты
Используется для объектов, у которых много различных настроек
Противопоставляется антипаттерну "телескоп"
 object.ctor(int par1, int par2, string par3, ..., int parN);
Альтернатива:
 список инициализации

Суть:
 Создается Builder:  builder = new()
 Задаются его параметры (в любом порядке и количестве)
  builder.SetPar1(10);
  builder.SetPar3("Test");
 Производится объект
  а) obj = builder.build()
  б) obj = new Obj(builder)

Пример: Напитки
 = кофе
 = какао
     К напитку можно добавить:
     - молоко
     - сахар
     - сливки
     - сироп
     - корицу
     - шоколад
     - мороженное

Д.З. UML диаграмма паттерна "Строитель"
(вариант использования а), рассмотренный на занятии)
 */
