using PolySpatialVisualScripting.Nodes;
using PolySpatialVisualScripting.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace PolySpatialVisualScripting.Test
{
    public class DummyTapInputManager : ITapInputManager
    {
        private GameObject target;
        private Vector3 interactionPosition;

        public DummyTapInputManager(GameObject target, Vector3 interactionPosition)
        {
            this.target = target;
            this.interactionPosition = interactionPosition;
        }
        
        public void CheckAndTriggerOnTapEvent()
        {
            var primaryTouchData = new SpatialPointerState
            {
                interactionPosition = interactionPosition,
                targetId = target.GetInstanceID()
            };
            EventBus.Trigger(EventNames.OnTapEvent, primaryTouchData);
        }
    }
}