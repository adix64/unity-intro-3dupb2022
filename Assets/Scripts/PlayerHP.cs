using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
	public Animator playerAnimator;
	Image image;
    // Start is called before the first frame update
    void Start()
	{
		image = GetComponent<Image>();

	}

	// Update is called once per frame
	void Update()
    {
		float xScale = (float)playerAnimator.GetInteger("HP") * 0.01f;
		xScale = Mathf.Clamp01(xScale);
		image.color = Color.Lerp(Color.red, Color.green, xScale);
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
		transform.localScale = new Vector3(xScale, 1, 1);
    }
}
