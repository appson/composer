using Appson.Composer.Emitter;
using Appson.Composer.UnitTests.EmitterTests.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appson.Composer.Utility;

namespace Appson.Composer.UnitTests.EmitterTests
{
	[TestClass]
	public class MethodEmittingTest
	{
		public TestContext TestContext { get; set; }
		private ComponentContext _context;
		private IClassEmitter _classEmitter;
		private TestingEmittedTypeHandler _eth;

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

//			UNCOMMENT FOLLOWING FOR DEBUGGING
//			_classEmitter = new DefaultClassEmitter { SaveEmittedAssembly = true, SaveTargetFolder = "D:\\" };
//			_context.InitializePlugs(_classEmitter, typeof(DefaultClassEmitter));

			_eth = new TestingEmittedTypeHandler();
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void VoidResultNoArgs()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithoutArgs>(_eth);
			Assert.IsNotNull(di);

			di.SomeMethod();

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithoutArgs), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(0, _eth.CallArguments.Length);
			Assert.AreEqual(0, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);
		}

		[TestMethod]
		public void IntResultNoArgs()
		{
			var di = _classEmitter.EmitInterfaceInstance<IIntMethodWithoutArgs>(_eth);
			Assert.IsNotNull(di);

			_eth.ReturnValue = 5;
			var result = di.SomeMethod();

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IIntMethodWithoutArgs), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(0, _eth.CallArguments.Length);
			Assert.AreEqual(0, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(int), _eth.CallResultType);

			Assert.AreEqual(5, result);
		}

		[TestMethod]
		public void StringResultNoArgs()
		{
			var di = _classEmitter.EmitInterfaceInstance<IStringMethodWithoutArgs>(_eth);
			Assert.IsNotNull(di);

			_eth.ReturnValue = "SomeString";
			var result = di.SomeMethod();

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IStringMethodWithoutArgs), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(0, _eth.CallArguments.Length);
			Assert.AreEqual(0, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(string), _eth.CallResultType);

			Assert.AreEqual("SomeString", result);
		}

		[TestMethod]
		public void VoidResultStringArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithStringArg>(_eth);
			Assert.IsNotNull(di);

			di.SomeMethod("ArgumentOne");

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithStringArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.AreEqual("ArgumentOne", _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string), _eth.CallArgumentTypes[0]);

			di.SomeMethod(null);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string), _eth.CallArgumentTypes[0]);
		}

		[TestMethod]
		public void VoidResultIntArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithIntArg>(_eth);
			Assert.IsNotNull(di);

			di.SomeMethod(11);

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithIntArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.AreEqual(11, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(int), _eth.CallArgumentTypes[0]);
		}

		[TestMethod]
		public void VoidResultStringArrayArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithStringArrayArg>(_eth);
			Assert.IsNotNull(di);

			di.SomeMethod(new [] {"ArgumentOneOne", "ArgumentOneTwo"});

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithStringArrayArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.IsInstanceOfType(_eth.CallArguments[0], typeof(string[]));
			Assert.AreEqual(2, ((string[])_eth.CallArguments[0]).Length);
			Assert.AreEqual("ArgumentOneOne", ((string[])_eth.CallArguments[0])[0]);
			Assert.AreEqual("ArgumentOneTwo", ((string[])_eth.CallArguments[0])[1]);
			Assert.AreEqual(typeof(string[]), _eth.CallArgumentTypes[0]);

			di.SomeMethod(null);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string[]), _eth.CallArgumentTypes[0]);
		}

		[TestMethod]
		public void VoidResultIntArrayArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithIntArrayArg>(_eth);
			Assert.IsNotNull(di);

			di.SomeMethod(new[] {11, 12});

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithIntArrayArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.IsInstanceOfType(_eth.CallArguments[0], typeof(int[]));
			Assert.AreEqual(2, ((int[])_eth.CallArguments[0]).Length);
			Assert.AreEqual(11, ((int[])_eth.CallArguments[0])[0]);
			Assert.AreEqual(12, ((int[])_eth.CallArguments[0])[1]);
			Assert.AreEqual(typeof(int[]), _eth.CallArgumentTypes[0]);

			di.SomeMethod(null);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(int[]), _eth.CallArgumentTypes[0]);
		}

		[TestMethod]
		public void VoidResultRefStringArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithRefStringArg>(_eth);
			Assert.IsNotNull(di);

			string arg1 = "ArgumentOne";
			_eth.CallArgumentReplacements = new object[] {"ReplacedArgumentOne"};
			di.SomeMethod(ref arg1);

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithRefStringArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.AreEqual("ArgumentOne", _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual("ReplacedArgumentOne", arg1);

			arg1 = null;
			di.SomeMethod(ref arg1);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual("ReplacedArgumentOne", arg1);
		}

		[TestMethod]
		public void VoidResultRefIntArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithRefIntArg>(_eth);
			Assert.IsNotNull(di);

			int arg1 = 11;
			_eth.CallArgumentReplacements = new object[] { 111 };
			di.SomeMethod(ref arg1);

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithRefIntArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.AreEqual(11, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(int).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(111, arg1);
		}

		[TestMethod]
		public void VoidResultRefStringArrayArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithRefStringArrayArg>(_eth);
			Assert.IsNotNull(di);

			var arg1 = new[] {"ArgumentOneOne", "ArgumentOneTwo"};
			_eth.CallArgumentReplacements = new object[] {new[] { "ReplacedArgumentOne"}};
			di.SomeMethod(ref arg1);

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithRefStringArrayArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.IsInstanceOfType(_eth.CallArguments[0], typeof(string[]));
			Assert.AreEqual(2, ((string[])_eth.CallArguments[0]).Length);
			Assert.AreEqual("ArgumentOneOne", ((string[])_eth.CallArguments[0])[0]);
			Assert.AreEqual("ArgumentOneTwo", ((string[])_eth.CallArguments[0])[1]);
			Assert.AreEqual(typeof(string[]).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(1, arg1.Length);
			Assert.AreEqual("ReplacedArgumentOne", arg1[0]);

			arg1 = null;
			di.SomeMethod(ref arg1);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string[]).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(1, arg1.Length);
			Assert.AreEqual("ReplacedArgumentOne", arg1[0]);
		}

		[TestMethod]
		public void VoidResultRefIntArrayArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithRefIntArrayArg>(_eth);
			Assert.IsNotNull(di);

			var arg1 = new[] {11, 12};
			_eth.CallArgumentReplacements = new object[] {new[] { 111 }};
			di.SomeMethod(ref arg1);

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithRefIntArrayArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.IsInstanceOfType(_eth.CallArguments[0], typeof(int[]));
			Assert.AreEqual(2, ((int[])_eth.CallArguments[0]).Length);
			Assert.AreEqual(11, ((int[])_eth.CallArguments[0])[0]);
			Assert.AreEqual(12, ((int[])_eth.CallArguments[0])[1]);
			Assert.AreEqual(typeof(int[]).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(1, arg1.Length);
			Assert.AreEqual(111, arg1[0]);

			arg1 = null;
			di.SomeMethod(ref arg1);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(int[]).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(1, arg1.Length);
			Assert.AreEqual(111, arg1[0]);
		}

		[TestMethod]
		public void VoidResultOutStringArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithOutStringArg>(_eth);
			Assert.IsNotNull(di);

			string arg1;
			_eth.CallArgumentReplacements = new object[] {"ReplacedArgumentOne"};
			di.SomeMethod(out arg1);

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithOutStringArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual("ReplacedArgumentOne", arg1);

			_eth.CallArgumentReplacements = new object[] { null };
			di.SomeMethod(out arg1);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(null, arg1);
		}

		[TestMethod]
		public void VoidResultOutIntArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithOutIntArg>(_eth);
			Assert.IsNotNull(di);

			int arg1;
			_eth.CallArgumentReplacements = new object[] { 111 };
			di.SomeMethod(out arg1);

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithOutIntArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(int).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(111, arg1);
		}

		[TestMethod]
		public void VoidResultOutStringArrayArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithOutStringArrayArg>(_eth);
			Assert.IsNotNull(di);

			string[] arg1;
			_eth.CallArgumentReplacements = new object[] {new[] { "ReplacedArgumentOne"}};
			di.SomeMethod(out arg1);

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithOutStringArrayArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string[]).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(1, arg1.Length);
			Assert.AreEqual("ReplacedArgumentOne", arg1[0]);

			_eth.CallArgumentReplacements = new object[] { null };
			di.SomeMethod(out arg1);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string[]).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(null, arg1);
		}

		[TestMethod]
		public void VoidResultOutIntArrayArg()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithOutIntArrayArg>(_eth);
			Assert.IsNotNull(di);

			int[] arg1;
			_eth.CallArgumentReplacements = new object[] {new[] { 111 }};
			di.SomeMethod(out arg1);

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithOutIntArrayArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(1, _eth.CallArguments.Length);
			Assert.AreEqual(1, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(int[]).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(1, arg1.Length);
			Assert.AreEqual(111, arg1[0]);

			_eth.CallArgumentReplacements = new object[] { null };
			di.SomeMethod(out arg1);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(int[]).MakeByRefType(), _eth.CallArgumentTypes[0]);
			Assert.AreEqual(null, arg1);
		}

		[TestMethod]
		public void VoidResultOptional()
		{
			var di = _classEmitter.EmitInterfaceInstance<IMethodWithOptionalArg>(_eth);
			Assert.IsNotNull(di);

			di.SomeMethod();

			Assert.IsNotNull(_eth.ReflectedType);
			Assert.IsNotNull(_eth.MemberName);
			Assert.IsNotNull(_eth.CallArguments);
			Assert.IsNotNull(_eth.CallArgumentTypes);
			Assert.IsNotNull(_eth.CallResultType);

			Assert.AreEqual(typeof(IMethodWithOptionalArg), _eth.ReflectedType);
			Assert.AreEqual("SomeMethod", _eth.MemberName);
			Assert.AreEqual(2, _eth.CallArguments.Length);
			Assert.AreEqual(2, _eth.CallArgumentTypes.Length);
			Assert.AreEqual(typeof(void), _eth.CallResultType);

			Assert.AreEqual("default", _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string), _eth.CallArgumentTypes[0]);

			Assert.AreEqual(1, _eth.CallArguments[1]);
			Assert.AreEqual(typeof(int), _eth.CallArgumentTypes[1]);

			di.SomeMethod("NonDefaultString");

			Assert.AreEqual("NonDefaultString", _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string), _eth.CallArgumentTypes[0]);

			Assert.AreEqual(1, _eth.CallArguments[1]);
			Assert.AreEqual(typeof(int), _eth.CallArgumentTypes[1]);

			di.SomeMethod(null);

			Assert.AreEqual(null, _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string), _eth.CallArgumentTypes[0]);

			Assert.AreEqual(1, _eth.CallArguments[1]);
			Assert.AreEqual(typeof(int), _eth.CallArgumentTypes[1]);

			di.SomeMethod("NonDefaultString", 22);

			Assert.AreEqual("NonDefaultString", _eth.CallArguments[0]);
			Assert.AreEqual(typeof(string), _eth.CallArgumentTypes[0]);

			Assert.AreEqual(22, _eth.CallArguments[1]);
			Assert.AreEqual(typeof(int), _eth.CallArgumentTypes[1]);
		}
	}
}
