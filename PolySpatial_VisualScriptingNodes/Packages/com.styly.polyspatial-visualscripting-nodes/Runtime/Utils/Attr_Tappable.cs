using System.Collections;
using System.Collections.Generic;
using Unity.PolySpatial;
using UnityEngine;

namespace PolySpatialVisualScripting.Utils
{
    /// <summary>
    /// This script is used to make a GameObject Tappable by PolySpatial
    /// </summary>
    public class Attr_Tappable : MonoBehaviour
    {
        void Start()
        {
        this.gameObject.AddComponent<VisionOSHoverEffect>();
        }
    }
}