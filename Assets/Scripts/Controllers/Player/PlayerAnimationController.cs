using planTopia.Core;
using planTopia.ScriptabileObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private static readonly int IDLE = Animator.StringToHash(Constants.Tag.IDLE);
        private static readonly int Running = Animator.StringToHash(Constants.Tag.Running);
        private static readonly int Jump = Animator.StringToHash(Constants.Tag.Jump);
        private static readonly int Death = Animator.StringToHash(Constants.Tag.Death);
        [SerializeField]
        private SFX Steps;
        [SerializeField]
        private AudioManager AudioManager;
        private Animator PlayerAnimator { get; set; }
        private float NextTime;
        private float StepTime=0.45f;

        private void Start()
        {
           
            NextTime = Time.time;
            PlayerAnimator = this.GetComponent<Animator>();
        }

        public void SetRunningTrue()
        {

            PlayerAnimator.SetBool(Running, true);
            if (Time.time > NextTime)
            {
                
                NextTime = Time.time + StepTime;
                AudioManager.Play(Steps);
            }

        }
        public void SetRunningFalse() => PlayerAnimator.SetBool(Running, false);
        public void SetTriggerJump() => PlayerAnimator.SetTrigger(Jump);
        public void SetTriggerDeath() => PlayerAnimator.SetTrigger(Death);
        public void SetTriggerIDLE() => PlayerAnimator.SetTrigger(IDLE);

    }
}
