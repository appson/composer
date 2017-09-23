
using System;
using System.Reflection;

namespace Appson.Composer
{
	public class InitializationPointSpecification
	{
		public InitializationPointSpecification(string name, MemberTypes memberType, bool required = true, ICompositionalQuery query = null)
		{
		    Name = name ?? throw new ArgumentNullException(nameof(name));
			MemberType = memberType;
			Required = required;
			Query = query;
		}

		public string Name { get; }
		public MemberTypes MemberType { get; }
		public bool Required { get; }

		public ICompositionalQuery Query { get; set; }
	}
}
