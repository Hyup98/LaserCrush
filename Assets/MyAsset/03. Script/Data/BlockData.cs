using UnityEngine;

namespace LaserCrush.Data
{
    [CreateAssetMenu(fileName = "Scriptable Data", menuName = "Scriptable/Block Data", order = int.MaxValue)]
    public class BlockData : ScriptableObject
    {
        [SerializeField] private Color m_NormalBlockColor;
        [SerializeField] private Color m_ReflectBlockColor;
        [SerializeField] private Vector2 m_InitScale;
        [SerializeField] private LayerMask m_NormalLayer;
        [SerializeField] private LayerMask m_ReflectLayer;


        public Color NormalBlockColor { get => m_NormalBlockColor; }
        public Color ReflectBlockColor { get => m_ReflectBlockColor; }
        public Vector2 InitScale { get => m_InitScale; }
        public LayerMask NormalLayer { get => m_NormalLayer; }
        public LayerMask ReflectLayer { get => m_ReflectLayer; }
    }
}
