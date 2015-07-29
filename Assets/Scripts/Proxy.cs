using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

[Serializable]
public class Proxy : GameObjectProxy
{
	public GameObject circlePrefab;
	public LevelVO levelVO = new LevelVO();
}

public class Names
{
	public const string GameState = "GameState";
}

public class LevelVO
{
	public int numCircles = 3;
	public float minScale = .2f;
	public float maxScale = .5f;
	public float force = .1f;

	public List<GameObject> gameObjectList = new List<GameObject>();
}