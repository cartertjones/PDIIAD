using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    public ProgressTracker tracker;

    private PostProcessVolume postProcessVolume;
    private Grain grain;
    private MotionBlur mb;

    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out grain);
        postProcessVolume.profile.TryGetSettings(out mb);

    }
    private void Update()
    {
        GrainOnOff();
    }

    public void GrainOnOff()
    {
        if (!tracker.movingForward)
        {
            grain.active = true;
            mb.active = true;
        }
        else
        {
            grain.active = false;
            mb.active = false;
        }
    }
}
