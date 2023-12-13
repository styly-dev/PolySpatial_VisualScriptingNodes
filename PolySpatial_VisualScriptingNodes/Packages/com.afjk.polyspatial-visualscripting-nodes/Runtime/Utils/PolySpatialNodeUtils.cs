using UnityEngine;
using Unity.VisualScripting;
using Unity.PolySpatial;
using Unity.PolySpatial.Internals;

namespace PolyspatialVisualScriptingNodes
{
    public class PolySpatialNodeUtils : MonoBehaviour
    {
        public static void SetTappable(GameObject gameObject)
        {
            gameObject.AddComponent<Attr_Tappable>();
        }

        public static void SetDraggable(GameObject gameObject)
        {
            gameObject.AddComponent<Attr_Draggable>();
        }
    }
}