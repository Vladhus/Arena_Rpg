using UnityEngine;
using UnityEngine.AI;

namespace SingleMagicMoba
{
    public class AttackedApplyForce : MonoBehaviour, IAttackable
    {
        public float ForceToAdd;
        private Rigidbody rBody;
        private NavMeshAgent agent;

        public void OnAttack(GameObject attacker, Attack attack)
        {

            var forceDirection = transform.up;
            //transform.position - 
            // attacker.transform.position;
            forceDirection.y += 0.5f;
            //forceDirection.Normalize();

            rBody.AddForce(forceDirection * ForceToAdd, ForceMode.Force);

        }

        private void Awake()
        {

            rBody = GetComponent<Rigidbody>();
        }
    }

}