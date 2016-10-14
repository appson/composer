using System;
using Appson.Composer.Emitter;
using Appson.Composer.Interceptor;
using Appson.Composer.UnitTests.InterceptorTests.Components;
using Appson.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.InterceptorTests
{
	[TestClass]
	public class InterceptorExceptionHandlingTest
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
		public void ExceptionInAdaptedCall()
		{
			var impl = new SomeInterfaceImplementation();
			var i = new RecordingInterceptor();
			var h = new InterceptingAdapterEmittedTypeHanlder(impl, i);
			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);

			bool exceptionBubbledUp = false;

			try
			{
				o.ThrowException();
			}
			catch (Exception e)
			{
				exceptionBubbledUp = true;
				Assert.IsInstanceOfType(e, typeof(AdaptedException));
				Assert.IsNotNull(e.InnerException);

				Assert.IsNotNull(((AdaptedException)e).Arguments);
				Assert.AreEqual("ThrowException", ((AdaptedException)e).MethodName);
				Assert.AreEqual(0, ((AdaptedException)e).Arguments.Length);

				Assert.IsFalse(((AdaptedException)e).BeforeCall);
				Assert.IsTrue(((AdaptedException)e).DuringCall);
				Assert.IsFalse(((AdaptedException)e).AfterCall);

				Assert.IsInstanceOfType(e.InnerException, typeof(NullReferenceException));
				Assert.IsNull(e.InnerException.InnerException);
			}

			Assert.IsTrue(exceptionBubbledUp);

			Assert.IsNotNull(i.BeforeCallInfo);
			Assert.IsNotNull(i.AfterCallInfo);

			Assert.IsNull(i.BeforeCallInfo.ThrownException);
			Assert.IsNotNull(i.AfterCallInfo.ThrownException);

			Assert.IsInstanceOfType(i.AfterCallInfo.ThrownException, typeof(NullReferenceException));
		}

		[TestMethod]
		public void ExceptionInBeforeCall()
		{
			var impl = new SomeInterfaceImplementation();
			var i = new RecordingInterceptor();
			var h = new InterceptingAdapterEmittedTypeHanlder(impl, i);
			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);

			i.BeforeCallAction = delegate(CallInfo ci) { throw new NullReferenceException(); };

			bool exceptionBubbledUp = false;

			try
			{
				o.SomeMethod("SomeString", 17);
			}
			catch (Exception e)
			{
				exceptionBubbledUp = true;
				Assert.IsInstanceOfType(e, typeof(InterceptionException));
				Assert.IsNotNull(e.InnerException);

				Assert.IsNotNull(((InterceptionException)e).Arguments);
				Assert.AreEqual("SomeMethod", ((InterceptionException)e).MethodName);
				
				Assert.AreEqual(2, ((InterceptionException)e).Arguments.Length);
				Assert.AreEqual("SomeString", ((InterceptionException)e).Arguments[0]);
				Assert.AreEqual(17, ((InterceptionException)e).Arguments[1]);

				Assert.IsTrue(((InterceptionException)e).BeforeCall);
				Assert.IsFalse(((InterceptionException)e).AfterCall);

				Assert.IsInstanceOfType(e.InnerException, typeof(NullReferenceException));
				Assert.IsNull(e.InnerException.InnerException);
			}

			Assert.IsTrue(exceptionBubbledUp);

			Assert.IsNull(impl.SomeMethodS);
			Assert.AreEqual(0, impl.SomeMethodI);

			Assert.IsNotNull(i.BeforeCallInfo);
			Assert.IsNull(i.AfterCallInfo);

			Assert.IsNull(i.BeforeCallInfo.ThrownException);
		}

		[TestMethod]
		public void ExceptionInAfterCall()
		{
			var impl = new SomeInterfaceImplementation();
			var i = new RecordingInterceptor();
			var h = new InterceptingAdapterEmittedTypeHanlder(impl, i);
			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);

			i.AfterCallAction = delegate(CallInfo ci) { throw new NullReferenceException(); };

			bool exceptionBubbledUp = false;

			try
			{
				o.SomeMethod("SomeString", 17);
			}
			catch (Exception e)
			{
				exceptionBubbledUp = true;
				Assert.IsInstanceOfType(e, typeof(InterceptionException));
				Assert.IsNotNull(e.InnerException);

				Assert.IsNotNull(((InterceptionException)e).Arguments);
				Assert.AreEqual("SomeMethod", ((InterceptionException)e).MethodName);

				Assert.AreEqual(2, ((InterceptionException)e).Arguments.Length);
				Assert.AreEqual("SomeString", ((InterceptionException)e).Arguments[0]);
				Assert.AreEqual(17, ((InterceptionException)e).Arguments[1]);

				Assert.IsFalse(((InterceptionException)e).BeforeCall);
				Assert.IsTrue(((InterceptionException)e).AfterCall);

				Assert.IsInstanceOfType(e.InnerException, typeof(NullReferenceException));
				Assert.IsNull(e.InnerException.InnerException);
			}

			Assert.IsTrue(exceptionBubbledUp);

			Assert.IsNotNull(impl.SomeMethodS);
			Assert.AreEqual("SomeString", impl.SomeMethodS);
			Assert.AreEqual(17, impl.SomeMethodI);

			Assert.IsNotNull(i.BeforeCallInfo);
			Assert.IsNotNull(i.AfterCallInfo);

			Assert.IsNull(i.BeforeCallInfo.ThrownException);
			Assert.IsNull(i.AfterCallInfo.ThrownException);
		}

		[TestMethod]
		public void SetThrownExceptionInBeforeCall()
		{
			var impl = new SomeInterfaceImplementation();
			var i = new RecordingInterceptor();
			var h = new InterceptingAdapterEmittedTypeHanlder(impl, i);
			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);

			i.BeforeCallAction = delegate(CallInfo ci) { ci.ThrownException = new NullReferenceException();
			                                           	ci.Completed = true; };

			bool exceptionBubbledUp = false;

			try
			{
				o.SomeMethod("SomeString", 17);
			}
			catch (Exception e)
			{
				exceptionBubbledUp = true;
				Assert.IsInstanceOfType(e, typeof(AdaptedException));
				Assert.IsNotNull(e.InnerException);

				Assert.IsNotNull(((AdaptedException)e).Arguments);
				Assert.AreEqual("SomeMethod", ((AdaptedException)e).MethodName);

				Assert.AreEqual(2, ((AdaptedException)e).Arguments.Length);
				Assert.AreEqual("SomeString", ((AdaptedException)e).Arguments[0]);
				Assert.AreEqual(17, ((AdaptedException)e).Arguments[1]);

				Assert.IsTrue(((AdaptedException)e).BeforeCall);
				Assert.IsFalse(((AdaptedException)e).DuringCall);
				Assert.IsFalse(((AdaptedException)e).AfterCall);

				Assert.IsInstanceOfType(e.InnerException, typeof(NullReferenceException));
				Assert.IsNull(e.InnerException.InnerException);
			}

			Assert.IsTrue(exceptionBubbledUp);

			Assert.IsNull(impl.SomeMethodS);
			Assert.AreEqual(0, impl.SomeMethodI);

			Assert.IsNotNull(i.BeforeCallInfo);
			Assert.IsNotNull(i.AfterCallInfo);

			Assert.IsNull(i.BeforeCallInfo.ThrownException);
			Assert.IsNotNull(i.AfterCallInfo.ThrownException);
		}

		[TestMethod]
		public void SetThrownExceptionInAfterCall()
		{
			var impl = new SomeInterfaceImplementation();
			var i = new RecordingInterceptor();
			var h = new InterceptingAdapterEmittedTypeHanlder(impl, i);
			var o = _classEmitter.EmitInterfaceInstance<ISomeInterface>(h);

			i.AfterCallAction = delegate(CallInfo ci) { ci.ThrownException = new NullReferenceException(); };

			bool exceptionBubbledUp = false;

			try
			{
				o.SomeMethod("SomeString", 17);
			}
			catch (Exception e)
			{
				exceptionBubbledUp = true;
				Assert.IsInstanceOfType(e, typeof(AdaptedException));
				Assert.IsNotNull(e.InnerException);

				Assert.IsNotNull(((AdaptedException)e).Arguments);
				Assert.AreEqual("SomeMethod", ((AdaptedException)e).MethodName);

				Assert.AreEqual(2, ((AdaptedException)e).Arguments.Length);
				Assert.AreEqual("SomeString", ((AdaptedException)e).Arguments[0]);
				Assert.AreEqual(17, ((AdaptedException)e).Arguments[1]);

				Assert.IsFalse(((AdaptedException)e).BeforeCall);
				Assert.IsFalse(((AdaptedException)e).DuringCall);
				Assert.IsTrue(((AdaptedException)e).AfterCall);

				Assert.IsInstanceOfType(e.InnerException, typeof(NullReferenceException));
				Assert.IsNull(e.InnerException.InnerException);
			}

			Assert.IsTrue(exceptionBubbledUp);

			Assert.IsNotNull(impl.SomeMethodS);
			Assert.AreEqual("SomeString", impl.SomeMethodS);
			Assert.AreEqual(17, impl.SomeMethodI);

			Assert.IsNotNull(i.BeforeCallInfo);
			Assert.IsNotNull(i.AfterCallInfo);

			Assert.IsNull(i.BeforeCallInfo.ThrownException);
			Assert.IsNull(i.AfterCallInfo.ThrownException);
		}
	}
}
