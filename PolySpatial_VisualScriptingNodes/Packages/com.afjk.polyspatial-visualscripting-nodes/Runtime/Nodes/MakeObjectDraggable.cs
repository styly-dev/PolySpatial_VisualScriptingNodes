using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEditor.UI;

namespace PolyspatialVisualScriptingNodes
{
    [UnitTitle("Make object draggable")]
    [UnitShortTitle("Make Draggable")]
    [UnitCategory("Polyspatial")]
    [UnitSubtitle("Game Object will be draggable by Polyspatial")]
    public class MakeObjectDraggable : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        [NullMeansSelf]
        public ValueInput gameObjectToBeDragged { get; private set; }

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput gameObjectListToBeDragged { get; private set; }

        [Serialize]
        [Inspectable, UnitHeaderInspectable("Input as GameObject List")]
        public bool inputAsGameobjectListFlag { get; set; } = false;

        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        protected override void Definition()
        {

            if (inputAsGameobjectListFlag)
            {
                gameObjectListToBeDragged = ValueInput<List<GameObject>>(nameof(gameObjectListToBeDragged), null);
            }
            else
            {
                gameObjectToBeDragged = ValueInput<GameObject>(nameof(gameObjectToBeDragged), null).NullMeansSelf();
            }

            inputTrigger = ControlInput("inputTrigger", Enter);
            outputTrigger = ControlOutput("outputTrigger");

        }

        private ControlOutput Enter(Flow flow)
        {
            if (inputAsGameobjectListFlag)
            {
                List<GameObject> gameObjectList = flow.GetValue<List<GameObject>>(gameObjectListToBeDragged);
                foreach (GameObject gameObject in gameObjectList)
                {
                    PolySpatialNodeUtils.SetDraggable(gameObject);
                }
            }
            else
            {
                GameObject gameObject = flow.GetValue<GameObject>(gameObjectToBeDragged);
                PolySpatialNodeUtils.SetDraggable(gameObject);
            }
            return outputTrigger;
        }
    }
}
