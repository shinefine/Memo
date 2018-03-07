using System;
using System.Collections.Generic;
using System.Text;

namespace GestureStudy
{
    //��Ԫ
    public class Neuron
    {
        #region "����"

        //��Ԫ������
        public int NumInputs;

        //Ȩֵ����
        public List<double> Weights;

        //ǰһ����Ȩֵ��������
        public List<double> PrevUpdate;

        //��Ծֵ
        public double Activation;

        //����ֵ
        public double Error;

        #endregion

        #region "����"
        
        //���캯��
        public Neuron(int _NumInputs)
        {
            NumInputs = _NumInputs + 1;
            Activation = 0;
            Error = 0;
            Weights = new List<double>();
            PrevUpdate = new List<double>();

            //�������Ȩ��
            for (int i = 0; i < NumInputs; i++)
            {
                Weights.Add(Useful.RandomClamped());
                PrevUpdate.Add(0.0);
            }
        }

        #endregion
    }

    //�������
    public class NeuronLayer
    {
        #region "����"

	    //������Ԫ��
        public int NumNeurons;

	    //��Ԫ
        public List<Neuron> Neurons;

        #endregion

        #region "����"

        //���캯��
        public NeuronLayer(int _NumNeurons, int _NumInputsPerNeuron)
        {
            NumNeurons = _NumNeurons;
            Neurons = new List<Neuron>();

            for (int i = 0; i < NumNeurons; i++)
            {
                Neurons.Add(new Neuron(_NumInputsPerNeuron));
            }
        }

        #endregion
    }

    //NeuralNet�ඨ����������
    public class NeuralNet
    {
        #region "����"

        //������
        private int NumInputs;

        //�����
        private int NumOutputs;

        //��������
        private int NumHiddenLayers;

        //ÿ�����������Ԫ��
        private int NeuronsPerHiddenLyr;

        //ѧϰ��
        private double LearningRate;

        //���۴���
        public double ErrorSum;

        //�Ƿ񾭹���ѵ��
        public bool Trained;

        //������
        public int NumEpochs;

        //������ĸ�����
        private List<NeuronLayer> Layers;

        //���巢����Ϣ��ί��
        public delegate void DelegateOfSendMessage(int Epochs, double Error);
        public event DelegateOfSendMessage SendMessage;

        #endregion

        #region "����"

        //ѵ��������ĵ���
        private bool NetworkTrainingEpoch(List<List<double>> SetIn, List<List<double>> SetOut)
        {
            if (Useful.WITH_MOMENTUM)
            {
                return NetworkTrainingEpochWithMomentum(SetIn, SetOut);
            }
            else
            {
                return NetworkTrainingEpochNonMomentum(SetIn, SetOut);
            }
        }

        //ѵ��������ĵ������޶�����
        private bool NetworkTrainingEpochNonMomentum(List<List<double>> SetIn, List<List<double>> SetOut)
        {

            int curWeight;
            int curNrnOut, curNrnHid;

            ErrorSum = 0;

            //������۴�������Ȩ��
            for (int vec = 0; vec < SetIn.Count; vec++)
            {
                List<double> outputs = Update(SetIn[vec]);

                if (outputs.Count == 0)
                {
                    return false;
                }

                //����Ȩ��
                for (int op = 0; op < NumOutputs; op++)
                {
                    //����ƫ��
                    double err = (SetOut[vec][op] - outputs[op]) * outputs[op] * (1.0 - outputs[op]);
                    Layers[1].Neurons[op].Error = err;

                    ErrorSum += (SetOut[vec][op] - outputs[op]) * (SetOut[vec][op] - outputs[op]);

                    curWeight = 0;
                    curNrnHid = 0;

                    //��bias֮���Ȩֵ
                    while (curWeight < Layers[1].Neurons[op].Weights.Count - 1)
                    {
                        //��Ȩֵ
                        Layers[1].Neurons[op].Weights[curWeight] += err * LearningRate * Layers[0].Neurons[curNrnHid].Activation;
                        ++curWeight; ++curNrnHid;
                    }

                    //bias
                    Layers[1].Neurons[op].Weights[curWeight] += err * LearningRate * Useful.BIAS;
                }

                curNrnHid = 0;

                int n = 0;

                //�����������ÿ����Ԫ����
                while (curNrnHid < Layers[0].Neurons.Count)
                {
                    double err = 0;

                    curNrnOut = 0;

                    //����������ÿ����Ԫ����
                    while (curNrnOut < Layers[1].Neurons.Count)
                    {
                        err += Layers[1].Neurons[curNrnOut].Error * Layers[1].Neurons[curNrnOut].Weights[n];
                        ++curNrnOut;
                    }

                    //����ƫ��
                    err *= Layers[0].Neurons[curNrnHid].Activation * (1.0 - Layers[0].Neurons[curNrnHid].Activation);

                    //������Ȩ��
                    for (int w = 0; w < NumInputs; w++)
                    {
                        //BP
                        Layers[0].Neurons[curNrnHid].Weights[w] += err * LearningRate * SetIn[vec][w];
                    }

                    //bias
                    Layers[0].Neurons[curNrnHid].Weights[NumInputs] += err * LearningRate * Useful.BIAS;

                    ++curNrnHid;
                    ++n;
                }
            }

            return true;
        }

        //ѵ��������ĵ��������Ӷ�����
        private bool NetworkTrainingEpochWithMomentum(List<List<double>> SetIn, List<List<double>> SetOut)
        {

            int curWeight;
            int curNrnOut, curNrnHid;

            double WeightUpdate = 0;

            ErrorSum = 0;

            //������۴�������Ȩ��
            for (int vec = 0; vec < SetIn.Count; vec++)
            {
                List<double> outputs = Update(SetIn[vec]);

                if (outputs.Count == 0)
                {
                    return false;
                }

                //����Ȩ��
                for (int op = 0; op < NumOutputs; op++)
                {
                    //����ƫ��
                    double err = (SetOut[vec][op] - outputs[op]) * outputs[op] * (1.0 - outputs[op]);
                    Layers[1].Neurons[op].Error = err;

                    ErrorSum += (SetOut[vec][op] - outputs[op]) * (SetOut[vec][op] - outputs[op]);

                    curWeight = 0;
                    curNrnHid = 0;

                    int w = 0;

                    //��bias֮���Ȩֵ
                    while (curWeight < Layers[1].Neurons[op].Weights.Count - 1)
                    {
                        //����Ȩ�ظ���
                        WeightUpdate = err * LearningRate * Layers[0].Neurons[curNrnHid].Activation;
                        //���붯��֮�����Ȩ��
                        Layers[1].Neurons[op].Weights[curWeight] += WeightUpdate + Layers[1].Neurons[op].PrevUpdate[w] * Useful.MOMENTUM;
                        //��¼Ȩ�ظ���
                        Layers[1].Neurons[op].PrevUpdate[w] = WeightUpdate;

                        ++curWeight; ++curNrnHid; ++w;
                    }

                    //bias
                    WeightUpdate = err * LearningRate * Useful.BIAS;
                    Layers[1].Neurons[op].Weights[curWeight] += WeightUpdate + Layers[1].Neurons[op].PrevUpdate[w] * Useful.MOMENTUM;
                    Layers[1].Neurons[op].PrevUpdate[w] = WeightUpdate;
      
                }

                curNrnHid = 0;

                int n = 0;

                //�����������ÿ����Ԫ����
                while (curNrnHid < Layers[0].Neurons.Count)
                {
                    double err = 0;

                    curNrnOut = 0;

                    //����������ÿ����Ԫ����
                    while (curNrnOut < Layers[1].Neurons.Count)
                    {
                        err += Layers[1].Neurons[curNrnOut].Error * Layers[1].Neurons[curNrnOut].Weights[n];
                        ++curNrnOut;
                    }

                    //����ƫ��
                    err *= Layers[0].Neurons[curNrnHid].Activation * (1.0 - Layers[0].Neurons[curNrnHid].Activation);

                    //������Ȩ��
                    int w;
                    for (w = 0; w < NumInputs; w++)
                    {
                        //BP
                        WeightUpdate = err * LearningRate * SetIn[vec][w];
                        Layers[0].Neurons[curNrnHid].Weights[w] += WeightUpdate + Layers[0].Neurons[curNrnHid].PrevUpdate[w] * Useful.MOMENTUM;
                        Layers[0].Neurons[curNrnHid].PrevUpdate[w] = WeightUpdate;
                    }

                    //bias
                    WeightUpdate = err * LearningRate * Useful.BIAS;
                    Layers[0].Neurons[curNrnHid].Weights[NumInputs] += WeightUpdate + Layers[0].Neurons[curNrnHid].PrevUpdate[w] * Useful.MOMENTUM;
                    Layers[0].Neurons[curNrnHid].PrevUpdate[w] = WeightUpdate;

                    ++curNrnHid;
                    ++n;
                }
            }

            return true;
        }

        //����������
        private void CreateNet()
        {
            if (NumHiddenLayers > 0)
            {
                //������
                Layers.Add(new NeuronLayer(NeuronsPerHiddenLyr, NumInputs));
                for (int i = 0; i < NumHiddenLayers - 1; i++)
                {
                    Layers.Add(new NeuronLayer(NeuronsPerHiddenLyr, NeuronsPerHiddenLyr));
                }

                //�����
                Layers.Add(new NeuronLayer(NumOutputs, NeuronsPerHiddenLyr));
            }
            else
            {
                //�����
                Layers.Add(new NeuronLayer(NumOutputs, NumInputs));
            }
        }

        //������Ȩ������Ϊ�����Сֵ
        private void InitializeNetwork()
        {
            //����ÿһ��ִ��
            for (int i = 0; i < NumHiddenLayers + 1; i++)
            {
                //����ÿ����Ԫִ��
                for (int n = 0; n < Layers[i].NumNeurons; n++)
                {
                    //����ÿ��Ȩ��ִ��
                    for (int k = 0; k < Layers[i].Neurons[n].NumInputs; k++)
                    {
                        Layers[i].Neurons[n].Weights[k] = Useful.RandomClamped();
                    }
                }
            }

            ErrorSum = 9999;
            NumEpochs = 0;
        }
  
        //S�ͺ���
        private double Sigmoid(double activation, double response)
        {
            return (1.0 / (1.0 + Math.Exp(- activation / response)));
        }

        //���캯��
        public NeuralNet(int _NumInputs, int _NumOutputs, int _HiddenNeurons, double _LearningRate)
        {
            NumInputs = _NumInputs;
            NumOutputs = _NumOutputs;
            NumHiddenLayers = 1;
            NeuronsPerHiddenLyr = _HiddenNeurons;
            LearningRate = _LearningRate;
            ErrorSum = 9999;
            Trained = false;
            NumEpochs = 0;
            Layers = new List<NeuronLayer>();
            CreateNet();
        }

	    //�����������
        public List<double> Update(List<double> _inputs)
        {
            List<double> inputs = new List<double>(_inputs);
            List<double> outputs = new List<double>();
            int cWeight = 0;

            //�������
            if (Useful.WITH_NOISE)
            {
                for (int k = 0; k < inputs.Count; k++)
                {
                    inputs[k] += Useful.RandomClamped() * Useful.MAX_NOISE_TO_ADD;
                }
            }

            //��֤���볤��
            if (inputs.Count != NumInputs)
            {
                return outputs;
            }

            //����ÿһ��ִ��
            for (int i = 0; i < NumHiddenLayers + 1; i++)
            {
                if (i > 0)
                {
                    inputs = new List<double>(outputs);
                }
                outputs.Clear();

                cWeight = 0;

                //����ÿ����Ԫִ��
                for (int n = 0; n < Layers[i].NumNeurons; n++)
                {
                    double netinput = 0;

                    int num = Layers[i].Neurons[n].NumInputs;

                    //����ÿ��Ȩ��ִ��
                    for (int k = 0; k < num - 1; k++)
                    {
                        netinput += Layers[i].Neurons[n].Weights[k] * inputs[cWeight++];
                    }

                    netinput += Layers[i].Neurons[n].Weights[num - 1] * Useful.BIAS;

                    Layers[i].Neurons[n].Activation = Sigmoid(netinput, Useful.ACTIVATION_RESPONSE);

                    outputs.Add(Layers[i].Neurons[n].Activation);

                    cWeight = 0;
                }
            }

            return outputs;
        }

        //ѵ��������
        public bool Train(GestureData data)
        {
            List<List<double>> SetIn = new List<List<double>>(data.SetIn);
            List<List<double>> SetOut = new List<List<double>>(data.SetOut);

            //У��ѵ����
            if ((SetIn.Count != SetOut.Count) || (SetIn[0].Count != NumInputs) || (SetOut[0].Count != NumOutputs))
            {
                throw new Exception("ѵ�����������������");
            }

            InitializeNetwork();

            //ѵ��ֱ������С����ֵ
            while (ErrorSum > Useful.ERROR_THRESHOLD)
            {
                //����ѵ��
                if (!NetworkTrainingEpoch(SetIn, SetOut))
                {
                    return false;
                }

                NumEpochs++;

                //����ˢ��
                SendMessage(NumEpochs, ErrorSum);
            }

            Trained = true;
            return true;
        }

        #endregion
    }
}
