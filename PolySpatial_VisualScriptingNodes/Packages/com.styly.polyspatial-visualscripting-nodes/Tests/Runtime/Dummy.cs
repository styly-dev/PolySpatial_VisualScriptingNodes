using PolySpatialVisualScripting.Nodes;
using PolySpatialVisualScripting.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace PolySpatialVisualScripting.Test
{
    public class DummyTapInputProcessor : ITapInputProcessor
    {
        private GameObject target;
        private Vector3 interactionPosition;

        public DummyTapInputProcessor(GameObject target, Vector3 interactionPosition)
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
    
    
    public class DummyDragInputProcessor : IDragInputProcessor
    {
        public enum Type
        {
            Begin,
            End
        }
        private GameObject target;
        public Vector3 InteractionPosition { get; set; }
        
        private Type type;

        public DummyDragInputProcessor(GameObject target)
        {
            this.target = target;
        }

        public void SetType(Type type, Vector3 position)
        {
            this.type = type;
            InteractionPosition = position;
        }
        
        public void Process()
        {
            var primaryTouchData = new SpatialPointerState
            {
                interactionPosition = InteractionPosition,
                targetId = target.GetInstanceID()
            };
            
            switch (type)
            {
                case Type.Begin:
                    EventBus.Trigger(EventNames.OnBeginDraggingEvent, primaryTouchData);
                    break;
                case Type.End:
                    EventBus.Trigger(EventNames.OnEndDraggingEvent, primaryTouchData);
                    break;
            }
        }
    }
}