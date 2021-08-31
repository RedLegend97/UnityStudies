namespace DesignPatterns.ObjectPoolingPattern.Scripts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ObjectSpawner : MonoBehaviour
    {
        private ObjectPooler _objectPooler;

        [SerializeField] [Tooltip("Objects spawn point")]
        private Vector3 spawnPosition;

        private void Start()
        {
            _objectPooler = ObjectPooler.Instance;
        }

        private void FixedUpdate()
        {
            _objectPooler.SpawnFromPool("PoolObject", spawnPosition, Quaternion.identity);
        }
    }
}