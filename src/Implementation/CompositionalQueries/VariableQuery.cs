using System;

namespace Appson.Composer.CompositionalQueries
{
	public class VariableQuery : ICompositionalQuery
	{
		public VariableQuery(string variableName)
		{
		    VariableName = variableName ?? throw new ArgumentNullException(nameof(variableName));
		}

		#region Implementation of ICompositionalQuery

		public object Query(IComposer composer)
		{
			IComposer composerToUse = ComposerOverride ?? composer;
			if (composerToUse == null)
				throw new ArgumentNullException(nameof(composer));

			return composerToUse.GetVariable(VariableName);
		}

		#endregion

		public override string ToString()
		{
			return $"Query for Variable named: '{VariableName}'";
		}

		public string VariableName { get; }

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
