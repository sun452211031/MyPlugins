using UnityEngine;
using System.Collections;

[AddComponentMenu("MechinePlugins/Popup")]
public class Machine_Popup : MonoBehaviour {
	public GameObject target;
	private bool thisbool = false;
	private TweenScale targetScale;
	void Start()
	{
		if(target)
		{
			targetScale = target.GetComponent<TweenScale>();
		}
	}
	void OnClick() 
	{
		if(targetScale)
		{
			if(thisbool == false)
			{
				targetScale.PlayForward();
				thisbool = !thisbool;
			}
			else
			{
				targetScale.PlayReverse();
				thisbool = !thisbool;
			}

		}
	}
}
