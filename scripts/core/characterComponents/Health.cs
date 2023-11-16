using Godot;

public partial class Health : Node2D
{
    [Export]
    private int _maxLives = 9;
    private int _currentLives;
    private int _bonusLives;
    [Signal]
    public delegate void DeathEventHandler();
    [Signal]
    public delegate void OnTakeDamageEventHandler();
    
    public override void _Process(double delta)
    {
        if (_currentLives <= 0)
        {
            //Owner.QueueFree();
            Node2D ownerNode = Owner as Node2D;
            ownerNode?.Hide();
            ownerNode?.SetProcess(false);
        }
    }

    public override void _Ready()
    {
        _currentLives = _maxLives;
    }

    public void TakeDamage(int damage)
    {
        _currentLives -= damage;
        //GD.Print("Current health:" +  _currentLives + " " + Owner.Name);
        if (_currentLives <= 0)
        {
            // Trigger the death event when lives reach zero
            EmitSignal("Death");
        }
        EmitSignal(SignalName.OnTakeDamage);
    }
	
    public void AddBonusLife(int bonus)
    {
        _bonusLives += bonus;
    }

    public void TakeBonusLife(int bonus)
    {
        _bonusLives = Mathf.Max(_bonusLives - bonus, 0);
    }

    public void GainLife(int livesToAdd)
    {
        _currentLives = Mathf.Min(_currentLives + livesToAdd, _maxLives);
    }

    public int GetCurrentLives()
    {
        return _currentLives;
    }
}