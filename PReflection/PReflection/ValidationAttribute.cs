using System;

namespace PReflection
{
	public abstract class ValidationAttribute : Attribute {
		public abstract string Validate(object value);
	}
}

