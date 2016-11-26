namespace FraktalZeichner
{
    public struct Complex
    {
        public float Real { get; set; }
        public float Imaginary { get; set; }

        // Constructor.
        public Complex(float real, float imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public Complex Square()
        {
            return new Complex(Real * Real - Imaginary * Imaginary, 2 * Real * Imaginary);
        }

        // Specify which operator to overload (+), 
        // the types that can be added (two Complex objects),
        // and the return type (Complex).
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        /// <summary>
        /// Squared Magnitude of the Complex Number
        /// </summary>
        /// <returns>Squared Magnitude of the Complex Number</returns>
        public double Magnitude2()
        {
            return Real * Real + Imaginary * Imaginary;
        }

        // Override the ToString() method to display a complex number 
        // in the traditional format:
        public override string ToString()
        {
            return ($"{Real} + {Imaginary}i");
        }
    }
}
