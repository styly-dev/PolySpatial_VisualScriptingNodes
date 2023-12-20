using System.Collections;
using NUnit.Framework;
using PolySpatialVisualScripting.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace PolySpatialVisualScripting.Test
{

    public class DragNodeTest
    {
        // SceneName
        private const string SceneName = "DragTestScene";

        [UnityTest]
        public IEnumerator MakeObjectDraggableTest()
        {
            PlayModeUtility.LoadScene(SceneName);
            yield return null;

            var targetObject = GameObject.Find("TargetSingle");
            Assert.That(targetObject, Is.Not.Null);

            var draggable = targetObject.GetComponent<Attr_Draggable>();
            Assert.That(draggable, Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator MakeObjectsDraggableTest()
        {
            PlayModeUtility.LoadScene(SceneName);
            yield return null;

            var targetObjectParent = GameObject.Find("TargetMultiple");
            Assert.That(targetObjectParent, Is.Not.Null);

            foreach (Transform targetObject in targetObjectParent.transform)
            {
                Debug.Log(targetObject.gameObject.name);
                var draggable = targetObject.GetComponent<Attr_Draggable>();
                Assert.That(draggable, Is.Not.Null);
            }
        }

        [UnityTest]
        public IEnumerator DragObjectTest()
        {
            PlayModeUtility.LoadScene(SceneName);
            yield return null;

            var targetObject = GameObject.Find("TargetSingle");
            Assert.That(targetObject, Is.Not.Null);

            Assert.That(targetObject.transform.position, Is.Not.EqualTo(Vector3.zero));

            // targetObjectをDragする
            var inputManagerComponent =
                GameObject.Find("PolySpatialDragInputManager").GetComponent<PolySpatialDragInputManager>();
            var dummyInputProcessor =  new DummyDragInputProcessor(targetObject);
            inputManagerComponent.InputProcessor = dummyInputProcessor;
            dummyInputProcessor.SetType(DummyDragInputProcessor.Type.Begin, Vector3.zero);
            yield return null;

            Assert.That(targetObject.transform.position, Is.EqualTo(Vector3.zero));
            
            dummyInputProcessor.SetType(DummyDragInputProcessor.Type.End, Vector3.one);
            yield return null;

            Assert.That(targetObject.transform.position, Is.EqualTo(Vector3.one));
            
        }
        

        [UnityTest]
        public IEnumerator DragObjectsTest()
        {
            PlayModeUtility.LoadScene(SceneName);
            yield return null;

            var targetObjectParent = GameObject.Find("TargetMultiple");
            Assert.That(targetObjectParent, Is.Not.Null);

            // targetObjectをDragする
            var inputManagerComponent =
                GameObject.Find("PolySpatialDragInputManager").GetComponent<PolySpatialDragInputManager>();
            
            foreach (Transform targetObjectTransform in targetObjectParent.transform)
            {
                var targetObject = targetObjectTransform.gameObject;
                Debug.Log(targetObject.gameObject.name);
                Assert.That(targetObject.transform.position, Is.Not.EqualTo(Vector3.zero));
                var dummyInputProcessor =  new DummyDragInputProcessor(targetObject);
                
                inputManagerComponent.InputProcessor = dummyInputProcessor;
                dummyInputProcessor.SetType(DummyDragInputProcessor.Type.Begin, Vector3.zero);
                yield return null;

                Assert.That(targetObject.transform.position, Is.EqualTo(Vector3.zero));
            
                dummyInputProcessor.SetType(DummyDragInputProcessor.Type.End, Vector3.one);
                yield return null;

                Assert.That(targetObject.transform.position, Is.EqualTo(Vector3.one));
            }
        }

    }
}