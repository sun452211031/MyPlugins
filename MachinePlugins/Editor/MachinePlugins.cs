using UnityEngine;
using UnityEditor;

public class MachinePlugins : EditorWindow{
	#region 设置材质
	bool P_bool_Foldout;
	Object P_Object_Cubmap;
	#endregion
	#region M_
	bool P_bool_M_ToggleGroup;
	Color P_Color_M_ReflectColor = new Color(0.75f,0.75f,0.75f);
	#endregion
	#region S_
	bool P_bool_S_ToggleGroup;
	Color P_Color_S_ReflectColor = new Color(0.2f,0.2f,0.3f);
	#endregion
	#region G_
	bool P_bool_G_ToggleGroup;
	Color P_Color_G_TransparentColor = new Color(1,1,1,0.2f);
	#endregion
	#region 颜色选择
	bool P_bool_Colorout;	
	#endregion
	#region 特殊功能
	bool P_bool_CombineModel;	
	Transform P_Tm_CombineParent;
	#endregion
	#region 配置环境
	bool P_bool_Environment;	
	#endregion
	[MenuItem("Window/MachinePlugins")]
	static void Init () 
	{
		EditorWindow.GetWindow (typeof (MachinePlugins));
	}
	void OnGUI () 
	{
		GUILayout.Box("Machine-Plugins",GUILayout.ExpandWidth(true));
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("反射贴图: ");
		P_Object_Cubmap = EditorGUILayout.ObjectField(P_Object_Cubmap,typeof(Cubemap), allowSceneObjects:false);
		if(GUILayout.Button("设 置"))
		{
			if(P_Object_Cubmap == null)
			{
				P_Object_Cubmap = Resources.Load("Reflection01") as Cubemap;
			}
			if(Selection.activeGameObject)
			{
				foreach(Renderer obj in Selection.activeGameObject.GetComponentsInChildren<Renderer>())
				{
					string objname = obj.name;
					if(objname.Substring(0,2) == "G_")
					{
						P_Color_G_TransparentColor = new Color(1,1,1,0.3f);
						foreach(Material objMaterial in obj.sharedMaterials)
						{
							objMaterial.shader = Shader.Find("Transparent/Diffuse");
							objMaterial.SetColor("_Color",P_Color_G_TransparentColor);
						}
					}
					if(objname.Substring(0,2) == "M_")
					{
						P_Color_M_ReflectColor = new Color(0.2f,0.2f,0.2f);
						foreach(Material objMaterial in obj.sharedMaterials)
						{
							objMaterial.shader = Shader.Find("Reflective/Diffuse");
							objMaterial.SetTexture("_Cube",P_Object_Cubmap as Cubemap);
							objMaterial.SetColor("_ReflectColor",P_Color_M_ReflectColor);
						}
					}
					if(objname.Substring(0,2) == "S_")
					{
						P_Color_S_ReflectColor.a = 0.2f;
						foreach(Material objMaterial in obj.sharedMaterials)
						{
							objMaterial.shader = Shader.Find("Reflective/Diffuse");
							objMaterial.SetTexture("_Cube",P_Object_Cubmap as Cubemap);
							objMaterial.SetColor("_ReflectColor",P_Color_S_ReflectColor);
						}
					}
				}
			}
		}
		GUILayout.EndHorizontal ();
		P_bool_Foldout = EditorGUILayout.Foldout(P_bool_Foldout, "统一修改材质");
		if(P_bool_Foldout)
		{
			P_bool_M_ToggleGroup = EditorGUILayout.BeginToggleGroup ("带漆金属材质（M_）", P_bool_M_ToggleGroup);
			P_Color_M_ReflectColor = EditorGUILayout.ColorField("ReflectColor", P_Color_M_ReflectColor);
			EditorGUILayout.EndToggleGroup ();
			P_bool_S_ToggleGroup = EditorGUILayout.BeginToggleGroup ("不锈钢材质（S_）", P_bool_S_ToggleGroup);
			P_Color_S_ReflectColor = EditorGUILayout.ColorField("ReflectColor", P_Color_S_ReflectColor);
			EditorGUILayout.EndToggleGroup ();
			P_bool_G_ToggleGroup = EditorGUILayout.BeginToggleGroup ("透明材质（G_）", P_bool_G_ToggleGroup);
			P_Color_G_TransparentColor = EditorGUILayout.ColorField("MainColor", P_Color_G_TransparentColor);
			EditorGUILayout.EndToggleGroup ();
		}
		else
		{
			P_bool_M_ToggleGroup = false;
			P_bool_S_ToggleGroup = false;
		}

		P_bool_Colorout = EditorGUILayout.Foldout(P_bool_Colorout, "颜色选择");
		if(P_bool_Colorout)
		{
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(Resources.Load("Color01")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.7f,0.7f,0.7f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color02")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.2f,0.2f,0.2f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color03")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.41f,0.42f,0.47f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color04")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.26f,0.3f,0.26f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color05")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.25f,0.21f,0.19f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color06")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.27f,0.35f,0.35f));
					}
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(Resources.Load("Color07")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.26f,0.59f,0.97f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color08")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.38f,0.84f,0.87f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color09")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.34f,0.66f,0.39f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color10")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.23f,0.38f,0.76f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color11")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.82f,0.78f,0.27f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color12")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.13f,0.26f,0.13f));
					}
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(Resources.Load("Color13")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.63f,0.68f,0.54f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color14")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.59f,0.75f,0.76f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color15")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.64f,0.47f,0.76f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color16")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.5f,0.16f,0.18f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color17")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.97f,0.6f,0.21f));
					}
				}
			}
			if(GUILayout.Button(Resources.Load("Color18")as Texture2D, GUILayout.Width(40), GUILayout.Height(40)))
			{
				if(Selection.activeGameObject && Selection.activeGameObject.renderer != null)
				{
					foreach(Material objMaterial in Selection.activeGameObject.renderer.sharedMaterials)
					{
						objMaterial.SetColor("_Color",new Color(0.13f,0.19f,0.29f));
					}
				}
			}
			GUILayout.EndHorizontal();
		}

		P_bool_CombineModel = EditorGUILayout.Foldout(P_bool_CombineModel, "特殊功能");
		if(P_bool_CombineModel)
		{
			GUILayout.BeginHorizontal ();
			GUILayout.Label("合并模型");
			if(GUILayout.Button("合 并"))
			{
				if(Selection.activeGameObject != null && Selection.activeGameObject.renderer != null)
				{
					P_Tm_CombineParent = Selection.activeGameObject.transform.parent;
					Object CopyOBJ;
					CopyOBJ = Instantiate(Selection.activeGameObject,Selection.activeGameObject.transform.localPosition,Selection.activeGameObject.transform.rotation);
					CopyOBJ.name = ""+Selection.activeGameObject.name + "(combine)";
					Selection.activeObject = CopyOBJ;
					CombineModel();	
				}
			}
			GUILayout.EndHorizontal ();
		}

		P_bool_Environment = EditorGUILayout.Foldout(P_bool_Environment, "配置环境");
		if(P_bool_Environment)
		{
			GUILayout.BeginHorizontal();
				if(GUILayout.Button(Resources.Load("Environment01UI")as Texture2D, GUILayout.Width(120), GUILayout.Height(120)))
				{
					Instantiate(Resources.Load("Environment01"));
				}
				if(GUILayout.Button(Resources.Load("Environment02UI")as Texture2D, GUILayout.Width(120), GUILayout.Height(120)))
				{
					Instantiate(Resources.Load("Environment02"));
				}
			GUILayout.EndHorizontal();
		}
	}
	void OnInspectorUpdate() 
	{
		if(Selection.activeGameObject)
		{
			if(P_bool_M_ToggleGroup == true)
			{
				foreach(Renderer obj in Selection.activeGameObject.GetComponentsInChildren<Renderer>())
				{
					string objname = obj.name;
					if(objname.Substring(0,2) == "M_")
					{
						foreach(Material objMaterial in obj.sharedMaterials)
						{
							objMaterial.SetColor("_ReflectColor",P_Color_M_ReflectColor);
						}
					}
				}
			}
			if(P_bool_S_ToggleGroup == true)
			{
				foreach(Renderer obj in Selection.activeGameObject.GetComponentsInChildren<Renderer>())
				{
					string objname = obj.name;
					if(objname.Substring(0,2) == "S_")
					{
						foreach(Material objMaterial in obj.sharedMaterials)
						{
							objMaterial.SetColor("_ReflectColor",P_Color_S_ReflectColor);
						}
					}
				}
			}
			if(P_bool_G_ToggleGroup == true)
			{
				foreach(Renderer obj in Selection.activeGameObject.GetComponentsInChildren<Renderer>())
				{
					string objname = obj.name;
					if(objname.Substring(0,2) == "G_")
					{
						foreach(Material objMaterial in obj.sharedMaterials)
						{
							objMaterial.SetColor("_Color",P_Color_G_TransparentColor);
						}
					}
				}
			}
		}
	}
	void CombineModel()
	{
		MeshFilter[] meshFilters = Selection.activeGameObject.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];    
		
		for (int i = 0; i < meshFilters.Length; i++)                            
		{
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			if(i > 0)
			{
				DestroyImmediate(meshFilters[i].gameObject);
			}
		}		
		Selection.activeGameObject.transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
		Selection.activeGameObject.transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);       
		Selection.activeGameObject.transform.gameObject.SetActive(true);
		Selection.activeGameObject.transform.localScale = new Vector3(1,1,1);
		Selection.activeGameObject.transform.localRotation = Quaternion.Euler(0,0,0);
		Selection.activeGameObject.transform.parent = P_Tm_CombineParent;
		Selection.activeGameObject.transform.localPosition = new Vector3(0,0,0);
	}
}

