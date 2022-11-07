using System;
using UnityEngine;

namespace Interface
{
    public class ReflectTest : MonoBehaviour
    {
        [SerializeField] private float rayDistance;

        private void Update()
        {
            Test();
        }

        private void Test()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDistance);
            Debug.DrawRay(transform.position, transform.right * rayDistance);

            if (hit.transform == null) return;

            Vector3 reflect = Vector3.Reflect(transform.right * rayDistance, hit.normal);
            Debug.DrawRay(hit.point, reflect);
        }
    }
}