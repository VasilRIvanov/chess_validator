using System;

namespace chess_validator.Core.Exceptions
{
    public class InvalidGameStateException : Exception
    {
        public InvalidGameStateException(string message) : base(message)
        {
        }
    }
}