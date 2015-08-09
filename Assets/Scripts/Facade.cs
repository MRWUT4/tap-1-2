﻿using System.Collections.Generic;
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

public class Facade : ScriptableObject
{
	public Proxy proxy = new Proxy();
	public StateMachine stateMachine;

	private Dictionary<string, StateVO> states;


	public Facade()
	{
		initStateMachine();
		initProxy();
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

		stateMachine.AddState( Names.Game, new SceneState( Names.Game ) );
		stateMachine.AddState( Names.Result, new SceneState( Names.Result ) );

		stateMachine.currentState = stateMachine.GetState( Application.loadedLevelName );
	}

	private void stateMachineOnExitHandler(State state, string message)
	{
		switch( state.id )
		{
			case Names.Game:
			
				switch( message )
				{
					case InteractionCircle.CONTINUE:
						stateMachine.SetState( Names.Game );
						break;

					case InteractionCircle.GAMEOVER:
						stateMachine.SetState( Names.Result );
						break;
				}

				break;
		}
	}
}