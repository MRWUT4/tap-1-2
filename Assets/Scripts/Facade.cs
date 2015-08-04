using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

public class Facade : MonoBehaviour
{
	public Proxy proxy = new Proxy();

	private StateMachine stateMachine;
	private Names names;
	private Dictionary<string, GameObject> states;


	/**
	 * Public interface.
	 */

	void Start() 
	{
		// initVariables();
		initStateMachine();
	}

	void FixedUpdate() 
	{
		stateMachine.FixedUpdate();
	}


	/** StateMachine functions. */
	private void initStateMachine()
	{
		stateMachine = new StateMachine();
		
		stateMachine.OnExit += stateMachineOnExitHandler;

		stateMachine.AddState( Names.Game, new GameState( Names.Game, proxy ) );
		stateMachine.AddState( Names.Result, new ResultState( Names.Result, proxy ) );
		stateMachine.SetState( Names.Game );
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