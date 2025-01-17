using UnityEngine;
using System;
using System.Collections;

namespace LaserCrush.Entity.Item
{
    public class DroppedItem : PoolableMonoBehaviour
    {
        #region Variable
        [SerializeField] private AnimationCurve m_AnimationCurve;
        [SerializeField] protected int m_AcquiredItemIndex;

        private Action<PoolableMonoBehaviour> m_ReturnAction;

        private readonly static string m_AcquireAudioKey = "ItemAcquired";

        private readonly static int s_MaxDuplicateCount = 2;
        private static int s_CurrentDuplicateCount;

        public const float m_AnimationTime = 0.5f;
        #endregion

        public void Init(Action<PoolableMonoBehaviour> returnAction)
        {
            s_CurrentDuplicateCount = 0;
            m_ReturnAction = returnAction;
        }

        public int GetItemIndex() => m_AcquiredItemIndex;
        
        public void GetItemWithAnimation(Vector2 pos)
            => StartCoroutine(GetItemAnimation(pos));

        protected IEnumerator GetItemAnimation(Vector2 destinationPos)
        {
            Vector2 startPos = transform.position;
            float elapsedTime = 0;
            float t;
            while (elapsedTime <= m_AnimationTime)
            {
                t = m_AnimationCurve.Evaluate(elapsedTime / m_AnimationTime);
                //t = 1 - Mathf.Pow(1 - elapsedTime / m_AnimationTime, 2);

                transform.position = Vector2.Lerp(startPos, destinationPos, t);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            BeforeReturnCall();
            transform.position = destinationPos;

            ReturnObject();
        }

        protected virtual void BeforeReturnCall()
        {
            if(++s_CurrentDuplicateCount < s_MaxDuplicateCount)
                Manager.AudioManager.AudioManagerInstance.PlayOneShotUISE(m_AcquireAudioKey);
        }

        public override void ReturnObject()
        {
            StopAllCoroutines();
            m_ReturnAction?.Invoke(this);
        }

        private void OnDestroy()
            => m_ReturnAction = null;
    }
}
