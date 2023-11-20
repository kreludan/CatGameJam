using Godot;

public static class Extensions
{
    public static bool IsValid<T>(this T node) where T : GodotObject
    {
        return node != null
               && GodotObject.IsInstanceValid(node)
               && !node.IsQueuedForDeletion();  
    }
    
    public static T IfValid<T>(this T control) where T : GodotObject
        => control.IsValid() ? control : null;
    
    public static void SafeQueueFree(this Node node)
    {
        if (node.IsValid())
        {
            node.QueueFree();
        }
    }
}