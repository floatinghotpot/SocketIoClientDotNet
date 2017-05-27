// you need this once (only), and it must be in this namespace
namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class
		 | AttributeTargets.Method)]
	public sealed class ExtensionAttribute : Attribute { }
}
