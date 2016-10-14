using System;

namespace Compositional.Composer.UnitTests.EmitterTests.Components
{
	public interface IGenericEvent<T> where T : EventArgs
	{
		event EventHandler<T> SomeEvent;
	}
}
