using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PolySpatialVisualScripting.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

namespace PolySpatialVisualScripting.Test
{
    public class TapNodeTest
    {
        // SceneName
        private const string SceneName = "TapTestScene";

        [UnityTest]
        public IEnumerator MakeObjectTappableTest()
        {
            PlayModeUtility.LoadScene(SceneName);
            yield return null;

            var targetObject = GameObject.Find("TargetSingle");
            Assert.That(targetObject, Is.Not.Null);

            var tappable = targetObject.GetComponent<Attr_Tappable>();
            Assert.That(tappable, Is.Not.Null);
        }
        
        [UnityTest]
        public IEnumerator MakeObjectsTappableTest()
        {
            PlayModeUtility.LoadScene(SceneName);
            yield return null;

            var targetObjectParent = GameObject.Find("TargetMultiple");
            Assert.That(targetObjectParent, Is.Not.Null);

            foreach (Transform targetObject in targetObjectParent.transform)
            {
                Debug.Log(targetObject.gameObject.name);
                var tappable = targetObject.GetComponent<Attr_Tappable>();
                Assert.That(tappable, Is.Not.Null);
            }
        }

        [UnityTest]
        public IEnumerator TapObjectTest()
        {
            PlayModeUtility.LoadScene(SceneName);
            yield return null;

            var targetObject = GameObject.Find("TargetSingle");
            Assert.That(targetObject, Is.Not.Null);

            Assert.That(targetObject.transform.position, Is.Not.EqualTo(Vector3.zero));

            // targetObjectをTapする
            var inputManagerComponent = GameObject.Find("PolySpatialTapInputManager").GetComponent<PolySpatialTapInputManager>();
            inputManagerComponent.InputProcessor = new DummyTapInputProcessor(targetObject, Vector3.zero);
            yield return null;
            
            // Tap検出
            Assert.That(targetObject.transform.position, Is.EqualTo(Vector3.zero));
        }
    }
    
}
