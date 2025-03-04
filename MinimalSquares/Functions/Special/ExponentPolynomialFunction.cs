using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;

using MinimalSquares.Components;

namespace MinimalSquares.Functions.Special
{
    class ExponentPolynomialFunction : AbstractFunction
    {
        public ExponentPolynomialFunction(BaseFunction function)
        {
            TargetFunction = function;
        }

        public BaseFunction TargetFunction { get; }

        public override string Name => $"Экспоненциальный {TargetFunction.Name}";

        public override int MonomialCount => TargetFunction.MonomialCount;

        public override bool IsAcceptablePoint(double x, double y)
        {
            return y > 0.0 && TargetFunction.IsAcceptablePoint(x, y);
        }

        public override double GetFreeValue(double x, double y)
        {
            return TargetFunction.GetFreeValue(x, Math.Log(y));
        }

        public override double GetValue(double x)
        {
            return Math.Exp(TargetFunction.GetValue(x));
        }

        public override double GetMonomialValue(int monomialIndex, double x, double y)
        {
            return TargetFunction.GetMonomialValue(monomialIndex, x, y);
        }

        public override void SetParameters(Vector<double> ansv)
        {
            TargetFunction.SetParameters(ansv);
        }

        public override string GetGeneralNotation()
        {
            StringBuilder sb = new(@"e^{");
            sb.Append(TargetFunction.GetGeneralNotation());
            sb.Append('}');

            return sb.ToString();
        }

        public override string GetFunctionNotation()
        {
            StringBuilder sb = new(@"e^{");
            sb.Append(TargetFunction.GetFunctionNotation());
            sb.Append('}');

            return sb.ToString();
        }

        public override string GetFormattedParameter(int order, bool sign)
        {
            return TargetFunction.GetFormattedParameter(order, sign);
        }
    }
}
