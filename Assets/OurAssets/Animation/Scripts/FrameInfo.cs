using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*  This class contains info about one frame. 
 *  Such info is basically a frame ID and the closest frame in each of the animation files.*/
public class FrameInfo
{
    public int frame;   // Frame ID
    public int walking; // Closest walking frame
    public int jogging; // Closest jogging frame
    public int turnLeft;    // Closest turning left frame
    public int turnRight;   // Closest turning right frame
    public int fastLeft;    // Closest fast turning left frame
    public int fastRight;   // Closest fast turning right frame
    public int running; // Closest running frame
    public int startWalk;
    public int stop;    // Closest stopping frame

    /* Returns the correct parameter based on what kind of animation we are looking for. */
    public int GetParam(AnimationController.ANIM_TYPE type) {
        if (type == AnimationController.ANIM_TYPE.Walking) return walking;
        else if (type == AnimationController.ANIM_TYPE.Jogging) return jogging;
        else if (type == AnimationController.ANIM_TYPE.TurnLeft) return turnLeft;
        else if (type == AnimationController.ANIM_TYPE.TurnRight) return turnRight;
        else if (type == AnimationController.ANIM_TYPE.FastLeft) return fastLeft;
        else if (type == AnimationController.ANIM_TYPE.FastRight) return fastRight;
        else if (type == AnimationController.ANIM_TYPE.Running) return running;
        else if (type == AnimationController.ANIM_TYPE.StartWalk) return startWalk;
        else if (type == AnimationController.ANIM_TYPE.Stop) return stop;
        else return walking;
    }
}