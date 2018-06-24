using UnityEngine;

public class PureLogic {
    public static Vector2 RandomCirclePosInQuad(Vector2 leftDown, Vector2 rightUp, float r) {
        float w = rightUp.y - leftDown.y;
        float h = rightUp.x - leftDown.x;

        float dw = w - r * 2;
        float dh = h - r * 2;
        float rx = Random.value * dw + leftDown.x + r;
        float ry = Random.value * dh + leftDown.y + r;

        return new Vector2(rx, ry);
    }
}