using System;
using NUnit.Framework;
using System.Numerics;
using Dispartior.Math;

namespace Dispartior.Tests.Math
{
    [TestFixture]
    public class BigIntegerExtensionsTest
    {

        [Test]
        [ExpectedException]
        public static void Test_Sqrt_Neg1throwsException()
        {
            var neg1 = new BigInteger(-1);

            var sqrt = neg1.Sqrt();
        }

        [Test]
        public static void Test_Sqrt_0returns0()
        {
            var sqrtZero = BigInteger.Zero.Sqrt();

            Assert.AreEqual(BigInteger.Zero, sqrtZero);
        }

        [Test]
        public static void Test_Sqrt_2returns1()
        {
            BigInteger two = 2;

            var sqrtTwo = two.Sqrt();

            Assert.AreEqual(BigInteger.One, sqrtTwo);
        }

        [Test]
        public static void Test_Sqrt_4returns2()
        {
            BigInteger four = 4;

            var sqrtFour = four.Sqrt();

            Assert.AreEqual(new BigInteger(2), sqrtFour);
        }

        [Test]
        public static void Test_Sqrt_9returns3()
        {
            BigInteger nine = 9;

            var sqrtNine = nine.Sqrt();

            Assert.AreEqual(new BigInteger(3), sqrtNine);
        }

        [Test]
        public static void Test_Sqrt_11returns3()
        {
            BigInteger eleven = 11;

            var sqrtEleven = eleven.Sqrt();

            Assert.AreEqual(new BigInteger(3), sqrtEleven);
        }
    }
}

