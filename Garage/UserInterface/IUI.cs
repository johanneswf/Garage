using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.UserInterface
{
    internal interface IUI
    {
        void Print(string m);
        string Read();
    }
}
