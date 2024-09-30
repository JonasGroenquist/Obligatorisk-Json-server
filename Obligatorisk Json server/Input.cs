using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonServerOpg
{
    public class Input
    {
        public string Method { get; set; }
        public int Num1 { get; set; }
        public int Num2 { get; set; }

        public Input()
        {
        }

        public Input(string method, int number1, int number2)
        {
            Method = method;
            Num1 = number1;
            Num2 = number2;

        }
    }
}