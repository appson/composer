using System;
using System.Collections.Generic;

namespace Appson.Composer
{
	/// <summary>
	/// Specifies the extension points for the Composer implementation to plug additional
	/// logic in the process of instantiating components.
	/// </summary>
	public interface ICompositionListener
	{
		/// <summary>
		/// Invoked when a component instance is created by the context.
		/// </summary>
		/// <remarks>
		/// 
		/// This method of registered composition listeners are invoked when
		/// there is a request for a component instance by providing a contract
		/// identity, which leads to instantiation of the component.
		/// 
		/// For singleton local components, it is called on the first request
		/// only, where for the prototype components it is called each time, as
		/// a new instance is constructed every time.
		/// 
		/// The composition listener has the option of replacing the component
		/// instance with a compatible type (which can be identified by the
		/// passed contract identity). If replaced, the new instance will
		/// be substituted for all later operations, including caching of
		/// singleton components. There will be no access to the original
		/// instance of the component possible afterwards, except in
		/// other composition listeners.
		/// 
		/// </remarks>
		/// <param name="identity">The contract identity used to query for the component, which lead to the creation of the instance.</param>
		/// <param name="componentFactory">The instance of the component factory which created the component instance.</param>
		/// <param name="componentTargetType">Target type of the component, if available.</param>
		/// <param name="componentInstance">Reference to the created component instance.</param>
		/// <param name="originalInstance">Reference to the originally created instance before any modifications by other listeners.</param>
		void OnComponentCreated(ContractIdentity identity, IComponentFactory componentFactory,
		                        Type componentTargetType, ref object componentInstance, object originalInstance);

		/// <summary>
		/// Invoked when a component finishes the composition process, and all its plugs
		/// and configuration points are set by composer.
		/// </summary>
		/// <remarks>
		/// 
		/// For the components which are created by the composer itself,
		/// the call frequency of this method is similar to that of 
		/// OnComponentCreated method.
		/// 
		/// This method is also called when a component instance is given to
		/// the composer for initializing the plugs (using InitializePlugs
		/// method).
		/// 
		/// This method can not modify the component instance, as there are
		/// usage scenarios in which this is not possible (like when calling
		/// InitializePlugs method)
		/// 
		/// The "AfterComposition" method of the component itself is invoked
		/// AFTER this method is called on all registered composition listeners,
		/// if any.
		/// 
		/// </remarks>
		/// <param name="identity">The contract identity used to lookup the componet.</param>
		/// <param name="initializationPoints">The list of initialization points used in the composition of the component.</param>
		/// <param name="initializationPointResults">The list of the results returned when applying initialization points to the component.</param>
		/// <param name="componentTargetType">The type of the component which is composed.</param>
		/// <param name="componentInstance">Reference to the component instance which is just finished being composed.</param>
		/// <param name="originalInstance">Reference to the original instance if any listeners have changed the component previously, or otherwise the component instance itself.</param>
		void OnComponentComposed(ContractIdentity identity, IEnumerable<InitializationPointSpecification> initializationPoints,
		                         IEnumerable<object> initializationPointResults, Type componentTargetType,
		                         object componentInstance, object originalInstance);

		/// <summary>
		/// Invoked when a component is looked up and retrieved by the composer.
		/// </summary>
		/// <remarks>
		/// 
		/// This method is called on the registered composition listeners every time
		/// a component is queried using GetComponent method, or when composing
		/// initialization points of other components. Even if the component
		/// is a local singleton one, this method is invoked every time the
		/// cache is accessed.
		/// 
		/// This method is not invoked when the InitializePlugs method of the
		/// composer is called for an externally-created and available component.
		/// 
		/// </remarks>
		/// <param name="identity">The contract identity used to lookup the componet.</param>
		/// <param name="componentFactory">The component factory which has created / cached the component instance.</param>
		/// <param name="componentTargetType">The type of the component itself.</param>
		/// <param name="componentInstance">The current instance of the component being retrieved.</param>
		/// <param name="originalInstance">Reference to the originally created component</param>
		void OnComponentRetrieved(ContractIdentity identity, IComponentFactory componentFactory,
		                          Type componentTargetType, ref object componentInstance, object originalInstance);
	}
}