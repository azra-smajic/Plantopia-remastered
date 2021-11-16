using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace planTopia.Controllers.Core
{
    public class InputManagment : MonoBehaviour
    {
        public Action<float, float> OnInputChanged;
        public Action OnJump;
        public Action<bool> ZoomCamera;
        public Action OnRotationCamera;
        public Action OnShooting;
        [SerializeField]
        private AudioSource StartSound;
        [SerializeField]
        private AudioSource VictorySound;
        
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnJump();
            OnInputChanged(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetButton("Fire1"))
            {
                OnRotationCamera?.Invoke();
                OnShooting?.Invoke();
            }

            if (Input.GetMouseButton(1))
            {
                ZoomCamera?.Invoke(true);
                OnRotationCamera?.Invoke();
            }
            else
                ZoomCamera?.Invoke(false);


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.None;
                // VictorySound.Stop();
                // StartSound.Play();
            }
        }
    }
}
