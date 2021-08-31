namespace DesignPatterns.ObjectPoolingPattern.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;

            [Tooltip("Maximum numbers of balls active on the scene")]
            public int size;
        }

        public static ObjectPooler Instance;

        private void Awake()
        {
            #region Singleton

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                //Only leave the line in the bottom if you want to use your pooled objects on your other scenes as well
                //todo may put a boolean to here in the future
                DontDestroyOnLoad(gameObject);
            }

            #endregion
        }


        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> PoolDictionary;

        // Start is called before the first frame update
        void Start()
        {
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform, true); //TODO ADD TO A SPECIFIC PARENT
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                PoolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!PoolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exists");
                return null;
            }

            GameObject objectToSpawn = PoolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            PoolDictionary[tag].Enqueue(objectToSpawn);

            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }

            return objectToSpawn;
        }
    }
}