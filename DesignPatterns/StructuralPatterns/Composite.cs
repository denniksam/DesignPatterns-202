using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.StructuralPatterns
{
    internal class CompositeDemo
    {
        public void Show()
        {
            Firm firm = new Firm();
            firm.Members.Add(new FinDirector());
            firm.Members.Add(new ExeDirector());
            firm.Members.Add(new LabourUnion());
            firm.Members.Add(new DivisionsDelegate());

            firm.MakeResolution("Bake a cookie");
        }
    }

    interface IMember
    {
        void TakeResolution(String resolution);
    }

    class FinDirector : IMember
    {
        public void TakeResolution(String resolution)
        {
            Console.WriteLine($"FinDirector take '{resolution}' ");
        }
    }
    class ExeDirector : IMember
    {
        public void TakeResolution(String resolution)
        {
            Console.WriteLine($"ExeDirector take '{resolution}' ");
        }
    }

    class Firm
    {
        public List<IMember> Members { get; set; }
        public Firm()
        {
            Members = new List<IMember>();
        }
        public void MakeResolution(String resolution)
        {
            Console.WriteLine($"Firm make resolution '{resolution}'");
            foreach (IMember member in Members)
            {
                if(member is CompositeMember)
                {
                    Console.Write("[]");  
                    // Д.З. Компоновщик:
                    //  Добавить в признак комплексного участника количество
                    //  конкретных участников в нем: []  -->  [3]
                    // Закончить UML диаграмму
                }
                member.TakeResolution(resolution);
            }
        }
    }
    //------------------------- Complex member ---------------------

    abstract class CompositeMember : IMember
    {
        public List<IMember> Members { get; set; }

        public abstract void TakeResolution(string resolution);
    }
    class BakersDelegate : IMember
    {
        public void TakeResolution(String resolution)
        {
            Console.WriteLine($"BakersDelegate take '{resolution}' ");
        }
    }
    class CleaningsDelegate : IMember
    {
        public void TakeResolution(String resolution)
        {
            Console.WriteLine($"CleaningsDelegate take '{resolution}' ");
        }
    }
    class LoadersDelegate : IMember
    {
        public void TakeResolution(String resolution)
        {
            Console.WriteLine($"LoadersDelegate take '{resolution}' ");
        }
    }
    class LabourUnion : CompositeMember
    {
        public LabourUnion()
        {
            Members = new List<IMember>() {
                new BakersDelegate(),
                new CleaningsDelegate(),
                new LoadersDelegate()
            };
        }
        override public void TakeResolution(String resolution)
        {
            Console.WriteLine($"LabourUnion take resolution '{resolution}'");
            foreach (IMember member in Members)
            {
                Console.Write(" ✅ "); 
                member.TakeResolution(resolution);
            }
        }
    }

    class MykolaivDelegate : IMember
    {
        public void TakeResolution(String resolution)
        {
            Console.WriteLine($"MykolaivDelegate take '{resolution}' ");
        }
    }
    class KhersonDelegate : IMember
    {
        public void TakeResolution(String resolution)
        {
            Console.WriteLine($"KhersonDelegate take '{resolution}' ");
        }
    }
    
    class IzmailDelegate : IMember
    {
        public void TakeResolution(String resolution)
        {
            Console.WriteLine($"IzmailDelegate take '{resolution}' ");
        }
    }
    class VilkovoDelegate : IMember
    {
        public void TakeResolution(String resolution)
        {
            Console.WriteLine($"VilkovoDelegate take '{resolution}' ");
        }
    }
    class OdessaDelegate : CompositeMember
    {
        public OdessaDelegate()
        {
            Members = new List<IMember>()
            {
                new IzmailDelegate(),
                new VilkovoDelegate()
            };
        }
        override public void TakeResolution(String resolution)
        {
            Console.WriteLine($"OdessaDelegate(s) take resolution '{resolution}'");
            foreach (IMember member in Members)
            {
                Console.Write("    ✅ ");
                member.TakeResolution(resolution);
            }
        }
    }
    
    class DivisionsDelegate : CompositeMember
    {
        public DivisionsDelegate()
        {
            Members = new List<IMember>() {
                new KhersonDelegate(),
                new MykolaivDelegate(),
                new OdessaDelegate()
            };
        }
        override public void TakeResolution(String resolution)
        {
            Console.WriteLine($"DivisionsDelegate(s) take resolution '{resolution}'");
            foreach (IMember member in Members)
            {
                Console.Write(" ✅ ");
                member.TakeResolution(resolution);
            }
        }
    }
}
/*
 Компоновщик (Composite)
Объединение составных объектов наравне с одиночными.
Возможность в качестве элемента использовать комплексный набор.

Похож на Декоратор, но Декоратор -- одномерный, Компоновщик -- древовидный
Также похож на Декоратор тем, что не рекомендуется как фундамент проекта,
 а приходит на помощь при необходимости расширять проект

Идея:
 Есть совет директоров, в нем участвуют представители
 В какой-то момент открывается филиал и появляется комплексный представитель
  с одной стороны, у него один голос (один участник)
  с другой стороны, реальных представителей несколько

Решение:
 Внедрение в систему комплексного участника, который
  реализует интерфейс одиночного участника
  имеет "внутреннюю" возможность распределять (дублировать) задачи
   для всех своих участников
 */
