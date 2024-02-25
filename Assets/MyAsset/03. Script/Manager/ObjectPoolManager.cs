using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaserCrush.Entity;

namespace LaserCrush.Manager
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private static Transform m_DeActivePool;

        private void Awake() => m_DeActivePool = transform;

        /// <summary>
        /// Pooing ��ü ��ϰ� ���ÿ� count��ŭ �̸� ����
        /// </summary>
        /// <param name="poolableScript"> PoolableScript�� ��ӹ޴� ��ü </param>
        /// <param name="parent"> Hierarchy ������Ʈ ��ġ </param>
        /// <param name="count"> �̸� ������ ����</param>>
        /// <returns>PoolingObject</returns>
        public static PoolingObject RegisterAndGenerate(PoolableMonoBehaviour poolableScript, Transform parent, int count)
        {
            PoolingObject poolingObject = new PoolingObject(poolableScript, parent);
            poolingObject.GenerateObj(count);
            return poolingObject;
        }

        /// <summary>
        /// Pooling ���� ��ü
        /// </summary>
        public class PoolingObject
        {
            private readonly Queue<PoolableMonoBehaviour> poolableQueue;
            private readonly PoolableMonoBehaviour script;
            private readonly Transform parent;

            public PoolingObject(PoolableMonoBehaviour script, Transform parent)
            {
                poolableQueue = new();
                this.script = script;
                this.parent = parent;
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
            private PoolableMonoBehaviour CreateNewObject()
            {
                var newObj = Instantiate(script);
                newObj.gameObject.SetActive(false);
                newObj.transform.SetParent(m_DeActivePool);
                return newObj;
            }

            /// <summary>
            /// ������ PoolableScript ��ȯ
            /// </summary>
            /// <param name="preActive">������Ʈ�� �̸� Ȱ��ȭ �Ұ��ΰ�</param>
            /// <returns>PoolableScript�� ��ȯ</returns>
            public PoolableMonoBehaviour GetObject(bool preActive)
            {
                PoolableMonoBehaviour obj;
                if (poolableQueue.Count > 0) obj = poolableQueue.Dequeue();
                else obj = CreateNewObject();

                obj.transform.SetParent(parent);
                obj.gameObject.SetActive(preActive);

                return obj;
            }

            /// <summary>
            /// ������Ʈ Pool�� �ݳ�
            /// </summary>
            /// <param name="obj">PoolableScript�� ��ӹ޴� ��ü</param>
            public void ReturnObject(PoolableMonoBehaviour obj)
            {
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(m_DeActivePool);
                poolableQueue.Enqueue(obj);
            }
        }
    }
}
