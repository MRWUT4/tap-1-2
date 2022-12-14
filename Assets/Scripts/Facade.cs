using System.Collections.Generic;
using UnityEngine;


/**
 * SceneState
 */

public class SceneState : State
{
	public SceneState(object proxy) : base(proxy){}


	/**
	 * Public interface.
	 */

	override public void Enter()
	{
		Application.LoadLevel( this.id );
	}
}



/**
 * Facade
 */

public class Facade : MonoBehaviour
{
	public Proxy proxy;
	public StateMachine stateMachine;

	private Dictionary<string, StateVO> states;


	public Facade(){}

	public void Awake()
	{
		initVariables();
		initProxy();
		initStateMachine();
	}


	/** Variables */
	private void initVariables()
	{
		Application.targetFrameRate = 60;
	}


	/** Proxy setup. */
	private void initProxy()
	{
		proxy.stateMachine = stateMachine;
	}


	/** StateMachine functions. */
	private void initStateMachine()
	{
		stateMachine = new StateMachine();
		
		stateMachine.OnExit += stateMachineOnExitHandler;

		stateMachine.AddState( Names.Menu, new SceneState( Names.Game ) );
		stateMachine.AddState( Names.Game, new SceneState( Names.Game ) );
		stateMachine.AddState( Names.Result, new SceneState( Names.Result ) );

		// stateMachine.currentState = stateMachine.GetState( Names.Game );
		stateMachine.currentState = stateMachine.GetState( Application.loadedLevelName );
	}

	private void stateMachineOnExitHandler(State state, string message)
	{
		switch( state.id )
		{
			case Names.Menu:
				stateMachine.SetState( Names.Game );
				break;

			case Names.Game:
			
				switch( message )
				{
					case Game.CONTINUE:
						stateMachine.SetState( Names.Game );
						break;

					case Game.GAMEOVER:
						stateMachine.SetState( Names.Result );
						break;
				}

				break;

			case Names.Result:
				proxy.Reset();
				stateMachine.SetState( Names.Menu );
				break;
		}
	}
}