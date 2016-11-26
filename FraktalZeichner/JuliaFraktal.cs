using System;
using System.Drawing;

namespace FraktalZeichner
{
    public class JuliaFraktal
    {
        public int MaxIterations { get; set; } = 1000;
        public Complex Constant { get; set; } = new Complex(-0.21f, 0.75f);
        public Func<int, int, Color> ColorMethod { get; set; } = (iterations, maxIterations) => Color.White;
        
        public Color IsInSet(Complex num)
        {
            for (int i = 0; i < MaxIterations; i++)
            {
                num = num.Square() + Constant;

                if (num.Magnitude2() > 2 * 2)
                    return ColorMethod(i, MaxIterations);
            }
            return Color.Black;
        }
    }
}
