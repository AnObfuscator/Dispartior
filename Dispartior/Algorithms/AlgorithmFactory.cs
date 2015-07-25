using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Dispartior.Data;

namespace Dispartior.Algorithms
{
    public class AlgorithmFactory
    {
        private readonly IDictionary<string, Type> algorithms;
        private readonly DataSource dataSource;

        public AlgorithmFactory(DataSource dataSource)
        {
            algorithms = new ConcurrentDictionary<string, Type>();
            this.dataSource = dataSource;
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
                var algo = (IAlgorithm)Activator.CreateInstance(algoType);
                algo.DataSource = dataSource;
                return algo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating algo " + algorithmName + ": " + ex.Message);
                return null;
            }
        }
    }
}

