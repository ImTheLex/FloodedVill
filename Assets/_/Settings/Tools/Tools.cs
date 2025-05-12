using UnityEngine;

namespace CustomTools
{
    public class Tools : MonoBehaviour
    {
        #region Game Loop

        /*protected abstract void OnUpdate(float deltaTime);
        protected abstract void OnMove();

        protected virtual void OnFixedUpdate(float deltaTime)
        {
            Info("On FixedUpdate Parent TCBehaviour");
        }*/

        #endregion

        #region Debug

        [SerializeField, Header("Debug")]
        protected bool m_isVerbose;
        
        
        protected void Info(string message)
        {
            if(!m_isVerbose){return;}
            Debug.Log(message,this);
        }
        protected void Info(int message)
        {
            if(!m_isVerbose){return;}
            Debug.Log(message,this);
        }
        
        protected void Warning(string message)
        {
            
        }

        protected void Error(string message)
        {
            
        }
        #endregion


        #region Getters

        private Rigidbody _rigidbody;
        private Collider _collider;
        
        public new Rigidbody rigidbody => _rigidbody ?
            _rigidbody : _rigidbody = GetComponent<Rigidbody>();
        
        public new Collider collider => _collider ?
            _collider : _collider = GetComponent<Collider>();
        


        #endregion
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
