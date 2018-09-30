namespace chess_validator.Core
{
    public class Player
    {
        private readonly string _firstName;
        private readonly string _lastName;

        public Player(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public override string ToString()
        {
            return $"{_firstName} {_lastName}";
        }
    }
}