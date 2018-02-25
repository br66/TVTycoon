using System.Collections.Generic;
using System;

public class NeuralNetwork
{
    private int[] layers;
    private float[][] nodes;
    private float[][][] weights;

    private Random rand;

    public NeuralNetwork(int[] layers)
    {
        this.layers = new int[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            this.layers[i] = layers[i];
        }

        rand = new Random(System.DateTime.Today.Millisecond); 

        CreateNodes();
        CreateWeights();
    }

    private void CreateNodes()
    {
        // new nodes
        List<float[]> nodeList = new List<float[]>();

        for (int i = 0; i < layers.Length; i++) // run through all layers
        {
            nodeList.Add(new float[layers[i]]); // using the numbers in layers[] to determine the size of the nodeList
        }
        nodes = nodeList.ToArray(); // convert list to array
    }

    // For every layer, make a new list of weights.
    // For every node, set the weights.
    private void CreateWeights()
    {
        // actual weights for one node from the previous nodes for all layers
        List <float[][]> weightList = new List<float[][]>();

        for (int i = 1; i < layers.Length; i++)
        {
            // float[] is actual weights for one node from the previous nodes for one layer
            List<float[]> layerWeightList = new List<float[]>();

            int previousLayerNodes = layers[i - 1]; // REMEMBER layers[i - 1] will return a set of numbers not just one

            for (int j = 0; j < nodes[i].Length; j++)
            {
                float[] nodeWeights = new float[previousLayerNodes]; // teh actual weights

                for (int k = 0; k < previousLayerNodes; k++)
                {
                    // set every individual weight
                    nodeWeights[k] = (float)rand.NextDouble() - 0.5f;
                }
                layerWeightList.Add(nodeWeights);
            }

            weightList.Add(layerWeightList.ToArray());
        }

        weights = weightList.ToArray();
    }
}
