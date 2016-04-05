using UnityEngine;
using UnityEditor;
using System;
public class ModelImpoterSet : AssetPostprocessor
{
    public void OnPreprocessModel()
    {
        if (artsPlugins.ModelImporterAnimationTypeBool)
        {
            ModelImporter modelImporter = (ModelImporter)assetImporter;
            switch (artsPlugins.ModelImporterAnimationTypeInt)
            {
                case 0:
                    modelImporter.animationType = ModelImporterAnimationType.None;
                    break;
                case 1:
                    modelImporter.animationType = ModelImporterAnimationType.Legacy;
                    break;
                case 2:
                    modelImporter.animationType = ModelImporterAnimationType.Generic;
                    break;
                case 3:
                    modelImporter.animationType = ModelImporterAnimationType.Human;
                    break;
            }
        }
    }
    public void OnPreprocessTexture()
    {
        if (artsPlugins.MaxTextureSizeBool)
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.maxTextureSize = 128;
            switch (artsPlugins.MaxTextureSizeInt)
            {
                case 0:
                    textureImporter.maxTextureSize = 64;
                    break;
                case 1:
                    textureImporter.maxTextureSize = 128;
                    break;
                case 2:
                    textureImporter.maxTextureSize = 256;
                    break;
                case 3:
                    textureImporter.maxTextureSize = 512;
                    break;
                case 4:
                    textureImporter.maxTextureSize = 1024;
                    break;
            }
        }
    }
}
public class artsPlugins : EditorWindow
{
    #region 导入控制
    public static bool ModelImporterAnimationTypeBool = false;
    string[] ModelImporterAnimationTypeString = new string[] { "None", "Legacy", "Generic", "Human" };
    public static int ModelImporterAnimationTypeInt = 0;
    public static bool MaxTextureSizeBool = false;
    string[] MaxTextureSizeString = new string[] { "64", "128", "256", "512", "1024" };
    public static int MaxTextureSizeInt = 0;
    #endregion
    #region 贴图指认
    string[] options = new string[] { "UV0", "UV1" };
    int index = 0;
    #endregion
    #region 指认烘焙贴图
    string nameSuffix = "";
    #endregion
    [MenuItem("Window/artsPlugins")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(artsPlugins));
    }
    void OnGUI()
    {
        #region 导入控制
        GUILayout.Label("导入设置");
        #region 模型动画类型控制
        GUILayout.BeginHorizontal();
        ModelImporterAnimationTypeBool = GUILayout.Toggle(ModelImporterAnimationTypeBool, "Model Animation Type");
        ModelImporterAnimationTypeInt = EditorGUILayout.Popup(ModelImporterAnimationTypeInt, ModelImporterAnimationTypeString);
        GUILayout.EndHorizontal();
        #endregion
        #region 贴图最大尺寸控制
        GUILayout.BeginHorizontal();
        MaxTextureSizeBool = GUILayout.Toggle(MaxTextureSizeBool, "Texture Max Size");
        MaxTextureSizeInt = EditorGUILayout.Popup(MaxTextureSizeInt, MaxTextureSizeString);
        GUILayout.EndHorizontal();
        #endregion
        #endregion
        GUILayout.Space(20);
        #region 层设置
        GUILayout.Label("Layers设置");
        if (GUILayout.Button("Layers Set"))
        {
            foreach (Renderer obj in Selection.activeGameObject.GetComponentsInChildren<Renderer>())
            {
                try
                {
                    for (int i = 0; i < obj.GetComponent<Renderer>().sharedMaterials.Length; i++)
                    {
                        if (obj.GetComponent<Renderer>().sharedMaterials[i].GetFloat("_Mode") >= 3)
                        {
                            obj.gameObject.layer = 1;
                            return;
                        }
                        else
                        {
                            obj.gameObject.layer = 2;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
        }
        #endregion
        GUILayout.Space(20);
        #region 贴图指认
        GUILayout.Label("UV通道指认");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("UV Set"))
        {
            foreach (Renderer obj in Selection.activeGameObject.GetComponentsInChildren<Renderer>())
            {
                try
                {
                    for (int i = 0; i < obj.GetComponent<Renderer>().sharedMaterials.Length; i++)
                    {
                        obj.GetComponent<Renderer>().sharedMaterials[i].SetFloat("_UVSec", index);
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
        }
        index = EditorGUILayout.Popup(index, options);
        GUILayout.EndHorizontal();
        #endregion
        GUILayout.Space(20);
        #region 指认烘焙贴图
        GUILayout.Label("\r");
        GUILayout.Label("指认灯光贴图");
        nameSuffix = EditorGUILayout.TextField("贴图后缀", nameSuffix);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (nameSuffix != "")
            {
                foreach (Renderer obj in Selection.activeGameObject.GetComponentsInChildren<Renderer>())
                {
                    string P_string_thisname = obj.name + "" + nameSuffix;
                    if (obj.GetComponent<Renderer>())
                    {
                        for (int i = 0; i < obj.GetComponent<Renderer>().sharedMaterials.Length; i++)
                        {
                            obj.GetComponent<Renderer>().sharedMaterials[i].SetTexture("_DetailAlbedoMap", Resources.Load(P_string_thisname) as Texture);
                        }
                    }
                }
            }
            else
            {
                foreach (Renderer obj in Selection.activeGameObject.GetComponentsInChildren<Renderer>())
                {
                    string P_string_thisname = obj.name;
                    if (obj.GetComponent<Renderer>())
                    {
                        for (int i = 0; i < obj.GetComponent<Renderer>().sharedMaterials.Length; i++)
                        {
                            obj.GetComponent<Renderer>().sharedMaterials[i].SetTexture("_DetailAlbedoMap", Resources.Load(P_string_thisname) as Texture);
                        }
                    }
                }
            }
        }
        if (GUILayout.Button("Clear"))
        {
            foreach (Renderer obj in Selection.activeGameObject.GetComponentsInChildren<Renderer>())
            {
                if (obj.GetComponent<Renderer>())
                {
                    for (int i = 0; i < obj.GetComponent<Renderer>().sharedMaterials.Length; i++)
                    {
                        obj.GetComponent<Renderer>().sharedMaterials[i].SetTexture("_DetailAlbedoMap", null);
                    }
                }
            }
        }
        GUILayout.EndHorizontal();
        #endregion
    }
}
