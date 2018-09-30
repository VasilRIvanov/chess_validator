using System;

namespace chess_validator.Core.Exceptions
{
    public class MissingTagException: Exception
    {
        public MissingTagException(string message) : base(message)
        {
        }
    }
}