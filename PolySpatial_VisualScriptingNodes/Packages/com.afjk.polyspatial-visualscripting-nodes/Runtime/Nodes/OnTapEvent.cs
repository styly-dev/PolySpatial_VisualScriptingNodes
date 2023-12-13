using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace PolyspatialVisualScriptingNodes
{
    [UnitTitle("On Tap Event")]//The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Events\\Polyspatial")]//Set the path to find the node in the fuzzy finder as Events > My Events.
    public class OnTapEvent : EventUnit<SpatialPointerState>
    {
        [Serialize]
        [Inspectable, UnitHeaderInspectable("Triggered by all objects")]
        public bool triggeredByAllGameObjectFlag { get; set; } = false;

        [DoNotSerialize]
        public ValueOutput triggeredGameObject { get; private set; }

        [DoNotSerialize]
        public ValueOutput interactionPosition { get; private set; }

        protected override bool register => true;
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OnTapEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            
            if (triggeredByAllGameObjectFlag){triggeredGameObject = ValueOutput<GameObject>(nameof(triggeredGameObject));}
            interactionPosition = ValueOutput<Vector3>(nameof(interactionPosition));
        }

        // Setting the value on output ports.
        protected override void AssignArguments(Flow flow, SpatialPointerState primaryTouchData)
        {
            Debug.Log("OnTapEvent - AssignArguments");

            //flow.SetValue(triggeredGameObject, primaryTouchData.targetObject);
            //flow.SetValue(interactionPosition, primaryTouchData.interactionPosition);
        }
    }

}
