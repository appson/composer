using System.Collections.Generic;
using System.Threading;

namespace Appson.Composer.Utility
{
	public static class ComposerLocalThreadBinder
	{
		private static readonly Dictionary<int, IComposer> composers;

		static ComposerLocalThreadBinder()
		{
			composers = new Dictionary<int, IComposer>();
		}

		public static void Bind(IComposer composer)
		{
			var currentThreadId = Thread.CurrentThread.ManagedThreadId;

			if (composer == null)
			{
				Unbind();
			}
			else
			{
				composers[currentThreadId] = composer;
			}
		}

		public static void Unbind()
		{
			var currentThreadId = Thread.CurrentThread.ManagedThreadId;

			if (composers.ContainsKey(currentThreadId))
				composers.Remove(currentThreadId);
		}

		public static void Unbind(int threadId)
		{
			if (composers.ContainsKey(threadId))
				composers.Remove(threadId);
		}

		public static void Unbind(IComposer composer)
		{
			if (!composers.ContainsValue(composer))
				return;

			foreach (var pair in composers)
			{
				if (ReferenceEquals(pair.Value, composer))
					composers.Remove(pair.Key);
			}
		}

		public static IComposer Lookup()
		{
			var currentThreadId = Thread.CurrentThread.ManagedThreadId;

			return !composers.ContainsKey(currentThreadId) ? null : composers[currentThreadId];
		}
	}
}