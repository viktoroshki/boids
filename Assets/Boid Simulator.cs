using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Boidsimulation : MonoBehaviour
{
    
    // Compute shader stuff
    public ComputeShader shader;
    int kernelHandle;
    ComputeBuffer boidsBuffer;
    int groupSizeX;
    int numOfBoids;
    RenderParams renderParams;
    GraphicsBuffer argsBuffer;

    public static Mesh boidMesh;
    public Material boidMaterial;

    void Start()
    {
        kernelHandle = shader.FindKernel("CSMain");

        uint x;
        shader.GetKernelThreadGroupSizes(kernelHandle, out x, out _, out _);
        groupSizeX = Mathf.CeilToInt(GlobalVariables.boidNumber / x);
        numOfBoids = groupSizeX * (int)x;

        InitShader();

        renderParams = new RenderParams(boidMaterial);
        renderParams.worldBounds = new Bounds(Vector3.zero, new Vector3(GlobalVariables.barrierX, GlobalVariables.barrierY, GlobalVariables.barrierZ));
    }

    void Update()
    {
        shader.SetFloat("time", Time.time);
        shader.SetFloat("deltaTime", Time.deltaTime);

        shader.Dispatch(this.kernelHandle, groupSizeX, 1, 1);

        Graphics.RenderMeshIndirect(renderParams, boidMesh, argsBuffer);
    }

    void InitShader()
    {
        boidsBuffer = new ComputeBuffer(GlobalVariables.boidNumber, 7 * sizeof(float));
        boidsBuffer.SetData(GlobalVariables.boidArray);

        // Don't quite understand this part quite yet!!!
        argsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, 1, GraphicsBuffer.IndirectDrawIndexedArgs.size);
        GraphicsBuffer.IndirectDrawIndexedArgs[] data = new GraphicsBuffer.IndirectDrawIndexedArgs[1];
        data[0].indexCountPerInstance = GlobalVariables.boidMesh.GetIndexCount(0);
        data[0].instanceCount = (uint)GlobalVariables.boidNumber;
        argsBuffer.SetData(data);

        shader.SetFloat("barrierX", GlobalVariables.barrierX);
        shader.SetFloat("barrierY", GlobalVariables.barrierY);
        shader.SetFloat("barrierZ", GlobalVariables.barrierZ);
        shader.SetFloat("seperationDistance", GlobalVariables.separationDistance);
        shader.SetFloat("alignmentPower", GlobalVariables.alignmentPower);
        shader.SetFloat("boidReactionDist", GlobalVariables.boidReactionDist);
        shader.SetFloat("maxSpeed", GlobalVariables.maxSpeed);
        shader.SetBuffer(this.kernelHandle, "boidsBuffer", boidsBuffer);

        boidMaterial.SetBuffer("boidsBuffer", boidsBuffer);
    }
}

