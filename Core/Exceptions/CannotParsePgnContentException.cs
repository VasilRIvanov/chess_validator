using System;

namespace chess_validator.Core.Exceptions
{
    public class CannotParsePgnContentException: Exception
    {
        public CannotParsePgnContentException(string message) : base(message)
        {
        }
    }
}