using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr
{
    class Program
    {
        static void Main(string[] args)
        {


            ExchangeRateDC.Inst.Dispose();

            Console.Read();
        }
    }
}
