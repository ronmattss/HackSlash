using System;

namespace ProjectAssets.Scripts
{
    [Serializable]
    public enum EntityState
    {
        Idle,
        Interacting,
        Moving,
        Walking,
        Running,
        Jumping,
        Falling,
        Attacking,
        Blocking,
        Charging,
        Casting,
        Dashing,
        Hurt,
        Dying,
        Dead
    }
}