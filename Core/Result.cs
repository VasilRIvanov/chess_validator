using chess_validator.Core.Exceptions;

namespace chess_validator.Core
{   
    public class Result
    {
        private readonly GameState _gameState;
        private readonly Player _winner;

        public Result(Player winner)
        {
            _winner = winner;
            _gameState = GameState.VICTORY;
        }

        public Result(GameState gameState)
        {
            if (gameState == GameState.VICTORY)
            {
                throw new InvalidGameStateException("Cannot define game result as a victory without a winner");
            }
            _gameState = gameState;
        }

        public GameState GameState => _gameState;

        public Player Winner => _winner;
    }
}