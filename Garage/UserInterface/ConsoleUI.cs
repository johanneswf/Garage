namespace Garage.UserInterface
{
    internal class ConsoleUI : IUI
    {
        public void Clear() => Console.Clear();
        public void Write(string m) => Console.Write(m);
        public void WriteLine(string m) => Console.WriteLine(m);
        public string ReadLine() => Console.ReadLine()!;
    }
}
