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
            StringBuilder sb = new();
            for (int i = MonomialCount - 1; i >= 0; i--)
            {
                int variableIndex = MonomialCount - 1 - i;
                char variable = ComponentManager.ReportManager.GetVariableName(variableIndex);

                sb.Append(variable);

                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        sb.Append('x');
                        break;
                    default:
                        sb.AppendFormat("x^{0}", i);
                        break;
                }

                if (i > 0)
                    sb.Append(" + ");
            }

            return sb.ToString();
        }

        public override string GetFunctionNotation()
        {
            StringBuilder sb = new();
            for (int i = MonomialCount - 1; i >= 0; i--)
            {
                int variableIndex = MonomialCount - 1 - i;

                string curParam = GetFormattedParameter(variableIndex, i != MonomialCount - 1);

                sb.Append(curParam);

                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        sb.Append('x');
                        break;
                    default:
                        sb.AppendFormat("x^{0}", i);
                        break;
                }
            }

            return sb.ToString();
        }

        public override string GetFormattedParameter(int order, bool sign)
        {
            return base.GetFormattedParameter(MonomialCount - 1 - order, sign);
        }
    }
}
