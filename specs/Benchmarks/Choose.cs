using System.Numerics;

namespace Benchmarks;

public class Choose
{
    [Benchmark(Baseline = true)]
    public long BigInt() => BigNumbers(Reference.BigInteger);

    [Benchmark]
    public long Int128() => BigNumbers(Reference.Int128);

    [Benchmark]
    public long Fastest() => BigNumbers(Maths.Choose);

    [Benchmark]
    public long Knuth() => BigNumbers(Reference.Knuth);

    static long BigNumbers(Func<long, long, long> choose)
    {
        var total = 0L;
        for (var k = 51; k <= 55; k++)
        {
            total += choose(k, 07);
            total += choose(k, 18);
        }
        return total;
    }
}

static class Reference
{
    public static long BigInteger(long n, long k)
    {
        var m = n - k;
        var up = Math.Max(m, n - m);
        var dw = n - up;

        BigInteger p = 1L;

        for (long i = up + 1; i <= n; i++)
        {
            p *= i;
        }
        for (long i = 2; i <= dw; i++)
        {
            p /= i;
        }
        return (long)p;
    }

    public static long Int128(long n, long k)
    {
        var m = n - k;
        var up = Math.Max(m, n - m);
        var dw = n - up;

        Int128 p = 1L;

        for (long i = up + 1; i <= n; i++)
        {
            p *= i;
        }
        for (long i = 2; i <= dw; i++)
        {
            p /= i;
        }
        return (long)p;
    }

    /// <summary>Calculates all combinations when choosing k out n.</summary>
    /// <remarks>
    /// Making the most use of the standard algorithm from Knuth's book "The Art of Computer Programming, 3rd Edition, Volume 2: Seminumerical Algorithms":
    /// </remarks>
    public static long Knuth(long n, long k)
    {
        var n_k = n - k;

        // swap as n over k equals n over (n - k)
        if (n_k < k)
        {
            k = n_k;
        }

        long r = 1;
        long d;
        for (d = 1; d <= k; ++d)
        {
            if (r > long.MaxValue / n)
                break;
            r *= n--;
            r /= d;
        }

        if (d > k)
            return r;

        // Let N be the original n,
        // n is the current n (when we reach here)
        // We want to calculate C(N,k),
        // Currently we already calculated the r value so far:
        // r = C(N, n) = C(N, N-n) = C(N, d-1)
        // Note that N-n = d-1
        // In addition we know the following identity formula:
        //  C(N,k) = C(N,d-1) * C(N-d+1, k-d+1) / C(k, k-d+1)
        //         = C(N,d-1) * C(n, k-d+1) / C(k, k-d+1)
        // Using this formula, we effectively reduce the calculation,
        // while recursively use the same function.
        long b = Knuth(n, k - d + 1);
        if (b == long.MaxValue)
        {
            return long.MaxValue;  // overflow
        }

        long c = Knuth(k, k - d + 1);
        if (c == long.MaxValue)
        {
            return long.MaxValue;  // overflow
        }

        // Now, the combinatorial should be r * b / c
        // We can use gcd() to calculate this:
        // We Pick b for gcd: b < r almost (if not always) in all cases
        long g = Maths.Gcd(b, c);
        b /= g;
        c /= g;
        r /= c;

        if (r > long.MaxValue / b)
            return long.MaxValue;   // overflow

        return r * b;
    }
}
