using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
    public double[] input;
    public double output;
}


public class Perceptron : MonoBehaviour
{

    public TrainingSet[] OrTs;
    public TrainingSet[] AndTs;
    public TrainingSet[] XorTs;
    public TrainingSet[] NandTs;



    private class PerceptronClass
    {
        public TrainingSet[] ts;
        double[] weights = { 0, 0 };
        double bias = 0;
        double totalError = 0;

        double DotProductBias(double[] v1, double[] v2)
        {
            if (v1 == null || v2 == null)
                return -1;

            if (v1.Length != v2.Length)
                return -1;

            double d = 0;
            for (int x = 0; x < v1.Length; x++)
            {
                d += v1[x] * v2[x];
            }

            d += bias;

            return d;
        }

        double CalcOutput(int i)
        {
            double dp = DotProductBias(weights, ts[i].input);
            if (dp > 0) return (1);
            return (0);
        }

        void InitialiseWeights()
        {
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = Random.Range(-1.0f, 1.0f);
            }
            bias = Random.Range(-1.0f, 1.0f);
        }

        void UpdateWeights(int j)
        {
            double error = ts[j].output - CalcOutput(j);
            totalError += Mathf.Abs((float)error);
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = weights[i] + error * ts[j].input[i];
            }
            bias += error;
        }

        public double CalcOutput(double i1, double i2)
        {
            double[] inp = new double[] { i1, i2 };
            double dp = DotProductBias(weights, inp);
            if (dp > 0) return (1);
            return (0);
        }

        public void Train(int epochs, TrainingSet[] ts)
        {
            this.ts = ts;
            InitialiseWeights();

            for (int e = 0; e < epochs; e++)
            {
                totalError = 0;
                for (int t = 0; t < ts.Length; t++)
                {
                    UpdateWeights(t);
                    Debug.Log("W1: " + (weights[0]) + " W2: " + (weights[1]) + " B: " + bias);
                }
                Debug.Log("TOTAL ERROR: " + totalError);
            }
        }
    }

    

    void Start()
    {
        int iterathions = 8;
        var orPerceptron = new PerceptronClass();
        orPerceptron.Train(iterathions, OrTs);
        var andPerceptron = new PerceptronClass();
        andPerceptron.Train(iterathions, AndTs);
        var xorPerceptron = new PerceptronClass();
        xorPerceptron.Train(iterathions, XorTs);
        var nandPerceptron = new PerceptronClass();
        nandPerceptron.Train(iterathions, NandTs);



        Debug.Log("Test 0 or 0: " + orPerceptron.CalcOutput(0, 0));
        Debug.Log("Test 0 or 1: " + orPerceptron.CalcOutput(0, 1));
        Debug.Log("Test 1 or 0: " + orPerceptron.CalcOutput(1, 0));
        Debug.Log("Test 1 or 1: " + orPerceptron.CalcOutput(1, 1));

        Debug.Log("Test 0 and 0: " + andPerceptron.CalcOutput(0, 0));
        Debug.Log("Test 0 and 1: " + andPerceptron.CalcOutput(0, 1));
        Debug.Log("Test 1 and 0: " + andPerceptron.CalcOutput(1, 0));
        Debug.Log("Test 1 and 1: " + andPerceptron.CalcOutput(1, 1));

        Debug.Log("Test 0 xor 0: " + xorPerceptron.CalcOutput(0, 0));
        Debug.Log("Test 0 xor 1: " + xorPerceptron.CalcOutput(0, 1));
        Debug.Log("Test 1 xor 0: " + xorPerceptron.CalcOutput(1, 0));
        Debug.Log("Test 1 xor 1: " + xorPerceptron.CalcOutput(1, 1));

        Debug.Log("Test 0 nand 0: " + nandPerceptron.CalcOutput(0, 0));
        Debug.Log("Test 0 nand 1: " + nandPerceptron.CalcOutput(0, 1));
        Debug.Log("Test 1 nand 0: " + nandPerceptron.CalcOutput(1, 0));
        Debug.Log("Test 1 nand 1: " + nandPerceptron.CalcOutput(1, 1));
    }

    void Update()
    {

    }
}