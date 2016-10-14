using System;
using System.Collections.Generic;
using System.Linq;
using Appson.Composer.Cache;
using Appson.Composer.Emitter;
using Appson.Composer.Factories;


namespace Appson.Composer
{
	[Contract]
	[Component]
	[ComponentCache(null)]
	public class ComponentContext : IComposer, IComponentContext
	{
		#region Private Data

		private readonly Dictionary<string, ICompositionListener> _compositionListeners;
		private readonly Repository _repository;
		private readonly Dictionary<string, object> _variables;

		#endregion

		#region Constructors

		public ComponentContext() : this(true)
		{
		}

		public ComponentContext(bool registerBuiltInComponents)
		{
			_repository = new Repository();
			_variables = new Dictionary<string, object>();
			_compositionListeners = new Dictionary<string, ICompositionListener>();

		    var componentContextFactory = new PreInitializedComponentFactory(this);
            InternalRegister(typeof(IComposer), null, componentContextFactory, false);
            InternalRegister(typeof(IComponentContext), null, componentContextFactory, false);
            InternalRegister(typeof(ComponentContext), null, componentContextFactory, false);

			if (registerBuiltInComponents)
				RegisterBuildInComponents();
		}

		private void RegisterBuildInComponents()
		{
		    InternalRegister(typeof (DefaultComponentCache), null,
		        CreateDefaultFactory(typeof (DefaultComponentCache)), false);
            InternalRegister(typeof(ContractAgnosticComponentCache), null,
                CreateDefaultFactory(typeof(ContractAgnosticComponentCache)), false);
            InternalRegister(typeof(StaticComponentCache), null,
                CreateDefaultFactory(typeof(StaticComponentCache)), false);

            InternalRegister(typeof(IClassEmitter), null,
                CreateDefaultFactory(typeof(DefaultClassEmitter)), false);
            InternalRegister(typeof(IMethodEmitter), null,
                CreateDefaultFactory(typeof(DefaultMethodEmitter)), false);
            InternalRegister(typeof(IPropertyEmitter), null,
                CreateDefaultFactory(typeof(DefaultPropertyEmitter)), false);
            InternalRegister(typeof(IEventEmitter), null,
                CreateDefaultFactory(typeof(DefaultEventEmitter)), false);
		}

		#endregion

		#region IComponentContext implementation

		public virtual void Register(Type contract, Type component)
		{
			Register(contract, ComponentContextUtils.GetComponentDefaultName(component), component);
		}

        public virtual void Register(Type component)
		{
			if (component == null)
				throw new ArgumentNullException();

			Register(ComponentContextUtils.GetComponentDefaultName(component), CreateDefaultFactory(component));
		}

        public virtual void Register(Type contract, string name, Type component)
		{
			if (contract == null)
				throw new ArgumentNullException("contract");
			if (component == null)
				throw new ArgumentNullException("component");

			ComponentContextUtils.ThrowIfNotSubTypeOf(contract, component);

			InternalRegister(contract, name, CreateDefaultFactory(component), true);
		}

        public virtual void Register(IComponentFactory componentFactory)
		{
			Register((string) null, componentFactory);
		}

        public virtual void Register(string name, IComponentFactory componentFactory)
		{
			if (componentFactory == null)
				throw new ArgumentNullException();

			var contracts = componentFactory.GetContractTypes();

			// TODO: Better description of the component causing the exception
			if (!contracts.Any())
				throw new CompositionException("No contracts found for the component factory " + componentFactory);

			foreach (var contract in contracts)
			{
				InternalRegister(contract, name, componentFactory, false);
			}
		}

        public virtual void Register(string name, Type componentType)
		{
			if (componentType == null)
				throw new ArgumentNullException("componentType");

			Register(name, CreateDefaultFactory(componentType));
		}

        public virtual void Register(Type contract, IComponentFactory factory)
		{
			Register(contract, null, factory);
		}

        public virtual void Register(Type contract, string name, IComponentFactory factory)
		{
			if (contract == null)
				throw new ArgumentNullException("contract");
			if (factory == null)
				throw new ArgumentNullException("factory");

			InternalRegister(contract, name, factory, true);
		}

        public virtual void Unregister(ContractIdentity identity)
		{
			if (identity == null)
				throw new ArgumentNullException();

			_repository.Remove(identity);
		}

        public virtual void UnregisterFamily(Type type)
		{
			if (type == null)
				throw new ArgumentNullException();

			_repository.RemoveAll(type);
		}

        public virtual void SetVariableValue(string name, object value)
		{
			if (value == null)
				RemoveVariable(name);
			else
				SetVariable(name, new Lazy<object>(() => value));
		}

        public virtual void SetVariable(string name, Lazy<object> value)
		{
			RemoveVariable(name);

			if (value == null)
				return;

			_variables.Add(name, value);
		}

        public virtual void RemoveVariable(string name)
		{
			if (name == null)
				throw new ArgumentNullException();

			_variables.Remove(name);
		}

        public virtual void RegisterCompositionListener(string name, ICompositionListener listener)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			if ((_compositionListeners.ContainsKey(name)) && (listener != null))
				throw new ArgumentException(
					string.Format("Another composition listener with the name '{0}' is already registered.", name));

			if (listener == null)
				_compositionListeners.Remove(name);
			else
				_compositionListeners[name] = listener;
		}

        public virtual void UnregisterCompositionListener(string name)
		{
			RegisterCompositionListener(name, null);
		}

		#endregion

		#region IComposer implementation

        public virtual TContract GetComponent<TContract>()
			where TContract : class
		{
			return GetComponent(typeof (TContract), null) as TContract;
		}

        public virtual TContract GetComponent<TContract>(string name)
			where TContract : class
		{
			return GetComponent(typeof (TContract), name) as TContract;
		}

        public virtual object GetComponent(Type contract)
		{
			return GetComponent(contract, null);
		}

        public virtual object GetComponent(Type contract, string name)
		{
			ComponentContextUtils.ThrowIfNotContract(contract);

			if (contract.ContainsGenericParameters)
				throw new CompositionException("Requested contract type " + contract.Name +
				                               " contains open generic parameters. Can not construct a concrete type.");

			var identity = new ContractIdentity(contract, name);
			var factories = _repository.FindFactories(identity);

            return factories
                .Select(f => f.GetComponentInstance(identity, _compositionListeners.Values))
                .FirstOrDefault(result => result != null);
		}

        public virtual IEnumerable<TContract> GetAllComponents<TContract>()
			where TContract : class
		{
			return GetAllComponents(typeof (TContract), null).Cast<TContract>();
		}

        public virtual IEnumerable<TContract> GetAllComponents<TContract>(string name)
			where TContract : class
		{
			return GetAllComponents(typeof (TContract), name).Cast<TContract>();
		}

        public virtual IEnumerable<object> GetAllComponents(Type contract)
		{
			return GetAllComponents(contract, null);
		}

        public virtual IEnumerable<object> GetAllComponents(Type contract, string name)
		{
			ComponentContextUtils.ThrowIfNotContract(contract);

			var identity = new ContractIdentity(contract, name);
			var factories = _repository.FindFactories(identity);

			return factories
				.Select(f => f.GetComponentInstance(identity, _compositionListeners.Values))
				.Where(result => result != null);
		}

        public virtual IEnumerable<TContract> GetComponentFamily<TContract>()
		{
			return GetComponentFamily(typeof (TContract)).Cast<TContract>();
		}

        public virtual IEnumerable<object> GetComponentFamily(Type contract)
		{
			var identities = _repository.GetContractIdentityFamily(contract);

			return identities.SelectMany(identity => _repository.FindFactories(identity),
			                             (identity, factory) =>
			                             factory.GetComponentInstance(identity, _compositionListeners.Values));
		}

        public virtual object GetVariable(string name)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			var variableValue = _variables.ContainsKey(name) ? _variables[name] : null;

			if (variableValue is Lazy<object>)
				variableValue = ((Lazy<object>) variableValue).Value;

			return variableValue;
		}

        public virtual void InitializePlugs<T>(T componentInstance)
		{
			InitializePlugs(componentInstance, typeof (T));
		}

        public virtual void InitializePlugs(object componentInstance, Type componentType)
		{
			var initializationPoints = ComponentContextUtils.ExtractInitializationPoints(this, componentType);

			var initializationPointResults = new List<object>();

			foreach (var initializationPoint in initializationPoints)
			{
				if (initializationPoint.Query == null)
					throw new CompositionException(
						string.Format("Query is null for initialization point '{0}' on component instance of type '{1}'",
						              initializationPoint.Name, componentType.FullName));

				var initializationPointResult = initializationPoint.Query.Query(this);
				initializationPointResults.Add(initializationPointResult);

				ComponentContextUtils.ApplyInitializationPoint(componentInstance,
				                                               initializationPoint.Name,
				                                               initializationPoint.MemberType,
				                                               initializationPointResult);
			}

			foreach (var compositionListener in _compositionListeners.Values)
			{
				compositionListener.OnComponentComposed(null, initializationPoints, initializationPointResults, componentType,
				                                        componentInstance, componentInstance);
			}

			var compositionNotificationMethods = ComponentContextUtils.FindCompositionNotificationMethods(componentType);
			if (compositionNotificationMethods != null)
			{
				foreach (var method in compositionNotificationMethods)
				{
					method.Invoke(componentInstance, new object[0]);
				}
			}
		}

		#endregion

		#region Private helper methods

		private IComponentFactory CreateDefaultFactory(Type component)
		{
			IComponentFactory result;
			if ((component.IsGenericType) && (component.ContainsGenericParameters))
				result = new GenericLocalComponentFactory(component);
			else
				result = new LocalComponentFactory(component);

			return result;
		}

		private void InternalRegister(Type contract, string name, IComponentFactory factory,
									  bool performChecking)
		{
			if (contract == null)
				throw new ArgumentNullException("contract");
			if (factory == null)
				throw new ArgumentNullException("factory");

			if (performChecking)
				ComponentContextUtils.ThrowIfNotContract(contract);

			factory.Initialize(this);

			_repository.Add(new ContractIdentity(contract, name), factory);
		}

		#endregion
	}
}