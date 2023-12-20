using Unity.PolySpatial.InputDevices;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatialVisualScriptingNodes
{
    /// <summary>
    /// PolyInputManager for hundle input from Polyspatial
    /// This script will call Visual Scripting custom nodes when tap, drag, etc. events are detected
    /// </summary>
    public class PolySpatialDragInputManager : MonoBehaviour
    {
        Attr_Draggable currentSelection;
        private Pose beginPose;

        private void OnEnable()
        {
            // enable enhanced touch support to use active touches for properly pooling input phases
            EnhancedTouchSupport.Enable();
        }

        private void Update()
        {
            var activeTouches = Touch.activeTouches;

            if (activeTouches.Count > 0)
            {
                var primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);
                
                if (primaryTouchData.Kind == SpatialPointerKind.DirectPinch || primaryTouchData.Kind == SpatialPointerKind.IndirectPinch)
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
                                currentSelection.Select(true, primaryTouchData.interactionPosition);
                                EventBus.Trigger(EventNames.OnBeginDraggingEvent, primaryTouchData);
                            }
                        }
                    }

                    if (activeTouches[0].phase == TouchPhase.Moved)
                    {
                        if (currentSelection != null)
                        {
                            currentSelection.transform.SetPositionAndRotation(primaryTouchData.interactionPosition, primaryTouchData.deviceRotation * beginPose.rotation);
                        }
                    }

                    if (activeTouches[0].phase == TouchPhase.Ended || activeTouches[0].phase == TouchPhase.Canceled)
                    {
                        if (currentSelection != null)
                        {
                            currentSelection.Select(false, primaryTouchData.interactionPosition);
                            currentSelection = null;
                            EventBus.Trigger(EventNames.OnEndDraggingEvent, primaryTouchData);
                        }
                    }
                }
                else
                {
                    if (currentSelection != null)
                    {
                        currentSelection.Select(false, primaryTouchData.interactionPosition);
                        currentSelection = null;
                    }
                }
            }
        }

    }
}