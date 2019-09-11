// A modified script from http://www.salusgames.com/2016/12/28/smooth-2d-camera-follow-in-unity3d/
// on 23 January 2018

using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    private void Update()
    {
        Vector3 newPosition = new Vector3(Target.position.x, Target.position.y+0.75f, Target.position.z);   // camera is slightly above player
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}