using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class StatusEffectHandler : Node2D
{
    private readonly Dictionary<Type, List<StatusEffect>> _statusEffectCache = new();

    public override void _Process(double delta)
    {
        UpdateStatusEffects((float)delta);
    }

    public void ApplyStatusEffect(StatusEffect statusEffect, Node2D target)
    {
        statusEffect.ApplyEffect(target);
    }
    
    public void UpdateStatusEffects(float delta)
    {
        foreach (KeyValuePair<Type, List<StatusEffect>> typeAndEffects in _statusEffectCache)
        {
            List<StatusEffect> effects = typeAndEffects.Value;
            for (int i = effects.Count - 1; i >= 0; i--)
            {
                StatusEffect statusEffect = effects[i];
                statusEffect.Update(delta);
                if (statusEffect.Timer >= statusEffect.Duration)
                {
                    effects.RemoveAt(i);
                }
            }
        }
    }
    
    private List<StatusEffect> GetOrCreateStatusEffectList(Type type)
    {
        if (_statusEffectCache.TryGetValue(type, out List<StatusEffect> cachedEffects))
        {
            return cachedEffects;
        }
        // If the list does not exist for the given type, create a new one
        List<StatusEffect> newEffects = new();
        _statusEffectCache[type] = newEffects;
        return newEffects;
    }
    
    private StatusEffect GetOrCreateStatusEffect<T>(float duration) where T : StatusEffect
    {
        Type type = typeof(T);
        List<StatusEffect> effects = GetOrCreateStatusEffectList(type);
        // Search for an existing effect of the given type
        StatusEffect existingEffect = effects.FirstOrDefault(effect => effect is T);
        if (existingEffect != null)
        {
            // If the effect is already in the list, return it
            existingEffect.Duration = duration;
            return existingEffect;
        }
        // If the effect is not in the list, create a new instance, add it to the list, and return it
        StatusEffect newEffect = Activator.CreateInstance<T>();
        newEffect.Duration = duration;
        effects.Add(newEffect);
        return newEffect;
    }

    public void CreateInvulStatusEffect(float duration, Node2D target)
    {
        StatusEffect invulnerableSe = GetOrCreateStatusEffect<InvulnerableSe>(duration);
        ApplyStatusEffect(invulnerableSe, target);
    }

    public void PrintAllCachedStatusEffects()
    {
        foreach (KeyValuePair<Type, List<StatusEffect>> typeAndEffects in _statusEffectCache)
        {
            List<StatusEffect> effects = typeAndEffects.Value;
            foreach (StatusEffect effect in effects)
            {
                // Assuming you have a property SeName in the StatusEffect class
                string seName = effect.SeName;
                float duration = effect.Duration;
                float timer = effect.Timer;
                GD.Print($"{seName} has a duration of: {duration} and current time is: {timer}");
            }
        }
    }
}