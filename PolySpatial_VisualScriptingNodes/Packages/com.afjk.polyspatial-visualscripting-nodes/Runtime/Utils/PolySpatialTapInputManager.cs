using PolySpatialVisualScripting.Nodes;
using Unity.PolySpatial.InputDevices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatialVisualScripting.Utils
{
    /// <summary>
    /// PolyInputManager for handle input from PolySpatial
    /// This script will call Visual Scripting custom nodes when tap, drag, etc. events are detected
    /// </summary>
    public class PolySpatialTapInputManager : MonoBehaviour
    {
        public ITapInputProcessor InputProcessor { get; set; }
        
        void OnEnable()
        {
            InputProcessor = new PolySpatialTapInputProcessor();
        }

        void Update()
        {
            InputProcessor.CheckAndTriggerOnTapEvent();
        }
    }

    public interface ITapInputProcessor
    {
        public void CheckAndTriggerOnTapEvent();
    }

    class PolySpatialTapInputProcessor : ITapInputProcessor
    {
        public PolySpatialTapInputProcessor()
        {
            EnhancedTouchSupport.Enable();
        }
        
        public void CheckAndTriggerOnTapEvent()
        {
            var activeTouches = Touch.activeTouches;
            if (activeTouches.Count <= 0){ return;}
            
            var primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);
            if (activeTouches[0].phase != TouchPhase.Began){ return;}

            if (primaryTouchData.Kind != SpatialPointerKind.IndirectPinch &&
                primaryTouchData.Kind != SpatialPointerKind.Touch){ return;}

            Attr_Tappable attrTappable;
            if(primaryTouchData.targetObject.TryGetComponent<Attr_Tappable>(out attrTappable)){
                EventBus.Trigger(EventNames.OnTapEvent, primaryTouchData);
            }
        }
    }
    
}
