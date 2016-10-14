
using System;
using System.Reflection;

namespace Appson.Composer
{
	public class InitializationPointSpecification
	{
		public InitializationPointSpecification(string name, MemberTypes memberType, bool required = true, ICompositionalQuery query = null)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			Name = name;
			MemberType = memberType;
			Required = required;
			Query = query;
		}

		public string Name { get; private set; }
		public MemberTypes MemberType { get; private set; }
		public bool Required { get; private set; }

		public ICompositionalQuery Query { get; set; }
	}
}
