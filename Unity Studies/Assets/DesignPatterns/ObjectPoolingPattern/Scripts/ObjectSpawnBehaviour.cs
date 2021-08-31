namespace DesignPatterns.ObjectPoolingPattern.Scripts
{
    using UnityEngine;

    public class ObjectSpawnBehaviour : MonoBehaviour, IPooledObject
    {
        public float upForce = 1f;

        public float sideForce = 1f;

        public void OnObjectSpawn()
        {
            float xForce = Random.Range(-sideForce, sideForce);
            float yForce = Random.Range(upForce / 2f, upForce);
            float zForce = Random.Range(-sideForce, sideForce);

            Vector3 force = new Vector3(xForce, yForce, zForce);

            GetComponent<Rigidbody>().velocity = force;
        }
    }
}