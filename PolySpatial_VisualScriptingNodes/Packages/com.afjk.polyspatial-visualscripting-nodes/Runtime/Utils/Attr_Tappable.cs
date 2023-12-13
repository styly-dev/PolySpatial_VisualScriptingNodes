using System.Collections;
using System.Collections.Generic;
using Unity.PolySpatial;
using UnityEngine;

namespace PolyspatialVisualScriptingNodes
{
    public class Attr_Tappable : MonoBehaviour
    {
        void Start()
        {
        this.gameObject.AddComponent<PolySpatialHoverEffect>();
        }

        void Update()
        {

        }
    }
}