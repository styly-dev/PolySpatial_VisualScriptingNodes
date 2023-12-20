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
        public ITapInputManager InputManager { get; set; }
        
        void OnEnable()
        {
            InputManager = new PolySpatialTapInputManager();
            // enable enhanced touch support to use active touches for properly pooling input phases
        }

        void Update()
        {
            InputManager.CheckAndTriggerOnTapEvent();
        }
    }

    public interface ITapInputManager
    {
        public void CheckAndTriggerOnTapEvent();
    }

    partial class PolySpatialTapInputManager : ITapInputManager
    {
        public PolySpatialTapInputManager()
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
