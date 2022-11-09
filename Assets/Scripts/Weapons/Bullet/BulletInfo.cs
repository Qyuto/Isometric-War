using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "bulletName", menuName = "Isometric/Bullet", order = 0)]
    public class BulletInfo : ScriptableObject
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int bulletDamage;
        
        public Bullet GetBulletPrefab() => bulletPrefab;
        public int GetBulletDamage() => bulletDamage;
    }
}