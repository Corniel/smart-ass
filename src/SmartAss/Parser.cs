namespace SmartAss
{
    public static class Parser
    {
        /// <summary>Parses a int.</summary>
        /// <param name="str">
        /// The input string.
        /// </param>
        /// <param name="n">
        /// The parsed int.
        /// </param>
        /// <returns>
        /// True if parsable, otherwise false.
        /// </returns>
        public static bool ToInt32(string str, out int n)
        {
            n = default;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            var start = 0;
            var end = str.Length;
            var number = 0;
            var negative = false;

            if (str[0] == '-')
            {
                start = 1;
                negative = true;
            }

            for (var i = start; i < end; i++)
            {
                var ch = str[i];

                if (ch < '0' || ch > '9')
                {
                    return false;
                }
                unchecked
                {
                    number *= 10;
                    number += ch - '0';
                }
                // becomes negative, so overflow.
                if ((number & 0x80000000) != 0)
                {
                    return false;
                }
            }

            n = negative ? -number : number;
            return true;
        }

        /// <summary>Parses a long.</summary>
        /// <param name="str">
        /// The input string.
        /// </param>
        /// <param name="n">
        /// The parsed long.
        /// </param>
        /// <returns>
        /// True if parsable, otherwise false.
        /// </returns>
        public static bool ToInt64(string str, out long n)
        {
            n = default;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            var start = 0;
            var end = str.Length;
            var number = 0L;
            var negative = false;

            if (str[0] == '-')
            {
                start = 1;
                negative = true;
            }

            for (var i = start; i < end; i++)
            {
                var ch = str[i];

                if (ch < '0' || ch > '9')
                {
                    return false;
                }
                unchecked
                {
                    number *= 10;
                    number += ch - '0';
                }
                if ((number & unchecked((long)0x8000000000000000)) != 0)
                {
                    return false;
                }
            }

            n = negative ? -number : number;
            return true;
        }

        /// <summary>Parses a decimal.</summary>
        /// <param name="str">
        /// The input string.
        /// </param>
        /// <param name="dec">
        /// The parsed decimal.
        /// </param>
        /// <returns>
        /// True if parsable, otherwise false.
        /// </returns>
        public static bool ToDecimal(string str, out decimal dec)
        {
            dec = default;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            var start = 0;
            var end = str.Length;
            var buffer = 0L;
            var negative = false;
            byte scale = 255;

            int lo = 0;
            int mid = 0;
            var block = 0;

            if (str[0] == '-')
            {
                start = 1;
                negative = true;
            }

            for (var i = start; i < end; i++)
            {
                var ch = str[i];

                // Not a digit.
                if (ch < '0' || ch > '9')
                {
                    // if a dot and not found yet.
                    if (ch == '.' && scale == 255)
                    {
                        scale = 0;
                        continue;
                    }
                    return false;
                }
                unchecked
                {
                    buffer *= 10;
                    buffer += ch - '0';

                    // increase scale if found.
                    if (scale != 255)
                    {
                        scale++;
                    }
                }

                // Maximum decimals allowed is 28.
                if (scale > 28)
                {
                    return false;
                }
                // No longer fits an int.
                if ((buffer & 0xFFFF00000000) != 0)
                {
                    if (block == 0)
                    {
                        lo = unchecked((int)buffer);
                    }
                    else if (block == 1)
                    {
                        mid = unchecked((int)buffer);
                    }
                    // Does not longer fits block 2, so overflow.
                    else if (block == 2)
                    {
                        return false;
                    }
                    buffer >>= 32;
                    block++;
                }
            }

            var hi = unchecked((int)buffer);
            dec = new decimal(lo, mid, hi, negative, scale == 255 ? default : scale);
            return true;
        }
    }
}
