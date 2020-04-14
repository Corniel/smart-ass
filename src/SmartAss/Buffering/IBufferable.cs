namespace SmartAss.Buffering
{
    /// <summary>Represents a class that can be reused by a <see cref="Buffer{T}"/>.</summary>
    public interface IBufferable
    {
        /// <summary>Reset the instance of the class to its initial state.</summary>
        void Reset();
    }
}
