﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns
{
    internal class Singleton
    {                                            // 
        public DateTime Moment => DateTime.Now;  // "динамическая" часть - доступная только
                                                 // через объект (instance)


        private static Singleton instance;       // Статическая часть, хранящая ссылку на 
        public static Singleton GetInstance()    // объект, а также создающая объект
        {                                        // при первом запросе
            if (instance == null)                // (когда instance == null)
            {                                    // 
                instance = new Singleton();      //   внутри класса приватный конструктор работает
            }                                    //
            return instance;                     // если instance != null, то объект не создается,
        }                                        //  возвращается хранимая ссылка
        private Singleton()                      // Объявление приватного конструктора
        {                                        // сделает невозможным создание объектов
                                                 // оператором new вне класса
        }                                        // 
    }                                            // 
}
/*
Singleton (одиночка)
Порождающий паттерн, обеспечивающий единственность и доступность
Единственность:
 - Есть один объект данного класса (Неправильно: чистый статик без объекта)
 - Объект только один, нет НИКАКИХ возможностей создать второй объект
 = Ленивость - объект создается при первом обращении
Доступность:
 - Все части программы могут получить доступ к объекту
 - Гарантируется, что все эти части получают доступ к одному и тому же 
    объекту (по ссылке) - изменения, внесенные одной частью, отражаются
    на других
Примеры:
 - Генератор случайных чисел
 - Логер
 - Подключения к данным (к БД)
 - Авторизованный пользователь (ссылка на данные авторизации)

Противопоставляется:
 - с одной позиции - классу с полностью статическими полями
 - с другой - внедрению зависимостей:  GetInstance --> [Dependency]

Д.З. Изобразить UML диаграмму созданного кода (Singleton - Program)
 */