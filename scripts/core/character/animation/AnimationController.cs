using Godot;

public partial class AnimationController : AnimationPlayer
{
    public void PlayAnimation(string animationName)
    {
        if (HasAnimation(animationName))
        {
            Play(animationName);
        }
        else
        {
            GD.PrintErr("Animation not found: " + animationName);
        }
    }

    // Check if a specific animation is currently playing
    public bool IsAnimationPlaying(string animationName)
    {
        return IsPlaying() && CurrentAnimation == animationName;
    }

    // Stop the currently playing animation
    public void StopAnimation()
    {
        Stop();
    }

    // Set the speed scale of the animation player
    public void SetAnimationSpeed(float speed)
    {
        SpeedScale = speed;
    }
}