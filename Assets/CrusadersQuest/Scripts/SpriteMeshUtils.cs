using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Anima2D;

namespace CrusadersQuest
{
    public static class SpriteMeshUtils
    {
        public static string GetSpriteMeshPath(Transform root, SpriteMeshInstance spm)
        {
            return GetPath(root, spm.transform);
        }
        public static string GetSpriteMeshPath(Transform root, SpriteMeshAnimation spm)
        {
            return GetPath(root, spm.transform);
        }

        public static string GetPath(Transform root, Transform transform)
        {
            string path = "";

            Transform current = transform;

            if (root)
            {
                while (current && current != root)
                {
                    path = current.name + path;

                    current = current.parent;

                    if (current != root)
                    {
                        path = "/" + path;
                    }
                }

                if (!current)
                {
                    path = "";
                }
            }

            return path;
        }
    }
}
