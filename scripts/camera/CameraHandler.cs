using Godot;

public partial class CameraHandler : Camera2D
{
	private static float _randomShakeStrength = 10f;
	private float _shakeFade = 8.0f;
	private RandomNumberGenerator _rng = new();
	private static float _currentShakeStrength;

	public static void ApplyShake()
	{
		_currentShakeStrength = _randomShakeStrength;
	}

	public override void _Process(double delta)
	{
		if (_currentShakeStrength > 0)
		{
			_currentShakeStrength = Lerp(_currentShakeStrength, 0, _shakeFade * (float)delta);
			Offset = RandomOffset();
		}
	}

	public Vector2 RandomOffset()
	{
		return new Vector2(_rng.RandfRange(-_currentShakeStrength, _currentShakeStrength),
			_rng.RandfRange(-_currentShakeStrength, _currentShakeStrength));
	}
	
	float Lerp(float firstFloat, float secondFloat, float by)
	{
		return firstFloat * (1 - by) + secondFloat * by;
	}
}
