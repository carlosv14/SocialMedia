using System;

namespace SocialMedia.Console
{
    //Array.Filter()
    class Program
    {
        static double Multiply(double value1, double value2)
        {
            return value1 * value2;
        }
        static void Main(string[] args)
        {
            Action<string, double> print = (message, result) => System.Console.WriteLine($"{message} : {result}");
            double op1 = 5;
            double op2 = 6;
            Operate((a, b) => a + b, op1, op2, print, "La suma es");
            Operate((a, b) => a - b, op1, op2, print, "La resta es");
            Operate(Multiply, op1, op2, print, "La multiplicación es");

           
        }

        static void Operate(Func<double, double, double> operation, double a, double b, Action<string, double> print, string message)
        {
            var result = operation(a, b);
            print(message, result);
        }
    }
}
