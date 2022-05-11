using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns
{
    internal class FactoryMethodDemo
    {
        public void Show()
        {
            IDialog TheDialog;
            TheDialog = new DialogTwo();
            IButton TheButton = TheDialog.MakeButton();

            Console.WriteLine(TheButton.Render());
            TheButton.Click();
        }
    }

    interface IDialog
    {
        IButton MakeButton();          // Фабричный метод - построит объект-кнопку
        void Show();
    }
    class DialogOne : IDialog
    {
        public IButton MakeButton()      // Реализация фабричного метода - 
        {                                // создания конкретного объекта ButtonOne,
            return new ButtonOne(this);  // согласующегося именно с этим классом DialogOne
        }
        public void Show()
        {
            Console.WriteLine("Dialog One");
        }
    }
    class DialogTwo : IDialog
    {
        public IButton MakeButton()
        {
            return new ButtonTwo(this);
        }
        public void Show()
        {
            Console.WriteLine("Dialog Two");
        }
    }


    interface IButton
    {
        String Render();               // Вывод (отрисовка) кнопки
        void Click();
    }
    class ButtonOne : IButton            // Конкретная кнопка для DialogOne
    {
        private DialogOne dialog;
        public ButtonOne(DialogOne dialog)
        {
            this.dialog = dialog;
        }
        public String Render()
        {
            return "<<Button One>>";
        }
        public void Click()
        {
            dialog.Show();
        }
    }
    class ButtonTwo : IButton          // Конкретная кнопка для DialogTwo
    {
        private DialogTwo dialog;
        public ButtonTwo(DialogTwo dialog)
        {
            this.dialog = dialog;
        }
        public String Render()
        {
            return "[[Button Two]]";
        }
        public void Click()
        {
            dialog.Show();
        }
    }

}
/* Фабричный метод 
 Суть - создание конкретных объектов делегируется в методы других объектов
В отличие от Простой Фабрики, у которой есть единый объект/класс по созданию
объектов, в Фабричном методе эти задачи разнесены по разным объектам/классам
CryptoFactory.GetInstance("MD5")    |   ( new MD5()  ).GetHasher()
CryptoFactory.GetInstance("SHA1")   |   ( new SHA1() ).GetHasher()

Пример (задача) - образец на сайте https://refactoring.guru/ru/design-patterns/factory-method
Кнопки, запускающие диалоги
Есть несколько форм диалогов под разные задачи, запускаемые кнопкой на интерфейсе
Необходимо иметь возможность оперативно переключаться между ними:
 кнопка одна, а запускаемый диалог нужно переключать

Решение (в стиле Фабричного Метода)
 Диалог должен уметь "нарисовать" кнопку.
 В области интерфейса (пользователя) - TheDialog.MakeButton()
  TheDialog - может поменяться, но код создания кнопки остается неизменным
 */
