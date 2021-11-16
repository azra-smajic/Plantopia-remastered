using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Controllers.Player
{
    public class IsGrounded: MonoBehaviour
    {
        [SerializeField]
        private bool CheckIsGrounded;
        

        private void OnCollisionEnter(Collision other)
        {
            CheckIsGrounded = true;
            Debug.LogWarning((CheckIsGrounded?("IS GROUNDED"):("IS JUMPING")));
        }

        private void OnCollisionExit(Collision other)
        {
            CheckIsGrounded = false;
            Debug.LogWarning((CheckIsGrounded?("IS GROUNDED"):("IS JUMPING")));
        }

        public bool GetIsGrounded() => CheckIsGrounded;
    }
}
