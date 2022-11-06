using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float pSuperSpeed;
        [SerializeField] private float pSpeed;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float inputH = Input.GetAxisRaw("Horizontal");
            float inputV = Input.GetAxisRaw("Vertical");


            // Vector2 direction = transform.up * -inputH + transform.right * inputV;
            Vector2 direction = Vector2.up * inputV + Vector2.right * inputH;

            if (Input.GetKey(KeyCode.LeftShift))
                _rigidbody2D.velocity = direction * pSuperSpeed;
            else
                _rigidbody2D.velocity = direction * pSpeed;
        }
    }
}