using System;

namespace FabricMethod
{
    //фабрика
    abstract class Builder
    {
        abstract public Plane GetPlane(string PlaneName);
    }

    //Конкретная фабрика
    class MillitaryPlaneBuilder : Builder
    {
        public override Plane GetPlane(string PlaneName)
        {
            return new MillitaryPlane(PlaneName);
        }
    }

    class DelivererPlaneBuilder : Builder
    {
        public override Plane GetPlane(string PlaneName)
        {
            return new DelivererPlane(PlaneName);
        }
    }

    class PassengerPlaneBuilder : Builder
    {
        public override Plane GetPlane(string PlaneName)
        {
            return new PassengerPlane(PlaneName);
        }
    }

    //продукт
    abstract class Plane
    {
        public Plane(string name)
        {
            this.name = name;
        }
        
        
        protected string name { get; set; }
        // создание самолета
        public abstract void Create();
    }

    //конкретный продукт 
    class MillitaryPlane : Plane
    {
        public MillitaryPlane(string name) : base(name) { }
        public override void Create()
        {
            Console.WriteLine("Военный самолёт " + name);
        }
    }

    class PassengerPlane : Plane
    {
        public PassengerPlane(string name) : base(name) { }
        public override void Create()
        {
            Console.WriteLine("Пассажирский самолёт " + name);
        }
    }

    class DelivererPlane : Plane
    {
        public DelivererPlane(string name) : base(name) { }
        public override void Create()
        {
            Console.WriteLine("Грузовой самолёт " + name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Builder bld1 = new MillitaryPlaneBuilder();
            Plane plane1 = bld1.GetPlane("СУ-57");
            plane1.Create();

            Builder bld2 = new PassengerPlaneBuilder();
            Plane plane2 = bld2.GetPlane("Boeing 747");
            plane2.Create();

            Builder bld3 = new DelivererPlaneBuilder();
            Plane plane3 = bld3.GetPlane("ИЛ-86");
            plane3.Create();

            Builder bld4 = new PassengerPlaneBuilder();
            Plane plane4 = bld4.GetPlane("Airbus 320");
            plane4.Create();
        }
    }
}