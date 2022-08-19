using Interfaces;

public interface IGameController : ISetup
{
    GameState _gameState { get; }
    /// <summary>
    /// LoadScene
    /// </summary>
    /// <param name="sceneName"></param>
    void LoadScene(string sceneName);

    /// <summary>
    ///  Play
    /// </summary>
    void Play();

    /// <summary>
    /// ReStart
    /// </summary>
    void ReStart();
}