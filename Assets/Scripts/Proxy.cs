using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;


/**
 * Names
 */

public class Names
{
	public const string Game = "Game";
	public const string Result = "Result";

	public const string ButtonPlay = "ButtonPlay";
}


/**
 * LevelVO
 */

public class LevelVO
{
	public float minScale = .2f;
	public float maxScale = .4f;
	public float force = .2f;

	public List<CircleVO> circleVOList = new List<CircleVO>();
}


/**
 * StateVO
 */

[Serializable]
public struct StateVO 
{
	public string name;
}


/**
 * Proxy
 */

[Serializable]
public class Proxy
{
	public GameObject circlePrefab;
	public StateMachine stateMachine;
	public LevelVO levelVO;
	
	[NonSerialized]
	public int level = 0;
	
	public int numCircles = 3;
	public int numLevels = 10;

	public float levelTime = 10;
	public float minScale = .2f;
	public float maxScale = .4f;
	public float force = .2f;

	private float _time = float.NaN; 
	private TweenFactory _tweenFactory;
	private CircleVOFactory _circleVOFactory;


	public Proxy(){}


	public void Reset()
	{
		level = 0;
		numCircles = 3;
	}


	/**
	 * System
	 */

	public TweenFactory tweenFactory
	{
		get 
	    { 
	        _tweenFactory = _tweenFactory != null ? _tweenFactory : new TweenFactory();
	        return _tweenFactory; 
	    }
	}
	
	public CircleVOFactory circleVOFactory
	{
		get 
	    { 
	        _circleVOFactory = _circleVOFactory != null ? _circleVOFactory : new CircleVOFactory();
	        return _circleVOFactory; 
	    }
	}


	/**
	 * Game
	 */
	
	public float time
	{
		get 
	    {
	    	_time = !float.IsNaN( _time ) ? _time : levelTime;
	    	_time = Mathf.Min( levelTime, Mathf.Max( 0, _time ) );

	        return _time; 
	    }
	
	    set
	    { 
	        _time = value; 
	    }
	}

	public LevelVO GetLevelVO()
	{
	   	LevelVO vo = new LevelVO();

	   	vo.minScale = minScale;
	   	vo.maxScale = maxScale;
	   	vo.force = force;
    	vo.circleVOList = levelCircleVOList;

    	return vo;
	}
	
	public List<CircleVO> levelCircleVOList
	{
		get 
	    { 
	    	circleVOFactory.level = level;
	    	circleVOFactory.numCircles = numCircles;
	    	circleVOFactory.circlePrefab = circlePrefab;
	    	circleVOFactory.numLevels = numLevels;

	    	List<CircleVO> list = circleVOFactory.getList();

	        return list;
	    }
	}

}