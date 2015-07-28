using UnityEngine;

public class GameState : GameObjectState
{
	public GameState(GameObject gameObject, Proxy proxy) : base(gameObject, proxy){}

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

	/** Add GameObject to parent. */
	private void initGameObject()
	{
		gameObject.transform.SetParent( (proxy as Proxy).Container.transform );
	}


	/** Add GameState modules. */
	private void initComponents()
	{
		// gameObject.AddComponent<ComponentName>();
	}
}