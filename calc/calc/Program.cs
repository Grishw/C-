using System;
using System.Text.RegularExpressions;

namespace calc
{
    class Program
    {
        const string numPattern = "";
        const string spacePattern = @"[ ]+";
        const string spaceReplacement = " ";
        const string startMessage =
            "Calc have some operation \n" +
            "{arg1} + {arg2}: for arg1 sum arg2 \n" +
            "{arg1} - {arg2}: for arg1 minus arg2\n" +
            "{arg1} * {arg2}: for arg1 multiplicat with arg2\n" +
            "{arg1} / {arg2}: for arg1 division on arg2\n\n" +
            "'S' or 's': for stop execution programm\n";
        const string inputStatements = "\nInput statements: ";
        const string errorInput = "Not right input";
        const string errorValue =
            "Incorrect argument value was entered," +
            " the expected input type of the argument is double";
        const string errorOperation = "Error execute operation";
        double ExecutOperation()
        {
            return 0;
        }


        static void Main(string[] args)
        {
            Console.WriteLine($"{startMessage}{inputStatements}");

            string inputString = Console.ReadLine().Trim(' ');
            double? result = null;

            while (inputString.ToLower() != "s")
            {
                result = null;
                string inputEcho = Regex.Replace(inputString, spacePattern, spaceReplacement);
                string[] stringParam = inputEcho.Split(" ");
                if (stringParam.Length != 3)
                {
                    Console.WriteLine(errorInput);
                }
                else
                {
                    if (Double.TryParse(stringParam[0], out double arg1) && Double.TryParse(stringParam[2], out double arg2))
                    {
                        string operation = stringParam[1];

                        if (operation == "+")
                        {
                            result = arg1 + arg2 < Double.MaxValue ? arg1 + arg2 : Double.MaxValue;
                        }
                        else if (operation == "-")
                        {
                            result = arg1 - arg2 > Double.MinValue ? arg1 - arg2 : Double.MinValue;
                        }
                        else if (operation == "*")
                        {
                            result = arg1 * arg2 < Double.MaxValue ? arg1 * arg2 : Double.MaxValue;
                        }
                        else if (operation == "/")
                        {
                            result = arg2 != 0 ? arg1 / arg2 : null;
                        }

                        if (result == null)
                        {
                            Console.WriteLine(errorOperation);
                        }
                        else
                        {
                            Console.WriteLine(inputEcho + $" = {result}\n{inputStatements}");
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine(errorValue);
                    }
                }

                inputString = Console.ReadLine().Trim(' ');
            }

            Console.WriteLine("End calc execution\n");

        }
    }
}
