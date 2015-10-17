using UnityEngine;
    	
public class ScaleText : MonoBehaviour
{
	public GameObject scaleGameObject;

	new private Collider2D collider2D;

	private MeshRenderer meshRenderer;
	private Vector3 meshSize;
	private Vector3 colliderSize;
	private Vector3 scale;


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
		if( meshSize == default( Vector3 )  )
			initVariables();
		else
		if( scale == default( Vector3 ) )
		{
			initScaling();
		}
	}


	/** Variables. */
	private void initVariables()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		collider2D = scaleGameObject.GetComponent<Collider2D>();
		meshSize = meshRenderer.bounds.size;
		colliderSize = collider2D.bounds.size;
	}


	/** Scaling functions. */
	private void initScaling()
	{
		if( collider2D )
		{
			float p = 1;
			float m = .8f;

			if( meshSize.x > meshSize.y )
				p = meshSize.x / colliderSize.x;
			else
				p = meshSize.y / colliderSize.y;

			scale = new Vector3();

			scale.x = ( transform.localScale.x / p ) * m;
			scale.y = ( transform.localScale.y / p ) * m;

			transform.localScale = scale;
		}
		else
			Debug.LogWarning( "scaleGameObject has to have component of type Collider2D.");
	}
}