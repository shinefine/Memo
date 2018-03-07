using System;
using System.Collections.Generic;
using System.Text;

namespace GestureStudy
{
    //Useful类用于常量定义
    static public class Useful
    { 
        //内置手势种数
        public const int NUM_PATTERNS = 10;

        //手势向量长度（坐标个数）
        public const int NUM_VECTORS = 12;

        //输出在多少以上认为是匹配
        public const double MATCH_TOLERANCE = 0.96;

        //神经网络参数
        public const double ACTIVATION_RESPONSE = 1.0;
        public const double BIAS = -1.0;
        static public double ERROR_THRESHOLD = 0.003;
        static public double LEARNING_RATE = 0.5;
        static public int NUM_HIDDEN_NEURONS = 6;
        static public bool WITH_MOMENTUM = false;
        static public double MOMENTUM = 0.9;
        static public bool WITH_NOISE = false;
        static public double MAX_NOISE_TO_ADD = 0.1;

        //预定义的手势名称
        static public string[] InitNames =
        {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"
        };

        //预定义的手势向量
        static private double[] G00 = { -0.96, 0.29, -0.71, 0.71, -0.29, 0.96, 0.29, 0.96, 0.71, 0.71, 0.96, 0.29, 0.96, -0.29, 0.71, -0.71, 0.29, -0.96, -0.29, -0.96, -0.71, -0.71, -0.96, -0.29 };
        static private double[] G01 = { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };
        static private double[] G02 = { 1, 0, 1, 0, 1, 0, 1, 0, -0.71, 0.71, -0.71, 0.71, -0.71, 0.71, -0.71, 0.71, 1, 0, 1, 0, 1, 0, 1, 0 };
        static private double[] G03 = { 1, 0, 1, 0, 0.91, 0.41, 0.41, 0.91, -0.41, 0.91, -0.91, 0.41, 0.91, 0.41, 0.41, 0.91, -0.41, 0.91, -0.91, 0.41, -1, 0, -1, 0 };
        static private double[] G04 = { -0.32, 0.95, -0.32, 0.95, -0.32, 0.95, -0.32, 0.95, -0.32, 0.95, -0.32, 0.95, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 };
        static private double[] G05 = { -1, 0, -1, 0, -1, 0, 0, 1, 0, 1, 1, 0, 0.91, 0.41, 0.41, 0.91, -0.41, 0.91, -0.91, 0.41, -1, 0, -0.91, -0.41 };
        static private double[] G06 = { -0.95, 0.32, -0.71, 0.71, -0.35, 0.94, 0, 1, 0.35, 0.94, 0.71, 0.71, 0.95, 0.32, 0.91, -0.41, 0.41, -0.91, -0.41, -0.91, -0.91, -0.41, -0.91, 0.41 };
        static private double[] G07 = { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };
        static private double[] G08 = { -0.89, -0.45, -0.89, 0.45, 0, 1, 0.89, 0.45, 0.89, 0.45, 0, 1, -0.89, 0.45, -0.89, -0.45, 0, -1, 0.89, -0.45, 0.89, -0.45, 0, -1 };
        static private double[] G09 = { -1, 0, -1, 0, -1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1 };

        static public double[][] InitPatterns = { G00, G01, G02, G03, G04, G05, G06, G07, G08, G09 };
        
        //生成-1~1的随机数
        static private Random ran = new Random();
        static public double RandomClamped()
        {
            double a = ran.NextDouble();
            double b = ran.NextDouble();
            return a - b;
        }

    }

    //程序的运行状态
    public enum RUN_MODE { LEARNING, ACTIVE, UNREADY, TRAINING };
}