using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    [RequireComponent(typeof(Health))]
    public class EnemyAICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        private Transform target;                                    // target to aim for

        public Transform firePoint;
        public GameObject projectile;
        public float firePower = 50f;
        public float fireDistance = 8f;
        public float fireRate = 15f;


        private float nextTimeToFire = 0f;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = true;
	        agent.updatePosition = true;
            agent.stoppingDistance = 5f;
        }

        private void Update()
        {
            //Set the target and properties to the agent accordingly
            if (target != null)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else
                character.Move(Vector3.zero, false, false);
            
            //Fire if player is near
            if (agent.remainingDistance <= fireDistance && target != null && Time.time >= nextTimeToFire)
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
