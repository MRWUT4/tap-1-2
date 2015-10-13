using UnityEngine;
    	
public class ScaleText : MonoBehaviour
{
	private MeshRenderer meshRenderer;
	private Vector3 size;


	/**
	 * Public interface.
	 */

	public void Update()
	{
		init();
	}


	/**
	 * Private interface.
	 */

	public void init()
	{
		if( size == default( Vector3 )  )
			initVariables();
		else
		{
			initScaling();
		}
	}


	/** Variables. */
	private void initVariables()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		size = meshRenderer.bounds.center;
	}


	/** Scaling functions. */
	private void initScaling()
	{

	}
}