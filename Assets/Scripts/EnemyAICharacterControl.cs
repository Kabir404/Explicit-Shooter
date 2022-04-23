using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    //These are required
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    [RequireComponent(typeof(Health))]
    //Main MonoBehaviour Class
    public class EnemyAICharacterControl : MonoBehaviour
    {
        //Navmesh Get Objects
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        
        [Header("NavMesh Properties")]
        public float distanceToStop = 4f; //Distance to stop at to avoid direct collisions.
        
        [Header("Projectile Properties")]
        public Transform firePoint = null; //Location to fire projectiles
        public GameObject projectile = null; // Projectile to fire(instantiate)
        
        [Header("Firing Properties")]
        public float firePower = 50f; // Firepower
        public float fireDistance = 8f; // Distance to start firing at
        public float fireRate = 8f; // The rate to fire at

        
        private Transform target = null;  //Target to Aim For
        private float nextTimeToFire = 0f; // Next Time to fire


        //Runs when the game loads
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = true;
	        agent.updatePosition = true;
            agent.stoppingDistance = distanceToStop;
        }
        //Runs on every frame update
        private void Update()
        {
            if (!target) { return; }
            EnemyMovement();
            EnemyFire();
        }
        void EnemyMovement()
        {
            agent.SetDestination(target.position); //Set position

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else {
                character.Move(Vector3.zero, false, false);
                return;
            }


        }

        void EnemyFire()
        {
            //Fire if player is near
            if (agent.remainingDistance <= fireDistance && target && Time.time >= nextTimeToFire)
            {
                //fires the projectiles
                nextTimeToFire = Time.time + 1f / fireRate;
                GameObject pro = Instantiate(projectile, firePoint.position, firePoint.rotation);
                pro.gameObject.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * 100 * firePower);
            }
        }

        //Check for player collisons
        void OnTriggerEnter(Collider other)
        {
            if (GetComponent<Collider>().GetType() == typeof(SphereCollider)) print("Sphere");
            {
                if (other.gameObject.tag == "Player")
                {
                    target = other.transform;
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (GetComponent<Collider>().GetType() == typeof(SphereCollider)) print("Sphere");
            {
                if (other.gameObject.tag == "Player")
                {
                    target = null;
                }
            }
        }

        //Set the target
        public void SetTarget(Transform target)
        {
            this.target = target;
        }


    }
}
