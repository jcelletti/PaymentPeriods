using System;

namespace JMC.Repositories.Abstractions.Exceptions
{
	public class DuplicateObjectException : Exception
	{
		public string Property { get; private set; }

		public DuplicateObjectException(string propertyName)
			: base($"Property {propertyName} is a duplicate.")
		{
			this.Property = propertyName;
		}
	}
}
