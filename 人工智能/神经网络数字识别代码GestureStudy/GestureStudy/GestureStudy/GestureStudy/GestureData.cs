using System;
using System.Collections.Generic;
using System.Text;

namespace GestureStudy
{
    //GestureData�����ڴ洢��������������
    public class GestureData
    {
        #region "����"

        //��������
        private List<string> Names;
        
        //��������
        private List<List<double>> Patterns;

        //���ص�������Ŀ
        private int PatternNumber;

        //������������
        private int PatternSize;

        //ѵ����
        public List<List<double>> SetIn;
        public List<List<double>> SetOut;

        #endregion

        #region "����"

        //��ʼ��Ԥ���������
        private void Init()
        {
            //����ÿ��Ԥ���������ִ��
            for (int j = 0; j < PatternNumber; j++)
            {
                List<double> temp = new List<double>();
                for (int v = 0; v < PatternSize * 2; v++)
                {
                    temp.Add(Useful.InitPatterns[j][v]);
                }
                Patterns.Add(temp);
                Names.Add(Useful.InitNames[j]);
            }
        }


        //���캯��
        public GestureData(int _PatternNumber, int _PatternSize)
        {
            Names = new List<string>();
            Patterns = new List<List<double>>();
            SetIn = new List<List<double>>();
            SetOut = new List<List<double>>();
            PatternNumber = _PatternNumber;
            PatternSize = _PatternSize;

            Init();
            CreateTrainingSet();
        }

        //�����������
        public string PatternName(int index)
        {
            if (Names[index] != null)
            {
                return Names[index];
            }
            else
            {
                return "";
            }
        }

        //�����µ�����
        public bool AddPattern(List<double> _Pattern, string _Name)
        {
            //���������������
            if (_Pattern.Count != PatternSize * 2)
            {
                throw new Exception("�����������ȴ���");
            }

            Names.Add(_Name);
            Patterns.Add(new List<double>(_Pattern));
            PatternNumber++;

            CreateTrainingSet();
            return true; 
        }

        //����ѵ����
        public void CreateTrainingSet()
        {
            //���ѵ����
            SetIn.Clear();
            SetOut.Clear();

            //��ÿ�����Ʋ���
            for (int j = 0; j < PatternNumber; j++)
            {
                SetIn.Add(Patterns[j]);

                //��ص����Ϊ1������ص����Ϊ0
                List<double> outputs = new List<double>();
                for (int i = 0; i < PatternNumber; i++)
                {
                    outputs.Add(0);
                }
                outputs[j] = 1;

                SetOut.Add(outputs);
            }
        }

        #endregion
    }
}
