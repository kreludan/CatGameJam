using System.Diagnostics;
using Godot;

public partial class StatusEffect
{
    public string SeName = "Status Effect";
    public float Duration { get; set; }
    public float Timer { get; protected set; }
    public int ActiveFrames { get; protected set; }

    public StatusEffect()
    {
        Timer = 0;
        Duration = Mathf.Inf;
    }
    
    public virtual void ApplyEffect(Node2D target)
    {
        // Implement the logic for applying the effect to the target
    }

    public virtual void Update(float delta)
    {
        Timer += delta;
        ActiveFrames = (int)(Timer * Engine.GetFramesPerSecond());
        if (Timer >= Duration)
        {
            RemoveEffect();
        }
    }

    public virtual void RemoveEffect()
    {
        // Implement the logic for removing the effect from the target
    }
}