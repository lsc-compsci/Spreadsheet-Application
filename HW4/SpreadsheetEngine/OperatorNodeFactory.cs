// <copyright file="OperatorNodeFactory.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This is a public static class for the Operator Factory.
    /// </summary>
    public static class OperatorNodeFactory
    {
        /// <summary>
        /// Static and Read-only Dict.
        /// Note: Operators Nodes have been modified to be parameter-less.
        /// Note: var name was initial in lower-case; corrected to follow stylecops.
        /// </summary>
        private static readonly Dictionary<char, Type> Operators = new Dictionary<char, Type>()
        {
            { '+', typeof(AdditionOperatorNode) },
            { '-', typeof(SubtractionOperatorNode) },
            { '*', typeof(MultiplyOperatorNode) },
            { '/', typeof(DivideOperatorNode) },
        };

        /// <summary>
        /// Initializes static members of the <see cref="OperatorNodeFactory"/> class.
        /// Updated OperatorNodeFactory Constructor.
        /// </summary>
        static OperatorNodeFactory()
        {
            TraverseAvailableOperator((char opSymbol, Type type) =>
            {
                Operators[opSymbol] = type;
            });
        }

        /// <summary>
        /// Delegate.
        /// </summary>
        /// <param name="op"> char type.</param>
        /// <param name="type"> Type variable. </param>
        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// This is the factory method. Modified from the lecture slide.
        /// Note: There were some slight changes so that it could be adopted into my program.
        /// </summary>
        /// <param name="op"> takes in a char symbol.</param>
        /// <param name="leftChild"> takes in left node.</param>
        /// <param name="rightChild"> takes in right node.</param>
        /// <returns> a new instance of of each operation OperatorNode.</returns>
        /// <exception cref="ArgumentException"> thrown if there isn't a valid operator symbol.</exception>
        public static OperatorNode FactoryMethod(char op, BaseNode leftChild, BaseNode rightChild)
        {
            if (Operators.ContainsKey(op))
            {
                object operatorNodeObj = System.Activator.CreateInstance(Operators[op]);

                if (operatorNodeObj is OperatorNode operatorNode)
                {
                    operatorNode.L = leftChild;
                    operatorNode.R = rightChild;
                    return operatorNode;
                }
            }

            throw new Exception("Unhandled Operator - FactoryMethod()");
        }

        /// <summary>
        /// Will be used an an auxiliary method to help Compile() method.
        /// </summary>
        /// <param name="op"> takes in char op symbol.</param>
        /// <returns> the enumerated precedence in class OperatorNode.</returns>
        public static int GetPrecedence(char op)
        {
            switch (op)
            {
                case '+':
                    return (int)OperatorNode.OpPrecedence.Add; // 1
                case '-':
                    return (int)OperatorNode.OpPrecedence.Subtract; // 1
                case '*':
                    return (int)OperatorNode.OpPrecedence.Multiply; // 2
                case '/':
                    return (int)OperatorNode.OpPrecedence.Divide; // 2
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Will be used as an auxiliary method to help Compile() method.
        /// </summary>
        /// <param name="op"> char op.</param>
        /// <returns> add, minus, multiply, divide.</returns>
        public static char SortOp(char op)
        {
            switch (op)
            {
                case '+':
                    return '+';
                case '-':
                    return '-';
                case '*':
                    return '*';
                case '/':
                    return '/';
                default:
                    return '\0';
            }
        }

        /// <summary>
        /// TraverseAvailableOperator taken from w8 lectures.
        /// Modified.
        /// </summary>
        /// <param name="onOperator"> delegate type parameter.</param>
        private static void TraverseAvailableOperator(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(OnOperator);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                foreach (var type in operatorTypes)
                {
                    PropertyInfo? operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        object value = operatorField.GetValue(type);

                        if (value is char)
                        {
                            char operatorSymbol = (char)value;

                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
