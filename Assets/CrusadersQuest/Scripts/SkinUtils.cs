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
            /*
            List<Ik2D> iks = new List<Ik2D>();

            for (int i = 0; i < entriesProp.arraySize; i++)
            {
                SerializedProperty element = entriesProp.GetArrayElementAtIndex(i);

                Transform boneTransform = root.Find(element.FindPropertyRelative("path").stringValue);

                if (boneTransform)
                {
                    Bone2D boneComponent = boneTransform.GetComponent<Bone2D>();

                    if (boneComponent && boneComponent.attachedIK && !iks.Contains(boneComponent.attachedIK))
                    {
                        iks.Add(boneComponent.attachedIK);
                    }

                    Undo.RecordObject(boneTransform, "Load Pose");

                    boneTransform.localPosition = element.FindPropertyRelative("localPosition").vector3Value;
                    boneTransform.localRotation = element.FindPropertyRelative("localRotation").quaternionValue;
                    boneTransform.localScale = element.FindPropertyRelative("localScale").vector3Value;
                }
            }

            EditorUpdater.SetDirty("Load Pose");*/
        }
    }
}
