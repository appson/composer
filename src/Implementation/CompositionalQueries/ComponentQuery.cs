using System;

namespace Appson.Composer.CompositionalQueries
{
	public class ComponentQuery : ICompositionalQuery
	{
		public ComponentQuery(Type contractType, string contractName)
		{
		    ContractType = contractType ?? throw new ArgumentNullException(nameof(contractType));
			ContractName = contractName;
		}

		#region Implementation of ICompositionalQuery

		public object Query(IComposer composer)
		{
		    if (ContractType == null)
		        return null;

			IComposer composerToUse = ComposerOverride ?? composer;
			if (composerToUse == null)
				throw new ArgumentNullException(nameof(composer));

			return composerToUse.GetComponent(ContractType, ContractName);
		}

		#endregion

		public override string ToString()
		{
			return
			    $"ComponentQuery('{ContractType.FullName}', '{ContractName ?? "<null>"}')";
		}

		public Type ContractType { get; }

		public string ContractName { get; }

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
