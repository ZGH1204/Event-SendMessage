using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationCenter : MonoBehaviour {

	private static NotificationCenter dafaultCenter = null;
	/// <summary>
	/// 单例模式，在场景中自动造一个挂有NotificationCenter脚本的Default Notification Center游戏物体，如果手动创建了一个则不再创建
	/// </summary>
	public static NotificationCenter DafaultCenter()
	{
		if (null == dafaultCenter) {
			GameObject notificationObjiect = new GameObject("Default Notification Center");
			dafaultCenter = notificationObjiect.AddComponent<NotificationCenter>();
		}
		return dafaultCenter;
	}

	private Hashtable notifications = null;
	void Awake()
	{
		this.notifications = new Hashtable ();
	}

	/// <summary>
	/// 注册观察者
	/// </summary>
	public void AddObserver (Component observer, string name)
	{ 
		AddObserver(observer, name, null); 
	}
	public void AddObserver(Component observer, string name, Component sender)
	{
		// 对观察者的名字进行检查
		if (name == null || name == "")
		{
			Debug.Log("在AddObserver函数中指定的是空名称!.");
			return; 
		}
		// 哈希表中的值是List，new list
		if (null == this.notifications[name]) 
		{
			this.notifications[name] = new List<Component>();
		}
		// 该条消息的所有观察者，通过List将他拉出来对其操作
		List<Component> notifyList = this.notifications[name] as List<Component>;  
		// 将观察者加入到哈希表中值LIST中去，也就是注册上了
		if (!notifyList.Contains(observer))
		{
			notifyList.Add(observer);
		}
	}

	/// <summary>
	///  事件源把发消息出去
	/// </summary>
	public void PostNotification(Component aSender, string aName) 
	{
		PostNotification(aSender, aName, null);
	}
	public void PostNotification(Component aSender, string aName, object aData)
	{
		PostNotification(new Notification(aSender, aName, aData)); 
	}
	public void PostNotification(Notification aNotification)
	{
		if (aNotification.name == null || aNotification.name == "")
		{
			Debug.Log("Null name sent to PostNotification."); 
			return; 
		}
		// 该条消息的所有观察者，通过List将他拉出来对其操作
		List<Component> notifyList = this.notifications[aNotification.name] as List<Component>;
		if (null == notifyList)
		{ 
			Debug.Log("在PostNotification的通知列表中未找到: " + aNotification.name);
			return;
		}
		
		List<Component> observersToRemove = new List<Component>();
		foreach (Component observer in notifyList) {           
			if (!observer) {
				observersToRemove.Add(observer);
			} else {
				// 最终以u3d api的SendMessage函数将消息发了过去,所以只能传递一个数据实参，受SendMessage函数本身限制
				observer.SendMessage(aNotification.name, aNotification, SendMessageOptions.DontRequireReceiver);
			}
		}  
		// 清除所有无效的观察者
		foreach (Component observer in observersToRemove) {
			notifyList.Remove(observer);
		}
	}
}

/// <summary>
/// 通信类是物体发送给接受物体的一个通信类型。这个类包含发送的游戏物体(U3D的Component类对象，
/// 而不是GameObject)，通信的名字(函数名)，可选的数据实参
/// </summary>
public class Notification
{
	public Component sender;
	public string name;
	public object data;
	// 构造函数
	public Notification(Component aSender, string aName) 
	{ 
		sender = aSender;
		name = aName; 
		data = null; 
	}
	public Notification(Component aSender, string aName, object aData)
	{ 
		sender = aSender;
		name = aName; 
		data = aData; 
	}   
}
