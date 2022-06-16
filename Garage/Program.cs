namespace Garage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUI ui = new ConsoleUI();
            Main main = new(ui);
            main.Run();
        }
    }
}