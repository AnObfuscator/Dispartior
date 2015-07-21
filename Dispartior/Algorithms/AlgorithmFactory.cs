using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Dispartior.Algorithms
{
    public class AlgorithmFactory
    {
        private readonly IDictionary<string, Type> algorithms;

        public AlgorithmFactory()
        {
            algorithms = new ConcurrentDictionary<string, Type>();
        }

        public void RegisterAlgorithm<T>() where T : IAlgorithm, new()
        {
            var name = typeof(T).Name;
            algorithms[name] = typeof(T);
        }

        public IAlgorithm CreateAlgorithm<T>() where T: IAlgorithm, new()
        {
            var name = typeof(T).Name;
            return CreateAlgorithm(name);
        }

        public IAlgorithm CreateAlgorithm(string algorithmName)
        {
			try
			{
				var algoType = algorithms[algorithmName];
				return (IAlgorithm)Activator.CreateInstance(algoType);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error creating algo " + algorithmName + ": " + ex.Message);
				return null;
			}
        }
    }
}

