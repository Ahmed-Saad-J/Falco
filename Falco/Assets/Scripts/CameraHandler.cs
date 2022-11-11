using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTrans;
    public Transform cameraTrans;
    public Transform cameraPivotTrans;
    private Transform pTransform;
    private Vector3 cameraTransPos;
    private LayerMask ignoreLayers;
    private Vector3 cameraFollowVel = Vector3.zero;

    public static CameraHandler singlton;
    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.3f;

    private float deafultPos;
    private float lookAng;
    private float pivotAng;

    public float minPivotAng = -35;
    public float maxPivotAng = 35;

    //camera colissions
    private float targetPos;
    public float cameraRad = 0.2f;
    public float cameraCollisionOff = 0.2f;
    public float minCollisionOff = 0.2f;

    private void Awake()
    {
        singlton = this;
        pTransform = transform;
        deafultPos = cameraTrans.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
    }

    public void FollowTarget(float delta)
    {
        //eases the camera transition while following the player position
        Vector3 targetPos = Vector3.SmoothDamp(pTransform.position, targetTrans.position,ref cameraFollowVel, delta / followSpeed);
        pTransform.position = targetPos;
        HandleCamCollision(delta);
    }
    public void HandleCameraRot(float delta, float mouseX,float mouseY)
    {
        lookAng += (mouseX * lookSpeed) / delta;
        pivotAng -= (mouseY * pivotSpeed) / delta;
        
        //force the camera to not go lower or higher than values minPivotAng and maxPivotAng
        pivotAng = Mathf.Clamp(pivotAng, minPivotAng, maxPivotAng);
        //set the x rotation
        Vector3 rot = Vector3.zero;
        rot.y = lookAng;
        Quaternion targetRot = Quaternion.Euler(rot);
        pTransform.rotation = targetRot;
        //set y rotation
        rot = Vector3.zero;
        rot.x = pivotAng;
        targetRot = Quaternion.Euler(rot);
        cameraPivotTrans.localRotation = targetRot;
    }

    private void HandleCamCollision(float delta)
    {
        targetPos = deafultPos;
        RaycastHit hit;
        Vector3 dir = cameraTrans.position - cameraPivotTrans.position;
        dir.Normalize();

        if(Physics.SphereCast(cameraPivotTrans.position,cameraRad,dir,out hit, Mathf.Abs(targetPos), ignoreLayers))
        {
            float distance = Vector3.Distance(cameraPivotTrans.position, hit.point);
            targetPos = -(distance - cameraCollisionOff);
        }
        if (Mathf.Abs(targetPos) < minCollisionOff)
        {
            targetPos = -minCollisionOff;
        }
        cameraTransPos.z = Mathf.Lerp(cameraTrans.localPosition.z, targetPos, delta / 0.2f);
        cameraTrans.localPosition = cameraTransPos;
    }

}
