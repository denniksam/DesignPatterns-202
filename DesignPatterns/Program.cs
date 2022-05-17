using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DesignPatterns
{

    internal class Program
    {
        
        static void Main(string[] args)
        {
            // консоль(системное меню) - свойства - переключить на терминал
            Console.OutputEncoding = System.Text.Encoding.UTF8; 

            Console.WriteLine("Design patterns");
            Console.WriteLine(" 1 Creational:");
            Console.WriteLine("   11 Singleton");
            Console.WriteLine("   12 Simple Factory");
            Console.WriteLine("   13 Factory Method");
            Console.WriteLine("   14 Abstract Factory");
            Console.WriteLine("   15 Builder");
            Console.WriteLine(" 2 Behavioral:");
            Console.WriteLine("   21 Strategy");
            Console.WriteLine("   22 Observer");
            Console.WriteLine("   23 State");
            Console.WriteLine("   24 Chain of Responsibility");
            Console.WriteLine(" 3 Structural:");
            Console.WriteLine("   31 Decorator"); 
            Console.WriteLine("   32 Bridge"); 
            Console.WriteLine("   33 Proxy"); 
            Console.WriteLine("   34 Composite"); 
            Console.WriteLine();

            String userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "11":
                    #region Singleton
                    // приватный конструктор не дает создать объект через new
                    // var obj = new CreationalPatterns.Singleton();

                    // Характерным признаком Singleton является запрос GetInstance()
                    var obj = CreationalPatterns.Singleton.GetInstance();
                    Console.WriteLine(obj.Moment);

                    var obj2 = CreationalPatterns.Singleton.GetInstance();
                    Console.WriteLine(obj == obj2 ? "Equals" : "Not equals");

                    #endregion
                    break;
                case "12":
                    new CreationalPatterns.FactoryDemo().Show();
                    break;
                case "13":
                    new CreationalPatterns.FactoryMethodDemo().Show();
                    break;
                case "14":
                    new CreationalPatterns.AbstractFactoryDemo().Show();
                    break;
                case "15":
                    new CreationalPatterns.BuilderDemo().Show();
                    break;
                case "21":
                    #region Strategy
                    var StrategyDemo = new BehavioralPatterns.Strategy();
                    // Автоматическая работа
                    StrategyDemo.Show();
                    // задание: вывести по всем стратегиям: название - значение
                    StrategyDemo.ShowDetails();
                    #endregion
                    break;
                case "22":
                    new BehavioralPatterns.ObserverDemo().Show();
                    break;
                case "23":
                    new BehavioralPatterns.StateDemo().Show();
                    break;
                case "24":
                    new BehavioralPatterns.ChainDemo().Show();
                    break;
                case "31":
                    #region Decorator
                    new StructuralPatterns.DecoratorDemo().Show();
                    #endregion
                    break;
                case "32":
                    new StructuralPatterns.BridgeDemo().Show();
                    break;
                case "33":
                    new StructuralPatterns.ProxyDemo().Show();
                    break;
                case "34":
                    new StructuralPatterns.CompositeDemo().Show();
                    break;
                default:
                    Console.WriteLine("Invalid Choice 💩");
                    break;
            }

            
        }
    }
}
