using UnityEngine;
using UnityEditor;
using System.Collections;
using Anima2D;
using System.Collections.Generic;

namespace CrusadersQuest
{
    public static class SkinUtils
    {
        public static void SavePose(Skin skin, Transform root)
        {
            List<SpriteMeshInstance> skins = new List<SpriteMeshInstance>(50);

            root.GetComponentsInChildren<SpriteMeshInstance>(true, skins);

            SerializedObject skin0s = new SerializedObject(skin);
            SerializedProperty entriesProp = skin0s.FindProperty("m_SkinEntries");
            

            skin0s.Update();
            entriesProp.arraySize = skins.Count;

            for (int i = 0; i < skins.Count; i++)
            {
                SpriteMeshInstance spriteMeshInstance = skins[i];

                if (spriteMeshInstance)
                {
                    SerializedProperty element = entriesProp.GetArrayElementAtIndex(i);
                    element.FindPropertyRelative("path").stringValue = SpriteMeshUtils.GetSpriteMeshPath(root, spriteMeshInstance);
                    element.FindPropertyRelative("skin").objectReferenceValue = spriteMeshInstance.spriteMesh;
                }
            }

            skin0s.ApplyModifiedProperties();
        }

        public static void LoadPose(Skin skin, Transform root)
        {
            SerializedObject skinSO = new SerializedObject(skin);
            SerializedProperty entriesProp = skinSO.FindProperty("m_SkinEntries");
            
            List<Skin> iks = new List<Skin>();

            for (int i = 0; i < entriesProp.arraySize; i++)
            {
                SerializedProperty element = entriesProp.GetArrayElementAtIndex(i);

                Transform skinTransform = root.Find(element.FindPropertyRelative("path").stringValue);

                if (skinTransform)
                {
                    SpriteMeshInstance skinComponent = skinTransform.GetComponent<SpriteMeshInstance>();

                    Undo.RecordObject(skinTransform, "Load Pose");
                    skinComponent.spriteMesh = element.FindPropertyRelative("skin").objectReferenceValue as SpriteMesh;
                }
            }

            //EditorUpdater.SetDirty("Load Pose");
        }
    }
}
