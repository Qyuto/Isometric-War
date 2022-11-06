using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Rotation();
        }

        private void Rotation()
        {
            Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 different = mousePosition - transform.position;

            float angel = Mathf.Atan2(different.y, different.x) * Mathf.Rad2Deg;
            _rigidbody2D.rotation = angel;
        }
    }
}