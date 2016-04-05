using UnityEngine;
using UnityEditor;

public class LingCat : EditorWindow{
	#region 便捷功能
	string[] options = new string[]{"正方体","胶囊体","球体","平面"};
	int index = 0;
	Transform P_Tm_CombineParent;
	#endregion
	#region 指认烘焙贴图
	string nameSuffix = "";
	#endregion
	#region 批量种树
	bool TreeEnabled;
	float P_float_zhixian = 10.0f;
	float P_float_jiange = 1.0f;
	#endregion
	#region 统一更改亮度
	float ColorChangeCount = 0.75f;
	bool groupEnabled;
	#endregion
	#region 录制视频
	string status = "闲 置";
	string recordButton = "记 录";
	bool recording = false;
	float lastFrameTime = 0.0f;
	int capturedFrame = 0;
	#endregion
	[MenuItem("Window/LingCat")]
	static void Init () 
	{
		EditorWindow.GetWindow (typeof (LingCat));
	}
	void OnGUI () 
	{
		#region Logo
		GUILayout.Box("LingCat-Plugins - 棂 猫",GUILayout.ExpandWidth(true));
		#endregion
		#region 便捷功能
		GUILayout.Label("便捷功能");            
		GUILayout.BeginHorizontal ();//创建模型(所选物体的中心点)
		GUILayout.Label("创建模型(所选物体的中心点)");
		index = EditorGUILayout.Popup(index, options);
		if(GUILayout.Button("创 建"))
		{
			if(Selection.activeTransform)
			{
				InstantiatePrimitive();
			}
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();//合并模型(选择同材质模型父物体)
		GUILayout.Label("合并模型(选择同材质模型父物体)");
		if(GUILayout.Button("合 并"))
		{
			if(Selection.activeGameObject != null)
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
		#endregion
		
		#region 指认烘焙贴图
		GUILayout.Label("\r");
		GUILayout.Label("指认灯光贴图");
		nameSuffix =  EditorGUILayout.TextField("贴图后缀", nameSuffix);
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("指 认"))
		{
			if(nameSuffix != "")
			{
				foreach (GameObject obj in Selection.gameObjects) 
				{
					string P_string_thisname = obj.name +""+ nameSuffix;
					if(obj.renderer)
					{
						obj.renderer.sharedMaterial.SetTexture("_LightMap",Resources.Load(P_string_thisname)as Texture);
					}
				}
			}
			else
			{
				foreach (GameObject obj in Selection.gameObjects) 
				{
					string P_string_thisname = obj.name;
					if(obj.renderer)
					{
						obj.renderer.sharedMaterial.SetTexture("_LightMap",Resources.Load(P_string_thisname)as Texture);
					}
				}
			}
		}
		if(GUILayout.Button("清 空"))
		{
			foreach (GameObject obj in Selection.gameObjects) 
			{
				if(obj.renderer)
				{
					obj.renderer.sharedMaterial.SetTexture("_LightMap",null);
				}
			}
		}
		GUILayout.EndHorizontal();
		#endregion
		
		#region 批量种树
		GUILayout.Label("\r");
		GUILayout.Label("批量种树");
		TreeEnabled = EditorGUILayout.Foldout(TreeEnabled, "直线型（点击鼠标中键 和 Ctrl+S更新）");
		if(TreeEnabled)//直线型
		{
			P_float_zhixian = EditorGUILayout.FloatField("距离(米)", P_float_zhixian);
			P_float_jiange = EditorGUILayout.FloatField("间隔(米)", P_float_jiange);
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("种 植"))
			{
				if(Selection.activeGameObject != null)
				{
					EditorApplication.SaveScene();
					float P_float_jiaodu = Selection.activeGameObject.transform.eulerAngles.y;
					float P_float_hudu = P_float_jiaodu*Mathf.PI/180.0f ;
					float P_float_cos = Mathf.Cos(P_float_hudu);
					float P_float_z = P_float_jiange * P_float_cos;
					float P_float_x = Mathf.Sqrt((P_float_jiange*P_float_jiange)-(P_float_z*P_float_z));
					Vector3 P_Vector3_zhongzhi = new Vector3(Selection.activeGameObject.transform.position.x,Selection.activeGameObject.transform.position.y,Selection.activeGameObject.transform.position.z);
					if(P_float_jiaodu >= 0.0f && P_float_jiaodu <= 180.0f)
					{
						if(P_float_jiaodu != 90.0f)
						{
							for(float i = 0;i < P_float_zhixian;i += P_float_jiange)
							{
								P_Vector3_zhongzhi.z += P_float_z;
								P_Vector3_zhongzhi.x += P_float_x;
								Object CopyOBJ;
								CopyOBJ = Instantiate(Selection.activeGameObject,P_Vector3_zhongzhi,Selection.activeGameObject.transform.rotation);
								CopyOBJ.name = ""+Selection.activeGameObject.name + "(tree)";
							}
						}
						else
						{
							for(float i = 0;i < P_float_zhixian;i += P_float_jiange)
							{
								P_Vector3_zhongzhi.x += P_float_jiange;
								Object CopyOBJ;
								CopyOBJ = Instantiate(Selection.activeGameObject,P_Vector3_zhongzhi,Selection.activeGameObject.transform.rotation);
								CopyOBJ.name = ""+Selection.activeGameObject.name + "(tree)";
							}
						}
					}
					if(P_float_jiaodu > 180.0f && P_float_jiaodu < 360.0f)
					{
						if(P_float_jiaodu != 270.0f)
						{
							for(float i = 0;i < P_float_zhixian;i += P_float_jiange)
							{
								P_Vector3_zhongzhi.z += P_float_z;
								P_Vector3_zhongzhi.x -= P_float_x;
								Object CopyOBJ;
								CopyOBJ = Instantiate(Selection.activeGameObject,P_Vector3_zhongzhi,Selection.activeGameObject.transform.rotation);
								CopyOBJ.name = ""+Selection.activeGameObject.name + "(tree)";
							}
						}
						else
						{
							for(float i = 0;i < P_float_zhixian;i += P_float_jiange)
							{
								P_Vector3_zhongzhi.x -= P_float_jiange;
								Object CopyOBJ;
								CopyOBJ = Instantiate(Selection.activeGameObject,P_Vector3_zhongzhi,Selection.activeGameObject.transform.rotation);
								CopyOBJ.name = ""+Selection.activeGameObject.name + "(tree)";
							}
						}
					}
				}
			}
			if(GUILayout.Button("撤 销"))
			{
				EditorApplication.OpenScene(EditorApplication.currentScene);
			}
			GUILayout.EndHorizontal();
		}
		#endregion
		
		#region 滑动条更改所选物体颜色
		GUILayout.Label("\r");
		groupEnabled = EditorGUILayout.BeginToggleGroup ("开启实时更新", groupEnabled);
		EditorGUILayout.EndToggleGroup ();
		ColorChangeCount = EditorGUILayout.Slider ("统一更改亮度", ColorChangeCount, 0, 1);
		#endregion
		
		#region 帧动画录制
		GUILayout.Label("\r");    
		GUILayout.Label("序列帧录制（保存于工程文件下)");    
		if(GUILayout.Button(recordButton))
		{
			if(recording) 
			{
				status = "闲 置";
				recordButton = "记 录";
				recording = false;
			} 
			else 
			{ 
				capturedFrame = 0;
				recordButton = "停 止";
				recording = true;
			}
		}
		EditorGUILayout.LabelField ("录制状态: ", status);
		#endregion
	}
	
	void OnInspectorUpdate() 
	{
		if(Selection.gameObjects != null && groupEnabled == true)//统一更改亮度
		{
			foreach(GameObject i in Selection.gameObjects)
			{
				if(i.renderer != null)
				{
					i.renderer.sharedMaterial.color = new Color(ColorChangeCount,ColorChangeCount,ColorChangeCount);
				}
			}
		}
		
		if(Selection.activeGameObject != null && TreeEnabled == true && P_float_jiange >= 0.1f)//批量种树-直线型
		{
			float P_float_jiaodu = Selection.activeGameObject.transform.eulerAngles.y;
			float P_float_hudu = P_float_jiaodu*Mathf.PI/180.0f ;
			float P_float_cos = Mathf.Cos(P_float_hudu);
			float P_float_z = P_float_jiange * P_float_cos;
			float P_float_x = Mathf.Sqrt((P_float_jiange*P_float_jiange)-(P_float_z*P_float_z));
			Vector3 ydirection = Selection.activeGameObject.transform.TransformDirection(Vector3.up) * 5;
			Vector3 P_Vector3_zhongzhi = new Vector3(Selection.activeGameObject.transform.position.x,Selection.activeGameObject.transform.position.y,Selection.activeGameObject.transform.position.z);
			if(P_float_jiaodu >= 0.0f && P_float_jiaodu <= 180.0f)
			{
				if(P_float_jiaodu != 90.0f)
				{
					for(float i = 0;i < P_float_zhixian;i += P_float_jiange)
					{
						P_Vector3_zhongzhi.z += P_float_z;
						P_Vector3_zhongzhi.x += P_float_x;
						Debug.DrawRay(P_Vector3_zhongzhi,ydirection, Color.green);
					}
				}
				else
				{
					for(float i = 0;i < P_float_zhixian;i += P_float_jiange)
					{
						P_Vector3_zhongzhi.x += P_float_jiange;
						Debug.DrawRay(P_Vector3_zhongzhi,ydirection, Color.green);
					}
				}
			}
			if(P_float_jiaodu > 180.0f && P_float_jiaodu < 360.0f)
			{
				if(P_float_jiaodu != 270.0f)
				{
					for(float i = 0;i < P_float_zhixian;i += P_float_jiange)
					{
						P_Vector3_zhongzhi.z += P_float_z;
						P_Vector3_zhongzhi.x -= P_float_x;
						Debug.DrawRay(P_Vector3_zhongzhi,ydirection, Color.green);
					}
				}
				else
				{
					for(float i = 0;i < P_float_zhixian;i += P_float_jiange)
					{
						P_Vector3_zhongzhi.x -= P_float_jiange;
						Debug.DrawRay(P_Vector3_zhongzhi,ydirection, Color.green);
					}
				}
			}
		}
	}
	void Update () 
	{
		if (recording) //视频录制
		{
			if (EditorApplication.isPlaying && !EditorApplication.isPaused){
				RecordImages();
				Repaint();
			} else
				status = "等待录制";
		}
	}
	
	void RecordImages() //视频录制
	{
		if(lastFrameTime < Time.time + (1/24f)) 
		{ 
			status = "捕捉帧数" + capturedFrame;
			Application.CaptureScreenshot("vidio" + capturedFrame + ".png");
			capturedFrame++;
			lastFrameTime = Time.time;
		}
	}
	
	void InstantiatePrimitive() //创建模型(所选物体的中心点)
	{
		switch (index) {
		case 0:
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = Selection.activeTransform.position;
			break;
		case 1:
			GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			capsule.transform.position = Selection.activeTransform.position;
			break;
		case 2:
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = Selection.activeTransform.position;
			break;
		case 3:
			GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Quad);
			plane.transform.position = Selection.activeTransform.position;
			break;
		default:
			Debug.LogError("Unrecognized Option");
			break;
		}
	}
	
	void CombineModel()//合并模型
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

