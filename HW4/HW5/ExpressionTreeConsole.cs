// <copyright file="ExpressionTreeConsole.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using System.Linq.Expressions;
using System.Net.Quic;
using System.Reflection.Metadata;
using SpreadsheetEngine;

namespace HW5
{
    /// <summary>
    /// This is a class where this demo for ExpressionTree will take place.
    /// </summary>
    internal static class ExpressionTreeConsole
    {
        /// <summary>
        /// This is demo application.
        /// </summary>
        private static void Main()
        {
            int userInput = 0;
            int quit = 0;
            string expression = "A1+B1+C1"; // default expression
            ExpressionTree expressionTree = new ExpressionTree(expression);

            do
            {
                Console.WriteLine("Menu (Current Expression: \"" + expression + "\")\n");
                Console.WriteLine("\t 1 = Enter a new expression.\n");
                Console.WriteLine("\t 2 = Set a variable value\n");
                Console.WriteLine("\t 3 = Evaluate tree\n");
                Console.WriteLine("\t 4 = Quit\n");

                userInput = int.Parse(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        Console.WriteLine("Enter an expression: ");
                        expression = Console.ReadLine();
                        expressionTree = new ExpressionTree(expression);
                        break;

                    case 2:
                        Console.WriteLine("Enter a variable: ");
                        string userVar = Console.ReadLine();
                        Console.WriteLine("What do you want to set " + userVar + " to? Enter a value: ");
                        double userVarValue = Convert.ToDouble(Console.ReadLine());
                        expressionTree.SetVariable(userVar, userVarValue);
                        break;

                    case 3:
                        Console.WriteLine("Evaluated value: " + expressionTree.Evaluate().ToString() + "\n");
                        break;

                    case 4:
                        Console.WriteLine("Done");
                        quit = 1;
                        break;

                    default:
                        break; // ignores invalid inputs
                }
            }
            while (quit != 1);
        }
    }
}