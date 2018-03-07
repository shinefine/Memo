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
        #region "�������������"

        //����������ݶ���
        private GestureData data;

        //���������
        private NeuralNet net;

        //������Ŀ
        private int NumValidPatterns;

        //��Ҫ��¼��������
        private int NumSmoothPoints;

        //�û�����������������
        private List<Point> RawPath;

        //�⻬��֮�����������
        private List<Point> SmoothPath;

        //��ƥ�������
        private List<double> Vectors;

        //������������������ƥ�䣩
        private double HighestOutput;

        //�������������Ӧ������
        private int BestMatch;

        //ƥ�������
        private int Match;

        //���������״̬
        private RUN_MODE Mode;

        #endregion

        #region "��ͼ�������"

        //��Դ
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pics));

        //��ͼ���ߴ�
        private const int WINDOW_W = 400;
        private const int WINDOW_H = 400;
       
        //�ڴ��е�λͼ
        private Bitmap map;

        //GDI+��ͼ����
        private Graphics grpMap, grpPnl;

        //��ˢ�뻭��
        private Brush brushLine = new SolidBrush(Color.Blue);
        private Pen penLine = new Pen(Color.Black);
        private Pen penSmooth = new Pen(Color.Red);

        //�Ƿ����ڻ�ͼ
        private bool IsDrawing;

        //��һ�β��񵽵�����
        private int lastX, lastY;
        
        #endregion

        #region "��������ط���"

        //��ʼ������
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

        //ѵ��������
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

        //ģʽƥ��
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
                    //��¼�����ƥ��
                    HighestOutput = outputs[i];
                    BestMatch = i;

                    //ȷ�����������
                    if (HighestOutput > Useful.MATCH_TOLERANCE)
                    {
                        Match = BestMatch;
                    }
                }
            }
            return true;
        }

        //������Ƽ�¼
        private void Clear()
        {
            RawPath.Clear();
            SmoothPath.Clear();
            Vectors.Clear();
        }

        //����û���������Ƽ�¼��
        private void AddPoint(Point newpoint) 
        {
            RawPath.Add(newpoint);
        }

        //��������������⻬��������ģʽƥ��
        private bool Smooth()
        {
            //ȷ����������������㹻�ĵ�
            if (RawPath.Count < NumSmoothPoints)
            {
                return false;
            }

            SmoothPath = new List<Point>(RawPath);

            //�����е���С��ȵ��ȡ�е㣬ɾ��ԭ���ĵ㣬ѭ��ִ��
            while (SmoothPath.Count > NumSmoothPoints)
            {
                double ShortestSoFar = double.MaxValue;
                int PointMarker = 0;

                //������С���
                for (int SpanFront = 2; SpanFront < SmoothPath.Count - 1; SpanFront++)
                {
                    //�����Ծ���
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

                //�����е㣬ɾ��ԭ���ĵ�
                Point newPoint = new Point();
                newPoint.X = (SmoothPath[PointMarker - 1].X + SmoothPath[PointMarker].X) / 2;
                newPoint.Y = (SmoothPath[PointMarker - 1].Y + SmoothPath[PointMarker].Y) / 2;
                SmoothPath[PointMarker - 1] = newPoint;
                SmoothPath.RemoveAt(PointMarker);
            }

            return true;
        }

        //���������������һ�������ɴ�ƥ�������
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

        //��ʼ����ʶ��
        private bool StartMatch()
        {
            if (Smooth())
            {
                ShowSmoothPoint();
                CreateVectors();

                //ʶ��
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
                //ѧϰ
                else if (Mode == RUN_MODE.LEARNING)
                {
                    //�����µ����Ʋ�����ѵ������
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

        //ʹ���µĲ����ؽ�������
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
        
        #region "��ͼ��ط���"

        //��ʼ����ͼ��
        private void InitDraw()
        {
            pnlDraw.Width = WINDOW_W;
            pnlDraw.Height = WINDOW_H;
            grpPnl = pnlDraw.CreateGraphics();
            ClearDraw();
        }

        //�����ͼ��
        private void ClearDraw()
        {
            map = new Bitmap(WINDOW_W, WINDOW_H);
            grpMap = Graphics.FromImage(map);
            IsDrawing = false;
            lastX = lastY = 0;
            grpPnl.Clear(Color.White);
        }

        //��ʾ�⻬��֮��ĵ�
        private void ShowSmoothPoint()
        {
            foreach (Point p in SmoothPath)
            {
                grpMap.DrawEllipse(penSmooth, p.X - 3, p.Y - 3, 7, 7);
            }
            grpPnl.DrawImage(map, 0, 0);
        }

        //��ʾѵ����Ϣ
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

        //��ʾƥ����
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
                //��¼һ����
                AddPoint(new Point(e.X, e.Y));
            }
            txtMouse.Text = ("(" + e.X.ToString() + "," + e.Y.ToString() + ")");
            this.Update();
        }

        private void pnlDraw_MouseDown(object sender, MouseEventArgs e)
        {
            //׼��Ϊ�������¼����
            Clear();
            ClearDraw();

            lastX = e.X;
            lastY = e.Y;
            IsDrawing = true;
        }

        private void pnlDraw_MouseUp(object sender, MouseEventArgs e)
        {
            IsDrawing = false;
            //��ʼģʽƥ��
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