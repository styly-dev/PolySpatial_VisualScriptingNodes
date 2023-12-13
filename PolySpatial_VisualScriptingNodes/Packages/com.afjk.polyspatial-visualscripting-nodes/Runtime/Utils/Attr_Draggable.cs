using Unity.PolySpatial;
using UnityEngine;

/// <summary>
/// This script is used to make a GameObject draggable by Polyspatial
/// </summary>
namespace PolyspatialVisualScriptingNodes
{
    public class Attr_Draggable : MonoBehaviour
{
    void Start()
    {
        this.gameObject.AddComponent<PolySpatialHoverEffect>();
    }
}
}