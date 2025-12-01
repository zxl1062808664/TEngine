namespace GameTool
{
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using UnityEngine.UI;

    public static class CommonHelper
    {
        public static T FindChildDeep<T>(this Transform self, string name) where T : Object
        {
            Transform findDeep = null;
            FindDeep(self, name, ref findDeep);
            if (findDeep == null)
            {
                Debug.Log("未找到此组件");
            }

            var component = findDeep.GetComponent<T>();
            return component;
        }

        static void FindDeep(Transform tran, string name, ref Transform transform)
        {
            if (tran.name == name)
            {
                transform = tran;
                return;
            }

            for (var i = 0; i < tran.childCount; i++)
            {
                FindDeep(tran.GetChild(i), name, ref transform);
            }
        }

        public static List<T> FindChildDeeps<T>(this Transform self) where T : Object
        {
            List<T> list = new List<T>();
            FindDeeps(self, ref list);
            if (list.Count <= 0)
            {
                Debug.Log("未找到此组件");
            }

            return list;
        }

        public static List<T> TryFindChildDeeps<T>(this Transform self) where T : Object
        {
            List<T> list = new List<T>();
            FindDeeps(self, ref list);
            return list;
        }

        static void FindDeeps<T>(Transform tran, ref List<T> list)
        {
            var component = tran.GetComponent<T>();
            if (component != null)
                list.Add(component);

            for (var i = 0; i < tran.childCount; i++)
            {
                FindDeeps(tran.GetChild(i), ref list);
            }
        }

        /// <summary>
        /// 判断是否在某个物体范围内（范围型判断，超出返回false）
        /// 基于MeshRender的实现方式
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsInRangeAutoFix(Transform tran, Transform self)
        {
            var meshRenderer = tran.GetComponent<MeshRenderer>();

            Bounds rendererBounds = self.GetComponent<MeshRenderer>().bounds;
            Bounds colliderBounds = meshRenderer.bounds;
            bool rendererIsInsideBox = colliderBounds.Intersects(rendererBounds);

            return rendererIsInsideBox;
        }

        /// <summary>
        /// 复制Citizen内容到Build内
        /// </summary>
        /// <param name="path"></param>
        public static void CopyCitizenToBuild(string path)
        {
            string citizenPath = "Assets/Citizen";
            var files = Directory.GetFiles(citizenPath);
            foreach (var file in files)
            {
                File.Copy(file, Path.Combine(path, Path.GetFileName(file)));
            }

            Debug.Log("copy finish");
        }

        public static void RebuildLayout(this RectTransform rectTransform)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }
}