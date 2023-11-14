using Godot;

public partial class TimeManager : Node
{
    public float ElapsedTimeSeconds { get; private set; }
    public int ElapsedTimeFrames { get; private set; }

    public override void _Process(double delta)
    {
        ElapsedTimeSeconds += (float)delta;
        ElapsedTimeFrames += 1;
    }

    public void ResetTime()
    {
        ElapsedTimeSeconds = 0f;
        ElapsedTimeFrames = 0;
    }

    public void PauseTime()
    {
        SetProcess(false); 
    }

    public void ResumeTime()
    {
        SetProcess(true);
    }
}