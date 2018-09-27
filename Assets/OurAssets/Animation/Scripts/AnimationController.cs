using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Globalization;


/* This class contains the implementation of the first iteration of our Motion Matching System.
 * Animations were recorded with a MoCap suit in .fbx format. 
 * In each situation it selects the correct frame using KNN algorithm. 
 * The two main methods of this class are 'ChooseNextFrame(...)' and 'ChooseAnimation(...)',
 * so please take a look at them. 
 * Less important methods are at the bottom of the class.  */
public class AnimationController : MonoBehaviour
{
    /* Enum for all the animations */
    public enum ANIM_TYPE {
        Walking = 0,
        Jogging = 1,
        TurnLeft = 2,
        TurnRight = 3,
        FastLeft = 4,
        FastRight = 5,
        Running = 6,
        StartWalk = 7,
        Stop = 8
    }

    #region Attributes
    private Animator animator;  // Character animator component

    private int frames = 309; //Amount of frames in the fbx
    private float frameNumber = 3.0f; //Which frame you want to play
    private int frameCount = 0; // How many frames have been played after last frame search. Used for loop protection

    private int animOffset = 1;

    private bool playingAnim = false;   // True if the system is automatically playing frames

    //private string nextAnim = "startWalk0";  // Animation state name of the Animator Controller
    private string nextAnim = "LongTake";
    private float nextAnimFrame = 0;    // Frame within the 'nextAnim' state

    private List<FrameInfo> walking, jogging, turnLeft, turnRight, fastLeft, fastRight, running, startWalk, stop;  // Frame info of each animation file
    private ANIM_TYPE currentAnim = ANIM_TYPE.StartWalk;  // Current animation type (see enum above)
    
    #region States and Frames Lists
    private List<string> walkingStates;
    private List<string> joggingStates;
    private List<string> turnLeftStates;
    private List<string> turnRightStates;
    private List<string> fastLeftStates;
    private List<string> fastRightStates;
    private List<string> runningStates;
    private List<string> startWalkStates;
    private List<string> stopStates;

    private List<int> walkingFrames;
    private List<int> joggingFrames;
    private List<int> turnLeftFrames;
    private List<int> turnRightFrames;
    private List<int> fastLeftFrames;
    private List<int> fastRightFrames;
    private List<int> runningFrames;
    private List<int> startWalkFrames;
    private List<int> stopFrames;
    #endregion


    #region Attributes for new DB approach
    private float currentFrame;

    private float longTakeFrameCount = 3037; // TODO: Delete this and read it from file
    private string longTakeStateName = "longTake";

    public FrameDB framedb;
    private float currSpeed, nextSpeed;
    private float currRot, nextRot;
    //private FrameDB framedb;
    #endregion

    #endregion


    void Start()
    {
        // Character animator setup
        animator = GetComponent<Animator>();
        animator.speed = 0f; // This allows the system to play animations frame by frame

        // New approach
        //framedb = new FrameDB();
        //FrameDB.InitializeDB();
        //ReadLongTakeCSV();

        //framedb = (FrameDB)Resources.Load("Scripts/ScriptableObjects/FrameDB 1", typeof(FrameDB));
        // Reading knn data
        //ReadCSV();
        //SetStatesAndFrames();

        Debug.Log(framedb.GetAllFrames().Count);

    }

    void Update()
    {
        #region Old version
        /*
        if (!playingAnim)
        {
            // TODO: ¿Change animation type?
            frameNumber = ChooseNextFrame((int)frameNumber, currentAnim);   // Needs to be int for list search
            ChooseAnimation((int)frameNumber, currentAnim);     // Chooses animation and frame within such animation
            animator.Play(nextAnim, 0, nextAnimFrame / frames); //Go to frame 'frameNumber'

            playingAnim = true;
            
        }
        else
        {
            nextAnimFrame = (nextAnimFrame + 1) % frames;
            frameNumber++;
            animator.Play(nextAnim, 0, nextAnimFrame / frames); //Go to frame 'nextAnimFrame'
            frameCount++;

            if (frameCount >= 10)
            {
                playingAnim = false;
                frameCount = 0;
            }
        }*/
        #endregion

        
        if (!playingAnim)
        {
            Frame f = GetBestFrame();   // Here the best possible frame is picked and it's ready to be played
            currentFrame = f.GetIndex();    // We get the index of such frame within the long animation

            animator.Play(longTakeStateName, 0, currentFrame / longTakeFrameCount); // The selected frame is played
            
            playingAnim = true; // Sets up the flag for indicating that some frames need to be played before choosing again

        }
        else
        {
            currentFrame = (currentFrame + 1) % longTakeFrameCount; // The next frame is selected to be played
            if (currentFrame == 0) { currentFrame = 2; }    // Avoid T-Pose

            animator.Play(longTakeStateName, 0, currentFrame / longTakeFrameCount); // Frame is played


            frameCount++;   // Increment count to know when to stop playing frames
            if (frameCount >= 10)   // When 10 frames are played, it's time to choose a frame again
            {
                playingAnim = false;
                frameCount = 0;
            }
        }

        //GetInput();
    }

    #region Old version
    /* Chooses next frame among the knn of the current frame.
     * It returns the closest frame meeting these requirements:
     *   1.  It is not one of the 'anim_window' previous frames of the animation.
     *   2.  TODO: Meet some velocity criteria.
     * Otherwise it returns the next frame.
     */
    private int ChooseNextFrame(int currentFrame, ANIM_TYPE nextAnim) {
        int next = -1;

        List<FrameInfo> frames = GetFrameList(currentAnim);
        if (currentFrame < frames.Count)
            next = frames[currentFrame].GetParam(nextAnim);
        else next = frames[0].GetParam(nextAnim);


        if (next == -1)
            next = currentFrame + 1;
        
        return next;
    }
    
    /* 
     * Given a frame id and a animation type, returns the animation state from the 
     * Animator Controller state machine and the frame within such state. 
     * The result is written at variables 'nextAnim' and 'nextAnimFrame' resp.
     */
    private void ChooseAnimation(int frame, ANIM_TYPE type) {
        float frameCount = 0;
        nextAnimFrame = frame;
        int animPos = 1;
        int N = GetFrameList(type).Count;
        List<string> states = GetAnimationStates(type);
        List<int> animationFrames = GetAnimationFrameCount(type);
        
        for (int i = 0; i < states.Count; i++) {
            if (frame >= frameCount && frame < frameCount + animationFrames[i])
            {
                nextAnim = states[i];
                nextAnimFrame = frame - frameCount;  // Offset fix for avoiding T-Pose frames

                if (nextAnimFrame < 6) nextAnimFrame = 6;   // Quick fix to avoid T-Poses. TODO: fix this properly for next iteration 
                else if(nextAnimFrame > animationFrames[i] - 7) nextAnimFrame = animationFrames[i] - 15;

                frames = animationFrames[i];
                Debug.Log("Changing to anim " + nextAnim + " and frame " + nextAnimFrame);
                break;
            }
            else {
                animPos++;
                frameCount += animationFrames[i];
                nextAnimFrame -= animationFrames[i];
            }
        }
    }



    /* Reads the CSV file of the KNN mapping. */
    private void ReadCSV()
    {
        /*
        Debug.Log("Loading animation data...");
        walking = CSV_Reader.ReadFile("walkingWithAll.csv");
        //jogging = CSV_Reader.ReadFile("joggingWithAll.csv");
        turnLeft = CSV_Reader.ReadFile("turnLeftWithAll.csv");
        turnRight = CSV_Reader.ReadFile("turnRightWithAll.csv");
        fastLeft = CSV_Reader.ReadFile("fastLeftWithAll.csv");
        fastRight = CSV_Reader.ReadFile("fastRightWithAll.csv");
        //running = CSV_Reader.ReadFile("runningWithAll.csv");
        startWalk = CSV_Reader.ReadFile("startWithAll.csv");
        stop = CSV_Reader.ReadFile("stopWithAll.csv");
        Debug.Log("Animation data loaded");*/
    }
    #endregion


    

    #region Functions for new approach
    /* Chooses the best frame based on the current frame */
    private Frame GetBestFrame() {
        List<int> neigh = framedb.GetFrame((int)currentFrame).GetNeighbours();

        float bestCost = float.MaxValue;
        float currCost = -1;
        int bestFrame = -1;
        
        foreach(int n in neigh){
            currCost = Cost(n);
            if (currCost < bestCost) {
                bestCost = currCost;
                bestFrame = n;
            }
        }

        return framedb.GetFrame(bestFrame);
    }

    // TODO: Define a cost function of jumping from current frame to frame 'nextFrame'
    private float Cost(int nextFrame) {
        Frame next = framedb.GetFrame(nextFrame);

        float deltaDirection = nextRot - next.GetDirection();
        float deltaSpeed = nextSpeed - next.GetSpeed();

        float cost = deltaDirection + deltaSpeed;

        return cost;
    }

    public void UpdateParams(float rot, float speed) {
        currSpeed = nextSpeed;
        nextSpeed = speed;

        currRot = nextRot;
        nextRot = rot;
    }

    #endregion

    

    #region 'Get' methods related to animation type
    private List<FrameInfo> GetFrameList(ANIM_TYPE type)
    {
        List<FrameInfo> frames;

        if (type == ANIM_TYPE.Walking) frames = walking;
        else if (type == ANIM_TYPE.Jogging) frames = jogging;
        else if (type == ANIM_TYPE.TurnLeft) frames = turnLeft;
        else if (type == ANIM_TYPE.TurnRight) frames = turnRight;
        else if (type == ANIM_TYPE.FastLeft) frames = fastLeft;
        else if (type == ANIM_TYPE.FastRight) frames = fastRight;
        else if (type == ANIM_TYPE.Running) frames = running;
        else if (type == ANIM_TYPE.StartWalk) frames = startWalk;
        else if (type == ANIM_TYPE.Stop) frames = stop;
        else frames = walking;

        return frames;
    }

    private List<string> GetAnimationStates(ANIM_TYPE type)
    {
        List<string> states;

        if (type == ANIM_TYPE.Walking) states = walkingStates;
        else if (type == ANIM_TYPE.Jogging) states = joggingStates;
        else if (type == ANIM_TYPE.TurnLeft) states = turnLeftStates;
        else if (type == ANIM_TYPE.TurnRight) states = turnRightStates;
        else if (type == ANIM_TYPE.FastLeft) states = fastLeftStates;
        else if (type == ANIM_TYPE.FastRight) states = fastRightStates;
        else if (type == ANIM_TYPE.Running) states = runningStates;
        else if (type == ANIM_TYPE.StartWalk) states = startWalkStates;
        else if (type == ANIM_TYPE.Stop) states = stopStates;
        else states = walkingStates;

        return states;
    }

    private List<int> GetAnimationFrameCount(ANIM_TYPE type)
    {
        List<int> frameN;

        if (type == ANIM_TYPE.Walking) frameN = walkingFrames;
        else if (type == ANIM_TYPE.Jogging) frameN = joggingFrames;
        else if (type == ANIM_TYPE.TurnLeft) frameN = turnLeftFrames;
        else if (type == ANIM_TYPE.TurnRight) frameN = turnRightFrames;
        else if (type == ANIM_TYPE.FastLeft) frameN = fastLeftFrames;
        else if (type == ANIM_TYPE.FastRight) frameN = fastRightFrames;
        else if (type == ANIM_TYPE.Running) frameN = runningFrames;
        else if (type == ANIM_TYPE.StartWalk) frameN = startWalkFrames;
        else if (type == ANIM_TYPE.Stop) frameN = stopFrames;
        else frameN = walkingFrames;

        return frameN;

    }


    /* Sets the current animation based on direction and rotation received from the player controller script */
    public void SelectAnimation(Vector3 currentDirection, Vector3 target) {

        // Angle between -1 and +1
        float fAngle = Vector3.Cross(currentDirection.normalized, target.normalized).y;

        // Convert to -180 to +180 degrees
        fAngle *= 180.0f;
        /*
        Debug.Log("Choosing animation with angle difference:");
        Debug.Log(fAngle);
        */
        if (fAngle < 30 && fAngle > -30) currentAnim = ANIM_TYPE.Walking;
        else if (fAngle < -30) currentAnim = ANIM_TYPE.TurnLeft;
        else if (fAngle > 30) currentAnim = ANIM_TYPE.TurnRight;
        
        
    }

    #endregion

    #region Input (used for debug, not important)
    /* DEBUG: Takes user input. Used for testing animation transitions.*/
    /*
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentAnim = ANIM_TYPE.TurnRight;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentAnim = ANIM_TYPE.TurnLeft;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentAnim = ANIM_TYPE.Walking;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentAnim = ANIM_TYPE.Stop;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentAnim = ANIM_TYPE.Running;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            currentAnim = ANIM_TYPE.StartWalk;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            currentAnim = ANIM_TYPE.FastLeft;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentAnim = ANIM_TYPE.FastRight;
        }
    }*/
    #endregion

    #region Hardcoded animation sizes
    private void SetStatesAndFrames() {
        #region State names
        walkingStates = new List<string>();
        /*walkingStates.Add("walk0");
        walkingStates.Add("walk1");
        walkingStates.Add("walk2");
        walkingStates.Add("walk3");
        walkingStates.Add("walk4");*/
        // New
        walkingStates.Add("walk5");
        walkingStates.Add("walk6");
        walkingStates.Add("walk7");
        walkingStates.Add("walk8");

        joggingStates = new List<string>();
        joggingStates.Add("jog");

        turnLeftStates = new List<string>();
        //turnLeftStates.Add("turnLeft0");
        // New
        turnLeftStates.Add("turnLeft1");

        turnRightStates = new List<string>();
        //turnRightStates.Add("turnRight0");
        /// New
        turnRightStates.Add("turnRight1");

        fastLeftStates = new List<string>();
        //fastLeftStates.Add("fastLeft0");
        // New
        fastLeftStates.Add("180Left0");
        fastLeftStates.Add("180Left1");

        fastRightStates = new List<string>();
        //fastRightStates.Add("fastRight0");
        // New
        fastRightStates.Add("180Right0");
        fastRightStates.Add("180Right1");

        runningStates = new List<string>();
        runningStates.Add("run");


        startWalkStates = new List<string>();
        startWalkStates.Add("startWalk0");
        startWalkStates.Add("startWalk1");
        startWalkStates.Add("startWalk2");
        startWalkStates.Add("startWalk3");

        stopStates = new List<string>();
        //stopStates.Add("stop0");
        //stopStates.Add("stop1");
        // New
        stopStates.Add("stopWalk0");
        stopStates.Add("stopWalk1");
        stopStates.Add("stopWalk2");
        stopStates.Add("stopWalk3");

        #endregion

        #region Frame numbers
        walkingFrames = new List<int>();
        /*walkingFrames.Add(283);
        walkingFrames.Add(289);
        walkingFrames.Add(338);
        walkingFrames.Add(316);
        walkingFrames.Add(302);*/
        // New animations
        walkingFrames.Add(87);
        walkingFrames.Add(126);
        walkingFrames.Add(124);
        walkingFrames.Add(129);


        joggingFrames = new List<int>();
        joggingFrames.Add(211);

        turnLeftFrames = new List<int>();
        //turnLeftFrames.Add(1615);
        // New animations
        turnLeftFrames.Add(945);

        turnRightFrames = new List<int>();
        //turnRightFrames.Add(1445);
        // New animations
        turnRightFrames.Add(870);


        fastLeftFrames = new List<int>();
        //fastLeftFrames.Add(308);
        // New animations
        fastLeftFrames.Add(125);
        fastLeftFrames.Add(102);

        fastRightFrames = new List<int>();
        //fastRightFrames.Add(309);
        // New animations
        fastRightFrames.Add(102);
        fastRightFrames.Add(89);

        runningFrames = new List<int>();
        //runningFrames.Add(202);

        startWalkFrames = new List<int>();
        startWalkFrames.Add(169);
        startWalkFrames.Add(126);
        startWalkFrames.Add(135);
        startWalkFrames.Add(164);

        stopFrames = new List<int>();
        //stopFrames.Add(202);
        //stopFrames.Add(211);
        // New animations
        stopFrames.Add(144);
        stopFrames.Add(135);
        stopFrames.Add(134);
        stopFrames.Add(142);
        #endregion
    }
    #endregion
}

