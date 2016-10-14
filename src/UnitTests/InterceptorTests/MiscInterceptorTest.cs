using Appson.Composer.Emitter;
using Appson.Composer.Interceptor;
using Appson.Composer.UnitTests.InterceptorTests.Components;
using Appson.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.InterceptorTests
{
	[TestClass]
	public class MiscInterceptorTest
	{
		public TestContext TestContext { get; set; }
		private ComponentContext _context;
		private IClassEmitter _classEmitter;

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
			_classEmitter = _context.GetComponent<IClassEmitter>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void CompleteCallInBeforeCall()
		{
			var impl = new SomeInterfaceImplementation();
			var i = new RecordingInterceptor();
			var h = new InterceptingAdapterEmittedTypeHanlder(impl, i);
			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);

			i.BeforeCallAction = delegate(CallInfo ci)
			                     	{
			                     		ci.ReturnValue = "AnotherValue";
			                     		ci.Completed = true;
			                     	};

			var result = o.SomeMethod("SomeString", 17);

			// Adapted method should not be called

			Assert.IsNull(impl.SomeMethodS);
			Assert.AreEqual(0, impl.SomeMethodI);

			// Both BeforeCall and AfterCall should have been called

			Assert.IsNotNull(i.BeforeCallInfo);
			Assert.IsNotNull(i.AfterCallInfo);

			// Result should be the value set by interceptor

			Assert.AreEqual("AnotherValue", result);
		}

		[TestMethod]
		public void ChangeValueInAfterCall()
		{
			var impl = new SomeInterfaceImplementation();
			var i = new RecordingInterceptor();
			var h = new InterceptingAdapterEmittedTypeHanlder(impl, i);
			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);

			i.AfterCallAction = delegate(CallInfo ci)
			                     	{
			                     		ci.ReturnValue = "AnotherValue";
			                     	};

			var result = o.SomeMethod("SomeString", 17);

			// Adapted method should have been called

			Assert.IsNotNull(impl.SomeMethodS);
			Assert.AreEqual("SomeString", impl.SomeMethodS);
			Assert.AreEqual(17, impl.SomeMethodI);

			// Both BeforeCall and AfterCall should have been called

			Assert.IsNotNull(i.BeforeCallInfo);
			Assert.IsNotNull(i.AfterCallInfo);

			// Result should be the value set by interceptor

			Assert.AreEqual("SomeValue", i.AfterCallInfo.ReturnValue);
			Assert.AreEqual("AnotherValue", result);
		}
	}
}