using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using planTopia.Controllers.Core;

namespace planTopia
{
    public class ZoomCameraController : MonoBehaviour
    {
        [SerializeField]
        private InputManagment InputManagment;

        [SerializeField] private bool LockCursor = false;

        private CinemachineFreeLook Camera;
        private void Start()
        {
            Camera = GetComponent<CinemachineFreeLook>();
            InputManagment.ZoomCamera += ZoomInOut;
            Cursor.lockState = LockCursor?CursorLockMode.Locked:CursorLockMode.None;
        }

        private void ZoomInOut(bool zoomed)
        {
            if (zoomed)
                Camera.m_Lens.FieldOfView = 25;
            else
                Camera.m_Lens.FieldOfView = 35;
        }
    }
}
