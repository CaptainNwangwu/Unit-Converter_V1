

namespace unit_converter
{
    public class ConversionRequest
    {
        public double Value { get; set; }
        public required string FromUnit { get; set; }
        public required string ToUnit { get; set; }
    }

    public class ConversionResponse
    {
        public double Result { get; set; }
        public double OriginalValue { get; set; }
        public required string FromUnit { get; set; }
        public required string ToUnit { get; set; }
        public required string FromUnitSymbol { get; set; }
        public required string ToUnitSymbol { get; set; }

    }
}