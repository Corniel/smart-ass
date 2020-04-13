using System.Diagnostics;

namespace SmartAss.Pooling
{
    /// <summary>Represents a handle that guarantees release of the item to its pool on <see cref="Dispose()"/>.</summary>
    /// <typeparam name="T">
    /// The type of the reusable.
    /// </typeparam>
    public ref struct Reusable<T> where T : class
    {
        internal Reusable(T item, ObjectPool<T> pool)
        {
            Item = item;
            Pool = pool;
            released = false;
        }

        /// <summary>Gets reusable item.</summary>
        public readonly T Item;

        /// <summary>Gets the pool to return when the item is released.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ObjectPool<T> Pool;

        /// <summary>Indicates that the item has been released.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool released;

        public override string ToString() => $"Resuable<{typeof(T)}>";

        /// <summary>Implicitly casts the reusable handle to the type it handles.</summary>
        public static implicit operator T(Reusable<T> handle) => handle.Item;

        /// <summary>Convention based c# 8 way of making ref structs IDisposable.</summary>
        public void Dispose()
        {
            if (!released)
            {
                Pool.Release(Item);
                released = true;
            }
        }
    }
}
