// <copyright file="ExpressionTreeTests.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using SpreadsheetEngine;

namespace HW5Tests
{
    /// <summary>
    /// Class to test ExpressionTree methods.
    /// </summary>
    public class ExpressionTreeTests
    {
        /// <summary>
        /// Not necessary, usually empty.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// This will test the normal case for addition +.
        /// </summary>
        [Test]
        public void TestExpressionTreeNormal1()
        {
            ExpressionTree test = new ExpressionTree("NIGHTMARE+9");
            test.SetVariable("NIGHTMARE", 10.5);
            double result = test.Evaluate();
            Assert.That(result, Is.EqualTo(19.5));
        }

        /// <summary>
        /// This will test the normal case for subtraction -.
        /// </summary>
        [Test]
        public void TestExpressionTreeNormal2()
        {
            ExpressionTree test = new ExpressionTree("NIGHTMARE-9.5");
            test.SetVariable("NIGHTMARE", 10.0);
            double result = test.Evaluate();
            Assert.That(result, Is.EqualTo(0.5));
        }

        /// <summary>
        /// This will test the normal case for multiplication *.
        /// </summary>
        [Test]
        public void TestExpressionTreeNormal3()
        {

            ExpressionTree test = new ExpressionTree("NIGHTMARE*9");
            test.SetVariable("NIGHTMARE", 10.0);
            double result = test.Evaluate();
            Assert.That(result, Is.EqualTo(90.0));
        }

        /// <summary>
        /// This will test the normal case for division /.
        /// </summary>
        [Test]
        public void TestExpressionTreeNormal4()
        {

            ExpressionTree test = new ExpressionTree("NIGHTMARE/9");
            test.SetVariable("NIGHTMARE", 10.0);
            double result = test.Evaluate();
            double expected = 10.0 / 9.0; // because having 10 / 9 is actually 1.0 instead of 1.11...
            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// This will test for the edge case scenario 1.
        /// </summary>
        [Test]
        public void TestExpressionTreeEdge1()
        {
            ExpressionTree test = new ExpressionTree("NINE+NINE+NINE+NINE+NINE+NINE+NINE+NINE+NINE+NINE");
            test.SetVariable("NINE", 9);
            double result = test.Evaluate();
            Assert.That(result, Is.EqualTo(90));
        }

        /// <summary>
        /// This will test will the edge case scenario 2.
        /// </summary>
        [Test]
        public void TestExpressionTreeEdge2()
        {
            ExpressionTree test = new ExpressionTree("100+100+100+100+100+25.5+25.5+0.0001");

            // lets not set any variable here
            double result = test.Evaluate();
            Assert.That(result, Is.EqualTo(551.0001));
        }

        /// <summary>
        /// This will test for the exceptional case of invalid expressions.
        /// </summary>
        [Test]
        public void TestExpressionTreeExcept1()
        {
            Assert.Throws<NotSupportedException>(() => new ExpressionTree("10;10"));
        }
    }
}