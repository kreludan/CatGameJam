using Godot;

public enum GameState
{
	Start,
	Playing,
	Paused,
	GameOver
}

public partial class GameStateManager : Node
{
	// Signals for state change events
	[Signal]
	public delegate void GameStartedEventHandler();
	[Signal]
	public delegate void GamePausedEventHandler();
	[Signal]
	public delegate void GameResumedEventHandler();
	[Signal]
	public delegate void GameOverEventHandler();
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
				EmitSignal(nameof(GameStartedEventHandler));
				break;
			case GameState.Paused:
				EmitSignal(nameof(GamePausedEventHandler));
				break;
			case GameState.Playing:
				EmitSignal(nameof(GameResumedEventHandler));
				break;
			case GameState.GameOver:
				EmitSignal(nameof(GameOverEventHandler));
				break;
		}
	}

	// private void DeactivateObject(Node2D)
	// {
	// 	
	// }
	
	
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
