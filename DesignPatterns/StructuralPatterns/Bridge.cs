using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.StructuralPatterns
{
    internal class BridgeDemo
    {
        public void Show()
        {
            Figure f1 = new Figure();
            f1.AddComponent(new ShapeComponent("Circle"));
            f1.AddComponent(new StrokeComponent("Tomato", 3));
            f1.AddComponent(new FillComponent("Salmon", "RadialGradient"));
            f1.Render();

            Figure f2 = 
                new Figure()
                .AddComponent(new ShapeComponent("Diamond"))
                .AddComponent(new FillComponent("Blue", "Solid"));
            f2.Render();
        }
    }

    interface IFigureComponent
    {
        void Render();  // Метод своего отображения (рисования)
    }

    class Figure
    {
        private List<IFigureComponent> components;
        public Figure()
        {
            components = new List<IFigureComponent>();
        }
        public Figure AddComponent(IFigureComponent component)
        {
            components.Add(component);
            return this;
        }
        public void Render()
        {
            if (components.Count == 0)
            {
                Console.WriteLine("👻");  // Empty Figure - no commponents
            }
            else
            {
                foreach (IFigureComponent component in components)
                {
                    component.Render();
                }
                Console.WriteLine();
            }
        }
    }

    class ShapeComponent : IFigureComponent
    {
        private readonly string Shape;
        public ShapeComponent(String shape)
        {
            Shape = shape;
        }
        public void Render()
        {
            Console.Write($" {Shape} ");
        }
    }

    class StrokeComponent : IFigureComponent
    {
        private readonly String Color;
        private readonly int Width;

        public StrokeComponent(String color, int width)
        {
            this.Color = color;
            this.Width = width;
        }

        public void Render()
        {
            Console.Write($" {Color} border {Width}px width ");
        }
    }

    class FillComponent : IFigureComponent
    {
        private readonly String Color;
        private readonly String Style;
        public FillComponent(String color, String style)
        {
            this.Color = color;
            this.Style = style;
        }

        public void Render()
        {
            Console.Write($" {Style} fill with {Color} color ");
        }
    }
}
/*
Мост (Bridge)
Структурный шаблон, заменяющий наследование/реализацию на агрегацию
а) Без паттерна
 Есть фигуры: базовый класс Figure, наследники Square:Figure, Circle:Figure
 Возникает необходимость рисовать фигуры с контуром StrokeSquare, StrokeCircle
 Фигуры с заполнением: FillSquare, FillCircle
 Фигуры с контуром и заполнением: FillStrokeSquare
 Добавляем концепцию рисования штрихом: DashFillStrokeSquare

б) Паттерн
 Создаем Фигуру как контейнер, а в ней - коллекцию компонент
 Figure {
   Commponents [FillComponent, StrokeComponent, ShapeComponent, ...]
 }
    
Д.З. Реализовать компонент ShadowComponent ( int OffsetX, int OffsetY, uint Blur )
Добавить в демонстрацию
Дополнить UML диаграмму паттерна "Мост" с учетом всех классов-участников
 */
