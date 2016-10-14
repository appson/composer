using System.Reflection;
using System.Reflection.Emit;

namespace Appson.Composer.Emitter
{
	[Contract]
	public interface IEventEmitter
	{
		EventInfo EmitEvent(TypeBuilder typeBuilder,
		                    EventInfo eventInfo);
	}
}