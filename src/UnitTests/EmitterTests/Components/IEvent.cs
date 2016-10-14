using System;

namespace Appson.Composer.UnitTests.EmitterTests.Components
{
	public interface IEvent
	{
		event EventHandler SomeEvent;
	}
}
