using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    public interface IDamageable
    {
        public void OnHit(GameObject source,int damage);
    }
}