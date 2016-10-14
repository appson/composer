using System.Reflection;
using System.Reflection.Emit;

namespace Appson.Composer.Emitter
{
	[Contract]
	public interface IPropertyEmitter
	{
		PropertyInfo EmitProperty(TypeBuilder typeBuilder,
		                          PropertyInfo propertyInfo);
	}
}