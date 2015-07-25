using System;

namespace Dispartior.Data
{
    /*
	 * Interface to make DataSource type system and serialization work
	 */
    public interface IDataSetDefinition
    {
        string TypeName { get; set; }
    }
}

