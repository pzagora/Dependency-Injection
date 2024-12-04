using System;
using System.Collections.Generic;
using DependencyInjection.Extensions;
using UnityEngine;

namespace DependencyInjection.IL2CPP.Tests
{
    public class WeavingTest : MonoBehaviour
    {
        private class NumberManager
        {
            public IEnumerable<int> Numbers { get; }

            public NumberManager(IEnumerable<int> numbers)
            {
                Numbers = numbers;
            }
        }
        
        private void OnGUI()
        {
            var content = GetContent(); 
            GUILabel(content);
        }

        private static string GetContent()  
        {
            try
            {
                return string.Join(", ", (IEnumerable<int>) new object[] {1, 2, 3, 42}.CastDynamic(typeof(int)));
            }
            catch (Exception e)
            {
                return e.ToString(); 
            }
        }

        private static void GUILabel(string content)
        {
            var area = new Rect(0, 0, Screen.width, Screen.height);
            var style = new GUIStyle("label") {fontSize = 16, alignment = TextAnchor.MiddleCenter};
            GUI.Label(area, content, style);
        }
    }
}