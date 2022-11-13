using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform shotPoint;
        [SerializeField] private Weapon baseWeapon;
        [SerializeField] private Weapon currentWeapon;

        private void Start()
        {
            if (currentWeapon == null) return;
            currentWeapon = currentWeapon.InitWeapon(shotPoint);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
                currentWeapon.Shoot(shotPoint);
        }

        public void ChangeCurrentWeapon(Weapon newWeapon)
        {
            Destroy(currentWeapon.gameObject);
            currentWeapon = newWeapon == null ? baseWeapon.InitWeapon(shotPoint) : newWeapon.InitWeapon(shotPoint);
        }
    }
}