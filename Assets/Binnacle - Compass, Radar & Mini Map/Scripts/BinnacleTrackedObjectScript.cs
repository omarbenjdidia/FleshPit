using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BinnacleTrackedObjectScript : MonoBehaviour {
	
	public Image radarMarkerImage;
	
	void Start () {
		if(GameObject.Find("Omar(Clone)") || GameObject.Find("Younes(Clone)") || GameObject.Find("Rami(Clone)") || GameObject.Find("Oubeid(Clone)"))
		{
            BinnacleScript.RegisterRadarObject(this.gameObject, radarMarkerImage);

        }
		else
		{
            BinnacleScript.RegisterRadarObject(this.gameObject, radarMarkerImage);

        }
    }
	
	void OnDestroy (){
		BinnacleScript.RemoveRadarObject (this.gameObject);
	}
	
}
