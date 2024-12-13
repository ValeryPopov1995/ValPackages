using System;
using System.Threading;

namespace Gimbal.UI
{
    /// <summary>
    /// Thread context to execute anything in setted (main) thread
    /// </summary>
    public static class ThreadContext
    {
        private static SynchronizationContext _context;

        public static void Set()
        {
            _context = SynchronizationContext.Current;
        }

        public static void Execute(Action action)
        {
            _context.Post((obj) => action(), null);
        }
    }
}
