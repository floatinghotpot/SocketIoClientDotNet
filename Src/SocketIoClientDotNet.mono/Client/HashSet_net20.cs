using System.Collections.Generic;

public class HashSet<T>
{
	private readonly Dictionary<T, object> dict = new Dictionary<T, object>();

	public void Add(T value)
	{
		dict[value] = null;
	}

	public void Remove(T value)
	{
		dict.Remove(value);
	}

	public int Count
	{
		get
		{
			return dict.Count;
		}
	}
}
