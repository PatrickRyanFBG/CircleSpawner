using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawn : MonoBehaviour {

	//obj to spawn
	public GameObject obj;
	//radius from center and offset from 0 around the circle
	public float rad, off;
	//number of objects
	public int nu;

	//private variables to check against
	private float curRad, curOff;
	private int curNu;
	private Vector3 pl;

	//list of current objects
	private List<GameObject> myObjs = new List<GameObject>();

	void Start () {
		pl = transform.position;

		CreatObjects(obj, pl, rad, off, nu);
		curRad = rad;
		curOff = off;
		curNu = nu;
	}

	//will only change if variables changes
	void Update () {
		if(rad != curRad){
			CreatObjects(obj, pl, rad, off, nu);
			curRad = rad;
		}

		if(off != curOff){
			CreatObjects(obj, pl, rad, off, nu);
			curOff = off;
		}

		if(nu != curNu){
			for(int i = myObjs.Count - 1; i >= 0 ; i--){
				GameObject.Destroy (myObjs[i]);
				myObjs.Remove (myObjs[i]);
			}

			CreatObjects(obj, pl, rad, off, nu);
			curNu = nu;
		}
	}

	//This creates and controlls the place of the objects
	private void CreatObjects(GameObject ob, Vector3 place, float radius, float offset, int num){
		if(myObjs.Count == 0 && num > 0){//Will only create new object if the number of object differs
			for(int i = 0; i < num; i++){
					GameObject objTemp =  (GameObject) Instantiate (ob, 
					                                             place + RotateVector2D(place - new Vector3(place.x, place.y + radius),((i*360/num)+offset)%360), 
					                                              Quaternion.identity);
				myObjs.Add (objTemp);
				objTemp.transform.parent = transform;
			}
		}
		else{//Will change the locaiton if offset or radius changes
			for(int i = 0; i < myObjs.Count; i++){
				myObjs[i].transform.position =  place + RotateVector2D(place - new Vector3(place.x, place.y + radius),((i*360/num)+offset)%360);
			}
		}
	}

	//function that rotates the location around the center
	private Vector3 RotateVector2D(Vector3 oldDirection, float angle){
		float newX = Mathf.Cos(angle*Mathf.Deg2Rad) * (oldDirection.x) - Mathf.Sin(angle*Mathf.Deg2Rad) * (oldDirection.y);
		float newY = Mathf.Sin(angle*Mathf.Deg2Rad) * (oldDirection.x) + Mathf.Cos(angle*Mathf.Deg2Rad) * (oldDirection.y);
		float newZ = oldDirection.z;
		return new Vector3(newX, newY, newZ);
	}
}
