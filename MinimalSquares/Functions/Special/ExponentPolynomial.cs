using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;

namespace MinimalSquares.Functions.Special
{
    class ExponentPolynomial : PolynomialFunction
    {
        public ExponentPolynomial(int monomialsCount) : base(monomialsCount)
        {
            Name = $"Экспоненциальный полином {monomialsCount - 1} степени";
        }

        public override string Name { get; }

        public override bool IsAcceptablePoint(double x, double y)
        {
            return y > 0.0;
        }

        public override double GetYValue(double y)
        {
            return Math.Log(y);
        }

        public override double GetValue(double x)
        {
            return Math.Exp(base.GetValue(x));
        }

        public override string GetGeneralNotation()
        {
            StringBuilder sb = new(@"y \sim e^{");
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

            sb.Append('}');

            return sb.ToString();
        }

        public override string GetFunctionNotation()
        {
            StringBuilder sb = new(@"y = e^{");
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

            sb.Append('}');

            return sb.ToString();
        }
    }
}
