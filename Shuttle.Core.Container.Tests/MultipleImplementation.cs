namespace Shuttle.Core.Container.Tests
{
	public class MultipleImplementation : 
		IMultipleImplementation<string>,
		IMultipleImplementation<int>
	{
	}
}