using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnRollV3.Drivers
{
    

    public static class AuthFileLock
    {
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> Locks = new();

        public static async Task<IDisposable> Acquire(string key)
        {
            var sem = Locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
            await sem.WaitAsync();
            return new Releaser(() => sem.Release());
        }

        private class Releaser : IDisposable
        {
            private readonly Action _release;
            public Releaser(Action release) => _release = release;
            public void Dispose() => _release();
        }
    }

}


