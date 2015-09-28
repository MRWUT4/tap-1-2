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
	public const string Menu = "Menu";
	public const string Game = "Game";
	public const string Result = "Result";

	public const string Circle = "Circle";
	public const string ProgressBar = "ProgressBar";
	public const string Camera = "Camera";

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

	public Color colorBackground;
	public Color colorCircle;

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

	public float levelTime = 30;
	public float minScale = .2f;
	public float maxScale = .4f;
	public float force = .2f;

	private float _time = float.NaN; 
	private TweenFactory _tweenFactory;
	private CircleVOFactory _circleVOFactory;
	private Color _colorBackground;	
	private Color _colorCircle;	

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


	/** Color handling. */
	public Color randomColor
	{
		get 
	    { 
	        return new Color( 
	        	UnityEngine.Random.value, 
	        	UnityEngine.Random.value, 
	        	UnityEngine.Random.value, 
	        	1
	        ); 
	    }
	}

	public Color colorBackground
	{
		get 
	    { 
	    	_colorBackground = _colorBackground != default( Color ) ? _colorBackground : randomColor;
	        return _colorBackground; 
	    }
	    set
	    {
	    	_colorBackground = value;
	    }
	}

	public Color colorCircle
	{
		get 
	    { 
	    	_colorCircle = _colorCircle != default( Color ) ? _colorCircle : randomColor;
	        return _colorCircle; 
	    }
	    set
	    {
	    	_colorCircle = value;
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
    	vo.colorBackground = colorBackground;
    	vo.colorCircle = colorCircle;

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
	    	// circleVOFactory.colorCircle = colorCircle;
	    	// circleVOFactory.colorBackground = colorBackground;

	    	List<CircleVO> list = circleVOFactory.getList();

	        return list;
	    }
	}

}