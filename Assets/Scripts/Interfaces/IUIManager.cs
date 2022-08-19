namespace Interfaces
{
    public interface IUIManager : ISetup
    {
        /// <summary>
        /// MainMenuPanel
        /// </summary>
        /// <param name="status"></param>
        void MainMenuPanel(bool status);

        /// <summary>
        /// GameOverPanel
        /// </summary>
        /// <param name="status"></param>
        void GameOverPanel(bool status);
    }
}