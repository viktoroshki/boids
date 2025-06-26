using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering;

public class Boidsimulation : MonoBehaviour
{
    
    // Compute shader stuff
    public ComputeShader computeShader;
    int kernelHandle;
    ComputeBuffer boidsBuffer;
    int groupSizeX;

    void Start()
    {
        kernelHandle = computeShader.FindKernel("CSMain");

        uint x;
        computeShader.GetKernelThreadGroupSizes(kernelHandle, out x, out _, out _);
        groupSizeX = Mathf.CeilToInt(GlobalVariables.boidNumber / (float)x);

        InitShader();
    }

    void Update()
    {
        computeShader.SetFloat("deltaTime", Time.deltaTime);

        computeShader.Dispatch(this.kernelHandle, groupSizeX, 1, 1);

        boidsBuffer.GetData(GlobalVariables.boidArray); // Fetch the data from the GPU

        moveBoids();
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

    void OnDisable()        // or OnDestroy
    {
        if (boidsBuffer != null)
        {
            boidsBuffer.Release();   // frees VRAM
            boidsBuffer = null;
        }
    }
}

