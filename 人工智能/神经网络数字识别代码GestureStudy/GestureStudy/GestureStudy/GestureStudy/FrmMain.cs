using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GestureStudy
{
    public partial class FrmMain : Form
    {
        #region "神经网络相关属性"

        //手势相关数据对象
        private GestureData data;

        //神经网络对象
        private NeuralNet net;

        //手势数目
        private int NumValidPatterns;

        //需要记录的鼠标点数
        private int NumSmoothPoints;

        //用户鼠标输入的手势向量
        private List<Point> RawPath;

        //光滑化之后的手势向量
        private List<Point> SmoothPath;

        //待匹配的向量
        private List<double> Vectors;

        //网络最大的输出（最像的匹配）
        private double HighestOutput;

        //网络最大的输出对应的手势
        private int BestMatch;

        //匹配的手势
        private int Match;

        //程序的运行状态
        private RUN_MODE Mode;

        #endregion

        #region "绘图相关属性"

        //资源
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pics));

        //绘图区尺寸
        private const int WINDOW_W = 400;
        private const int WINDOW_H = 400;
       
        //内存中的位图
        private Bitmap map;

        //GDI+绘图对象
        private Graphics grpMap, grpPnl;

        //画刷与画笔
        private Brush brushLine = new SolidBrush(Color.Blue);
        private Pen penLine = new Pen(Color.Black);
        private Pen penSmooth = new Pen(Color.Red);

        //是否正在画图
        private bool IsDrawing;

        //上一次捕获到的坐标
        private int lastX, lastY;
        
        #endregion

        #region "神经网络相关方法"

        //初始化数据
        private void InitData()
        {
            Mode = RUN_MODE.UNREADY;
            NumValidPatterns = Useful.NUM_PATTERNS;
            NumSmoothPoints = Useful.NUM_VECTORS + 1;
            HighestOutput = 0.0;
            BestMatch = -1;
            Match = -1;
            RawPath = new List<Point>();
            SmoothPath = new List<Point>();
            Vectors = new List<double>();
            data = new GestureData(NumValidPatterns, Useful.NUM_VECTORS);
            net = new NeuralNet(Useful.NUM_VECTORS * 2, NumValidPatterns, Useful.NUM_HIDDEN_NEURONS, Useful.LEARNING_RATE);
            net.SendMessage += new NeuralNet.DelegateOfSendMessage(ShowMessage);
        }

        //训练神经网络
        private bool TrainNetwork()
        {
            Mode = RUN_MODE.TRAINING;
            if (!(net.Train(data)))
            {
                return false;
            }

            Mode = RUN_MODE.ACTIVE;
            return true;
        }

        //模式匹配
        private bool TestForMatch()
        {
            List<double> outputs = net.Update(Vectors);
            if (outputs.Count == 0)
            {
                return false;
            }

            HighestOutput = 0;
            BestMatch = 0;
            Match = -1;

            for (int i = 0; i < outputs.Count; i++)
            {
                if (outputs[i] > HighestOutput)
                {
                    //记录最像的匹配
                    HighestOutput = outputs[i];
                    BestMatch = i;

                    //确定是这个手势
                    if (HighestOutput > Useful.MATCH_TOLERANCE)
                    {
                        Match = BestMatch;
                    }
                }
            }
            return true;
        }

        //清除手势记录
        private void Clear()
        {
            RawPath.Clear();
            SmoothPath.Clear();
            Vectors.Clear();
        }

        //添加用户输入的手势记录点
        private void AddPoint(Point newpoint) 
        {
            RawPath.Add(newpoint);
        }

        //对鼠标手势向量光滑化，便于模式匹配
        private bool Smooth()
        {
            //确保包含计算所需的足够的点
            if (RawPath.Count < NumSmoothPoints)
            {
                return false;
            }

            SmoothPath = new List<Point>(RawPath);

            //对所有的最小跨度点对取中点，删除原来的点，循环执行
            while (SmoothPath.Count > NumSmoothPoints)
            {
                double ShortestSoFar = double.MaxValue;
                int PointMarker = 0;

                //计算最小跨度
                for (int SpanFront = 2; SpanFront < SmoothPath.Count - 1; SpanFront++)
                {
                    //计算点对距离
                    double length = Math.Sqrt((double)
                        ((SmoothPath[SpanFront - 1].X - SmoothPath[SpanFront].X) *
                          (SmoothPath[SpanFront - 1].X - SmoothPath[SpanFront].X) +
                          (SmoothPath[SpanFront - 1].Y - SmoothPath[SpanFront].Y) *
                          (SmoothPath[SpanFront - 1].Y - SmoothPath[SpanFront].Y)));

                    if (length < ShortestSoFar)
                    {
                        ShortestSoFar = length;
                        PointMarker = SpanFront;
                    }
                }

                //插入中点，删除原来的点
                Point newPoint = new Point();
                newPoint.X = (SmoothPath[PointMarker - 1].X + SmoothPath[PointMarker].X) / 2;
                newPoint.Y = (SmoothPath[PointMarker - 1].Y + SmoothPath[PointMarker].Y) / 2;
                SmoothPath[PointMarker - 1] = newPoint;
                SmoothPath.RemoveAt(PointMarker);
            }

            return true;
        }

        //对鼠标手势向量归一化，生成待匹配的向量
        private void CreateVectors()
        {
            for (int p = 1; p < SmoothPath.Count; ++p)
            {
                double x = (double)(SmoothPath[p].X - SmoothPath[p - 1].X);
                double y = (double)(SmoothPath[p].Y - SmoothPath[p - 1].Y);
                double len = Math.Sqrt((double)(x * x + y * y));
                Vectors.Add(x / len);
                Vectors.Add(y / len);
            }
        }

        //开始手势识别
        private bool StartMatch()
        {
            if (Smooth())
            {
                ShowSmoothPoint();
                CreateVectors();

                //识别
                if (Mode == RUN_MODE.ACTIVE)
                {
                    if (!TestForMatch())
                    {
                        MessageBox.Show("An internal error accured when matching!", "GestureStudy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        ShowResult();
                    }
                }
                //学习
                else if (Mode == RUN_MODE.LEARNING)
                {
                    //保存新的手势并重新训练网络
                    if (MessageBox.Show("Do you want to save this gesture?", "GestureStudy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string name = Microsoft.VisualBasic.Interaction.InputBox("Name for this gesture?", "GestureStudy", "", this.Left + 100, this.Top + 100);
                        data.AddPattern(Vectors, name);
                        NumValidPatterns++;

                        net = new NeuralNet(Useful.NUM_VECTORS * 2, NumValidPatterns, Useful.NUM_HIDDEN_NEURONS, Useful.LEARNING_RATE);
                        net.SendMessage += new NeuralNet.DelegateOfSendMessage(ShowMessage);

                        txtState.BackColor = Color.FromKnownColor(KnownColor.Control);
                        RenewNetwork();

                        Mode = RUN_MODE.ACTIVE;
                    }
                }
            }
            else
            {
                MessageBox.Show("Too few points captured, please draw line slowly!", "GestureStudy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                ClearDraw();
            }

            return true;
        }

        //使用新的参数重建神经网络
        private void RenewNetwork()
        {
            Useful.LEARNING_RATE = (double)(txtLearning.Value);
            Useful.ERROR_THRESHOLD = (double)(txtThreshold.Value);
            Useful.NUM_HIDDEN_NEURONS = (int)(txtHidden.Value);
            Useful.WITH_MOMENTUM = chkMomentum.Checked;
            Useful.MOMENTUM = (double)(txtMomentum.Value);
            Useful.WITH_NOISE = chkNoise.Checked;
            Useful.MAX_NOISE_TO_ADD = (double)(txtNoise.Value);

            net = new NeuralNet(Useful.NUM_VECTORS * 2, NumValidPatterns, Useful.NUM_HIDDEN_NEURONS, Useful.LEARNING_RATE);
            net.SendMessage += new NeuralNet.DelegateOfSendMessage(ShowMessage);

            txtState.Text = "Training";
            TrainNetwork();
            txtState.Text = "Ready";
        }

        #endregion
        
        #region "绘图相关方法"

        //初始化绘图区
        private void InitDraw()
        {
            pnlDraw.Width = WINDOW_W;
            pnlDraw.Height = WINDOW_H;
            grpPnl = pnlDraw.CreateGraphics();
            ClearDraw();
        }

        //清除绘图区
        private void ClearDraw()
        {
            map = new Bitmap(WINDOW_W, WINDOW_H);
            grpMap = Graphics.FromImage(map);
            IsDrawing = false;
            lastX = lastY = 0;
            grpPnl.Clear(Color.White);
        }

        //显示光滑化之后的点
        private void ShowSmoothPoint()
        {
            foreach (Point p in SmoothPath)
            {
                grpMap.DrawEllipse(penSmooth, p.X - 3, p.Y - 3, 7, 7);
            }
            grpPnl.DrawImage(map, 0, 0);
        }

        //显示训练消息
        private void ShowMessage(int Epochs, double Error)
        {
            txtEpochs.Text = Epochs.ToString();
            txtError.Text = Error.ToString("F6");
            double pct = Useful.ERROR_THRESHOLD / Error * 100.0;
            int val = (int)pct;
            if (val > 100)
            {
                val = 100;
            }
            prgTrain.Value = val;
            this.Update();
        }

        //显示匹配结果
        private void ShowResult()
        {
            txtResult.Text = data.PatternName(BestMatch);
            txtProbability.Text = HighestOutput.ToString("F6");
            int pct = (int)(HighestOutput*10.0);
            if (pct > 9)
            {
                pct = 9;
            }
            picFace.Image = (Image)(resources.GetObject("_" + pct.ToString()));
        }

        #endregion

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtState.Text = "Unready";
            picFace.Image = (Image)(resources.GetObject("_a"));
            InitDraw();
            InitData();
        }

        private void pnlDraw_Paint(object sender, PaintEventArgs e)
        {
            grpPnl.DrawImage(map, 0, 0);
        }

        private void pnlDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDrawing)
            {
                grpMap.DrawLine(penLine, lastX, lastY, e.X, e.Y);
                grpMap.FillEllipse(brushLine, e.X - 2, e.Y - 2, 5, 5);
                grpPnl.DrawImage(map, 0, 0);
                lastX = e.X;
                lastY = e.Y;
                //记录一个点
                AddPoint(new Point(e.X, e.Y));
            }
            txtMouse.Text = ("(" + e.X.ToString() + "," + e.Y.ToString() + ")");
            this.Update();
        }

        private void pnlDraw_MouseDown(object sender, MouseEventArgs e)
        {
            //准备为神经网络记录数据
            Clear();
            ClearDraw();

            lastX = e.X;
            lastY = e.Y;
            IsDrawing = true;
        }

        private void pnlDraw_MouseUp(object sender, MouseEventArgs e)
        {
            IsDrawing = false;
            //开始模式匹配
            StartMatch();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            ClearDraw();
        }

        private void btnLearn_Click(object sender, EventArgs e)
        {
            Mode = RUN_MODE.LEARNING;
            txtState.Text = "Learning";
            txtState.BackColor = Color.Yellow;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            RenewNetwork();
        }

        private void txtCopyright_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.linjian.cn");
        }

    }
}