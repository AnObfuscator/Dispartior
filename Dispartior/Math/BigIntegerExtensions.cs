using System;
using System.Numerics;

namespace Dispartior.Math
{
    public static class BigIntegerExtensions
    {
        public static BigInteger Sqrt(this BigInteger number)
        {
            var one = BigInteger.One;

            var bytes = number.ToByteArray().GetLength(0);

            var nextBit = bytes * 8;
            BigInteger result = 0; 
            BigInteger resultSquared = 0; 

            while(nextBit>=0) 
            {
                var sr2=resultSquared;
                var sr=result;

                resultSquared += (result<<(1+nextBit)) + (1<<(nextBit+nextBit));      
                result += one << nextBit;
                if (resultSquared > number) 
                {
                    result = sr;
                    resultSquared = sr2;
                }
                nextBit--;
            }
            return result;
        }
            
    }
}

