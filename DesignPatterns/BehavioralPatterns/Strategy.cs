using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.BehavioralPatterns
{
    internal class Strategy
    {
        List<double> data = new List<double>() { 1, 2, 3, 4, 5 };
        MeanCalculator meanCalculator;

        public Strategy()
        {
            meanCalculator = new MeanCalculator();
            meanCalculator.Strategies.Add(new MeanArithmetic());
            meanCalculator.Strategies.Add(new MeanHarmonic());
            meanCalculator.Strategies.Add(new MeanGeometric());
        }
        public void Show()
        {
            Console.WriteLine("Means: ");
            foreach(var mean in meanCalculator.GetAll(data))
            {
                Console.WriteLine(mean);
            }
            Console.WriteLine($"Greatest: {meanCalculator.GetGreatest(data)}");
        }
        public void ShowDetails()
        {
            Console.WriteLine("Details: ");
            foreach (IMeanValue strategy in meanCalculator.Strategies)
            {
                String algo = strategy.GetType().Name;
                double val = strategy.GetMean(data);
                Console.WriteLine($"Algo: {algo}, Value: {val}");
            }
        }
    }

    interface IMeanValue
    {
        double GetMean(List<double> arr);
    }

    class MeanArithmetic : IMeanValue
    {
        public double GetMean(List<double> arr)
        {
            double ret = 0;
            int n = arr.Count;
            foreach (var x in arr)
            {
                ret += x;
            }
            return ret / n;
        }
    }

    class MeanHarmonic : IMeanValue
    {
        public double GetMean(List<double> arr)
        {
            double ret = 0;
            int n = arr.Count;
            foreach (var x in arr)
            {
                ret += 1 / x;
            }
            return n / ret;
        }
    }

    class MeanGeometric : IMeanValue
    {
        public double GetMean(List<double> arr)
        {
            double ret = 1;
            int n = arr.Count;
            foreach (var x in arr)
            {
                ret *= x;
            }
            return Math.Pow(ret, 1.0 / n);
        }
    }

    class MeanCalculator
    {
        public List<IMeanValue> Strategies = new List<IMeanValue>();

        public double GetGreatest(List<double> arr)
        {
            return GetAll(arr).Max();
        }

        public List<double> GetAll(List<double> arr)
        {
            List<double> ret = new List<double>();
            foreach (IMeanValue strategy in Strategies)
            {
                ret.Add(strategy.GetMean(arr));
            }
            return ret;
        }
    }
}
/*
Паттерн Стратегия (Strategy)
Позволяет реализовывать (разрабатывать) несколько алгоритмов (версий, вариантов)
работы и
а) переключаться между ними, оставляя один
б) выполнять все и выбирать оптимальный

Пример-задача: расчет средних значений.
Разновидности средних
 - арифметическое
 - геометрическое
 - медиана
 - степенное
 - квадратическое
 - гармоническое

Д.З. "Стратегия": Реализовать (на выбор) еще одну стратегию расчета среднего значения
встроить ее в стратегии калькулятора, обеспечить вывод результатов.
Изобразить на UML диаграмме новые стратегии, новые методы (ShowDetails)
и отношение с классом Program

 */
