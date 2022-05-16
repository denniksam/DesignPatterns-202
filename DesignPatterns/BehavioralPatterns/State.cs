using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.BehavioralPatterns
{
    internal class StateDemo
    {
        public void Show()
        {
            new Game().Update();
        }
    }

    class Game
    {
        private IGameState state;
        public IGameState State                      // Альтернатива:
        {                                            // IGameState GetState() {
            get => state;                            //   return state;
            set                                      // }
            {                                        // void SetState(IGameState newState) {
                state = value;                       //   state = newState;
                // Изменение вызывает "перерисовку"  //   Update();
                Update();                            // }
            }                                        // 
        }                                            // 
        public Game()
        {
            state = new MenuState(this);
        }
        public void Update()
        {
            state.Update();
        }

        // -------------- вариант б) ----------------
        private List<IGameState> states = new List<IGameState>();

        public void Play() 
        {
            // проверяем, есть ли уже созданный PlayState в предыдущих состояниях
            foreach(var state in states)
            {
                if(state is PlayState)
                {
                    State = state;
                    return;
                }
            }
            // сюда мы попадаем если ни один return не сработал, то есть нет такого состояния
            var newState = new PlayState(this);
            states.Add(newState);
            State = newState;
        }
        public void Pause() { State = new PauseState(this); }
        public void Menu() { State = new MenuState(this); }

    }

    interface IGameState
    {
        void Update();
    }

    class MenuState : IGameState
    {
        private readonly Game game;
        public MenuState(Game game)
        {
            this.game = game;
        }
        public void Update()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("A key: Play");
            Console.ReadKey(true);
            // Переводим контекст (game) в новое состояние
            // game.State = new PlayState(game);  // вариант а)
            game.Play();  // вариант б)
        }
    }

    class PlayState : IGameState
    {
        private readonly Game game;
        public PlayState(Game game)
        {
            this.game = game;
        }
        public void Update()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Playing....");
                key = Console.ReadKey(true);
                if(key.Key == ConsoleKey.P)
                {
                    // game.State = new PauseState(game);  // вариант а)
                    game.Pause();  // вариант б)
                    return;
                }
            } while (key.Key != ConsoleKey.Escape);
            
        }
    }

    class PauseState : IGameState
    {
        private readonly Game game;
        public PauseState(Game game)
        {
            this.game = game;
        }
        public void Update()
        {
            Console.WriteLine("Paused....");
            Console.WriteLine(" Press a key");
            Console.ReadKey(true);
            // game.State = new PlayState(game);  // вариант а)
            game.Play();  // вариант б)
        }
    }

}
/* Cостояние (State)
Шаблон "Состояние" используется для реализациии приложений, имеющих разные
"режимы" работы - состояния.
Например, игра           // приложение подразумевает использование управления - 
 - Состояние "Меню"      //  кнопок, манипуляторов. В режиме меню это управление
 - Состояние "Игра"      //  выбирает пункты, а в "игре" - управляет игрой

Без паттерна - 
switch(GameMode){         // Без шаблона -- проблема дублирования кода
    case Mode.Menu: ...   //  ? получения сигнала управления (KeyUp) - общее
    case Mode.Play: ...   //  ? реакция на него различная
    case Mode.Loading:    //  а в этом состоянии управление не нужно, то есть перенос
}                         //   управление в общую область - нецелесообразно

С паттерном - создаются классы, реализующие поведения в разных состояниях
 один из них является "текущим"
class Game {
  GameState state;
  ... state = new MenuState(); ...
}
class MenuState, PlayState, LoadingState : GameState
LoadingState { OnFinish() { Game.state = new PlayState() | Game.Play()  } }
PlayState {    OnEscape() { Game.state = new MenuState() | Game.Menu()  } }

а) подход Game.state = new MenuState()
 + хорошая читаемость
 - сложно перевести на повторное использование объектов (Singleton), т.к.
    в конструктор передается ссылка на контекст. Можно отделить SetContext(game), но
    тогда надо вызывать методы "дублетом":
     MenuState.SetContext(game);
     Game.State = MenuState.GetInstance(); 
    можно передать ссылку в GetInstance(game); но придется принимать решения что
     делать если в новом вызове game поменяется

б) подход Game.Menu()
 + переносит логику создания / повторного использования объектов-состояний
    в контекст -- не объекты помнят о своем предыдущем создании, 
    а контекст помнит о предыдущих состояниях

Д.З. Реализовать методы Game::Pause() и Game::Menu() для варианта б) с проверкой
      на существование ранее созданного объекта-состояния
     Доработать UML-диаграмму отобразив 
      поля game в Состояниях и 
      методы Play, Menu, Pause в Game
      взаимосвязи с Show, Program
*/
