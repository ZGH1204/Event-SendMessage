using UnityEngine;

public class EventDispatcher : MonoBehaviour {
	//单例模式
	public static EventDispatcher dafaultCenter = null;
	public static EventDispatcher DafaultCenter()
	{
		if (null == dafaultCenter) {
			GameObject notificationObjiect = new GameObject("Default Event Center");
			dafaultCenter = notificationObjiect.AddComponent<EventDispatcher>();
		}
		return dafaultCenter;
	}

	public delegate void EventHandler(GameObject e);  
	public event EventHandler MouseOver;

	void Update () {
		if(Input.GetKeyUp(KeyCode.P)){
			if (MouseOver != null) 
			{
				MouseOver (this.gameObject);  
			}
		}
	}

}
