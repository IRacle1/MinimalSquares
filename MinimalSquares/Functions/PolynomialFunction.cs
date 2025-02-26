using System;
using System.Text;

using MinimalSquares.Components;

namespace MinimalSquares.Functions
{
    public class PolynomialFunction : BaseFunction
    {
        public override string Name { get; }

        public PolynomialFunction(int monomialsCount)
            : base(monomialsCount)
        {
            Name = $"Полином {monomialsCount - 1} степени";
        }

        public override double GetMonomialValue(int monomialIndex, double x) =>
            monomialIndex switch
            {
                0 => 1,
                1 => x,
                _ => Math.Pow(x, monomialIndex),
            };

        public override double GetValue(double x)
        {
            double y = 0.0;

            for (int i = 0; i < Parameters.Length; i++)
            {
                y += Parameters[i] * GetMonomialValue(i, x);
            }

            return y;
        }

        public override string GetGeneralNotation()
        {
            StringBuilder sb = new(@"y \sim ");
            for (int i = MonomialCount - 1; i >= 0; i--)
            {
                int variableIndex = MonomialCount - 1 - i;
                char variable = ComponentManager.ReportManager.GetVariableName(variableIndex);

                sb.Append(variable);

                if (i > 0)
                {
                    sb.AppendFormat("x^{0} + ", i);
                }
            }

            return sb.ToString();
        }

        public override string GetFunctionNotation()
        {
            StringBuilder sb = new(@"y = ");
            for (int i = MonomialCount - 1; i >= 0; i--)
            {
                int variableIndex = MonomialCount - 1 - i;

                double curParam = GetFormattedParameter(variableIndex);

                if (i != MonomialCount - 1 && curParam > 0f)
                {
                    sb.Append(" + ");
                }

                sb.Append(GetFormattedParameter(variableIndex))
                    .AppendFormat("x^{0}", i);
            }

            return sb.ToString();
        }

        public override double GetFormattedParameter(int order)
        {
            return base.GetFormattedParameter(MonomialCount - 1 - order);
        }
    }
}
