using UnityEngine;
using System.Collections;

[AddComponentMenu("MechinePlugins/Drag")]
public class Machine_Drag : MonoBehaviour {
	public float speed = 1.0f;
	public GameObject Drag;
	public GameObject target;
	public string functionName;
	public Transform Up;
	public Transform Down;
	void OnDrag() 
	{
		float translation = speed*Time.deltaTime * Input.GetAxis ("Mouse Y");
		Drag.transform.Translate(0, translation, 0);
		if(Drag.transform.localPosition.y < Down.localPosition.y)
		{
			Drag.transform.localPosition = new Vector3(Drag.transform.localPosition.x,Down.localPosition.y,Drag.transform.localPosition.z);
		}
		if(Drag.transform.localPosition.y > Up.localPosition.y)
		{
				Drag.transform.localPosition = new Vector3(Drag.transform.localPosition.x, Up.localPosition.y,Drag.transform.localPosition.z);
		}

	}
	void OnClick()
	{
		if (target == null)
		{
			target = gameObject;
		}
		target.SendMessage(functionName, gameObject, SendMessageOptions.DontRequireReceiver);
	}
}
