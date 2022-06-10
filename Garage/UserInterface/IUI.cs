namespace Garage.UserInterface
{
    internal interface IUI
    {
        void Write(string m);
        void WriteLine(string m);
        string ReadLine();
    }
}
