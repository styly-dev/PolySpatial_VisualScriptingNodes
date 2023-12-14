using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace PolyspatialVisualScriptingNodes
{

    [UnitTitle("On Tap Event")]//The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Events\\Plyspatial")]//Set the path to find the node in the fuzzy finder as Events > My Events.
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

        // Add an EventHook with the name of the Event to the list of Visual Scripting Events.
        public override EventHook GetHook(GraphReference reference)
        {
            PolySpatialNodeUtils.InitializeEventNode();
            return new EventHook(EventNames.OnTapEvent);
        }

        protected override void Definition()
        {
            base.Definition();
            if (triggeredByAllGameObjectFlag){triggeredGameObject = ValueOutput<GameObject>(nameof(triggeredGameObject));}
            interactionPosition = ValueOutput<Vector3>(nameof(interactionPosition));
        }

        protected override void AssignArguments(Flow flow, SpatialPointerState data)
        {
            if (triggeredByAllGameObjectFlag){flow.SetValue(triggeredGameObject, data.targetObject);}
            flow.SetValue(interactionPosition, data.interactionPosition);
        }

        protected override bool ShouldTrigger(Flow flow, SpatialPointerState data)
        {
            flow.SetValue(interactionPosition, data.interactionPosition);
            if (triggeredByAllGameObjectFlag)
            {
                flow.SetValue(triggeredGameObject, data.targetObject);
                return true;
            }else{
                if(data.targetObject == flow.stack.self){
                    return true;
                }else{
                    return false;
                }
            }
        }


    }

}

