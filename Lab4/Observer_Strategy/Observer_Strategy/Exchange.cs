using System;
using System.Collections.Generic;

namespace Exchange
{
    // Наблюдатель
    interface IObserver
    {
        void Update(Object ob);
    }

    // Издатель
    interface IObservable
    {
        void RegisterObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObservers();
    }

    class Stock : IObservable
    {
        // Информация о торгах
        public StockInfo sInfo;
        // Список наблюдателей
        public List<IObserver> observers;

        public Stock()
        {
            observers = new List<IObserver>();
            sInfo = new StockInfo();
            sInfo.Euro = 80;
            sInfo.USD = 70;
        }

        public void RegisterObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers()
        {
            Console.WriteLine("Уведомляю об изменении курсов");
            foreach (IObserver o in observers)
            {
                o.Update(sInfo);
            }
        }

        public void Market()
        {
            Random rnd = new Random();
            sInfo.USD = rnd.Next(70, 80);
            sInfo.Euro = rnd.Next(80, 90);
            NotifyObservers();
        }
    }

    class StockInfo
    {
        public int USD { get; set; }
        public int Euro { get; set; }
    }

    class Broker : IObserver
    {
        public string Name { get; set; }
        public IObservable stock;

        public Broker(string name, IObservable obs)
        {
            this.Name = name;
            stock = obs;
            stock.RegisterObserver(this);
        }

        public void Update(object ob)
        {
            StockInfo sInfo = (StockInfo)ob;

            Console.WriteLine("Принял новые котировки");
        }

    }

    interface IStrategy
    {
        void ExecuteStrategy();
    }

    class Sell : IStrategy
    {
        public void ExecuteStrategy() => Console.WriteLine("Продажа");

    }

    class Buy : IStrategy
    {
        public void ExecuteStrategy() => Console.WriteLine("Покупка");

    }

    class Director
    {
        public Director()
        {
            _stock = new Stock();
        }

        private IStrategy _strategy;

        private Stock _stock;
        private List<IObserver> _observer = new List<IObserver>();

        public void Execute() => this._strategy.ExecuteStrategy();

        public void SetStrategy(IStrategy _strategy) => this._strategy = _strategy;

        public void SetObserver(IObserver observer) => this._observer.Add(observer);

        public void Trading()
        {
            for (int i = 0; i < 3; i++)
            {
                var old_eur = _stock.sInfo.Euro;
                var old_dol = _stock.sInfo.USD;

                _stock.Market();
                foreach (var ob in _observer)
                {
                    ob.Update(_stock.sInfo);
                }


                var new_eur = _stock.sInfo.Euro;
                var new_dol = _stock.sInfo.USD;

                if (old_eur >= new_eur)
                {
                    Console.WriteLine("Старый курс евро " + old_eur + "; Новый курс евро " + new_eur);
                    SetStrategy(new Buy());

                    _strategy.ExecuteStrategy();
                }
                else
                {
                    Console.WriteLine("Старый курс евро " + old_eur + "; Новый курс евро " + new_eur);
                    SetStrategy(new Sell());
                    _strategy.ExecuteStrategy();

                }

                if (old_dol >= new_dol)
                {
                    Console.WriteLine("Старый курс доллара " + old_dol + "; Новый курс доллара " + new_dol);
                    SetStrategy(new Buy());
                    _strategy.ExecuteStrategy();
                }
                else
                {
                    Console.WriteLine("Старый курс доллара " + old_dol + "; Новый курс доллара " + new_dol);
                    SetStrategy(new Sell());
                    _strategy.ExecuteStrategy();
                }

                Console.WriteLine();

                System.Threading.Thread.Sleep(250);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();
            Stock stock = new Stock();
            Broker broker = new Broker("Александр", stock);
            director.SetObserver(broker);
            //Broker broker1 = new Broker("Александр", stock);
            //director.SetObserver(broker1);

            director.Trading();
        }
    }
}