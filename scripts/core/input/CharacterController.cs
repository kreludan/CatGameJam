using System.Diagnostics;
using Godot;

public partial class CharacterController : Node2D
{
    private InputManager _inputManager;
    private Entity _entity;
    private Vector2 _velocity = Vector2.Zero;
    [Export]
    private float _speed;

    private bool _dodgePressed;
    private float _initialDodgeSpeed = 1;
    private float _maxDodgeSpeed = 2.5f;
    private float _currentDodgeSpeed = 1;
    private const float DodgeDecaySpeed = 8f;
    private float _dodgeCooldown = 1.5f; // Set your desired cooldown time
    private float _dodgeTimer = 0;

    public override void _Ready()
    {
        _inputManager = GetNode<InputManager>("/root/InputManager");
        _entity = Owner as Entity;
        if (_entity == null)
        {
            Debug.Print(Owner.Name + " is not a character!!!");
        }
    }

    public override void _Process(double delta)
    {
        HandleInput(delta);
        UpdateDodgeCooldown(delta);
    }

    private void HandleInput(double delta)
    {
        if (_entity == null) return;

        Vector2 moveDirection = _inputManager.GetMoveDirection();
        _dodgePressed = _inputManager.DodgePressed();

        if (_dodgePressed && CanDodge())
        {
            _currentDodgeSpeed = _maxDodgeSpeed;
            _dodgeTimer = _dodgeCooldown;
        }
        else
        {
            if (_currentDodgeSpeed > _initialDodgeSpeed)
            {
                _currentDodgeSpeed -= DodgeDecaySpeed * (float)delta;
            }
        }
        _entity.Velocity = moveDirection * _speed * _currentDodgeSpeed;
    }

    private bool CanDodge()
    {
        return _dodgeTimer <= 0;
    }

    private void UpdateDodgeCooldown(double delta)
    {
        if (_dodgeTimer > 0)
        {
            _dodgeTimer -= (float)delta;
        }
    }
}