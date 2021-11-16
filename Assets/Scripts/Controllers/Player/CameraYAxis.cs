using Cinemachine;
using UnityEngine;

namespace planTopia
{
    public class CameraYAxis : CinemachineExtension
    {
        public AxisState yAxis;
        public AxisState.Recentering Recentering = new AxisState.Recentering(false, 1, 2);

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
           if(stage==CinemachineCore.Stage.Aim)
           {
                if (deltaTime < 0 || yAxis.Update(deltaTime))
                    Recentering.CancelRecentering();
                if (deltaTime >= 0)
                    Recentering.DoRecentering(ref yAxis, deltaTime, 0);
                state.RawOrientation = state.RawOrientation * Quaternion.Euler(yAxis.Value, 0, 0);
           }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            var provider = VirtualCamera.GetInputAxisProvider();
            if (provider != null)
                yAxis.SetInputAxisProvider(1, provider);

        }
        private void OnValidate()
        {
            yAxis.Validate();
        }
        private void Reset()
        {
            yAxis = new AxisState(-89, 89, false, false, 50f, 0.1f, 0.1f, "Mouse Y", false);
        }


    }
}
