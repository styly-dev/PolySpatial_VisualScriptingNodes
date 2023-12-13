using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEditor.UI;

namespace PolyspatialVisualScriptingNodes
{
    [UnitTitle("Make object tappable")]
    [UnitShortTitle("Make Tappable")]
    [UnitCategory("Polyspatial")]
    [UnitSubtitle("Game Object will be tapped by Polyspatial")]
    public class MakeObjectTappable : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        [NullMeansSelf]
        public ValueInput gameObjectToBeTapped { get; private set; }

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput gameObjectListToBeTapped { get; private set; }

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
                gameObjectListToBeTapped = ValueInput<List<GameObject>>(nameof(gameObjectListToBeTapped), null);
            }
            else
            {
                gameObjectToBeTapped = ValueInput<GameObject>(nameof(gameObjectToBeTapped), null).NullMeansSelf();
            }

            inputTrigger = ControlInput("inputTrigger", Enter);
            outputTrigger = ControlOutput("outputTrigger");

        }

        private ControlOutput Enter(Flow flow)
        {
            if (inputAsGameobjectListFlag)
            {
                List<GameObject> gameObjectList = flow.GetValue<List<GameObject>>(gameObjectListToBeTapped);
                foreach (GameObject gameObject in gameObjectList)
                {
                    PolySpatialNodeUtils.SetTappable(gameObject);
                }
            }
            else
            {
                GameObject gameObject = flow.GetValue<GameObject>(gameObjectToBeTapped);
                PolySpatialNodeUtils.SetTappable(gameObject);
            }
            return outputTrigger;
        }
    }
}
