namespace SmartAss
{
    /// <summary>Operator that can tweak/manipulate bits.</summary>
    public interface IBitsOperator<T> where T : struct
    {
        /// <summary>Counts the number of bits with the value 1.</summary>
        int Count(T bits);

        /// <summary>Gets the number of bits needed to represent the number.</summary>
        int Size(T bits);

        /// <summary>Gets the index of the first bit needed to represent the number.</summary>
        int First(T bits);

        /// <summary>Flags the bit at the specified position.</summary>
        T Flag(T bits, int position);

        /// <summary>Unflags the bit at the specified position.</summary>
        T Unflag(T bits, int position);

        /// <summary>Represents the bits as binary string.</summary>
        string ToString(T bits);
    }
}
