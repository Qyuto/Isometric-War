using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "weaponInfo", menuName = "Isometric/Weapon", order = 0)]
    public class WeaponInfo : ScriptableObject
    {
        [SerializeField] private string weaponDescription;
        [SerializeField] private Sprite weaponSprite;

        public string GetWeaponDescription() => weaponDescription;
        public Sprite GetWeaponSprite() => weaponSprite;
    }
}