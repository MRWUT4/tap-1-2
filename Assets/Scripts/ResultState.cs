using UnityEngine;

public class ResultState : GameObjectState
{
	public ResultState(string id, Proxy proxy) : base(id, proxy){}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initGameObject();
		initComponents();
	}


	/**
	 * Private interface.
	 */

	/** Add ResultState modules. */
	private void initComponents()
	{
		gameObject.AddComponent<GravitateCircle>();
		gameObject.AddComponent<TweenCircle>();
	}
}