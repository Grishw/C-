using System;

namespace calc
{
    class Program
    {

        bool GetParamFromConsole()
        {
            return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(
                "Calc have some operation \n" +
                "{arg1} + {arg2}: for arg1 sum arg2 \n" +
                "{arg1} - {arg2}: for arg1 minus arg2\n" +
                "{arg1} * {arg2}: for arg1 multiplicat with arg2\n" +
                "{arg1} / {arg2}: for arg1 division on arg2\n\n" +
                "{'C'}: for stop execude programm\n");



        }
    }
}
