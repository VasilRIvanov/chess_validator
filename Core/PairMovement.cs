namespace chess_validator.Core
{
    public class PairMovement
    {
        private readonly Movement _white;
        private readonly Movement _black;
        private readonly int _order;

        public PairMovement(Movement white, Movement black, int order)
        {
            _white = white;
            _black = black;
            _order = order;
        }

        public Movement White() => _white;
        public Movement Black() => _black;
        public int Order() => _order;

        private bool Equals(PairMovement other)
        {
            return Equals(_white, other._white) && Equals(_black, other._black) && _order == other._order;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PairMovement) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_white != null ? _white.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_black != null ? _black.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _order;
                return hashCode;
            }
        }
    }
}