// <copyright file="OperatorFactoryTests.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using SpreadsheetEngine;

namespace HW6Tests
{
    /// <summary>
    /// Class to test the Factory Method.
    /// </summary>
    public class OperatorFactoryTests
    {
        /// <summary>
        /// Not necessary, usually empty.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Normal test case.
        /// </summary>
        [Test]
        public void TestNormalCase1()
        {
            ExpressionTree test = new ExpressionTree("(A1+9)*5");
            test.SetVariable("A1", 10.5);
            double result = test.Evaluate();
            Assert.That(result, Is.EqualTo(97.5));
        }

        /// <summary>
        /// Normal test case.
        /// </summary>
        [Test]
        public void TestNormalCase2()
        {
            ExpressionTree test = new ExpressionTree("((A1+9)-5)/10");
            test.SetVariable("A1", 50);
            double result = test.Evaluate();
            Assert.That(result, Is.EqualTo(5.4));
        }

        /// <summary>
        /// Edge test case.
        /// </summary>
        [Test]
        public void TestEdge1()
        {
            ExpressionTree test = new ExpressionTree("10");
            double result = test.Evaluate();
            Assert.That(result, Is.EqualTo(10));
        }

        /// <summary>
        /// Edge test case.
        /// </summary>
        [Test]
        public void TestEdge2()
        {
            ExpressionTree test = new ExpressionTree("A1");
            double result = test.Evaluate();
            Assert.That(result, Is.EqualTo(0));
        }

        /// <summary>
        /// Exceptional test case.
        /// </summary>
        [Test]
        public void TestException1()
        {
            ExpressionTree test = new ExpressionTree("1/0");
            Assert.Throws<DivideByZeroException>(() => test.Evaluate());
        }

        /// <summary>
        /// Exceptional test case.
        /// </summary>
        [Test]
        public void TestException2()
        {
            // mismatched parentheses
            Assert.Throws<ArgumentException>(() => new ExpressionTree("(5"));
        }
    }
}