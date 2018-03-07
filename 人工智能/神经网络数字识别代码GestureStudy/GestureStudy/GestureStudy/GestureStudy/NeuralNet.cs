using System;
using System.Collections.Generic;
using System.Text;

namespace GestureStudy
{
    //神经元
    public class Neuron
    {
        #region "属性"

        //神经元输入数
        public int NumInputs;

        //权值向量
        public List<double> Weights;

        //前一步的权值更新向量
        public List<double> PrevUpdate;

        //活跃值
        public double Activation;

        //错误值
        public double Error;

        #endregion

        #region "方法"
        
        //构造函数
        public Neuron(int _NumInputs)
        {
            NumInputs = _NumInputs + 1;
            Activation = 0;
            Error = 0;
            Weights = new List<double>();
            PrevUpdate = new List<double>();

            //生成随机权重
            for (int i = 0; i < NumInputs; i++)
            {
                Weights.Add(Useful.RandomClamped());
                PrevUpdate.Add(0.0);
            }
        }

        #endregion
    }

    //神经网络层
    public class NeuronLayer
    {
        #region "属性"

	    //本层神经元数
        public int NumNeurons;

	    //神经元
        public List<Neuron> Neurons;

        #endregion

        #region "方法"

        //构造函数
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

    //NeuralNet类定义了神经网络
    public class NeuralNet
    {
        #region "属性"

        //输入数
        private int NumInputs;

        //输出数
        private int NumOutputs;

        //隐含层数
        private int NumHiddenLayers;

        //每个隐含层的神经元数
        private int NeuronsPerHiddenLyr;

        //学习率
        private double LearningRate;

        //积累错误
        public double ErrorSum;

        //是否经过了训练
        public bool Trained;

        //迭代数
        public int NumEpochs;

        //神经网络的各个层
        private List<NeuronLayer> Layers;

        //向窗体发送消息的委托
        public delegate void DelegateOfSendMessage(int Epochs, double Error);
        public event DelegateOfSendMessage SendMessage;

        #endregion

        #region "方法"

        //训练神经网络的迭代
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

        //训练神经网络的迭代（无动量）
        private bool NetworkTrainingEpochNonMomentum(List<List<double>> SetIn, List<List<double>> SetOut)
        {

            int curWeight;
            int curNrnOut, curNrnHid;

            ErrorSum = 0;

            //计算积累错误，修正权重
            for (int vec = 0; vec < SetIn.Count; vec++)
            {
                List<double> outputs = Update(SetIn[vec]);

                if (outputs.Count == 0)
                {
                    return false;
                }

                //修正权重
                for (int op = 0; op < NumOutputs; op++)
                {
                    //计算偏差
                    double err = (SetOut[vec][op] - outputs[op]) * outputs[op] * (1.0 - outputs[op]);
                    Layers[1].Neurons[op].Error = err;

                    ErrorSum += (SetOut[vec][op] - outputs[op]) * (SetOut[vec][op] - outputs[op]);

                    curWeight = 0;
                    curNrnHid = 0;

                    //除bias之外的权值
                    while (curWeight < Layers[1].Neurons[op].Weights.Count - 1)
                    {
                        //新权值
                        Layers[1].Neurons[op].Weights[curWeight] += err * LearningRate * Layers[0].Neurons[curNrnHid].Activation;
                        ++curWeight; ++curNrnHid;
                    }

                    //bias
                    Layers[1].Neurons[op].Weights[curWeight] += err * LearningRate * Useful.BIAS;
                }

                curNrnHid = 0;

                int n = 0;

                //对于隐含层的每个神经元计算
                while (curNrnHid < Layers[0].Neurons.Count)
                {
                    double err = 0;

                    curNrnOut = 0;

                    //对于输出层的每个神经元计算
                    while (curNrnOut < Layers[1].Neurons.Count)
                    {
                        err += Layers[1].Neurons[curNrnOut].Error * Layers[1].Neurons[curNrnOut].Weights[n];
                        ++curNrnOut;
                    }

                    //计算偏差
                    err *= Layers[0].Neurons[curNrnHid].Activation * (1.0 - Layers[0].Neurons[curNrnHid].Activation);

                    //计算新权重
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

        //训练神经网络的迭代（增加动量）
        private bool NetworkTrainingEpochWithMomentum(List<List<double>> SetIn, List<List<double>> SetOut)
        {

            int curWeight;
            int curNrnOut, curNrnHid;

            double WeightUpdate = 0;

            ErrorSum = 0;

            //计算积累错误，修正权重
            for (int vec = 0; vec < SetIn.Count; vec++)
            {
                List<double> outputs = Update(SetIn[vec]);

                if (outputs.Count == 0)
                {
                    return false;
                }

                //修正权重
                for (int op = 0; op < NumOutputs; op++)
                {
                    //计算偏差
                    double err = (SetOut[vec][op] - outputs[op]) * outputs[op] * (1.0 - outputs[op]);
                    Layers[1].Neurons[op].Error = err;

                    ErrorSum += (SetOut[vec][op] - outputs[op]) * (SetOut[vec][op] - outputs[op]);

                    curWeight = 0;
                    curNrnHid = 0;

                    int w = 0;

                    //除bias之外的权值
                    while (curWeight < Layers[1].Neurons[op].Weights.Count - 1)
                    {
                        //计算权重更新
                        WeightUpdate = err * LearningRate * Layers[0].Neurons[curNrnHid].Activation;
                        //加入动量之后的新权重
                        Layers[1].Neurons[op].Weights[curWeight] += WeightUpdate + Layers[1].Neurons[op].PrevUpdate[w] * Useful.MOMENTUM;
                        //记录权重更新
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

                //对于隐含层的每个神经元计算
                while (curNrnHid < Layers[0].Neurons.Count)
                {
                    double err = 0;

                    curNrnOut = 0;

                    //对于输出层的每个神经元计算
                    while (curNrnOut < Layers[1].Neurons.Count)
                    {
                        err += Layers[1].Neurons[curNrnOut].Error * Layers[1].Neurons[curNrnOut].Weights[n];
                        ++curNrnOut;
                    }

                    //计算偏差
                    err *= Layers[0].Neurons[curNrnHid].Activation * (1.0 - Layers[0].Neurons[curNrnHid].Activation);

                    //计算新权重
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

        //创建神经网络
        private void CreateNet()
        {
            if (NumHiddenLayers > 0)
            {
                //隐含层
                Layers.Add(new NeuronLayer(NeuronsPerHiddenLyr, NumInputs));
                for (int i = 0; i < NumHiddenLayers - 1; i++)
                {
                    Layers.Add(new NeuronLayer(NeuronsPerHiddenLyr, NeuronsPerHiddenLyr));
                }

                //输出层
                Layers.Add(new NeuronLayer(NumOutputs, NeuronsPerHiddenLyr));
            }
            else
            {
                //输出层
                Layers.Add(new NeuronLayer(NumOutputs, NumInputs));
            }
        }

        //将所有权重设置为随机的小值
        private void InitializeNetwork()
        {
            //对于每一层执行
            for (int i = 0; i < NumHiddenLayers + 1; i++)
            {
                //对于每个神经元执行
                for (int n = 0; n < Layers[i].NumNeurons; n++)
                {
                    //对于每个权重执行
                    for (int k = 0; k < Layers[i].Neurons[n].NumInputs; k++)
                    {
                        Layers[i].Neurons[n].Weights[k] = Useful.RandomClamped();
                    }
                }
            }

            ErrorSum = 9999;
            NumEpochs = 0;
        }
  
        //S型函数
        private double Sigmoid(double activation, double response)
        {
            return (1.0 / (1.0 + Math.Exp(- activation / response)));
        }

        //构造函数
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

	    //计算网络输出
        public List<double> Update(List<double> _inputs)
        {
            List<double> inputs = new List<double>(_inputs);
            List<double> outputs = new List<double>();
            int cWeight = 0;

            //添加噪声
            if (Useful.WITH_NOISE)
            {
                for (int k = 0; k < inputs.Count; k++)
                {
                    inputs[k] += Useful.RandomClamped() * Useful.MAX_NOISE_TO_ADD;
                }
            }

            //验证输入长度
            if (inputs.Count != NumInputs)
            {
                return outputs;
            }

            //对于每一层执行
            for (int i = 0; i < NumHiddenLayers + 1; i++)
            {
                if (i > 0)
                {
                    inputs = new List<double>(outputs);
                }
                outputs.Clear();

                cWeight = 0;

                //对于每个神经元执行
                for (int n = 0; n < Layers[i].NumNeurons; n++)
                {
                    double netinput = 0;

                    int num = Layers[i].Neurons[n].NumInputs;

                    //对于每个权重执行
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

        //训练神经网络
        public bool Train(GestureData data)
        {
            List<List<double>> SetIn = new List<List<double>>(data.SetIn);
            List<List<double>> SetOut = new List<List<double>>(data.SetOut);

            //校验训练集
            if ((SetIn.Count != SetOut.Count) || (SetIn[0].Count != NumInputs) || (SetOut[0].Count != NumOutputs))
            {
                throw new Exception("训练集输入输出不符！");
            }

            InitializeNetwork();

            //训练直至错误小于阈值
            while (ErrorSum > Useful.ERROR_THRESHOLD)
            {
                //迭代训练
                if (!NetworkTrainingEpoch(SetIn, SetOut))
                {
                    return false;
                }

                NumEpochs++;

                //窗体刷新
                SendMessage(NumEpochs, ErrorSum);
            }

            Trained = true;
            return true;
        }

        #endregion
    }
}
