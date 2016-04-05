using UnityEngine;
using System.Collections;

[AddComponentMenu("MechinePlugins/Button")]
public class Machine_Button : MonoBehaviour {
	public GameObject target;
	public string functionName;
	private UITexture thisUITexture;
	void Awake()
	{
		thisUITexture = GetComponent<UITexture> ();
	}
	void OnHover(bool isHover)
	{
		if(isHover)
		{
			thisUITexture.alpha = 0.93f;
		}
		else
		{
			thisUITexture.alpha = 1;
		}

	}
	void OnPress(bool isPress)
	{
		if(isPress)
		{
			transform.localScale = new Vector3 (0.97f,0.97f,0.97f);
		}
		else
		{
			transform.localScale = new Vector3 (1, 1, 1);
		}
		if (string.IsNullOrEmpty(functionName))
		{
			return;
		}
		if (target == null)
		{
			target = gameObject;
		}
		target.SendMessage(functionName, gameObject, SendMessageOptions.DontRequireReceiver);
	}
}
