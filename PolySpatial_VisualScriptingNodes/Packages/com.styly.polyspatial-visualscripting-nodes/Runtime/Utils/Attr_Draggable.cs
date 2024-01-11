using System;
using Unity.PolySpatial;
using UnityEngine;

namespace PolySpatialVisualScripting.Utils
{
    /// <summary>
    /// This script is used to make a GameObject draggable by PolySpatial
    /// </summary>
    public class Attr_Draggable : MonoBehaviour
    {
        private Rigidbody rigidbody;
        private bool preIsKinematic;
        private bool isDragging;
        private Pose moveToPose;
        
        void Start()
        {
            this.gameObject.AddComponent<PolySpatialHoverEffect>();
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Select(bool selected, Pose interactionPose)
        {
            Debug.Log($"Select! :{selected} {interactionPose}");
            isDragging = selected;
            moveToPose = interactionPose;
        }
      
        private const float duration = 0.1f;

        private void Update()
        {
            if (isDragging)
            {
                MoveTo(moveToPose);
            }
        }

        public void MoveTo(Pose pose)
        {
            moveToPose = pose;
            if (rigidbody && !rigidbody.isKinematic)
            {
                SetRigidbodyPosition(rigidbody, pose.position, duration);
                SetRigidbodyRotation(rigidbody);
            }
            else
            {
                gameObject.transform.SetPositionAndRotation(pose.position, pose.rotation);
            }
        }
        
        void SetRigidbodyPosition(Rigidbody rb, Vector3 moveTo, float duration)
        {
            var diffPos = moveTo - gameObject.transform.position;
            if (Mathf.Approximately(diffPos.sqrMagnitude, 0f))
            {
                rb.velocity = Vector3.zero;
            }
            else
            {
                rb.velocity = diffPos / duration;
            }
        }

        void SetRigidbodyRotation(Rigidbody rb)
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}