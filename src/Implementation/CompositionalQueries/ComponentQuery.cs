using System;

namespace Appson.Composer.CompositionalQueries
{
	public class ComponentQuery : ICompositionalQuery
	{
		public ComponentQuery(Type contractType, string contractName)
		{
			if (contractType == null)
				throw new ArgumentNullException("contractType");

			ContractType = contractType;
			ContractName = contractName;
		}

		#region Implementation of ICompositionalQuery

		public object Query(IComposer composer)
		{
			IComposer composerToUse = ComposerOverride ?? composer;
			if (composerToUse == null)
				throw new ArgumentNullException("composer");

			return composerToUse.GetComponent(ContractType, ContractName);
		}

		#endregion

		public override string ToString()
		{
			return string.Format("Query for Component with ContractType: '{0}', ContractName: '{1}'",
			                     ContractType.FullName,
			                     ContractName ?? "<null>");
		}

		public Type ContractType { get; private set; }

		public string ContractName { get; private set; }

		/// <summary>
		/// Specifies the instance of IComposer to use for resolving references.
		/// </summary>
		/// <remarks>
		/// Setting this property is not required for the query to work.
		/// If this property is set, its value will be used to resolve the component.
		/// Otherwise, the default instance of the composer (that is passed to
		/// the Query method) will be used to query for the value.
		/// </remarks>
		public IComposer ComposerOverride { get; set; }

	}
}
