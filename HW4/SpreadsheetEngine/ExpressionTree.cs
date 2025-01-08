// <copyright file="ExpressionTree.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using System.ComponentModel.Design;
using System.Xml.Linq;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Expression Tree class in the logic engine DLL.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Member BaseNode root.
        /// </summary>
        private BaseNode root;

        /// <summary>
        /// Member Dictionary of variables and its assigned value.
        /// </summary>
        private Dictionary<string, double> variables = new Dictionary<string, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> string parameter.</param>
        public ExpressionTree(string expression)
        {
            this.root = Compile(expression);
        }

        /// <summary>
        /// Dictionary assignment.
        /// </summary>
        /// <param name="name"> the sub-string you want to set a value to.</param>
        /// <param name="value"> the value of the sub-string you want to set.</param>
        public void SetVariable(string name, double value)
        {
            this.variables[name] = value; // dict assignment

            if (this.variables.ContainsKey(name))
            {
                this.variables[name] = value;
            }
            else
            {
                this.variables.TryAdd(name, value);
            }
        }

        /// <summary>
        /// This method is going to recursively call the private Evaluate().
        /// </summary>
        /// <returns> the value of the expression.</returns>
        public double Evaluate()
        {
            return this.Evaluate(this.root);
        }

        /// <summary>
        /// Compiler of the Expression tree, also recursively calls private Compile().
        /// </summary>
        /// <param name="expression"> takes a string parameter.</param>
        /// <returns> either a null or BaseNode type object.</returns>
        private static BaseNode? Compile(string expression) // HEAVY MODIFICATIONS HERE
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            // shunting yard makes me wanna cry.
            Stack<char> opStack = new Stack<char>(); // remark: where if operator symbols are encounter is pushed into
            Stack<BaseNode> postfixStack = new Stack<BaseNode>(); // remark: handles the result of the shunting algorithm (what ever is in the postfixQueue)
            Queue<object> postfixQueue = new Queue<object>(); // remark: holds expression after infix evaluate and is then operated on to following precedence of operators
            int parenthesesCount = 0; // remark: implemented for parentheses robustness

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                // handles constants
                if (char.IsDigit(c)) // if there's a value that starts with a digit, extraction needed to be done (if its a multi-digit constant)
                {
                    double number = 0.0;
                    int start = i;
                    bool isDecimal = false;

                    // as long as there's still a digit after the last, continue to extract
                    // feature to handle decimals added
                    while (i < expression.Length && (char.IsDigit(expression[i]) || (expression[i] == '.' && !isDecimal)))
                    {
                        // number = (number * 10) + (expression[i] - '0'); // multi-digit extraction
                        if (expression[i] == '.')
                        {
                            isDecimal = true;
                        }

                        i++; // keeps parser alive
                    }

                    // once number including decimal has been recognized, is extracted and parsed
                    string numberStr = expression.Substring(start, i - start);
                    number = double.Parse(numberStr);
                    i--; // moves back to avoid skipping characters after the numbers

                    postfixQueue.Enqueue(number);
                }

                // handles operators and parentheses
                else if (c == OperatorNodeFactory.SortOp(c)) // if operator is encountered pushed onto stack
                {
                    if (expression.Length < 2) // somre more invalid formula handling
                    {
                        throw new Exception($"ERROR: Formula cannot start with '{expression}' - Compile()");
                    }

                    // pretty much if the if an operatory symbol follows another its already an invalid formula
                    else if (expression[i + 1] == OperatorNodeFactory.SortOp(expression[i + 1])) // checks for consecutive symbols
                    {
                        throw new Exception("ERROR: Consecutive Operation Symbols - Compile()");
                    }

                    // while opStack is not empty and the top of the stack is not '(' character
                    // and that the precedence of the current operator is less than or equal the precedence of the operator on the stack
                    while (opStack.Count > 0 && opStack.Peek() != '(' && OperatorNodeFactory.GetPrecedence(c) <= OperatorNodeFactory.GetPrecedence(opStack.Peek()))
                    {
                        postfixQueue.Enqueue(opStack.Pop());
                    }

                    opStack.Push(c);
                }
                else if (c == '(') // open parentheses are automatically pushed onto the stack
                {
                    opStack.Push(c);
                    parenthesesCount++;
                }
                else if (c == ')')
                {
                    if (parenthesesCount == 0) // parentheses check 1
                    {
                        throw new ArgumentException("ERROR: Mismatched Closing Parentheses. - Compile()");
                    }

                    // makes sure that pairs of parentheses are being evaluated properly
                    // checks is the stack isn't empty and that the top of the stack isn't a '('
                    // pops the top op and adds it to the queue until an ')' is encounter completing the parenthese syntax
                    while (opStack.Count > 0 && opStack.Peek() != '(')
                    {
                        postfixQueue.Enqueue(opStack.Pop());
                    }

                    opStack.Pop(); // discards '(' from the original pair
                    parenthesesCount--;
                }

                // handling variables
                else if (char.IsLetter(c))
                {
                    string variableName = string.Empty;

                    while (i < expression.Length && char.IsLetterOrDigit(expression[i])) // as long as we recognized the initial letter
                    {
                        variableName += expression[i]; // string appending

                        i++;
                    }

                    i--;
                    postfixQueue.Enqueue(variableName); // same logic as the first conditional statement.
                }

                // handling invalid symbols
                else if (OperatorNodeFactory.SortOp(c) == '\0')
                {
                    throw new Exception("ERROR: Unsupported Operator(s) - Compile()");
                }
            }

            if (parenthesesCount > 0) // parentheses check 2
            {
                throw new ArgumentException("ERROR: Mismatched Opening Parentheses. - Compile()");
            }

            while (opStack.Count > 0) // emptying the stack before proceeding (makes sure no operators are fell behind).
            {
                postfixQueue.Enqueue(opStack.Pop());
            }

            // should be in correct order for evaluation0
            // building the expression tree - setting up for post-fix evaluation
            foreach (var element in postfixQueue)
            {
                if (element is double number)
                {
                    postfixStack.Push(new ConstantNode { Value = number });
                }
                else if (element is string variableName)
                {
                    postfixStack.Push(new VariableNode { Name = variableName });
                }
                else if (element is char opSymbol) // operator encountered must pop first two numbers and perform operation based on he operator symbol encountered
                {
                    BaseNode right = postfixStack.Pop(); // right comes first (this is very important for the final evaluated value)
                    BaseNode left = postfixStack.Pop();
                    postfixStack.Push(OperatorNodeFactory.FactoryMethod(opSymbol, left, right)); // pushing instance of add, subtract, multiply, divide
                }
            }

            return postfixStack.Pop(); // get post-fix expression for evaluation
        }

        /// <summary>
        /// This is what is being called in the public Evaluate() method.
        /// </summary>
        /// <param name="node"> BaseNode type parameter.</param>
        /// <returns> returns the value after arithmetic operations have been performed.</returns>
        /// <exception cref="NotSupportedException"> if there is an invalid symbol.</exception>
        private double Evaluate(BaseNode node)
        {
            ConstantNode? constantNode = node as ConstantNode;
            if (constantNode != null)
            {
                return constantNode.Value;
            }

            VariableNode? variableNode = node as VariableNode;
            if (variableNode != null)
            {
                if (this.variables.ContainsKey(variableNode.Name))
                {
                    return this.variables[variableNode.Name];
                }
            }

            OperatorNode? operatorNode = node as OperatorNode; // MODIFICATIONS HERE
            if (operatorNode != null)
            {
                // the fun part
                var whichOp = OperatorNodeFactory.FactoryMethod(operatorNode.Operator, operatorNode.L, operatorNode.R);

                switch (whichOp)
                {
                    case AdditionOperatorNode:
                        return this.Evaluate(operatorNode.L) + this.Evaluate(operatorNode.R);

                    case SubtractionOperatorNode:
                        return this.Evaluate(operatorNode.L) - this.Evaluate(operatorNode.R);

                    case MultiplyOperatorNode:
                        return this.Evaluate(operatorNode.L) * this.Evaluate(operatorNode.R);

                    case DivideOperatorNode:
                        if (this.Evaluate(operatorNode.R) == 0)
                        {
                            throw new DivideByZeroException("ERROR: Divide by Zero - Evaluate()");
                        }

                        return this.Evaluate(operatorNode.L) / this.Evaluate(operatorNode.R);

                    default:
                        break;
                }
            }

            throw new NotSupportedException("ERROR: Unsupported Node type - Evaluate()");
        }
    }
}