using Godot;
using System;

public partial class Torch : PointLight2D
{
    private readonly Random _rand = new();
    private double _flickerTimer;
    private float _flickerInterval = 0.1f;
    private float _flickerIntensityRange = 0.2f;

    public override void _Process(double delta)
    {
        Flicker(delta);
    }

    private void Flicker(double delta)
    {
        _flickerTimer -= delta;

        // Check if it's time to flicker
        if (_flickerTimer <= 0)
        {
            // Reset the timer
            _flickerTimer = _flickerInterval;
            // Randomly adjust intensity within the specified range
            float randomIntensity = (float)(_rand.NextDouble() * 2 * _flickerIntensityRange - _flickerIntensityRange);
            float flickerIntensity = Energy + randomIntensity;
            // Make sure the intensity stays within the valid range 
            flickerIntensity = Mathf.Clamp(flickerIntensity, 0.75f, 2f);
            // Apply flicker intensity
            Energy = flickerIntensity;
        }
    }
}