using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public CombinedController Controller;
    public Grabber Grabber;
    public LoopSound WalkSound;
    public AudioSource DragSound;
    public VariationSounds PickupSound;
    public VariationSounds DropSound;

    private bool _wasGrabbing;

    public void Update()
    {
        UpdateDragSound();
        UpdateWalkSound();
        UpdatePickupAndDrop();
    }

    private void UpdateWalkSound()
    {
        var moveDelta = Controller.Velocity.magnitude;
        if (moveDelta > 1)
            WalkSound.Play();
        else
            WalkSound.Stop();
    }

    private void UpdateDragSound()
    {
        var dragDiff = 0f;
        var currentlyGrabbedObject = Grabber.CurrentlyGrabbedObject();
        if (currentlyGrabbedObject != null)
        {
            dragDiff = currentlyGrabbedObject.velocity.magnitude*0.75f;
        }
        DragSound.volume = Mathf.Min(1, dragDiff);

        if (Grabber.HoldingHeavy && !DragSound.isPlaying)
        {
            DragSound.volume = 0;
            DragSound.Play();
        }

        if (!Grabber.HoldingHeavy)
            DragSound.Stop();
    }

    private void UpdatePickupAndDrop()
    {
        if(_wasGrabbing && !Grabber.grabbing)
            DropSound.Play();
        if(!_wasGrabbing && Grabber.grabbing)
            PickupSound.Play();

        _wasGrabbing = Grabber.grabbing;
    }
}
