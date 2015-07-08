using UnityEngine;
using System.Collections;

public class demo : MonoBehaviour {

	private GameObject s;

	void Start () {
		//单例模式。注册添加监听的函数事件
		EventDispatcher.DafaultCenter ().MouseOver += Listener;
	}

	void Listener(GameObject g) {  
		print (this.gameObject.name); 
		this.gameObject.transform.position += new Vector3 (2, 0, 0);
	} 

}
