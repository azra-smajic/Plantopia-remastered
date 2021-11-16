using System;
using Cinemachine;
using planTopia.Controllers.Core;
using planTopia.Controllers.Player;
using planTopia.Core;
using UnityEngine;

namespace planTopia
{
    public class PlayerController : BaseBehaviour
    {
        [SerializeField] private InputManagment InputManagment;
        [SerializeField] private Transform Camera;
        [SerializeField] private Transform GroundCheckTransfrom;
        [SerializeField] public float JumpHeight;

        [SerializeField]
        [Range(0.1f, 100f)]
        public float Speed;
        [SerializeField]
        [Range(0.1f, 3f)]
        private float turnSmoothTime = 0.35f;
        private Rigidbody Rigidbody { get; set; }
        private SphereCollider Collider { get; set; }
        private PlayerAnimationController PlayerAnimator { get; set; }

        float turnSmoothVelicoty;
        private bool isGrounded = true;
        private void Start()
        {
            InputManagment.OnInputChanged += OnInputChanged;
            InputManagment.OnRotationCamera += OnXMouseMove;
            InputManagment.OnJump += Jump;

            Rigidbody = this.GetComponent<Rigidbody>();
            Collider = this.GetComponent<SphereCollider>();
            PlayerAnimator = this.GetComponent<PlayerAnimationController>();
        }

        private void OnXMouseMove()
        {
            float angle = Camera.transform.rotation.eulerAngles.y;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, angle, 0), turnSmoothTime);
        }
        private void OnInputChanged(float horizontal, float vertical)
        {
            if (startMove)
            {
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                if (direction.magnitude > 0)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelicoty, turnSmoothTime);

                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    Rigidbody.MovePosition(transform.position + moveDir.normalized * Speed * Time.deltaTime);

                    PlayerAnimator.SetRunningTrue();
                }
                else
                    PlayerAnimator.SetRunningFalse();
            }
        }
        private void Jump()
        {
            if (Physics.OverlapSphere(GroundCheckTransfrom.position, 0.03f).Length > 1)
            {
                this.Rigidbody.AddForce(Vector3.up * JumpHeight, ForceMode.VelocityChange);
                PlayerAnimator.SetTriggerJump();
            }
        }
    }
}
