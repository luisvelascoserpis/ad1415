using System;

namespace PReflection
{
	public class ErrorInfo
	{
		public ErrorInfo (string property, string message) {
			this.property = property;
			this.message = message;
		}
		private string property;
		private string message;
		public string Property { get { return property; } }
		public string Message { get { return message; } }
	}
}

