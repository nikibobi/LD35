using UnityEngine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;

public class Shifter : MonoBehaviour {

    public string WeaponType = null;

    private SkeletonAnimation skeletonAnimation;
    private float speed = 5f; // default

    string RandomAttachmentForSlot(string slot)
    {
        var slotIndex = skeletonAnimation.skeleton.FindSlotIndex(slot);
        var slotAttachments = new List<Spine.Attachment>();
        skeletonAnimation.skeleton.skin.FindAttachmentsForSlot(slotIndex, slotAttachments);
        //slotAttachments.ForEach(at => Debug.Log(at.Name));
        return slotAttachments[Random.Range(0, slotAttachments.Count)].Name;
    }

    void Start () {
        this.skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.skeleton.SetColor(Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f));

        skeletonAnimation.skeleton.SetAttachment("Face", RandomAttachmentForSlot("Face"));
        if (WeaponType != null)
        {
            skeletonAnimation.skeleton.SetAttachment("Weapon", WeaponType);
        }
        this.speed = Random.Range(1f, 6f);
        this.skeletonAnimation.state.TimeScale += Mathf.Log10(speed);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            skeletonAnimation.state.SetAnimation(0, "Attack", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            skeletonAnimation.AnimationName = "Move";
            skeletonAnimation.skeleton.FlipX = false;
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            skeletonAnimation.AnimationName = "Move";
            skeletonAnimation.skeleton.FlipX = true;
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            skeletonAnimation.AnimationName = "Move";
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            skeletonAnimation.AnimationName = "Move";
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else
        {
            skeletonAnimation.AnimationName = "Idle";
        }
    }
}
