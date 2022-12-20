﻿using FluentAssertions.Execution;
using SmartAss.Collections;
using System.Numerics;
using System.Text;

namespace SmartAss.Specs.FluentAssertions;

public class SmartAssCircleAssertions<T> where T : IEqualityOperators<T, T, bool>
{
    public SmartAssCircleAssertions(Circle<T> subject) => Subject = subject;

    public Circle<T> Subject { get; }

    public void HaveElements(params T[] values)
    {

        Subject.Take(Subject.Count).Should().HaveCount(values.Length);

        var curr = Subject.First();

        var sb = new StringBuilder();

        for(var i = 0; i < values.Length; i++) 
        {
            var a_p = values[(i - 1).Mod(values.Length)];
            var a_c = values[i];
            var a_n = values[(i + 1).Mod(values.Length)];

            if (curr.Prev.Value != a_p || curr.Value != a_c || curr.Next.Value != a_n)
            {
                sb.AppendLine($"[{i}] => {curr.Prev.Value}, {curr.Value}, {curr.Next.Value} != {a_p}, {a_c}, {a_n}");
            }
            curr = curr.Next;
        }

        Execute.Assertion
            .ForCondition(sb.Length == 0)
            .FailWith(sb.ToString());
    }
}
