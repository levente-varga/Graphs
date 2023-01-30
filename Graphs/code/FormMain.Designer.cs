
namespace Graphs_Framework
{
    partial class FormMain
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelChart = new System.Windows.Forms.Panel();
            this.bChart1 = new System.Windows.Forms.Button();
            this.bChart2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lAverageDegreeSamples = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lAverageDegreeValue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lEdgeCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bGenerateSamples = new System.Windows.Forms.Button();
            this.pProgressBar = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.trackBarPower = new System.Windows.Forms.TrackBar();
            this.trackBarProbability = new System.Windows.Forms.TrackBar();
            this.trackBarNodes = new System.Windows.Forms.TrackBar();
            this.lValueOfProbability = new System.Windows.Forms.TextBox();
            this.lValueOfPower = new System.Windows.Forms.TextBox();
            this.lMaxF = new System.Windows.Forms.Label();
            this.bShowValues = new System.Windows.Forms.Button();
            this.bShowNodes = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bSort = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.bShowDegree = new System.Windows.Forms.Button();
            this.bGradient = new System.Windows.Forms.Button();
            this.bStretch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lValueOfNodes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bPopularity = new System.Windows.Forms.Button();
            this.bRandom = new System.Windows.Forms.Button();
            this.bArrangement = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bChart3 = new System.Windows.Forms.Button();
            this.panelGraph = new DoubleBufferedPanel();
            this.lVersion = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProbability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChart
            // 
            this.panelChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panelChart.Location = new System.Drawing.Point(30, 450);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(530, 200);
            this.panelChart.TabIndex = 9;
            this.panelChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseDown);
            this.panelChart.MouseEnter += new System.EventHandler(this.panelChart_MouseEnter);
            this.panelChart.MouseLeave += new System.EventHandler(this.panelChart_MouseLeave);
            this.panelChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseMove);
            this.panelChart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelChart_MouseUp);
            // 
            // bChart1
            // 
            this.bChart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(189)))), ((int)(((byte)(0)))));
            this.bChart1.FlatAppearance.BorderSize = 0;
            this.bChart1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bChart1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.bChart1.Location = new System.Drawing.Point(30, 660);
            this.bChart1.Name = "bChart1";
            this.bChart1.Size = new System.Drawing.Size(92, 30);
            this.bChart1.TabIndex = 14;
            this.bChart1.Text = "Fokszámok";
            this.bChart1.UseVisualStyleBackColor = false;
            this.bChart1.Click += new System.EventHandler(this.bChart1_Click);
            // 
            // bChart2
            // 
            this.bChart2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bChart2.FlatAppearance.BorderSize = 0;
            this.bChart2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bChart2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.bChart2.Location = new System.Drawing.Point(132, 660);
            this.bChart2.Name = "bChart2";
            this.bChart2.Size = new System.Drawing.Size(127, 30);
            this.bChart2.TabIndex = 15;
            this.bChart2.Text = "Fokszámeloszlás";
            this.bChart2.UseVisualStyleBackColor = false;
            this.bChart2.Click += new System.EventHandler(this.bChart2_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel2.Controls.Add(this.lAverageDegreeSamples);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.lAverageDegreeValue);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lEdgeCount);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(30, 370);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(13, 26, 26, 26);
            this.panel2.Size = new System.Drawing.Size(530, 50);
            this.panel2.TabIndex = 20;
            // 
            // lAverageDegreeSamples
            // 
            this.lAverageDegreeSamples.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lAverageDegreeSamples.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lAverageDegreeSamples.Location = new System.Drawing.Point(465, 0);
            this.lAverageDegreeSamples.Name = "lAverageDegreeSamples";
            this.lAverageDegreeSamples.Size = new System.Drawing.Size(45, 50);
            this.lAverageDegreeSamples.TabIndex = 5;
            this.lAverageDegreeSamples.Text = "-";
            this.lAverageDegreeSamples.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label9.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label9.Location = new System.Drawing.Point(350, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 50);
            this.label9.TabIndex = 4;
            this.label9.Text = "Eloszlás minták:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lAverageDegreeValue
            // 
            this.lAverageDegreeValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lAverageDegreeValue.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lAverageDegreeValue.Location = new System.Drawing.Point(310, 0);
            this.lAverageDegreeValue.Name = "lAverageDegreeValue";
            this.lAverageDegreeValue.Size = new System.Drawing.Size(35, 50);
            this.lAverageDegreeValue.TabIndex = 3;
            this.lAverageDegreeValue.Text = "-";
            this.lAverageDegreeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(190, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 50);
            this.label8.TabIndex = 2;
            this.label8.Text = "Átlagos fokszám:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lEdgeCount
            // 
            this.lEdgeCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lEdgeCount.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lEdgeCount.Location = new System.Drawing.Point(65, 0);
            this.lEdgeCount.Name = "lEdgeCount";
            this.lEdgeCount.Size = new System.Drawing.Size(120, 50);
            this.lEdgeCount.TabIndex = 1;
            this.lEdgeCount.Text = "-";
            this.lEdgeCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(20, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 50);
            this.label6.TabIndex = 0;
            this.label6.Text = "Élek:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel1.Controls.Add(this.bGenerateSamples);
            this.panel1.Controls.Add(this.pProgressBar);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.trackBarPower);
            this.panel1.Controls.Add(this.trackBarProbability);
            this.panel1.Controls.Add(this.trackBarNodes);
            this.panel1.Controls.Add(this.lValueOfProbability);
            this.panel1.Controls.Add(this.lValueOfPower);
            this.panel1.Controls.Add(this.lMaxF);
            this.panel1.Controls.Add(this.bShowValues);
            this.panel1.Controls.Add(this.bShowNodes);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.bSort);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.bShowDegree);
            this.panel1.Controls.Add(this.bGradient);
            this.panel1.Controls.Add(this.bStretch);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lValueOfNodes);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.bPopularity);
            this.panel1.Controls.Add(this.bRandom);
            this.panel1.Controls.Add(this.bArrangement);
            this.panel1.Location = new System.Drawing.Point(30, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 310);
            this.panel1.TabIndex = 0;
            // 
            // bGenerateSamples
            // 
            this.bGenerateSamples.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bGenerateSamples.FlatAppearance.BorderSize = 0;
            this.bGenerateSamples.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGenerateSamples.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bGenerateSamples.ForeColor = System.Drawing.Color.White;
            this.bGenerateSamples.Location = new System.Drawing.Point(98, 195);
            this.bGenerateSamples.Name = "bGenerateSamples";
            this.bGenerateSamples.Size = new System.Drawing.Size(116, 25);
            this.bGenerateSamples.TabIndex = 28;
            this.bGenerateSamples.Text = "Minták generálása";
            this.bGenerateSamples.UseVisualStyleBackColor = false;
            this.bGenerateSamples.Click += new System.EventHandler(this.bGenerateSamples_Click);
            // 
            // pProgressBar
            // 
            this.pProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.pProgressBar.Location = new System.Drawing.Point(0, 305);
            this.pProgressBar.Name = "pProgressBar";
            this.pProgressBar.Size = new System.Drawing.Size(530, 5);
            this.pProgressBar.TabIndex = 27;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(189)))), ((int)(((byte)(0)))));
            this.panel4.Location = new System.Drawing.Point(115, 125);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(35, 3);
            this.panel4.TabIndex = 26;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(229)))), ((int)(((byte)(129)))));
            this.panel3.Location = new System.Drawing.Point(115, 165);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(35, 3);
            this.panel3.TabIndex = 25;
            // 
            // trackBarPower
            // 
            this.trackBarPower.LargeChange = 1;
            this.trackBarPower.Location = new System.Drawing.Point(156, 145);
            this.trackBarPower.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarPower.Maximum = 20;
            this.trackBarPower.Name = "trackBarPower";
            this.trackBarPower.Size = new System.Drawing.Size(200, 45);
            this.trackBarPower.TabIndex = 4;
            this.trackBarPower.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarPower.Value = 10;
            this.trackBarPower.Scroll += new System.EventHandler(this.trackBarPower_Scroll);
            // 
            // trackBarProbability
            // 
            this.trackBarProbability.LargeChange = 1;
            this.trackBarProbability.Location = new System.Drawing.Point(156, 105);
            this.trackBarProbability.Maximum = 50;
            this.trackBarProbability.Name = "trackBarProbability";
            this.trackBarProbability.Size = new System.Drawing.Size(200, 45);
            this.trackBarProbability.TabIndex = 6;
            this.trackBarProbability.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarProbability.Value = 25;
            this.trackBarProbability.Scroll += new System.EventHandler(this.trackBarProbability_Scroll);
            // 
            // trackBarNodes
            // 
            this.trackBarNodes.LargeChange = 1;
            this.trackBarNodes.Location = new System.Drawing.Point(156, 65);
            this.trackBarNodes.Maximum = 100;
            this.trackBarNodes.Minimum = 3;
            this.trackBarNodes.Name = "trackBarNodes";
            this.trackBarNodes.Size = new System.Drawing.Size(200, 45);
            this.trackBarNodes.TabIndex = 2;
            this.trackBarNodes.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarNodes.Value = 13;
            this.trackBarNodes.Scroll += new System.EventHandler(this.trackBarNodes_Scroll);
            // 
            // lValueOfProbability
            // 
            this.lValueOfProbability.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lValueOfProbability.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lValueOfProbability.CausesValidation = false;
            this.lValueOfProbability.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lValueOfProbability.ForeColor = System.Drawing.Color.White;
            this.lValueOfProbability.Location = new System.Drawing.Point(115, 107);
            this.lValueOfProbability.Name = "lValueOfProbability";
            this.lValueOfProbability.Size = new System.Drawing.Size(35, 18);
            this.lValueOfProbability.TabIndex = 5;
            this.lValueOfProbability.Text = "0.50";
            this.lValueOfProbability.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lValueOfProbability.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lValueOfProbability_KeyDown);
            // 
            // lValueOfPower
            // 
            this.lValueOfPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lValueOfPower.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lValueOfPower.CausesValidation = false;
            this.lValueOfPower.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lValueOfPower.ForeColor = System.Drawing.Color.White;
            this.lValueOfPower.Location = new System.Drawing.Point(115, 147);
            this.lValueOfPower.Name = "lValueOfPower";
            this.lValueOfPower.Size = new System.Drawing.Size(35, 18);
            this.lValueOfPower.TabIndex = 3;
            this.lValueOfPower.Text = "1.00";
            this.lValueOfPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lValueOfPower.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lValueOfPower_KeyDown);
            // 
            // lMaxF
            // 
            this.lMaxF.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lMaxF.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lMaxF.Location = new System.Drawing.Point(110, 0);
            this.lMaxF.Name = "lMaxF";
            this.lMaxF.Size = new System.Drawing.Size(0, 0);
            this.lMaxF.TabIndex = 6;
            this.lMaxF.Text = "0";
            this.lMaxF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bShowValues
            // 
            this.bShowValues.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bShowValues.FlatAppearance.BorderSize = 0;
            this.bShowValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bShowValues.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bShowValues.ForeColor = System.Drawing.Color.White;
            this.bShowValues.Location = new System.Drawing.Point(30, 255);
            this.bShowValues.Name = "bShowValues";
            this.bShowValues.Size = new System.Drawing.Size(63, 25);
            this.bShowValues.TabIndex = 21;
            this.bShowValues.Text = "Értékek";
            this.bShowValues.UseVisualStyleBackColor = false;
            this.bShowValues.Click += new System.EventHandler(this.bShowValues_Click);
            // 
            // bShowNodes
            // 
            this.bShowNodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.bShowNodes.FlatAppearance.BorderSize = 0;
            this.bShowNodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bShowNodes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bShowNodes.ForeColor = System.Drawing.Color.White;
            this.bShowNodes.Location = new System.Drawing.Point(30, 225);
            this.bShowNodes.Name = "bShowNodes";
            this.bShowNodes.Size = new System.Drawing.Size(113, 25);
            this.bShowNodes.TabIndex = 7;
            this.bShowNodes.Text = "Csúcsok mutatása";
            this.bShowNodes.UseVisualStyleBackColor = false;
            this.bShowNodes.Click += new System.EventHandler(this.bShowNodes_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(366, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 30);
            this.label5.TabIndex = 14;
            this.label5.Text = "Generálás";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(26, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "Paraméterek";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bSort
            // 
            this.bSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bSort.FlatAppearance.BorderSize = 0;
            this.bSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bSort.ForeColor = System.Drawing.Color.White;
            this.bSort.Location = new System.Drawing.Point(219, 255);
            this.bSort.Name = "bSort";
            this.bSort.Size = new System.Drawing.Size(129, 25);
            this.bSort.TabIndex = 11;
            this.bSort.Text = "Grafikon rendezése";
            this.bSort.UseVisualStyleBackColor = false;
            this.bSort.Click += new System.EventHandler(this.bSort_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(36, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 30);
            this.label3.TabIndex = 20;
            this.label3.Text = "Érdeklődés";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bShowDegree
            // 
            this.bShowDegree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bShowDegree.FlatAppearance.BorderSize = 0;
            this.bShowDegree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bShowDegree.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bShowDegree.ForeColor = System.Drawing.Color.White;
            this.bShowDegree.Location = new System.Drawing.Point(148, 225);
            this.bShowDegree.Name = "bShowDegree";
            this.bShowDegree.Size = new System.Drawing.Size(131, 25);
            this.bShowDegree.TabIndex = 8;
            this.bShowDegree.Text = "Fokszámok mutatása";
            this.bShowDegree.UseVisualStyleBackColor = false;
            this.bShowDegree.Click += new System.EventHandler(this.bShowDegree_Click);
            // 
            // bGradient
            // 
            this.bGradient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bGradient.FlatAppearance.BorderSize = 0;
            this.bGradient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGradient.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bGradient.ForeColor = System.Drawing.Color.White;
            this.bGradient.Location = new System.Drawing.Point(284, 225);
            this.bGradient.Name = "bGradient";
            this.bGradient.Size = new System.Drawing.Size(64, 25);
            this.bGradient.TabIndex = 9;
            this.bGradient.Text = "Gradiens";
            this.bGradient.UseVisualStyleBackColor = false;
            this.bGradient.Click += new System.EventHandler(this.bGradient_Click);
            // 
            // bStretch
            // 
            this.bStretch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bStretch.FlatAppearance.BorderSize = 0;
            this.bStretch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStretch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bStretch.ForeColor = System.Drawing.Color.White;
            this.bStretch.Location = new System.Drawing.Point(98, 255);
            this.bStretch.Name = "bStretch";
            this.bStretch.Size = new System.Drawing.Size(116, 25);
            this.bStretch.TabIndex = 10;
            this.bStretch.Text = "Grafikon nyújtása";
            this.bStretch.UseVisualStyleBackColor = false;
            this.bStretch.Click += new System.EventHandler(this.bStretch_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(26, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Extrák";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lValueOfNodes
            // 
            this.lValueOfNodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lValueOfNodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lValueOfNodes.CausesValidation = false;
            this.lValueOfNodes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lValueOfNodes.ForeColor = System.Drawing.Color.White;
            this.lValueOfNodes.Location = new System.Drawing.Point(115, 67);
            this.lValueOfNodes.Name = "lValueOfNodes";
            this.lValueOfNodes.Size = new System.Drawing.Size(35, 18);
            this.lValueOfNodes.TabIndex = 1;
            this.lValueOfNodes.Text = "13";
            this.lValueOfNodes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lValueOfNodes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lValueOfNodes_KeyDown);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(36, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 30);
            this.label2.TabIndex = 7;
            this.label2.Text = "Élsűrűség";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(36, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "Csúcsok";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bPopularity
            // 
            this.bPopularity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(229)))), ((int)(((byte)(129)))));
            this.bPopularity.FlatAppearance.BorderSize = 0;
            this.bPopularity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPopularity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.bPopularity.Location = new System.Drawing.Point(370, 230);
            this.bPopularity.Name = "bPopularity";
            this.bPopularity.Size = new System.Drawing.Size(130, 50);
            this.bPopularity.TabIndex = 13;
            this.bPopularity.Text = "Népszerűségi";
            this.bPopularity.UseVisualStyleBackColor = false;
            this.bPopularity.Click += new System.EventHandler(this.buttonGenPop_Click);
            // 
            // bRandom
            // 
            this.bRandom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(189)))), ((int)(((byte)(0)))));
            this.bRandom.FlatAppearance.BorderSize = 0;
            this.bRandom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRandom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.bRandom.Location = new System.Drawing.Point(370, 170);
            this.bRandom.Name = "bRandom";
            this.bRandom.Size = new System.Drawing.Size(130, 50);
            this.bRandom.TabIndex = 0;
            this.bRandom.Text = "Véletlenszerű";
            this.bRandom.UseVisualStyleBackColor = false;
            this.bRandom.Click += new System.EventHandler(this.buttonGenRnd_Click);
            // 
            // bArrangement
            // 
            this.bArrangement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bArrangement.FlatAppearance.BorderSize = 0;
            this.bArrangement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bArrangement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bArrangement.ForeColor = System.Drawing.Color.White;
            this.bArrangement.Location = new System.Drawing.Point(219, 195);
            this.bArrangement.Name = "bArrangement";
            this.bArrangement.Size = new System.Drawing.Size(129, 25);
            this.bArrangement.TabIndex = 22;
            this.bArrangement.Text = "Hálózati elrendezés";
            this.bArrangement.UseVisualStyleBackColor = false;
            this.bArrangement.Click += new System.EventHandler(this.bArrangement_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bChart3);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.bChart2);
            this.splitContainer1.Panel1.Controls.Add(this.panelChart);
            this.splitContainer1.Panel1.Controls.Add(this.bChart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.splitContainer1.Panel2.Controls.Add(this.panelGraph);
            this.splitContainer1.Panel2.Controls.Add(this.lVersion);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(25, 26, 26, 26);
            this.splitContainer1.Size = new System.Drawing.Size(1280, 720);
            this.splitContainer1.SplitterDistance = 560;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 21;
            // 
            // bChart3
            // 
            this.bChart3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bChart3.FlatAppearance.BorderSize = 0;
            this.bChart3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bChart3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.bChart3.Location = new System.Drawing.Point(269, 660);
            this.bChart3.Name = "bChart3";
            this.bChart3.Size = new System.Drawing.Size(180, 30);
            this.bChart3.TabIndex = 21;
            this.bChart3.Text = "Átlagos fokszámeloszlás";
            this.bChart3.UseVisualStyleBackColor = false;
            this.bChart3.Click += new System.EventHandler(this.bChart3_Click);
            // 
            // panelGraph
            // 
            this.panelGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGraph.Location = new System.Drawing.Point(30, 30);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(660, 660);
            this.panelGraph.TabIndex = 11;
            this.panelGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelGraph_MouseDown);
            this.panelGraph.MouseEnter += new System.EventHandler(this.panelGraph_MouseEnter);
            this.panelGraph.MouseLeave += new System.EventHandler(this.panelGraph_MouseLeave);
            this.panelGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelGraph_MouseMove);
            this.panelGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelGraph_MouseUp);
            // 
            // lVersion
            // 
            this.lVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lVersion.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lVersion.Location = new System.Drawing.Point(633, 703);
            this.lVersion.Name = "lVersion";
            this.lVersion.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lVersion.Size = new System.Drawing.Size(86, 17);
            this.lVersion.TabIndex = 9;
            this.lVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(606, 759);
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphs";
            this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProbability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNodes)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Button bChart1;
        private System.Windows.Forms.Button bChart2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lAverageDegreeValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lEdgeCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bSort;
        private System.Windows.Forms.TrackBar trackBarProbability;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lValueOfPower;
        private System.Windows.Forms.TrackBar trackBarPower;
        private System.Windows.Forms.Button bShowDegree;
        private System.Windows.Forms.Button bGradient;
        private System.Windows.Forms.Button bStretch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lValueOfProbability;
        private System.Windows.Forms.TextBox lValueOfNodes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bPopularity;
        private System.Windows.Forms.Button bRandom;
        private System.Windows.Forms.TrackBar trackBarNodes;
        private System.Windows.Forms.Button bShowNodes;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button bShowValues;
        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.Button bChart3;
        private System.Windows.Forms.Label lAverageDegreeSamples;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lMaxF;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bArrangement;
        private System.Windows.Forms.Panel pProgressBar;
        private System.Windows.Forms.Timer timer;
        private DoubleBufferedPanel panelGraph;
        private System.Windows.Forms.Button bGenerateSamples;
    }
}

