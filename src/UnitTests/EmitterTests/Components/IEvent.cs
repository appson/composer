using System;

namespace Compositional.Composer.UnitTests.EmitterTests.Components
{
	public interface IEvent
	{
		event EventHandler SomeEvent;
	}
}
