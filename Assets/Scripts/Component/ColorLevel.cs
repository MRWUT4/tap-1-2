using UnityEngine;
using System.Collections.Generic;

using Image = UnityEngine.UI.Image;

public class ColorLevel : MonoBehaviour
{
    private Setup setup;
    private Proxy proxy;
    private LevelVO levelVO;
    private GameObject progressBar;
    private GameObject cameraGameObject;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initColorSwap();
        initCircleColor();
        initProgressBarColor();
        initBackground();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        setup = gameObject.GetComponent<Setup>();
        proxy = setup.proxy as Proxy;
       	levelVO = proxy.levelVO;
       	progressBar = GameObject.Find( Names.ProgressBar );
       	cameraGameObject = GameObject.Find( Names.Camera );
    }

    private void initColorSwap()
    {
    	proxy.colorBackground = proxy.colorCircle;
    	proxy.colorCircle = default( Color );
    }

    private void initCircleColor()
    {
    	List<CircleVO> list = levelVO.circleVOList;

    	for( int i = 0; i < list.Count; ++i )
    	{
    	    CircleVO circleVO = list[ i ];
    	    GameObject gameObject = circleVO.gameObject;

    	    SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    	    spriteRenderer.color = levelVO.colorCircle;

		    TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();
		    textMesh.color = levelVO.colorBackground;
    	}
    }

    private void initProgressBarColor()
    {
       	Image image = progressBar.GetComponent<Image>();
		image.color = levelVO.colorCircle;    	
    }

    private void initBackground()
    {
    	Camera camera = cameraGameObject.GetComponent<Camera>();
    	camera.backgroundColor = levelVO.colorBackground;
    }
}