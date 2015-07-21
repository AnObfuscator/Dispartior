using System;

namespace Dispartior.Algorithms
{
    public interface IAlgorithm
    {
		IAlgorithmRunner AlgorithmRunner { get; set; }

		void Run();
    }
}

