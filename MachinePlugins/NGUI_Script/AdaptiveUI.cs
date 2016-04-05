using UnityEngine;
using System.Collections;
using System.Text;

[AddComponentMenu("MechinePlugins/AdaptiveUI")]
public class AdaptiveUI : MonoBehaviour {
    public enum UIDistance
	{
		up,
		down,
		left,
		right,
		center,
		center_width,
		center_height,
		scrollviewGird,
	}
	public UIDistance thisDistance = UIDistance.down;
	private float screenHeight;
	private float screenWidth;
	public float thisWidth;
	public float thisHeight;
	private float thisHeightproportion;
	private float thisWidthproportion;
	private float thisHeightAdd;
	public GameObject scrollViewAdaptive;
	private float ThisWidth
	{
		get
		{
			if(thisDistance == UIDistance.scrollviewGird)
			{
				return 0.0f;
			}
			else
			{
				return thisWidth;
			}
		}
		set
		{
			thisWidth = value;
		}
	}
	private float ThisHeight
	{
		get
		{
			if(thisDistance == UIDistance.scrollviewGird)
			{
				return 0.0f;
			}
			else
			{
				return thisHeight;
			}
		}
		set
		{
			thisHeight = value;
		}
	}
	private GameObject ScrollViewAdaptive
	{
		get
		{
			if(thisDistance == UIDistance.scrollviewGird)
			{
				return scrollViewAdaptive;
			}
			else
			{
				return null;
			}
		}
		set
		{
			scrollViewAdaptive = value;
		}
	}
	void Awake() 
	{
		screenHeight = Screen.height;
		screenWidth = Screen.width;
	    switch(thisDistance)
		{
		    case UIDistance.down : Downfunction();
			break;
		    case UIDistance.up : Upfunction();
			break;
		    case UIDistance.left : Leftfunction();
			break;
		    case UIDistance.right : Rightfunction();
			break;
		    case UIDistance.center : Centerfunction();
			break;
		    case UIDistance.center_width : CenterWidthfunction();
			break;
		    case UIDistance.center_height : CenterHeightfunction();
			break;
		    case UIDistance.scrollviewGird : scrollviewGirdfunction();
			break;
		}
	}
	void Downfunction () 
	{
		thisWidthproportion =  screenWidth / ThisWidth;
		transform.localScale = new Vector3(thisWidthproportion,thisWidthproportion,1);
		transform.localPosition = new Vector3(0, -(screenHeight/2), 0);
	}
	void Upfunction () 
	{
		thisWidthproportion =  screenWidth / ThisWidth;
		transform.localScale = new Vector3(thisWidthproportion,thisWidthproportion,1);
		transform.localPosition = new Vector3(0, (screenHeight/2), 0);
	}
	void Leftfunction () 
	{
		thisHeightproportion =  screenHeight / ThisHeight;
		transform.localScale = new Vector3(thisHeightproportion,thisHeightproportion,1);
		transform.localPosition = new Vector3(-(screenWidth/2),0,0);
	}
	void Rightfunction () 
	{
		thisHeightproportion =  screenHeight / ThisHeight;
		transform.localScale = new Vector3(thisHeightproportion,thisHeightproportion,1);
		transform.localPosition = new Vector3((screenWidth/2),0,0);
	}
	void Centerfunction()
	{
		thisWidthproportion =  screenWidth / ThisWidth;
		thisHeightproportion =  screenHeight / ThisHeight;
		transform.localScale = new Vector3(thisWidthproportion,thisHeightproportion,1);
		transform.localPosition = new Vector3(0,0,0);
	}
	void CenterWidthfunction()
	{
		thisWidthproportion =  screenWidth / ThisWidth;
		transform.localScale = new Vector3(thisWidthproportion,thisWidthproportion,1);
		transform.localPosition = new Vector3(0, 0, 0);
	}
	void CenterHeightfunction()
	{
		thisHeightproportion =  screenHeight / ThisHeight;
		transform.localScale = new Vector3(thisHeightproportion,thisHeightproportion,1);
		transform.localPosition = new Vector3(0, 0, 0);
	}
	void scrollviewGirdfunction()
	{
		if(ScrollViewAdaptive != null)
		{
			this.transform.localScale = ScrollViewAdaptive.transform.localScale;
		}
	}
}
