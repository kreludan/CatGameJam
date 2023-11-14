using Godot;

public partial class Health : Node
{
    [Signal]
    public delegate void DeathEventHandler();
    private const int MaxLives = 9;
    private int _currentLives = MaxLives;
    private int _bonusLives;

    public void TakeDamage(int damage)
    {
        _currentLives -= damage;
        if (_currentLives <= 0)
        {
            // Trigger the death event when lives reach zero
            EmitSignal("Death");
        }
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
        _currentLives = Mathf.Min(_currentLives + livesToAdd, MaxLives);
    }

    public int GetCurrentLives()
    {
        return _currentLives;
    }
}