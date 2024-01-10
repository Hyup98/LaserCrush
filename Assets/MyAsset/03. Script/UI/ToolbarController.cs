using System;
using UnityEngine;
using LaserCrush.Entity;
using LaserCrush.Manager;
using LaserCrush.Controller.InputObject;

namespace LaserCrush.Controller
{
    public class ToolbarController : MonoBehaviour
    {
        [SerializeField] private Transform m_BatchedItemTransform;
        [SerializeField] private GridLineController m_GridLineController;
        [SerializeField] private SubLineController m_SubLineController;

        private Camera m_MainCamera;
        private AcquiredItemUI m_CurrentItem;
        private GameObject m_CurrentInstallingItem;
        private GameObject m_InstantiatingObject;

        private Func<Vector3, Result> m_CheckAvailablePosFunc;
        private Action<InstalledItem, AcquiredItemUI> m_AddInstallItemAction;

        private bool m_IsInstallMode;
        private bool m_IsDragging;

        public event Func<Vector3, Result> CheckAvailablePosFunc
        {
            add => m_CheckAvailablePosFunc += value;
            remove => m_CheckAvailablePosFunc -= value;
        }

        public event Action<InstalledItem, AcquiredItemUI> AddInstalledItemAction
        {
            add => m_AddInstallItemAction += value;
            remove => m_AddInstallItemAction -= value;
        }

        private void Awake()
            => m_MainCamera = Camera.main;

        public void Init(AcquiredItemUI[] acquiredItemUI)
        {
            foreach(AcquiredItemUI acquiredItem in acquiredItemUI)
                acquiredItem.PointerDownAction += OnPointerDown;
        }

        private void OnPointerDown(AcquiredItemUI clickedItem)
        {
            if (clickedItem.HasCount <= 0) return;

            m_CurrentItem = clickedItem;
            m_CurrentInstallingItem = m_CurrentItem.ItemObject;
            m_GridLineController.OnOffGridLine(true);
            m_SubLineController.IsInitItemDrag(true);
            m_IsInstallMode = true;
        }

        private void Update()
        {
            if (!m_IsInstallMode) return;

            #if UNITY_EDITOR
            EditorOrWindow();
            #elif UNITY_ANDROID || UNITY_IOS
            AndroidOrIOS();
            #endif
        }

        private void EditorOrWindow()
        {
            bool isHit = RaycastToTouchable(out RaycastHit2D hit2D);
            if (Input.GetMouseButtonDown(0) && !m_IsDragging)
            {
                m_IsDragging = true;
                m_InstantiatingObject = Instantiate(m_CurrentInstallingItem, hit2D.point, Quaternion.identity);
                m_InstantiatingObject.transform.SetParent(m_BatchedItemTransform);
            }

            if (m_IsDragging)
            {
                if (!isHit) m_InstantiatingObject.transform.position = Vector3.zero;
                else
                {
                    Result result = (Result)(m_CheckAvailablePosFunc?.Invoke(hit2D.point));
                    if (!result.m_IsAvailable) m_InstantiatingObject.transform.position = Vector3.zero;
                    else m_InstantiatingObject.transform.position = result.m_ItemGridPos;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                //board�ٱ� || ������ ������ || ������, �� ��ġ ��ħ
                if (!isHit || !m_SubLineController.IsActiveSubLine || !BatchAcquireItem(hit2D.point))
                    Destroy(m_InstantiatingObject);

                BatchComp();
            }
        }

        private void AndroidOrIOS()
        {
            //�̿ϼ�
            bool isHit = RaycastToTouchable(out RaycastHit2D hit2D);
            if (Input.GetMouseButtonDown(0) && !m_IsDragging)
            {
                m_IsDragging = true;
                m_InstantiatingObject = Instantiate(m_CurrentInstallingItem, hit2D.point, Quaternion.identity);
                m_InstantiatingObject.transform.SetParent(m_BatchedItemTransform);
            }

            if (m_IsDragging)
            {
                m_InstantiatingObject.transform.position = hit2D.point;
            }

            if (Input.GetMouseButtonUp(0))
            {
                //board�ٱ� || ������ ������ || ������, �� ��ġ ��ħ
                if (!isHit || !m_SubLineController.IsActiveSubLine || !BatchAcquireItem(hit2D.point))
                    Destroy(m_InstantiatingObject);

                BatchComp();
            }
        }

        private bool BatchAcquireItem(Vector3 origin)
        {
            Result result = (Result)(m_CheckAvailablePosFunc?.Invoke(origin));
            if (!result.m_IsAvailable) return false;

            m_InstantiatingObject.transform.position = result.m_ItemGridPos;

            InstalledItem installedItem = m_InstantiatingObject.GetComponent<InstalledItem>();
            m_AddInstallItemAction?.Invoke(installedItem, m_CurrentItem);
            installedItem.Init(result.m_RowNumber, result.m_ColNumber, m_SubLineController.IsInitItemDrag);

            return true;
        }

        private void BatchComp()
        {
            m_IsDragging = false;
            m_IsInstallMode = false;
            m_GridLineController.OnOffGridLine(false);
            m_SubLineController.IsInitItemDrag(false);
            m_SubLineController.UpdateLineRenderer();
        }

        private bool RaycastToTouchable(out RaycastHit2D hit)
        {
            hit = Physics2D.Raycast(m_MainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, LayerManager.s_TouchableAreaLayer);
            return hit.collider != null;
        }

        private void OnDestroy()
        {
            m_AddInstallItemAction = null;
        }
    }
}
