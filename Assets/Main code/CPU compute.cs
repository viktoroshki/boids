using UnityEngine;

public class CPUcompute : MonoBehaviour
{
    // Compute shader stuff
    public ComputeShader computeShader;
    int kernelHandle;
    ComputeBuffer boidsBuffer;
    int groupSizeX;

    // Cache property IDs to avoid per-frame string hashing
    // Should reduce CPU-GPU sync overhead, according to: chatGPT? Don't fully understand it quite yet though.
    // Currently only updating deltaTime, but might as well do it for all properties incase we want to update ither stuff in the future
    static readonly int _BoidsBufferID = Shader.PropertyToID("boidsBuffer");
    static readonly int _DeltaTimeID = Shader.PropertyToID("deltaTime");
    static readonly int _BarrierXID = Shader.PropertyToID("barrierX");
    static readonly int _BarrierYID = Shader.PropertyToID("barrierY");
    static readonly int _BarrierZID = Shader.PropertyToID("barrierZ");
    static readonly int _SeparationDistanceID = Shader.PropertyToID("separationDistance");
    static readonly int _AlignmentPowerID = Shader.PropertyToID("alignmentPower");
    static readonly int _BoidReactionDistID = Shader.PropertyToID("boidReactionDist");
    static readonly int _MaxSpeedID = Shader.PropertyToID("maxSpeed");
    static readonly int _BoidNumberID = Shader.PropertyToID("boidNumber");

    void Start()
    {
        kernelHandle = computeShader.FindKernel("CSMain");

        uint x, y, z;
        computeShader.GetKernelThreadGroupSizes(kernelHandle, out x, out y, out z);
        groupSizeX = Mathf.CeilToInt(GlobalVariables.boidNumber / (float)x);

        InitShader();
    }

    void Update()
    {
        computeShader.SetFloat(_DeltaTimeID, Time.deltaTime);

        computeShader.Dispatch(this.kernelHandle, groupSizeX, 1, 1);

        boidsBuffer.GetData(GlobalVariables.boidArray); // Fetch the data from the GPU

        moveBoids();
    }
    void moveBoids()
    {
        for (int i = 0; i < GlobalVariables.boidNumber; i++)
        {
            // Set th new positions of the transforms
            GlobalVariables.transformArray[i].position = GlobalVariables.boidArray[i].position;

            // Set the boids rotation
            Quaternion offset = Quaternion.Euler(90f, 0f, 0f); // offset due to the tip of the cone being on the y-axis
            GlobalVariables.transformArray[i].rotation = Quaternion.LookRotation(GlobalVariables.boidArray[i].velocity.normalized) * offset;
        }
    }

    void InitShader()
    {
        boidsBuffer = new ComputeBuffer(GlobalVariables.boidNumber, 6 * sizeof(float));
        boidsBuffer.SetData(GlobalVariables.boidArray);

        computeShader.SetFloat("barrierX", GlobalVariables.barrierX);
        computeShader.SetFloat("barrierY", GlobalVariables.barrierY);
        computeShader.SetFloat("barrierZ", GlobalVariables.barrierZ);
        computeShader.SetFloat("separationDistance", GlobalVariables.separationDistance);
        computeShader.SetFloat("alignmentPower", GlobalVariables.alignmentPower);
        computeShader.SetFloat("boidReactionDist", GlobalVariables.boidReactionDist);
        computeShader.SetFloat("maxSpeed", GlobalVariables.maxSpeed);
        computeShader.SetInt("boidNumber", GlobalVariables.boidNumber);
        computeShader.SetBuffer(this.kernelHandle, "boidsBuffer", boidsBuffer);
    }
    void OnDisable()        // or OnDestroy
    {
        if (boidsBuffer != null)
        {
            boidsBuffer.Release();   // frees VRAM
            boidsBuffer = null;
        }
    }
}
