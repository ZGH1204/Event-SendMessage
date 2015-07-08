using UnityEngine;
using System.Collections;

public class TestTongXing_source : MonoBehaviour {

	private A aa = null;
	
	void Start () {
		aa = new A(10,48);
	}   
	
	void Update () {
		if(Input.GetKeyUp(KeyCode.P)){
			// 事件源，将消息发出去，注册了的观察者会接受消息，进行对应的处理
			NotificationCenter.DafaultCenter().PostNotification(this, "printShow", this); 
		}
	}
	
	public A getA(){
		return this.aa;
	}
}

public class A {
	
	public int i, j;
	
	public A(int i,int j) {
		this.i = i;
		this.j = j;
	}
}
