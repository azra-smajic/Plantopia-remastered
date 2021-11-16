
using System;
using UnityEngine;
using UnityEngine.AI;

namespace planTopia.Boss
{
    public class BossController : Attack
    {
        [SerializeField]
        private GameObject Target;
        [SerializeField]
        [Range(0, 20)]
        private float MovingDistance = 2f;
        private Animator Animator { get; set; }
        private NavMeshAgent Agent { get; set; }
        // [SerializeField]
        // private AudioSource AudioSource;
        //
       

        private void Start()
        {
            Agent = this.GetComponent<NavMeshAgent>();
            Animator = this.GetComponent<Animator>();
           

        }
        private void Update()
        {


            if (!startMove)
            {
                Agent.isStopped = true; return; 
            }

          
            if (Mathf.Abs(Vector3.Distance(this.transform.position, Target.transform.position)) < MovingDistance)
            {
                Agent.isStopped = true;
                ActivateAttack(Animator);
                // if(!AudioSource.isPlaying)
                //    Invoke( nameof(PlayAudio),0.5f);

            }
            else
            {
                Agent.isStopped = false;
                DeactivateAttack(Animator);
                // AudioSource.Stop();
                Agent.SetDestination(Target.transform.position);
            }
        }

        // private void PlayAudio()
        // {
        //     AudioSource.Play();
        //         
        //  }
    }
}
