using Server.Models;

namespace Server
{
    /// <summary>
    /// Class GameBoardHolder for singleton.
    /// </summary>
    public static class GameBoardHolder
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static GameBoard _instance;

        /// <summary>
        /// The synchronize root
        /// </summary>
        private static readonly object SynchronizeRoot = new object();

        /// <summary>
        /// Gets the game board instance.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="turnTimeMs">The turn time ms.</param>
        /// <returns>GameBoard.</returns>
        public static GameBoard GetGameBoardInstance(int width = 20, int height = 20, long turnTimeMs = 600, bool flagTimer = true)
        {
            if (_instance == null)
                lock (SynchronizeRoot)
                {
                    if (_instance == null)
                        _instance = new GameBoard(width, height, turnTimeMs, flagTimer);
                }
            return _instance;
        }
    }
}
