using Unity.PolySpatial.InputDevices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolyspatialVisualScriptingNodes
{
    public class PolyInputManager : MonoBehaviour
    {
        [SerializeField]
        Transform m_InputAxisTransform;

        void OnEnable()
        {
            // enable enhanced touch support to use active touches for properly pooling input phases
            EnhancedTouchSupport.Enable();
        }

        void Update()
        {
            var activeTouches = Touch.activeTouches;
            if (activeTouches.Count > 0)
            {
                var primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);
                if (activeTouches[0].phase == TouchPhase.Began)
                {
                    if (primaryTouchData.Kind == SpatialPointerKind.IndirectPinch || primaryTouchData.Kind == SpatialPointerKind.Touch)
                    {
                        if(primaryTouchData.targetObject.GetComponent<Attr_Tappable>() != null){
                            EventBus.Trigger(EventNames.OnTapEvent, primaryTouchData);
                        }

                        
                    }
                }
            }
        }





    }


}
