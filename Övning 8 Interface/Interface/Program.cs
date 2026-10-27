namespace Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program1 p1 = new();
            Program2 p2 = new();
            //p1.Run();
            //p2.Go();
            p1.Start();
            p2.Start();
            p1.End();

            Program3 p3 = new();
            ((IRunnerOne)p3).Run();
            ((IRunnerTwo)p3).Run();
        }
    }

    internal class Program1 : IStarter, IEnder
    {
        public void Start()
        {
            Console.WriteLine("Nu startar vi program 1");
        }

        public void End()
        {
            Console.WriteLine("Nu avslutar vi program 1");
        }

        //public void Run()
        //{
        //    Console.WriteLine("Nu kör vi program 1");
        //}
    }

    internal class Program2 : IStarter
    {
        public void Start()
        {
            Console.WriteLine("Nu startar vi program 2");
        }

        //public void Go()
        //{
        //    Console.WriteLine("Nu kör vi program 2");
        //}
    }

    public class Program3 : IRunnerOne, IRunnerTwo
    {
        void IRunnerOne.Run()
        {
            Console.WriteLine("IRunnerOne.Run");
        }

        int IRunnerTwo.Run()
        {
            Console.WriteLine("IRunnerTwo.Run");
            return 7;
        }
    }

    interface IStarter
    {
        void Start();
    }

    interface IEnder
    {
        void End();
    }

    public interface IRunnerOne
    {
        void Run();
    }

    public interface IRunnerTwo
    {
        int Run();
    }
}
