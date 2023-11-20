using Godot;

public partial class BatFollow : EnemyState
{
    private float _moveSpeed = 80f;
    private float _enemyFollowRange = 225f;
    private float _maxDeviationAngle = 45f;
    private float _deviationUpdateInterval = 0.5f;
    private float _timeSinceLastUpdate;

    public override void Enter() { }
    
    public override void PhysicsUpdate(float delta)
    {
        Vector2 targetDirection = PlayerDirection.Normalized();

        _timeSinceLastUpdate += delta;
        if (_timeSinceLastUpdate >= _deviationUpdateInterval)
        {
            float deviationAngle = Mathf.DegToRad(_maxDeviationAngle);
            float randomDeviation = Rng.RandfRange(-deviationAngle, deviationAngle);
            float cosRandomDeviation = Mathf.Cos(randomDeviation);
            float sinRandomDeviation = Mathf.Sin(randomDeviation);

            float newDirX = targetDirection.X * cosRandomDeviation - targetDirection.Y * sinRandomDeviation;
            float newDirY = targetDirection.X * sinRandomDeviation + targetDirection.Y * cosRandomDeviation;

            Vector2 randomizedDirection = new(newDirX, newDirY);

            Enemy.Velocity = randomizedDirection.Normalized() * _moveSpeed;

            _timeSinceLastUpdate = 0f;
        }

        if (PlayerDirection.Length() > _enemyFollowRange)
        {
            EmitSignal(nameof(Transitioned), this, "idle");
        }
    }
}