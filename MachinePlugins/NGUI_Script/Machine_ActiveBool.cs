using UnityEngine;
using System.Collections;

[AddComponentMenu("MechinePlugins/ActiveBool")]
public class Machine_ActiveBool : MonoBehaviour {
	public GameObject[] target;
	void  OnClick() 
	{
	    if(target != null)
		{
			for(int i = 0;i<target.Length;i++)
			{
				if(target[i].activeSelf == true)
				{
					target[i].SetActive(false);
				}
				else
				{
					target[i].SetActive(true);
				}
			}
		}
	}
}


