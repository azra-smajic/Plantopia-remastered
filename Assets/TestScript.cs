using System.Collections;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

namespace planTopia
{
    public class TestScript: MonoBehaviour
    {
        [SerializeField] 
        private Transform Head;
        [SerializeField] 
        private float Duration;
        [SerializeField] 
        private float Magnitude;

        private Vector3 OriginalPos;
        
        [SerializeField]
        private AnimationCurve ShakeCurve;
      
        [Button("Test shake")]
        private void Test()
        {
            StartCoroutine(Shake(Duration, Magnitude));
        }
        
        public void StartShake(float dur, float mag)
        {
            StartCoroutine(Shake(dur, mag));
        }
        
        IEnumerator Shake(float duration, float magnitude)
        {
            OriginalPos = Head.localPosition;
            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                Head.localPosition = new Vector3(ShakeCurve.Evaluate(elapsed) * magnitude, OriginalPos.y, OriginalPos.z);
                elapsed += Time.deltaTime;
                yield return null;
            }

            Head.localPosition = OriginalPos;
        }
    }
}
