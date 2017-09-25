using System;

namespace Appson.Composer.CompositionalQueries
{
	public class ResourcePlugQuery : ICompositionalQuery
	{
		public ResourcePlugQuery(string resourceId)
		{
		    ResourceId = resourceId ?? throw new ArgumentNullException(nameof(resourceId));
		}

		#region Implementation of ICompositionalQuery

		public object Query(IComposer composer)
		{
			IComposer composerToUse = ComposerOverride ?? composer;
			if (composerToUse == null)
				throw new ArgumentNullException(nameof(composer));

			var provider = composerToUse.GetComponent<IResourceProvider>();
			if (provider == null)
				throw new CompositionException(
					"Can't query for resource managers, no resource provider component is registered with the composer.");

			return provider.GetResourceManager(ResourceId);
		}

		#endregion

		public override string ToString()
		{
			return $"ResourceQuery('{ResourceId}')";
		}

		public string ResourceId { get; }

		/// <summary>
		/// Specifies the instance of IComposer to use for resolving references.
		/// </summary>
		/// <remarks>
		/// Setting this property is not required for the query to work.
		/// If this property is set, its value will be used to resolve the 
		/// IResourceProvider component.
		/// Otherwise, the default instance of the composer (that is passed to
		/// the Query method) will be used to query for the value.
		/// </remarks>
		public IComposer ComposerOverride { get; set; }
	}
}
