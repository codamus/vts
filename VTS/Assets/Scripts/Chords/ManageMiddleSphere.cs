using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class ManageMiddleSphere : MonoBehaviour
{
    //collider for detection wether a space or note is at the right position
    //chord Top: order clockwise with start at this space: space -> noteLeft -> spaceLeft -> note -> spaceRight -> noteRight -> space
    //chord Bottom: order clockwise with start at this space: space -> noteRight -> spaceRight -> note -> spaceLeft -> noteLeft -> space
    public ColliderIntervalCheck spaceColliderRight;
    public ColliderIntervalCheck spaceColliderLeft;
    public ColliderNoteCheck noteCollider;
    public ColliderNoteCheck noteColliderRight;
    public ColliderNoteCheck noteColliderLeft;

    //materials for shere Color major=dur, minor=moll
    public Material majorMaterial;
    public Material minorMaterial;

    //object for showing a sphere
    private MeshRenderer meshRenderer;
    //collider for hover
    private SphereCollider sphereCollider;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    //check if the triangle is completed if yes activate sphere in the middle of the triangle and change the color
    //called everytime one of the space and note Collider collides with a suitable object
    public void check()
    {
        if (spaceColliderRight.isInterval == true && spaceColliderLeft.isInterval == true && noteCollider.isNote == true && noteColliderRight.isNote == true && noteColliderLeft.isNote == true)
        {
            updateMaterial();
            sphereCollider.enabled = true;
            meshRenderer.enabled = true;

        }
        else
        {
            sphereCollider.enabled = false;
            meshRenderer.enabled = false;
        }
    }

    //play all three note sounds of the triangle
    //called when hover in sphere
    public void playSound()
    {
        noteCollider.note.playSound();
        noteColliderRight.note.playSound();
        noteColliderLeft.note.playSound();
    }

    //update material to vanilla if chord is major and to dark purple if chord is minor
    private void updateMaterial()
    {
        Interval leftInterval = spaceColliderLeft.interval;
        Interval rightInterval = spaceColliderRight.interval;
        if(spaceColliderLeft.interval.intervalType == IntervalType.P5)
        {
            if((noteColliderLeft.note.noteNumber+7) % 12 == noteCollider.note.noteNumber)
            {
                meshRenderer.material = minorMaterial;
            }
            else
            {
                meshRenderer.material = majorMaterial;
            }

        }
        else if (spaceColliderRight.interval.intervalType == IntervalType.P5)
        {
            if((noteColliderRight.note.noteNumber + 7) % 12 == noteCollider.note.noteNumber)
            {
                meshRenderer.material = minorMaterial;
            }
            else
            {
                meshRenderer.material = majorMaterial;
            }
        }
    }
}
