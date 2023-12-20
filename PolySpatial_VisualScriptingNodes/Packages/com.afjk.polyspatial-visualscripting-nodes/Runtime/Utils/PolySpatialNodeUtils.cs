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
            if (GameObject.Find(nameof(PolySpatialTapInputManager)) == null)
            {
                GameObject polyInputManager = new GameObject(nameof(PolySpatialTapInputManager));
                polyInputManager.AddComponent<PolySpatialTapInputManager>();
            }
            
            if (GameObject.Find(nameof(PolySpatialDragInputManager)) == null)
            {
                GameObject polyInputManager = new GameObject(nameof(PolySpatialDragInputManager));
                polyInputManager.AddComponent<PolySpatialDragInputManager>();
            }
        }
    }
}