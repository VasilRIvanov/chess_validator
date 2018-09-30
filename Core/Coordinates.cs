namespace chess_validator.Core
{
    public class Coordinates
    {
        private readonly char _verticalValue;
        private readonly int _horizontalValue;

        public Coordinates(char verticalValue, int horizontalValue)
        {
            _verticalValue = verticalValue;
            _horizontalValue = horizontalValue;
        }

        public char VerticalValue => _verticalValue;

        public int HorizontalValue => _horizontalValue;

        public override string ToString()
        {
            return $"{_verticalValue} {_horizontalValue}";
        }

        private bool Equals(Coordinates other)
        {
            return _verticalValue == other._verticalValue && _horizontalValue == other._horizontalValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Coordinates) obj);
        }
    }
}