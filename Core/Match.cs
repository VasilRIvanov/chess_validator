using System;
using System.Collections.Generic;

namespace chess_validator.Core
{
    /// <summary>
    /// This class represents a chess game.
    /// </summary>
    public class Match
    {
        private readonly string _event;
        private readonly string _location;
        private readonly DateTime _date;
        private readonly int _round;
        private readonly Player _white;
        private readonly Player _black;
        private readonly Result _result;
        private readonly LinkedList<PairMovement> _movements;

        public Match(
            string @event,
            string location,
            DateTime date,
            int round,
            Player white,
            Player black,
            Result result,
            LinkedList<PairMovement> movements
        )
        {
            _event = @event;
            _location = location;
            _date = date;
            _round = round;
            _white = white;
            _black = black;
            _result = result;
            _movements = movements;
        }

        private bool Equals(Match other)
        {
            return string.Equals(_event, other._event) &&
                   string.Equals(_location, other._location) &&
                   _date.Equals(other._date) &&
                   _round == other._round &&
                   Equals(_white, other._white) &&
                   Equals(_black, other._black) &&
                   Equals(_result, other._result) &&
                   Equals(_movements, other._movements);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Match) obj);
        }
    }
}