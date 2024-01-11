using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ObjectPoolManager : MonoBehaviour
    {
        [SerializeField] private Transform m_ActiveMonsterPool;
        [SerializeField] private Transform m_ActiveObjectPool;
        private static Transform cachedTransform;

        private void Awake() => cachedTransform = transform;

        /// <summary>
        /// Pooing ��ü ���
        /// </summary>
        /// <param name="_poolableScript"> PoolableScript�� ��ӹ޴� ��ü </param>
        /// <param name="_parent"> Hierarchy ������Ʈ ��ġ </param>
        /// <returns>PoolingObject</returns>
        public static PoolingObject Register(PoolableScript _poolableScript, Transform _parent) => new(_poolableScript, _parent);

        /// <summary>
        /// Pooling ���� ��ü
        /// </summary>
        public class PoolingObject
        {
            private readonly Queue<PoolableScript> poolableQueue;
            private readonly PoolableScript script;
            private readonly Transform parent;

            public PoolingObject(PoolableScript _script, Transform _parent)
            {
                poolableQueue = new();
                script = _script;
                parent = _parent;
            }

            /// <summary>
            /// ��ϵ� ������Ʈ ����
            /// </summary>
            /// <param name="_count"> ������ ���� </param>
            public void GenerateObj(int _count)
            {
                for (int i = 0; i < _count; ++i) poolableQueue.Enqueue(CreateNewObject());
            }

            /// <summary>
            /// ������Ʈ ����
            /// </summary>
            /// <returns></returns>
            private PoolableScript CreateNewObject() 
            {
                var newObj = Instantiate(script);
                newObj.gameObject.SetActive(false);
                newObj.transform.SetParent(cachedTransform);
                return newObj;
            }

            /// <summary>
            /// ������ PoolableScript ��ȯ
            /// </summary>
            /// <param name="preActive">������Ʈ�� �̸� Ȱ��ȭ �Ұ��ΰ�</param>
            /// <returns>PoolableScript�� ��ȯ</returns>
            public PoolableScript GetObject(bool preActive, bool preSetParent = true)
            {
                PoolableScript obj;
                if (poolableQueue.Count > 0) obj = poolableQueue.Dequeue();
                else                         obj = CreateNewObject();

                if (preSetParent) obj.transform.SetParent(parent);
                obj.gameObject.SetActive(preActive);

                return obj;
            }

            /// <summary>
            /// ������Ʈ Pool�� �ݳ�
            /// </summary>
            /// <param name="obj">PoolableScript�� ��ӹ޴� ��ü</param>
            public void ReturnObject(PoolableScript obj)
            {
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(cachedTransform);
                poolableQueue.Enqueue(obj);
            }
        }
    }
}
