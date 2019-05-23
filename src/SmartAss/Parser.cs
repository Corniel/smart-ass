namespace SmartAss
{
    public static class Parser
    {
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
                if ((number & 0x800000000000000) != 0)
                {
                    return false;
                }
            }

            n = negative ? -number : number;
            return true;
        }
    }
}
