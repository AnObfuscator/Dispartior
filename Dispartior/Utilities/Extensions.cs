using System;
using System.Collections.Generic;

namespace Dispartior.Utilities
{
	public static class Extensions
	{
		public static bool IsEmpty<T>(this Queue<T> queue)
		{
			return queue.Count == 0;
		}
	}
}

