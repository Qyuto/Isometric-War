using NPC;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerAttack : EntityAttack
    {
        [SerializeField] private WeaponBase baseWeapon;
        
        private void Update()
        {
            if (Input.GetMouseButton(0))
                base.Attack();
        }

        public void ChangeCurrentWeapon(WeaponBase newWeapon)
        {
            Destroy(entityWeapon.gameObject);
            entityWeapon = newWeapon == null ? baseWeapon.InitWeapon(shootPoint) : newWeapon.InitWeapon(shootPoint);
        }
    }
}