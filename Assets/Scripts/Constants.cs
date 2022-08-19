public static class Constants
{
    public const float WaitTimeForGroundDisable = 5f; 
    public const string UnTagged = "Untagged";
    public const string SpawnCollider = "SpawnCollider";
    public const string EndCollider = "EndCollider";
    public const string Finish = "RedCube";
    public const string GreenCube = "GreenCube";
    public const string Game = "Game";
    public const string BestScore= "BestScore";
    public const string TotalScore = "TotalScore";
}

public enum Positions
{
    Middle,
    Left,
    Right
}

public enum GameState
{
    Idle,
    Pause,
    Play,
    Over
}