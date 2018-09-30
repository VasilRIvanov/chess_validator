namespace chess_validator.Core.Movements
{
    public class Move : Movement
    {
        private readonly Figure _figure;
        private readonly Coordinates _movedFrom;
        private readonly Coordinates _movedTo;

        public Move(Figure figure, Coordinates movedFrom, Coordinates movedTo)
        {
            _figure = figure;
            _movedFrom = movedFrom;
            _movedTo = movedTo;
        }

        private bool Equals(Move other)
        {
            return Equals(_figure, other._figure) && Equals(_movedFrom, other._movedFrom) &&
                   Equals(_movedTo, other._movedTo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Move) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_figure != null ? _figure.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_movedFrom != null ? _movedFrom.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_movedTo != null ? _movedTo.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}