using UnityEngine;

public class GameState : GameObjectState
{
	public GameState(string id, Proxy proxy) : base(id, proxy){}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initGameObject();
		initComponents();
	}

	public override void Exit()
	{
		GameObject.Destroy( gameObject );
	}


	/**
	 * Private interface.
	 */

	/** Add GameState modules. */
	private void initComponents()
	{
		gameObject.AddComponent<ClearLevel>();
		gameObject.AddComponent<SetupCircle>();
		gameObject.AddComponent<TweenCircle>();
		gameObject.AddComponent<GravitateCircle>();
		gameObject.AddComponent<InteractionCircle>();
	}
}