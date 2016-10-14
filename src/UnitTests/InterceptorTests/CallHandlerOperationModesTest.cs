using Appson.Composer.Emitter;
using Appson.Composer.Interceptor;
using Appson.Composer.UnitTests.InterceptorTests.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appson.Composer.Utility;

namespace Appson.Composer.UnitTests.InterceptorTests
{
	[TestClass]
	public class CallHandlerOperationModesTest
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
		public void InterceptorOnly()
		{
			var i = new RecordingInterceptor();
			var h = new InterceptingAdapterEmittedTypeHanlder(i);

			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);
			o.SomeMethod("StringParam", 17);

			Assert.IsNotNull(i.BeforeCallInfo);
			Assert.IsNotNull(i.AfterCallInfo);

			//
			// BeforeCall

			Assert.AreEqual(2, i.BeforeCallInfo.Arguments.Length);
			Assert.AreEqual("StringParam", i.BeforeCallInfo.Arguments[0]);
			Assert.AreEqual(17, i.BeforeCallInfo.Arguments[1]);

			Assert.AreEqual(2, i.BeforeCallInfo.ArgumentTypes.Length);
			Assert.AreEqual(typeof(string), i.BeforeCallInfo.ArgumentTypes[0]);
			Assert.AreEqual(typeof(int), i.BeforeCallInfo.ArgumentTypes[1]);

			Assert.AreEqual(typeof(ISomeInterface), i.BeforeCallInfo.MethodOwner);
			Assert.AreEqual("SomeMethod", i.BeforeCallInfo.MethodName);
			Assert.AreEqual(typeof(string), i.BeforeCallInfo.ResultType);

			Assert.IsNull(i.BeforeCallInfo.ReturnValue);
			Assert.IsNull(i.BeforeCallInfo.ThrownException);
			Assert.IsFalse(i.BeforeCallInfo.Completed);

			//
			// AfterCall

			Assert.AreEqual(2, i.AfterCallInfo.Arguments.Length);
			Assert.AreEqual("StringParam", i.AfterCallInfo.Arguments[0]);
			Assert.AreEqual(17, i.AfterCallInfo.Arguments[1]);

			Assert.AreEqual(2, i.AfterCallInfo.ArgumentTypes.Length);
			Assert.AreEqual(typeof(string), i.AfterCallInfo.ArgumentTypes[0]);
			Assert.AreEqual(typeof(int), i.AfterCallInfo.ArgumentTypes[1]);

			Assert.AreEqual(typeof(ISomeInterface), i.AfterCallInfo.MethodOwner);
			Assert.AreEqual("SomeMethod", i.AfterCallInfo.MethodName);
			Assert.AreEqual(typeof(string), i.AfterCallInfo.ResultType);

			Assert.IsNull(i.AfterCallInfo.ReturnValue);
			Assert.IsNull(i.AfterCallInfo.ThrownException);
			Assert.IsTrue(i.AfterCallInfo.Completed);
		}

		[TestMethod]
		public void AdaptedOnly()
		{
			var impl = new SomeInterfaceImplementation();
			var h = new InterceptingAdapterEmittedTypeHanlder(impl);

			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);
			var result = o.SomeMethod("StringParam", 17);

			Assert.AreEqual("StringParam", impl.SomeMethodS);
			Assert.AreEqual(17, impl.SomeMethodI);

			Assert.AreEqual("SomeValue", result);
		}

		[TestMethod]
		public void AdaptedAndInterceptor()
		{
			var impl = new SomeInterfaceImplementation();
			var i = new RecordingInterceptor();
			var h = new InterceptingAdapterEmittedTypeHanlder(impl, i);

			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);
			var result = o.SomeMethod("StringParam", 17);

			Assert.IsNotNull(i.BeforeCallInfo);
			Assert.IsNotNull(i.AfterCallInfo);

			Assert.AreEqual("StringParam", impl.SomeMethodS);
			Assert.AreEqual(17, impl.SomeMethodI);

			Assert.AreEqual("SomeValue", result);

			//
			// BeforeCall

			Assert.AreEqual(2, i.BeforeCallInfo.Arguments.Length);
			Assert.AreEqual("StringParam", i.BeforeCallInfo.Arguments[0]);
			Assert.AreEqual(17, i.BeforeCallInfo.Arguments[1]);

			Assert.AreEqual(2, i.BeforeCallInfo.ArgumentTypes.Length);
			Assert.AreEqual(typeof(string), i.BeforeCallInfo.ArgumentTypes[0]);
			Assert.AreEqual(typeof(int), i.BeforeCallInfo.ArgumentTypes[1]);

			Assert.AreEqual(typeof(ISomeInterface), i.BeforeCallInfo.MethodOwner);
			Assert.AreEqual("SomeMethod", i.BeforeCallInfo.MethodName);
			Assert.AreEqual(typeof(string), i.BeforeCallInfo.ResultType);

			Assert.IsNull(i.BeforeCallInfo.ReturnValue);
			Assert.IsNull(i.BeforeCallInfo.ThrownException);
			Assert.IsFalse(i.BeforeCallInfo.Completed);

			//
			// AfterCall

			Assert.AreEqual(2, i.AfterCallInfo.Arguments.Length);
			Assert.AreEqual("StringParam", i.AfterCallInfo.Arguments[0]);
			Assert.AreEqual(17, i.AfterCallInfo.Arguments[1]);

			Assert.AreEqual(2, i.AfterCallInfo.ArgumentTypes.Length);
			Assert.AreEqual(typeof(string), i.AfterCallInfo.ArgumentTypes[0]);
			Assert.AreEqual(typeof(int), i.AfterCallInfo.ArgumentTypes[1]);

			Assert.AreEqual(typeof(ISomeInterface), i.AfterCallInfo.MethodOwner);
			Assert.AreEqual("SomeMethod", i.AfterCallInfo.MethodName);
			Assert.AreEqual(typeof(string), i.AfterCallInfo.ResultType);

			Assert.AreEqual("SomeValue", i.AfterCallInfo.ReturnValue);
			Assert.IsNull(i.AfterCallInfo.ThrownException);
			Assert.IsTrue(i.AfterCallInfo.Completed);
		}
	}
}
