using Unity.PolySpatial;
using UnityEngine;

namespace PolySpatialVisualScripting.Utils
{
    /// <summary>
    /// This script is used to make a GameObject draggable by PolySpatial
    /// </summary>
    public class Attr_Draggable : MonoBehaviour
    {
        void Start()
        {
            this.gameObject.AddComponent<PolySpatialHoverEffect>();
        }

        public void Select(bool selected, Vector3 interactionPosition)
        {
            Debug.Log($"Select! :{selected} {interactionPosition}");
            
        }
    }
}