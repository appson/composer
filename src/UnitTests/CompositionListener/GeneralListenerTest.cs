using System;
using Appson.Composer.Diagnostics;
using Appson.Composer.UnitTests.CompositionListener.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.CompositionListener
{
	[TestClass]
	public class GeneralListenerTest
	{
		public TestContext TestContext { get; set; }
		private ComponentContext _context;

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
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullNameThrows()
		{
			_context.RegisterCompositionListener(null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void DuplicationNameRegistrationThrows()
		{
			var listener1 = new CountingCompositionListener();
			var listener2 = new CountingCompositionListener();

			_context.RegisterCompositionListener("duplicateName", listener1);
			_context.RegisterCompositionListener("duplicateName", listener2);
		}

		[TestMethod]
		public void RemoveListener()
		{
			_context.Register(typeof(NonSharedComponent));

			var listener = new CountingCompositionListener();
			_context.RegisterCompositionListener("counter", listener);

			_context.GetComponent<ISampleContract>();
			_context.GetComponent<ISampleContract>();

			_context.UnregisterCompositionListener("counter");

			_context.GetComponent<ISampleContract>();
			_context.GetComponent<ISampleContract>();
			_context.GetComponent<ISampleContract>();

			Assert.AreEqual(2, listener.OnComponentCreatedCount);
			Assert.AreEqual(2, listener.OnComponentComposedCount);
			Assert.AreEqual(2, listener.OnComponentRetrievedCount);
		}
	}
}
