using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ValPackage.Common.Editor
{
    public class FindSamePos : EditorWindow
    {
        private const string menuFolder = "Tools/ValPackage/Window/";

        [MenuItem(menuFolder + "Find Same Poses")]
        public static void ShowWindow()
        {
            //GetWindow<FindSamePos>("Find same pos");
            GetWindow(typeof(FindSamePos));
        }


        [Serializable]
        public struct Group
        {
            public string Name;
            public Vector3 Position;
            public List<Transform> _sameObjects;

            public Group(string name, Vector3 key, List<Transform> transforms) : this()
            {
                Name = name;
                Position = key;
                _sameObjects = transforms;
            }
        }

        public List<Group> _sameObjects = new();
        public bool _includeInactive;
        private Vector2 scrollPos;

        void OnGUI()
        {
            _includeInactive = GUILayout.Toggle(_includeInactive, "Include Inactive");

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Find same pos objects"))
            {
                _sameObjects.Clear();
                _sameObjects = FindSames();
            }
            if (GUILayout.Button("Clear"))
                _sameObjects.Clear();

            GUILayout.EndHorizontal();



            scrollPos = GUILayout.BeginScrollView(scrollPos);

            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty stringsProperty = so.FindProperty("_sameObjects");
            EditorGUILayout.PropertyField(stringsProperty, true);
            so.ApplyModifiedProperties();

            GUILayout.EndScrollView();
        }

        private List<Group> FindSames()
        {
            return FindObjectsOfType<Transform>(_includeInactive)
                    .GroupBy(x => x.position)
                    .Where(g => g.Count() > 1)
                    .Where(g => NotParent(g))
                    .OrderBy(g => g.Count())
                    .Select(g => new Group(g.First().name, g.Key, g.ToList()))
                    .ToList();
        }

        private bool NotParent(IGrouping<Vector3, Transform> g)
        {
            var childList = g.ToList();
            foreach (var child1 in childList)
            {
                foreach (var child2 in childList)
                {
                    if (child1 == child2)
                        continue;

                    if (child1 == child2.parent ||
                        child2 == child1.parent)
                        return false;
                }
            }

            return true;
        }
    }
}