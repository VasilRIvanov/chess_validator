namespace chess_validator.Core
{
    /// <summary>
    /// Converts PGN content to object ChessMatch
    /// </summary>
    public interface IPgnParser
    {
        Match Parse();
    }
}