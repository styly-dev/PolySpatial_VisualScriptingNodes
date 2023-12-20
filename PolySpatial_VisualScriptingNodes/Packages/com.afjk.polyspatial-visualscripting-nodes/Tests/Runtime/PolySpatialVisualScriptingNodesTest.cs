using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PolySpatialVisualScripting.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace PolySpatialVisualScripting.Test
{
    public class PolySpatialVisualScriptingNodesTest
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

    }
    
}
