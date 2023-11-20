using Godot;

public partial class EnemyState : State
{
    protected CharacterBody2D Player;
    protected CharacterBody2D Enemy;
    protected Vector2 PlayerDirection;
    protected RandomNumberGenerator Rng = new();

    public override void _Ready()
    {
        base._Ready();
        if (!Player.IsValid())
        {
            Player =(CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");
        }
        if (!Enemy.IsValid())
        {
            Enemy = Owner as CharacterBody2D;
        }
    }

    public override void _Process(double delta)
    {
        PlayerDirection = Player.GlobalPosition - Enemy.GlobalPosition;
        base._Process(delta);
    }
}