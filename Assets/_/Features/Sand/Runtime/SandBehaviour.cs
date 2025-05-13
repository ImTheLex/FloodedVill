using System;
using CustomTools;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sand.Runtime
{
    public class SandBehaviour : Tools
    {
        [SerializeField]
        private GameObject m_waterPrefab;

        private void Awake()
        {
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        /*private void OnMouseDown()
        {
            Info($"Transform : {gameObject.transform.position}");
            Instantiate(m_waterPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            
        }*/
        
    }
}
