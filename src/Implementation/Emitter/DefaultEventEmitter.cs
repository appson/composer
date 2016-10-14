using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Compositional.Composer.Emitter
{
	[Component]
	[ComponentCache(null)]
	public class DefaultEventEmitter : IEventEmitter
	{
		#region Implementation of IEventEmitter

		public EventInfo EmitEvent(TypeBuilder typeBuilder,
		                                   EventInfo eventInfo)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}