using System;

namespace Appson.Composer
{
	/// <summary>
	/// The primary key structure to identify component contracts, which contains a
	/// contract 'type' along with a name string.
	/// </summary>
	/// <remarks>
	/// The 'name' is null by default, but providing a 'type' for the contract is required.
	/// </remarks>
	public class ContractIdentity
	{
		private readonly string _name;
		private readonly Type _type;

		public ContractIdentity(Type type)
			: this(type, null)
		{
		}

		public ContractIdentity(Type type, string name)
		{
            _type = type ?? throw new ArgumentNullException(nameof(type));
			_name = name;
		}

		public Type Type => _type;

	    public string Name => _name;

	    public bool Equals(Type type, string name)
		{
			return _type == type && Equals(_name, name);
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;

			var contractIdentity = obj as ContractIdentity;
			if (ReferenceEquals(contractIdentity, null))
				return false;

			return _type == contractIdentity._type && Equals(_name, contractIdentity._name);
		}

		public override int GetHashCode()
		{
			return _type.GetHashCode() + 29*(_name != null ? _name.GetHashCode() : 0);
		}

		public static bool operator ==(ContractIdentity a, object b)
		{
			return Equals(a, b);
		}

		public static bool operator !=(ContractIdentity a, object b)
		{
			return !Equals(a, b);
		}
	}
}