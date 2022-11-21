using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace NPC.Enemy
{
    public class EnemyStaticAttack : EntityAttack
    {
        [SerializeField] private float enemyDelayShoot;
        [SerializeField] private List<Transform> otherShootPoint;

        private float _shootTimer;
        private bool _canShoot;
        private WeaponBase[] _weapons;

        private void Start()
        {
            otherShootPoint.Add(shootPoint);
            _weapons = new WeaponBase[otherShootPoint.Count];
            UpdateWeapons();
        }

        private void Update()
        {
            _shootTimer += Time.deltaTime;
            if (_shootTimer >= enemyDelayShoot)
                _canShoot = true;
        }

        private void UpdateWeapons()
        {
            for (int i = 0; i < _weapons.Length; i++)
                _weapons[i] = entityWeapon.InitWeapon(otherShootPoint[i]);
        }

        public override void Attack()
        {
            if (!_canShoot) return;
            for (int i = 0; i < _weapons.Length; i++)
                _weapons[i].Attack(otherShootPoint[i]);
            _canShoot = false;
            _shootTimer = 0f;
        }
    }
}