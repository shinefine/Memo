using System;
using System.Collections.Generic;
using System.Text;

namespace GestureStudy
{
    //GestureData类用于存储鼠标手势相关数据
    public class GestureData
    {
        #region "属性"

        //手势名称
        private List<string> Names;
        
        //手势向量
        private List<List<double>> Patterns;

        //加载的手势数目
        private int PatternNumber;

        //手势向量长度
        private int PatternSize;

        //训练集
        public List<List<double>> SetIn;
        public List<List<double>> SetOut;

        #endregion

        #region "方法"

        //初始化预定义的手势
        private void Init()
        {
            //对于每个预定义的手势执行
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


        //构造函数
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

        //获得手势名称
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

        //增加新的手势
        public bool AddPattern(List<double> _Pattern, string _Name)
        {
            //检查手势向量长度
            if (_Pattern.Count != PatternSize * 2)
            {
                throw new Exception("手势向量长度错误！");
            }

            Names.Add(_Name);
            Patterns.Add(new List<double>(_Pattern));
            PatternNumber++;

            CreateTrainingSet();
            return true; 
        }

        //创建训练集
        public void CreateTrainingSet()
        {
            //清空训练集
            SetIn.Clear();
            SetOut.Clear();

            //对每个手势操作
            for (int j = 0; j < PatternNumber; j++)
            {
                SetIn.Add(Patterns[j]);

                //相关的输出为1，不相关的输出为0
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
