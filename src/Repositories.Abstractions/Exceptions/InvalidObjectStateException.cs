using System;

namespace JMC.Repositories.Abstractions.Exceptions
{
	public class InvalidObjectStateException : Exception
	{
		public string Property { get; private set; }

		public InvalidObjectStateException(string propertyName)
			: base($"Property {propertyName} must be set.")
		{
			this.Property = propertyName;
		}
	}
}
