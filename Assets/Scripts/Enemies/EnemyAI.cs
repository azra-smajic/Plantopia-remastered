using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;


namespace planTopia.Enemies
{
    public class EnemyAI: MonoBehaviour
    {
        [SerializeField] 
        private Transform player;
        [SerializeField] 
        private LayerMask whatIsPlayer;
        [SerializeField] 
        private float health=100;
        private NavMeshAgent agent { get; set; }
        
        //Patroling
        [SerializeField] 
        private List<GameObject> patrolingPositions;
        [SerializeField] 
        float patrolingSpeed;
        private Vector3 walkPoint;
        private bool walkPointSet;
        private Vector3 DistanceToWalkPoint;

        //Attacking
        [SerializeField] 
        private float timeBetweenAttacks;
        [SerializeField]
        public GameObject projectile;
        private bool alreadyAttacked;

        //States
        [SerializeField] 
        private float sightRange, attackRange;
        [SerializeField] 
        private bool playerInSightRange, playerInAttackRange;
        
        //Hide
        [SerializeField] 
        private List<GameObject> hiddenPlaces;
        [SerializeField] 
        private float escapeSpeed;
        private Vector3 nextHiddenPlace;
        private bool hiddenPlaceSet;
        private Vector3 distanceToFleePoint;
        

        private Collider[] colliders;
        private RaycastHit info;
        private Ray ray;

        private void Start()
        {
            agent = this.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            
            if (health <= 20) Hide();
            else if(!playerInSightRange&&!playerInAttackRange) Patroling();
            else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            else if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }

        private void Hide()
        {
            if(!hiddenPlaceSet)
                NextFleePoint();
            
            if (playerInAttackRange&&hiddenPlaceSet)
            {
                agent.SetDestination(nextHiddenPlace);
                agent.speed = escapeSpeed;
            }
            
            distanceToFleePoint = transform.position - nextHiddenPlace;
            
            if (distanceToFleePoint.magnitude < 1f)
                hiddenPlaceSet = false;
            

        }

        private void NextFleePoint()
        {
            nextHiddenPlace = hiddenPlaces[UnityEngine.Random.Range(0, hiddenPlaces.Count)].transform.position;
            hiddenPlaceSet = true;
        }

        private void Patroling()
        {
            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
                agent.speed = patrolingSpeed;
            }
            else NextWalkPoint();
            
            DistanceToWalkPoint = transform.position - walkPoint;

            if (DistanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
        
        private void NextWalkPoint()
        {
            walkPoint = patrolingPositions[UnityEngine.Random.Range(0, patrolingPositions.Count)].transform.position;
            walkPointSet = true;
        }
        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);
            // transform.LookAt(Player);
            LookAtPlayer();
            if (!alreadyAttacked)
            {
                ray.origin = transform.position;
                ray.direction = player.position - transform.position;
                
                if (Physics.Raycast(ray, out info))
                {
                    if (info.collider.gameObject.tag == Constants.Tag.PLAYER)
                    {
                        GameObject projectile = Instantiate(this.projectile, transform.position, Quaternion.identity);
                        Rigidbody rb = projectile.GetComponent<Rigidbody>();

                        float dist=Vector3.Distance(player.position, transform.position);
                        
                        rb.AddForce(transform.forward * dist*2, ForceMode.Impulse);
                        rb.AddForce(transform.up * 1.4f, ForceMode.Impulse);
                        
                        alreadyAttacked = true;
                        Invoke(nameof(ResetAttack), timeBetweenAttacks);

                    }
                }
            }
        }

        private void DestroyProjectile(GameObject obj)
        {
            Destroy(obj);
        }

        private void ResetAttack()=>alreadyAttacked = false;
        private void ChasePlayer()
        {
            agent.SetDestination(player.position);
            // transform.LookAt(Player);
            LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            Vector3 direction = (player.position - transform.position).normalized;
            
            Quaternion lookRoration=Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation=Quaternion.Slerp(transform.rotation, lookRoration, Time.deltaTime*5f);
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
            
            if(health<=0)
                Invoke(nameof(DestroyEnemy), .5f);
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color=Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color=Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
            
        }
    }
}
