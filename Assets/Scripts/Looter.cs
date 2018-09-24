using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looter : MonoBehaviour {


	public Button DecreaseButton;
	public Button Win;
	public ParticleSystem PressParticle;
	public Animation Buttonpop;
	public float MaxHealth = 10;
	public float ClickDecrease = 0.2f;

	private bool _InZone;
	private float _value = 10;

	// Use this for initialization
	void Start () {
		DecreaseButton.onClick.AddListener(() => {
			_value -= ClickDecrease;
			PressParticle.Play();
			Buttonpop.Play();
		});
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    _value -= ClickDecrease;
                    if(_value <= 0) 
                    {
                    	Win.gameObject.SetActive(true);
                    }
                }
            }
        }
	}

	private void OnTriggerEnter(Collider other){
		_InZone = true;
		DecreaseButton.gameObject.SetActive(true);

	}

	private void OnTriggerExit(Collider other) {
		_InZone = false;
		DecreaseButton.gameObject.SetActive(false);
	}

	void OnGUI() {
		if (_InZone)
		GUI.HorizontalSlider(new Rect(25, 25, 300, 130), _value, 0.0F, MaxHealth);
	}
}
