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
    private Timer _damageCooldownTimer;
    private Sprite2D _sprite;
    private Material _spriteMat;
    
    public override void _Ready()
    {
        _currentLives = _maxLives;
        _sprite = Owner.GetNode<Sprite2D>("Sprite");
        _spriteMat = _sprite.Material;
        _damageCooldownTimer = GetNode<Timer>("TakeDamageTimer");
        _damageCooldownTimer.OneShot = true;
    }
    
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
    
    public void TakeDamage(int damage)
    {
        if (!_damageCooldownTimer.IsStopped()) return;
        
        _currentLives -= damage;
        if (_currentLives <= 0)
        {
            //EmitSignal("DeathEventHandler");
        }
        ((ShaderMaterial)_spriteMat).SetShaderParameter("opacity", 0.7f);
        ((ShaderMaterial)_spriteMat).SetShaderParameter("r", 1.0f);
        ((ShaderMaterial)_spriteMat).SetShaderParameter("g", 0f);
        ((ShaderMaterial)_spriteMat).SetShaderParameter("b", 0f);
        ((ShaderMaterial)_spriteMat).SetShaderParameter("mix_color", 0.7f);
        GD.Print("Take damage");
        GD.Print( Owner.Name + "'s opacity: " + _sprite.Material.Get("opacity"));
        _damageCooldownTimer.Start();

    }

    public void _on_take_damage_timer_timeout()
    {
        ((ShaderMaterial)_spriteMat).SetShaderParameter("opacity", 1.0f);
        ((ShaderMaterial)_spriteMat).SetShaderParameter("mix_color", 0f);
        GD.Print("Timer timeout opacity: " + _sprite.Material.Get("opacity"));
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