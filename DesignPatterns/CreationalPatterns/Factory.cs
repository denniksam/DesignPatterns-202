using System;

namespace DesignPatterns.CreationalPatterns
{
    internal class FactoryDemo
    {
        public void Show()
        {
            Console.WriteLine("Which algo?");
            String algo = Console.ReadLine();
            try
            {
                IHasher hasher = CryptoFactory.GetInstance(algo);
                Console.WriteLine(hasher.Hash("content"));
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message + "💩");
            }
        }
    }

    interface IHasher
    {
        String Hash(String s);
    }

    class Md5Hasher : IHasher
    {
        public String Hash(String s)
        {
            return $"MD5 hash of {s}";
        }
    }
    class Sha1Hasher : IHasher
    {
        public String Hash(String s)
        {
            return $"SHA-1 hash of {s}";
        }
    }
    class KupinaHasher : IHasher
    {
        public String Hash(String s)
        {
            return $"Kupina-256 hash of {s}";
        }
    }

    class CryptoFactory
    {
        public static IHasher GetInstance(String algoName)
        {
            switch (algoName)
            {
                case "MD5":
                case "MD-5":
                case "Md5":
                    return new Md5Hasher();
                case "SHA":
                case "SHA-1":
                case "SHA-160":
                    return new Sha1Hasher();
                case "Kupina":
                case "DSTU":
                case "DSTU-256":
                    return new KupinaHasher();
                default:
                    throw new ArgumentException($"Algo '{algoName}' invalid");
            }
        }
    }
}
/*
  Фабрика (Factory)
Фабрики (в целом) - шаблоны, задачей которых является делегирование
задач создание объектов
в специальные "подразделы" - фабрики

Фабрика (просто) - класс/объект, создающие другие одиночные объекты
 CryptoFactory.GetInstance("MD5") --> MD5    : IHasher     | Абстракция:
 CryptoFactory.GetInstance("SHA-1") --> Sha1 : IHasher     |  CryptoFactory --> Hasher

Абстрактная фабрика - для задач создания связанных объектов

Фабричный метод - перенос задач создания "своих" объектов в сами объекты
 Logger
  FileLogger 
    .GetJournal() --> File              : IJournal    | Абстракция:
  ConsoleLogger                                       |  Logger.GetJ() --> IJournal
    .GetJournal() --> Console (Handle)  : IJournal    |
  DbLogger                                            |
    .GetJournal() --> Db                : IJournal    |
  

Д.З. Простая Фабрика: добавить еще одну реализацию (на выбор - SHA-2),
встроить ее в фабрику, добавить на диаграмму UML

О классифицирующих связях:
Структура                Наследование              Агрегация
содержит все              Класс-наследник           Внутренний массив
возможные поля,           реализации                Структур либо классов
реализуется только        с необходимыми           
часть из них              наборами полей           

На примере литературы
Book                       Literature                   Literature
Author +                     Book : Literature           .Entity[0] - Book
Date   -                      Author                    
Title  +                      Title                     
Num    -                     
---------------------------------------------------------------------------------
Простые языки              Только ООП                   Универсальная совместимость
БД - табличное               компактность               
 представление                                          
---------------------------------------------------------------------------------
Избыточность               Хуже совместимость с БД      Сложность реализации

 */
