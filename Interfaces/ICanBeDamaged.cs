using Godot;
using System;

public interface ICanBeDamaged 
{
    
    public void setInvulnerable(Boolean invulnerable);
    public Boolean isInvulnerable();
    public void addDamage(ulong damage);
    public void removeDamage(ulong damage);
    public void killSelf();
    public Boolean isDead();
    public void healToMaximum();
}
