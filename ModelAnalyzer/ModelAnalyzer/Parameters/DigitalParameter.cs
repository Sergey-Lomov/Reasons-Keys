namespace ModelAnalyzer.Parameters
{
    internal abstract class DigitalParameter : Parameter
    {
        public int fractionalDigits;
        readonly protected int unroundFractionalDigits = 3;

        public abstract string UnroundValueToString();
    }
}
