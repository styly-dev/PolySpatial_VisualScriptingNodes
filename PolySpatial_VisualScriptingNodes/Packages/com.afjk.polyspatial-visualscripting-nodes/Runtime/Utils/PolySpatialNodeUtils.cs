using UnityEngine;

namespace PolySpatialVisualScripting.Utils
{
    public class PolySpatialNodeUtils : MonoBehaviour
    {
        public static void SetTappable(GameObject gameObject)
        {
            if (gameObject.GetComponent<Attr_Tappable>() == null)
            {
                gameObject.AddComponent<Attr_Tappable>();
            }
        }

        public static void SetDraggable(GameObject gameObject)
        {
            if (gameObject.GetComponent<Attr_Draggable>() == null)
            {
                gameObject.AddComponent<Attr_Draggable>();
            }
        }

        public static void InitializeEventNode()
        {
            AddPolyInputManagerToHierarchy();
        }

        private static void AddPolyInputManagerToHierarchy()
        {
            if (GameObject.Find(nameof(PolyInputManager)) == null)
            {
                GameObject polyInputManager = new GameObject(nameof(PolyInputManager));
                polyInputManager.AddComponent<PolyInputManager>();
            }
            
            if (GameObject.Find(nameof(PolySpatialDragInputManager)) == null)
            {
                GameObject polyInputManager = new GameObject(nameof(PolySpatialDragInputManager));
                polyInputManager.AddComponent<PolySpatialDragInputManager>();
            }
        }
    }
}