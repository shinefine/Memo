namespace GestureStudy
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.txtEpochs = new System.Windows.Forms.TextBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.lblEpochs = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlDraw = new System.Windows.Forms.Panel();
            this.barState = new System.Windows.Forms.StatusStrip();
            this.txtState = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtMouse = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgTrain = new System.Windows.Forms.ToolStripProgressBar();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblProbability = new System.Windows.Forms.Label();
            this.txtProbability = new System.Windows.Forms.TextBox();
            this.btnLearn = new System.Windows.Forms.Button();
            this.grpTrain = new System.Windows.Forms.GroupBox();
            this.grpMatch = new System.Windows.Forms.GroupBox();
            this.picFace = new System.Windows.Forms.PictureBox();
            this.grpParameters = new System.Windows.Forms.GroupBox();
            this.txtNoise = new System.Windows.Forms.NumericUpDown();
            this.chkNoise = new System.Windows.Forms.CheckBox();
            this.txtMomentum = new System.Windows.Forms.NumericUpDown();
            this.chkMomentum = new System.Windows.Forms.CheckBox();
            this.btnRenew = new System.Windows.Forms.Button();
            this.txtHidden = new System.Windows.Forms.NumericUpDown();
            this.txtThreshold = new System.Windows.Forms.NumericUpDown();
            this.txtLearning = new System.Windows.Forms.NumericUpDown();
            this.lblThreshold = new System.Windows.Forms.Label();
            this.lblLearning = new System.Windows.Forms.Label();
            this.lblHidden = new System.Windows.Forms.Label();
            this.grpOperation = new System.Windows.Forms.GroupBox();
            this.picGraffiti = new System.Windows.Forms.PictureBox();
            this.txtGraffiti = new System.Windows.Forms.Label();
            this.txtCopyright = new System.Windows.Forms.ToolStripStatusLabel();
            this.barState.SuspendLayout();
            this.grpTrain.SuspendLayout();
            this.grpMatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFace)).BeginInit();
            this.grpParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMomentum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHidden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLearning)).BeginInit();
            this.grpOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGraffiti)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEpochs
            // 
            this.txtEpochs.Location = new System.Drawing.Point(84, 19);
            this.txtEpochs.Name = "txtEpochs";
            this.txtEpochs.ReadOnly = true;
            this.txtEpochs.Size = new System.Drawing.Size(90, 20);
            this.txtEpochs.TabIndex = 1;
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(84, 45);
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(90, 20);
            this.txtError.TabIndex = 3;
            // 
            // lblEpochs
            // 
            this.lblEpochs.AutoSize = true;
            this.lblEpochs.Location = new System.Drawing.Point(10, 22);
            this.lblEpochs.Name = "lblEpochs";
            this.lblEpochs.Size = new System.Drawing.Size(43, 13);
            this.lblEpochs.TabIndex = 0;
            this.lblEpochs.Text = "Epochs";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(10, 48);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(29, 13);
            this.lblError.TabIndex = 2;
            this.lblError.Text = "Error";
            // 
            // pnlDraw
            // 
            this.pnlDraw.BackColor = System.Drawing.Color.White;
            this.pnlDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDraw.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pnlDraw.Location = new System.Drawing.Point(279, 12);
            this.pnlDraw.Name = "pnlDraw";
            this.pnlDraw.Size = new System.Drawing.Size(400, 400);
            this.pnlDraw.TabIndex = 4;
            this.pnlDraw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlDraw_MouseDown);
            this.pnlDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlDraw_MouseMove);
            this.pnlDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDraw_Paint);
            this.pnlDraw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlDraw_MouseUp);
            // 
            // barState
            // 
            this.barState.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtState,
            this.txtMouse,
            this.prgTrain,
            this.txtCopyright});
            this.barState.Location = new System.Drawing.Point(0, 523);
            this.barState.Name = "barState";
            this.barState.Size = new System.Drawing.Size(694, 22);
            this.barState.SizingGrip = false;
            this.barState.TabIndex = 6;
            // 
            // txtState
            // 
            this.txtState.AutoSize = false;
            this.txtState.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.txtState.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(100, 17);
            this.txtState.Text = "state";
            // 
            // txtMouse
            // 
            this.txtMouse.AutoSize = false;
            this.txtMouse.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.txtMouse.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.txtMouse.Name = "txtMouse";
            this.txtMouse.Size = new System.Drawing.Size(100, 17);
            this.txtMouse.Text = "(0,0)";
            // 
            // prgTrain
            // 
            this.prgTrain.Name = "prgTrain";
            this.prgTrain.Size = new System.Drawing.Size(100, 16);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(56, 48);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(140, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear Pad";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(9, 19);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(37, 13);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "Result";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(84, 16);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(90, 20);
            this.txtResult.TabIndex = 1;
            // 
            // lblProbability
            // 
            this.lblProbability.AutoSize = true;
            this.lblProbability.Location = new System.Drawing.Point(9, 48);
            this.lblProbability.Name = "lblProbability";
            this.lblProbability.Size = new System.Drawing.Size(55, 13);
            this.lblProbability.TabIndex = 2;
            this.lblProbability.Text = "Probability";
            // 
            // txtProbability
            // 
            this.txtProbability.Location = new System.Drawing.Point(84, 45);
            this.txtProbability.Name = "txtProbability";
            this.txtProbability.ReadOnly = true;
            this.txtProbability.Size = new System.Drawing.Size(90, 20);
            this.txtProbability.TabIndex = 3;
            // 
            // btnLearn
            // 
            this.btnLearn.Location = new System.Drawing.Point(56, 19);
            this.btnLearn.Name = "btnLearn";
            this.btnLearn.Size = new System.Drawing.Size(140, 23);
            this.btnLearn.TabIndex = 0;
            this.btnLearn.Text = "Learn New Gesture";
            this.btnLearn.UseVisualStyleBackColor = true;
            this.btnLearn.Click += new System.EventHandler(this.btnLearn_Click);
            // 
            // grpTrain
            // 
            this.grpTrain.Controls.Add(this.txtError);
            this.grpTrain.Controls.Add(this.txtEpochs);
            this.grpTrain.Controls.Add(this.lblEpochs);
            this.grpTrain.Controls.Add(this.lblError);
            this.grpTrain.Location = new System.Drawing.Point(12, 294);
            this.grpTrain.Name = "grpTrain";
            this.grpTrain.Size = new System.Drawing.Size(253, 78);
            this.grpTrain.TabIndex = 2;
            this.grpTrain.TabStop = false;
            this.grpTrain.Text = "Train Output";
            // 
            // grpMatch
            // 
            this.grpMatch.Controls.Add(this.picFace);
            this.grpMatch.Controls.Add(this.lblProbability);
            this.grpMatch.Controls.Add(this.txtResult);
            this.grpMatch.Controls.Add(this.lblResult);
            this.grpMatch.Controls.Add(this.txtProbability);
            this.grpMatch.Location = new System.Drawing.Point(13, 378);
            this.grpMatch.Name = "grpMatch";
            this.grpMatch.Size = new System.Drawing.Size(253, 78);
            this.grpMatch.TabIndex = 3;
            this.grpMatch.TabStop = false;
            this.grpMatch.Text = "Match Result";
            // 
            // picFace
            // 
            this.picFace.Location = new System.Drawing.Point(192, 22);
            this.picFace.Name = "picFace";
            this.picFace.Size = new System.Drawing.Size(43, 43);
            this.picFace.TabIndex = 15;
            this.picFace.TabStop = false;
            // 
            // grpParameters
            // 
            this.grpParameters.Controls.Add(this.txtNoise);
            this.grpParameters.Controls.Add(this.chkNoise);
            this.grpParameters.Controls.Add(this.txtMomentum);
            this.grpParameters.Controls.Add(this.chkMomentum);
            this.grpParameters.Controls.Add(this.btnRenew);
            this.grpParameters.Controls.Add(this.txtHidden);
            this.grpParameters.Controls.Add(this.txtThreshold);
            this.grpParameters.Controls.Add(this.txtLearning);
            this.grpParameters.Controls.Add(this.lblThreshold);
            this.grpParameters.Controls.Add(this.lblLearning);
            this.grpParameters.Controls.Add(this.lblHidden);
            this.grpParameters.Location = new System.Drawing.Point(12, 12);
            this.grpParameters.Name = "grpParameters";
            this.grpParameters.Size = new System.Drawing.Size(253, 186);
            this.grpParameters.TabIndex = 0;
            this.grpParameters.TabStop = false;
            this.grpParameters.Text = "Network Parameters";
            // 
            // txtNoise
            // 
            this.txtNoise.DecimalPlaces = 3;
            this.txtNoise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.txtNoise.Location = new System.Drawing.Point(147, 124);
            this.txtNoise.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNoise.Name = "txtNoise";
            this.txtNoise.Size = new System.Drawing.Size(89, 20);
            this.txtNoise.TabIndex = 9;
            this.txtNoise.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // chkNoise
            // 
            this.chkNoise.AutoSize = true;
            this.chkNoise.Location = new System.Drawing.Point(13, 125);
            this.chkNoise.Name = "chkNoise";
            this.chkNoise.Size = new System.Drawing.Size(78, 17);
            this.chkNoise.TabIndex = 8;
            this.chkNoise.Text = "With Noise";
            this.chkNoise.UseVisualStyleBackColor = true;
            // 
            // txtMomentum
            // 
            this.txtMomentum.DecimalPlaces = 3;
            this.txtMomentum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.txtMomentum.Location = new System.Drawing.Point(147, 98);
            this.txtMomentum.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMomentum.Name = "txtMomentum";
            this.txtMomentum.Size = new System.Drawing.Size(89, 20);
            this.txtMomentum.TabIndex = 7;
            this.txtMomentum.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            // 
            // chkMomentum
            // 
            this.chkMomentum.AutoSize = true;
            this.chkMomentum.Location = new System.Drawing.Point(13, 99);
            this.chkMomentum.Name = "chkMomentum";
            this.chkMomentum.Size = new System.Drawing.Size(103, 17);
            this.chkMomentum.TabIndex = 6;
            this.chkMomentum.Text = "With Momentum";
            this.chkMomentum.UseVisualStyleBackColor = true;
            // 
            // btnRenew
            // 
            this.btnRenew.Location = new System.Drawing.Point(57, 150);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(140, 23);
            this.btnRenew.TabIndex = 10;
            this.btnRenew.Text = "Train Network";
            this.btnRenew.UseVisualStyleBackColor = true;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // txtHidden
            // 
            this.txtHidden.Location = new System.Drawing.Point(147, 72);
            this.txtHidden.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.txtHidden.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHidden.Name = "txtHidden";
            this.txtHidden.Size = new System.Drawing.Size(89, 20);
            this.txtHidden.TabIndex = 5;
            this.txtHidden.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // txtThreshold
            // 
            this.txtThreshold.DecimalPlaces = 6;
            this.txtThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.txtThreshold.Location = new System.Drawing.Point(147, 46);
            this.txtThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(89, 20);
            this.txtThreshold.TabIndex = 3;
            this.txtThreshold.Value = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            // 
            // txtLearning
            // 
            this.txtLearning.DecimalPlaces = 3;
            this.txtLearning.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.txtLearning.Location = new System.Drawing.Point(147, 20);
            this.txtLearning.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLearning.Name = "txtLearning";
            this.txtLearning.Size = new System.Drawing.Size(89, 20);
            this.txtLearning.TabIndex = 1;
            this.txtLearning.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // lblThreshold
            // 
            this.lblThreshold.AutoSize = true;
            this.lblThreshold.Location = new System.Drawing.Point(10, 48);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(79, 13);
            this.lblThreshold.TabIndex = 2;
            this.lblThreshold.Text = "Error Threshold";
            // 
            // lblLearning
            // 
            this.lblLearning.AutoSize = true;
            this.lblLearning.Location = new System.Drawing.Point(10, 22);
            this.lblLearning.Name = "lblLearning";
            this.lblLearning.Size = new System.Drawing.Size(74, 13);
            this.lblLearning.TabIndex = 0;
            this.lblLearning.Text = "Learning Rate";
            // 
            // lblHidden
            // 
            this.lblHidden.AutoSize = true;
            this.lblHidden.Location = new System.Drawing.Point(10, 74);
            this.lblHidden.Name = "lblHidden";
            this.lblHidden.Size = new System.Drawing.Size(84, 13);
            this.lblHidden.TabIndex = 4;
            this.lblHidden.Text = "Hidden Neurons";
            // 
            // grpOperation
            // 
            this.grpOperation.Controls.Add(this.btnLearn);
            this.grpOperation.Controls.Add(this.btnClear);
            this.grpOperation.Location = new System.Drawing.Point(13, 204);
            this.grpOperation.Name = "grpOperation";
            this.grpOperation.Size = new System.Drawing.Size(252, 84);
            this.grpOperation.TabIndex = 1;
            this.grpOperation.TabStop = false;
            this.grpOperation.Text = "Operation";
            // 
            // picGraffiti
            // 
            this.picGraffiti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picGraffiti.Image = ((System.Drawing.Image)(resources.GetObject("picGraffiti.Image")));
            this.picGraffiti.Location = new System.Drawing.Point(377, 460);
            this.picGraffiti.Name = "picGraffiti";
            this.picGraffiti.Size = new System.Drawing.Size(302, 60);
            this.picGraffiti.TabIndex = 19;
            this.picGraffiti.TabStop = false;
            // 
            // txtGraffiti
            // 
            this.txtGraffiti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGraffiti.AutoSize = true;
            this.txtGraffiti.Location = new System.Drawing.Point(276, 460);
            this.txtGraffiti.Name = "txtGraffiti";
            this.txtGraffiti.Size = new System.Drawing.Size(61, 13);
            this.txtGraffiti.TabIndex = 5;
            this.txtGraffiti.Text = "Graffiti Font";
            // 
            // txtCopyright
            // 
            this.txtCopyright.IsLink = true;
            this.txtCopyright.Name = "txtCopyright";
            this.txtCopyright.Size = new System.Drawing.Size(346, 17);
            this.txtCopyright.Spring = true;
            this.txtCopyright.Text = "by Lin Jian , http://www.linjian.cn";
            this.txtCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtCopyright.Click += new System.EventHandler(this.txtCopyright_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 545);
            this.Controls.Add(this.txtGraffiti);
            this.Controls.Add(this.picGraffiti);
            this.Controls.Add(this.grpOperation);
            this.Controls.Add(this.grpMatch);
            this.Controls.Add(this.grpParameters);
            this.Controls.Add(this.grpTrain);
            this.Controls.Add(this.barState);
            this.Controls.Add(this.pnlDraw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "GestureStudy";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.barState.ResumeLayout(false);
            this.barState.PerformLayout();
            this.grpTrain.ResumeLayout(false);
            this.grpTrain.PerformLayout();
            this.grpMatch.ResumeLayout(false);
            this.grpMatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFace)).EndInit();
            this.grpParameters.ResumeLayout(false);
            this.grpParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMomentum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHidden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLearning)).EndInit();
            this.grpOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picGraffiti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEpochs;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Label lblEpochs;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Panel pnlDraw;
        private System.Windows.Forms.StatusStrip barState;
        private System.Windows.Forms.ToolStripStatusLabel txtMouse;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblProbability;
        private System.Windows.Forms.TextBox txtProbability;
        private System.Windows.Forms.Button btnLearn;
        private System.Windows.Forms.ToolStripStatusLabel txtState;
        private System.Windows.Forms.ToolStripProgressBar prgTrain;
        private System.Windows.Forms.GroupBox grpTrain;
        private System.Windows.Forms.GroupBox grpMatch;
        private System.Windows.Forms.PictureBox picFace;
        private System.Windows.Forms.GroupBox grpParameters;
        private System.Windows.Forms.Label lblLearning;
        private System.Windows.Forms.Label lblHidden;
        private System.Windows.Forms.NumericUpDown txtHidden;
        private System.Windows.Forms.NumericUpDown txtThreshold;
        private System.Windows.Forms.NumericUpDown txtLearning;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.Button btnRenew;
        private System.Windows.Forms.GroupBox grpOperation;
        private System.Windows.Forms.CheckBox chkMomentum;
        private System.Windows.Forms.NumericUpDown txtMomentum;
        private System.Windows.Forms.NumericUpDown txtNoise;
        private System.Windows.Forms.CheckBox chkNoise;
        private System.Windows.Forms.PictureBox picGraffiti;
        private System.Windows.Forms.Label txtGraffiti;
        private System.Windows.Forms.ToolStripStatusLabel txtCopyright;
    }
}

