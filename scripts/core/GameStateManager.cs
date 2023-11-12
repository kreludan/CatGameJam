using Godot;

public enum GameState
{
    Start,
    Playing,
    Paused,
    GameOver
}

public class GameStateManager : Node
{
    // Signals for state change events
    [Signal]
    public delegate void GameStarted();
    [Signal]
    public delegate void GamePaused();
    [Signal]
    public delegate void GameResumed();
    [Signal]
    public delegate void GameOver();
    // Property to access the current state
    public GameState CurrentState { get; private set; }

    // Method to change the game state
    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        // Emit signals based on the new state
        switch (CurrentState)
        {
            case GameState.Start:
                EmitSignal(nameof(GameStarted));
                break;
            case GameState.Paused:
                EmitSignal(nameof(GamePaused));
                break;
            case GameState.Playing:
                EmitSignal(nameof(GameResumed));
                break;
            case GameState.GameOver:
                EmitSignal(nameof(GameOver));
                break;
        }
    }
    
    //example psuedo usage
    // public class Game : Node
    // {
    //     private GameStateManager _gameStateManager;
    //
    //     public override void _Ready()
    //     {
    //         // Get the GameStateManager instance
    //         _gameStateManager = GetNode<GameStateManager>("/root/GameStateManager");
    //
    //         // Connect to state change signals
    //         _gameStateManager.Connect("GameStarted", this, nameof(OnGameStarted));
    //         _gameStateManager.Connect("GamePaused", this, nameof(OnGamePaused));
    //         _gameStateManager.Connect("GameResumed", this, nameof(OnGameResumed));
    //         _gameStateManager.Connect("GameOver", this, nameof(OnGameOver));
    //
    //         // Initial state
    //         _gameStateManager.ChangeState(GameState.Start);
    //     }
    //
    //     private void OnGameStarted()
    //     {
    //         GD.Print("Game started!");
    //         // Implement start game logic here
    //     }
    //
    //     private void OnGamePaused()
    //     {
    //         GD.Print("Game paused!");
    //         // Implement pause game logic here
    //     }
    //
    //     private void OnGameResumed()
    //     {
    //         GD.Print("Game resumed!");
    //         // Implement resume game logic here
    //     }
    //
    //     private void OnGameOver()
    //     {
    //         GD.Print("Game over!");
    //         // Implement game over logic here
    //     }
    // }

}