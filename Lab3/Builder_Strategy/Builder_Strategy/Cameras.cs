using System;

namespace Cameras
{
    interface ICamera
    {
        void setLens();
        void setFlash();
        void setNightVision();
        void setThermalImager();
    }

    class Camera : ICamera
    {
        public void setFlash()
        {
            Console.WriteLine("Установлена вспышка");
        }

        public void setLens()
        {
            Console.WriteLine("Установлена линза");
        }

        public void setNightVision()
        {
            Console.WriteLine("Установлен прибор ночного видения");
        }

        public void setThermalImager()
        {
            Console.WriteLine("Установлен тепловизор");
        }
    }

    interface IStrategy
    {
        void Execute();
        void setICamera(ICamera _cmr);
    }

    class FullConf : IStrategy
    {
        private ICamera _cmr;
        public void Execute()
        {
            _cmr.setFlash();
            _cmr.setLens();
            _cmr.setNightVision();
            _cmr.setThermalImager();
        }
        public void setICamera(ICamera _cmr) => this._cmr = _cmr;

    }

    class MinimalConf : IStrategy
    {
        private ICamera _cmr;
        public void Execute()
        {
            _cmr.setFlash();
            _cmr.setLens();
        }
        public void setICamera(ICamera _cmr) => this._cmr = _cmr;
    }

    class Director
    {
        private IStrategy _str;

        public void setStrategy(IStrategy _str) => this._str = _str;
        public void ExecuteStrategy() => this._str.Execute();

    }

    class Program
    {
        static void Main(string[] args)
        {
            var dr = new Director();
            ICamera cm = new Camera();
            IStrategy st = new MinimalConf();
            st.setICamera(cm);

            dr.setStrategy(st);
            dr.ExecuteStrategy();
            Console.WriteLine();
            st = new FullConf();
            st.setICamera(cm);
            dr.setStrategy(st);
            dr.ExecuteStrategy();

        }
    }
}