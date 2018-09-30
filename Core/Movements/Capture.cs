namespace chess_validator.Core.Movements
{
    public class Capture : Movement
    {
        private readonly Figure _figure;
        private readonly Figure _capturedFigure;
        private readonly Coordinates _movedFrom;
        private readonly Coordinates _movedTo;

        public Capture(Figure figure, Figure capturedFigure, Coordinates movedFrom, Coordinates movedTo)
        {
            _figure = figure;
            _capturedFigure = capturedFigure;
            _movedFrom = movedFrom;
            _movedTo = movedTo;
        }

        private bool Equals(Capture other)
        {
            return Equals(_figure, other._figure) &&
                   Equals(_capturedFigure, other._capturedFigure) &&
                   Equals(_movedFrom, other._movedFrom) &&
                   Equals(_movedTo, other._movedTo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Capture) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_figure != null ? _figure.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_capturedFigure != null ? _capturedFigure.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_movedFrom != null ? _movedFrom.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_movedTo != null ? _movedTo.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}