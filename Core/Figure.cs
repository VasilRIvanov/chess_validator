namespace chess_validator.Core
{
    public abstract class Figure
    {
        private readonly Coordinates _initialPosition;
        private readonly Color _color;

        protected Figure(Coordinates initialPosition, Color color)
        {
            _initialPosition = initialPosition;
            _color = color;
        }

        public Coordinates InitialPosition => _initialPosition;

        public Color Color => _color;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Figure) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_initialPosition != null ? _initialPosition.GetHashCode() : 0) * 397) ^ (int) _color;
            }
        }

        private bool Equals(Figure other)
        {
            return Equals(_initialPosition, other._initialPosition) && _color == other._color;
        }
    }
}