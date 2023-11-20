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
    [Export]
    private bool _cameraShakeOnDamage;
    private Entity _entity;
    
    public override void _Ready()
    {
        _entity = Owner as Entity;
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
            Node2D ownerNode = Owner as Node2D;
            ownerNode?.Hide();
            ownerNode?.SetProcess(false);
        }
    }

    public void HandleDamage(int damage)
    {
        switch (damage)
        {
            case > 0:
                TakeDamage(damage);
                break;
            case < 0:
                HealDamage(damage);
                break;
        }
    }
    
    private void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        if (!_damageCooldownTimer.IsStopped()) return;
        
        _currentLives -= damage;
        if (_currentLives <= 0)
        {
            _entity?.Die();
            //EmitSignal("DeathEventHandler");
        }
        if (_cameraShakeOnDamage)
        {
            CameraHandler.ApplyShake();
        }
        if (_spriteMat != null)
        {
            ((ShaderMaterial)_spriteMat).SetShaderParameter("opacity", 0.7f);
            ((ShaderMaterial)_spriteMat).SetShaderParameter("r", 1.0f);
            ((ShaderMaterial)_spriteMat).SetShaderParameter("g", 0f);
            ((ShaderMaterial)_spriteMat).SetShaderParameter("b", 0f);
            ((ShaderMaterial)_spriteMat).SetShaderParameter("mix_color", 0.7f);
        }
        _damageCooldownTimer.Start();
    }

    public void _on_take_damage_timer_timeout()
    {
        if (_spriteMat != null)
        {
            ((ShaderMaterial)_spriteMat).SetShaderParameter("opacity", 1.0f);
            ((ShaderMaterial)_spriteMat).SetShaderParameter("mix_color", 0f);
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

    private void HealDamage(int livesToAdd)
    {
        if (livesToAdd >= 0) return;
        
        _currentLives = Mathf.Min(_currentLives + livesToAdd, _maxLives);
    }

    public int GetCurrentLives()
    {
        return _currentLives;
    }
}