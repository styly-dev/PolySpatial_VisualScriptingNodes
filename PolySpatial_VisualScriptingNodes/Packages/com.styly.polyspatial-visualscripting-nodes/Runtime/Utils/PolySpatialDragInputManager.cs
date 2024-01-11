using PolySpatialVisualScripting.Nodes;
using Unity.PolySpatial.InputDevices;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
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
    public class PolySpatialDragInputManager : MonoBehaviour
    {
        public IDragInputProcessor InputProcessor { get; set; }

        private void OnEnable()
        {
            InputProcessor = new PolySpatialDragInputProcessor();
        }

        private void Update()
        {
            InputProcessor.Process();
        }
    }

    public interface IDragInputProcessor
    {
        public void Process();
    }

    class PolySpatialDragInputProcessor : IDragInputProcessor
    {
        Attr_Draggable currentSelection;
        private Pose beginPose;

        public PolySpatialDragInputProcessor()
        {
            EnhancedTouchSupport.Enable();
        }
        
        public void Process()
        {
            var activeTouches = Touch.activeTouches;

            if (activeTouches.Count <= 0) {return;}
            var primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);

            if (primaryTouchData.Kind is SpatialPointerKind.DirectPinch or SpatialPointerKind.IndirectPinch)
            {
                var targetObject = primaryTouchData.targetObject;
                
                if (targetObject != null)
                {
                    if (targetObject.TryGetComponent(out Attr_Draggable attrDraggable))
                    {
                        if (currentSelection != attrDraggable)
                        {
                            beginPose = targetObject.transform.GetWorldPose();
                            currentSelection = attrDraggable;
                            currentSelection.Select(true, new Pose(primaryTouchData.interactionPosition,
                                primaryTouchData.deviceRotation * beginPose.rotation));
                            EventBus.Trigger(EventNames.OnBeginDraggingEvent, primaryTouchData);
                            return;
                        }
                    }
                }

                if (currentSelection == null) { return; }

                switch (activeTouches[0].phase)
                {
                    case TouchPhase.Moved:
                        currentSelection.MoveTo(new Pose(primaryTouchData.interactionPosition,
                            primaryTouchData.deviceRotation * beginPose.rotation));
                        break;
                    case TouchPhase.Ended or TouchPhase.Canceled:
                        currentSelection.Select(false, new Pose(primaryTouchData.interactionPosition,
                            primaryTouchData.deviceRotation * beginPose.rotation));
                        currentSelection = null;
                        EventBus.Trigger(EventNames.OnEndDraggingEvent, primaryTouchData);
                        break;
                }
            }
            else
            {
                if (currentSelection == null) {return;}
                currentSelection.Select(false, new Pose(primaryTouchData.interactionPosition,
                    primaryTouchData.deviceRotation * beginPose.rotation));
                currentSelection = null;
            }
        }
    }
}