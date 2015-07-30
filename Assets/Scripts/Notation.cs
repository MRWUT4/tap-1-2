using UnityEngine;

public class Notation
{
	public static NotationVO addition(int level, int index, float seed)
	{
		NotationVO notationVO = new NotationVO();

		float m = Random.value > .5 ? -1 : 1;
		float a = m * ( seed * 30 * ( index + 1 ) ) % 99;

		string aString = ( (int)a ).ToString();

		notationVO.value = a;
		notationVO.text = aString;

		return notationVO;
	}
}


public class NotationVO
{
	public string text;
	public float value;
}