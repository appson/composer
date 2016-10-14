using System.Linq;
using Compositional.Composer.Factories;
using Compositional.Composer.UnitTests.CompositionListener.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.CompositionListener
{
	[TestClass]
	public class EventParamsTest
	{
		public TestContext TestContext { get; set; }
		private ComponentContext _context;
		private ParameterRecorderListener _listener;

		#region Additional test attributes

		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new ComponentContext();
			_context.Register(typeof(SampleComponentOne));

			_listener = new ParameterRecorderListener();
			_context.RegisterCompositionListener("recorder", _listener);

			_context.GetComponent<ISampleContract>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void OnComponentCreatedParams()
		{
			Assert.IsNotNull(_listener.CreatedIdentity);
			Assert.AreEqual(_listener.CreatedIdentity.Type, typeof (ISampleContract));
			Assert.IsNull(_listener.CreatedIdentity.Name);

			Assert.IsNotNull(_listener.CreatedComponentFactory);
			Assert.IsInstanceOfType(_listener.CreatedComponentFactory, typeof(LocalComponentFactory));
			Assert.AreEqual(((LocalComponentFactory) _listener.CreatedComponentFactory).TargetType, typeof (SampleComponentOne));

			Assert.IsNotNull(_listener.CreatedComponentTargetType);
			Assert.AreEqual(_listener.CreatedComponentTargetType, typeof(SampleComponentOne));

			Assert.IsNotNull(_listener.CreatedComponentInstance);
			Assert.IsInstanceOfType(_listener.CreatedComponentInstance, typeof(SampleComponentOne));

			Assert.IsNotNull(_listener.CreatedOriginalInstance);
			Assert.IsInstanceOfType(_listener.CreatedOriginalInstance, typeof(SampleComponentOne));
			Assert.AreSame(_listener.CreatedComponentInstance, _listener.CreatedOriginalInstance);
		}

		[TestMethod]
		public void OnComponentComposedParams()
		{
			Assert.IsNotNull(_listener.ComposedIdentity);
			Assert.AreEqual(_listener.ComposedIdentity.Type, typeof (ISampleContract));
			Assert.IsNull(_listener.ComposedIdentity.Name);
			
			Assert.IsNotNull(_listener.ComposedInitializationPoints);
			Assert.AreEqual(0, _listener.ComposedInitializationPoints.Count());

			Assert.IsNotNull(_listener.ComposedInitializationPointResults);
			Assert.AreEqual(0, _listener.ComposedInitializationPointResults.Count());

			Assert.IsNotNull(_listener.ComposedComponentTargetType);
			Assert.AreEqual(_listener.ComposedComponentTargetType, typeof(SampleComponentOne));

			Assert.IsNotNull(_listener.ComposedComponentInstance);
			Assert.IsInstanceOfType(_listener.ComposedComponentInstance, typeof(SampleComponentOne));

			Assert.IsNotNull(_listener.ComposedOriginalInstance);
			Assert.IsInstanceOfType(_listener.ComposedOriginalInstance, typeof(SampleComponentOne));
			Assert.AreSame(_listener.ComposedComponentInstance, _listener.ComposedOriginalInstance);
		}

		[TestMethod]
		public void OnComponentRetrievedParams()
		{
			Assert.IsNotNull(_listener.RetrievedIdentity);
			Assert.AreEqual(_listener.RetrievedIdentity.Type, typeof (ISampleContract));
			Assert.IsNull(_listener.RetrievedIdentity.Name);

			Assert.IsNotNull(_listener.RetrievedComponentFactory);
			Assert.IsInstanceOfType(_listener.RetrievedComponentFactory, typeof(LocalComponentFactory));
			Assert.AreEqual(((LocalComponentFactory) _listener.RetrievedComponentFactory).TargetType, typeof (SampleComponentOne));

			Assert.IsNotNull(_listener.RetrievedComponentTargetType);
			Assert.AreEqual(_listener.RetrievedComponentTargetType, typeof(SampleComponentOne));

			Assert.IsNotNull(_listener.RetrievedComponentInstance);
			Assert.IsInstanceOfType(_listener.RetrievedComponentInstance, typeof(SampleComponentOne));

			Assert.IsNotNull(_listener.RetrievedOriginalInstance);
			Assert.IsInstanceOfType(_listener.RetrievedOriginalInstance, typeof(SampleComponentOne));
			Assert.AreSame(_listener.RetrievedComponentInstance, _listener.RetrievedOriginalInstance);
		}
	}
}
