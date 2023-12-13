using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace PolyspatialVisualScriptingNodes
{

    public class CodeTriggerCustomEvent : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            EventBus.Trigger(EventNames.MyCustomEvent, 99);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }


}