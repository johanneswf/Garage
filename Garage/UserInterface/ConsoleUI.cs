namespace Garage.UserInterface
{
    internal class ConsoleUI : IUI
    {
        public void Print(string m) => Console.WriteLine(m);

        public string Read() => Console.ReadLine()!;
    }
}
