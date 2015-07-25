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
        public static void Test_Sqrt()
        {
            BigInteger two = 2;
            BigInteger four = 4;
            BigInteger nine = 9;
            BigInteger eleven = 11;

            var sqrtZero = BigInteger.Zero.Sqrt();
            var sqrtTwo = two.Sqrt();
            var sqrtFour = four.Sqrt();
            var sqrtNine = nine.Sqrt();
            var sqrtEleven = eleven.Sqrt();

            Assert.AreEqual(BigInteger.Zero, sqrtZero);
            Assert.AreEqual(BigInteger.One, sqrtTwo);
            Assert.AreEqual(new BigInteger(2), sqrtFour);
            Assert.AreEqual(new BigInteger(3), sqrtNine);
        }
    }
}

