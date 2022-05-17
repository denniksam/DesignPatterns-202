using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.BehavioralPatterns
{
    internal class ChainDemo
    {
        public void Show()
        {                                     // "Leakage" в С# - сохранение именованной
            Manipulation                      //  ссылки на ненужный объект.
                coffee = new Coffee(),        // Рекомендация - без необходимости не 
                boiling = new Boiling(),      //  именовать объекты, исп. анонимные
                ins = new CoffeeInserter();   // но это выглядит неэффектно:
                                              // coffee.Next = new Boiling()
            coffee.Next = boiling;            // coffee.Next.Next = new CoffeeInserter()
            boiling.Next = ins;               //
                                              // coffee.Next = new Boiling(); coffee = coffee.Next;
            coffee.Execute(150);              // coffee.Next = new CoffeeInserter(); coffee = coffee.Next;

            Console.WriteLine("-----------------");
            Manipulation coffe2 = new Coffee();
            Console.Write("Variation (1/2)? ");
            if (Console.ReadKey().Key == ConsoleKey.D1)
            {
                coffe2
                    .SetNext(new Boiling())
                    .SetNext(new CoffeeInserter())
                    .SetNext(new SugarInserter())
                    .SetNext(new MilkInserter());
            }
            else
            {
                coffe2
                    .SetNext(new MilkInserter())
                    .SetNext(new Boiling())
                    .SetNext(new CoffeeInserter())
                    .SetNext(new SugarInserter());
            }
            Console.WriteLine();
            Console.Write("Volume: ");
            try
            {
                coffe2.Execute(
                    Int32.Parse(Console.ReadLine())
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Process failed with message '{ex.Message}'");
            }
        }
    }

    abstract class Manipulation
    {
        public Manipulation Next { get; set; }
        public Manipulation SetNext(Manipulation next)
        {
            Next = next;
            return this.Next;
        }
        abstract public void Execute(int volume);
    }

    class Coffee : Manipulation
    {
        override public void Execute(int volume)
        {
            Console.WriteLine("Coffee preparing starts...");
            Next?.Execute(volume);
        }
    }
    class Boiling : Manipulation
    {
        override public void Execute(int volume)
        {
            if(volume > 300)
            {
                throw new ArgumentException("Boiler capacity overloaded (300 ml max)");
            }
            Console.WriteLine("Water boiling starts...");
            Next?.Execute(volume);
        }
    }
    class CoffeeInserter : Manipulation
    {
        override public void Execute(int volume)
        {
            Console.WriteLine("Coffee inserts...");
            Next?.Execute(volume);
        }
    }
    class MilkInserter : Manipulation
    {
        public override void Execute(int volume)
        {
            Console.WriteLine("Adding milk 50ml");    // В цепочке обрабатываемые данные
            Next?.Execute(volume + 50);               // могут меняться
        }
    }
    class SugarInserter : Manipulation
    {
        public override void Execute(int volume)
        {
            Console.WriteLine("Adding sugar");
            Next?.Execute(volume);
        }
    }
}

/* Цепочка обязанностей (Chain of Responsibilities, CoR)
С развитием также получил название MiddleWare
Создает "цепочку" рабочих процессов и дает возможность
 встраивать в нее новые элементы "в середину"
Общая идея похожа на 
 - связанный список, но есть возможность прервать цепочку на любом звене
 - хуки, но встраиванием обычно занимается контейнер (хук обычно не хранит
    ссылку на следующий)
 - callback (по завершению тела ф-ции вызывается следующий callback)
В некоторых случаях кроме возможности прервать цепочку есть возможность
 перейти на другое звено, в т.ч. на ее начало

Реализации паттерна:
 - в стиле связанного списка: каждое звено хранит ссылку на следующее звено
    Node { next;  handle() { ... next.handle() } }
 - в стиле хука: каждое звено получает зависимость от цепочки
    Node {  handle(chain) {  ... chain.doNext() } } 
Основные отличия в характере связывания звеньев в цепь:
 - node1 = new ConcreteNode1(); node2 = new ConcreteNode2(); node2.next = node1
 - chain = new { new ConcreteNode1(), new ConcreteNode2() }
    а также можно декларативно: chain.config [ ConcreteNode1; ConcreteNode2 ]
?? как встроить новый узел в середину (между 1 и 2) ??
 - разорвать старую связь 2-1 и построить новые 2-new-1
 - добавить новый узел в нужное место списка инициализации chain
?? как поменять порядок работы узлов ?? 
?? а потом вернуть в исходный или снова поменять ??

Шаблон популярен в Enterprise разработке, в частности, в веб-проектах
запрос - [] - ответ
[] = [есть подключение к БД?  -- Есть данные авторизации? -- Права админа? -- ... ]
             \ лендинг                \ гостевая                \ ...

ТЗ.
Кофе: приготовление кофе состоит из следующих процессов
 - кипячение воды
 - добавление кофе
 - добавление сахара
 - добавление молока
Рассматриваем возможность изменения как порядка так и состава (количества) процессов

Д.З. Закончить UML диаграмму для паттерна CoR
Доработать все шаблоны, приложить ссылку или архив проекта со всеми рассмотренными
шаблонами проектирования.
 */
