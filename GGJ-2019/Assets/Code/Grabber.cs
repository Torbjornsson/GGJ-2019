using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public LayerMask Layers;
    public float GrabDistance;

    public float HoldDistance;
    public float HoldElevation;

    Joint joint;

    [ContextMenu("Grab")]
    void TryGrab()
    {
        RaycastHit hit;
        Ray r = new Ray(transform.position, transform.forward);

        Debug.DrawRay(r.origin, r.direction * GrabDistance, Color.red, 1f);

        if (Physics.Raycast(r, out hit, GrabDistance, Layers))
        {
            var g = hit.collider.GetComponentInParent<Grabbable>();
            if(g != null)
            {
                Grab(g, hit.point);
            }
        }
    }

    void Grab(Grabbable target, Vector3 point)
    {
        if (target.Heavy)
        {
            var spring = gameObject.AddComponent<SpringJoint>();
            spring.autoConfigureConnectedAnchor = false;
            spring.connectedBody = target.GetComponent<Rigidbody>();
            spring.anchor = Vector3.forward * HoldDistance + Vector3.up * HoldElevation;
            spring.connectedAnchor = target.transform.InverseTransformPoint(point);
            spring.spring = 50;
            spring.damper = 10;

            joint = spring;
        }
        else
        {
            var fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.autoConfigureConnectedAnchor = false;
            fixedJoint.connectedBody = target.GetComponent<Rigidbody>();
            fixedJoint.anchor = Vector3.forward * HoldDistance + Vector3.up * HoldElevation;
            fixedJoint.connectedAnchor = target.transform.InverseTransformPoint(point);

            joint = fixedJoint;
        }
    }

    [ContextMenu("Release")]
    void Release()
    {
        Destroy(joint);
    }
}
