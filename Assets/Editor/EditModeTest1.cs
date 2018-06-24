using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EditModeTest1 {

	[Test]
	public void RandomCirclePosInQuad() {
		float w = 10;
		float h = 10;
		float r = 5;
		Vector2 leftDown = new Vector2(-w, -h);
		Vector2 rightUp = new Vector2(w, h);
		Vector2 result = PureLogic.RandomCirclePosInQuad(leftDown, rightUp, 5);
		float left = -(w - r);
		float top = h - r;
		float right = w - r;
		float down = -(h - r);

		Assert.IsTrue(left <= result.x && result.x <= right);
		Assert.IsTrue(down <= result.y && result.y <= top, "y = {0} down = {1} top = {2}", result.y, down, top);
	}
	
	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator EditModeTest1WithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
