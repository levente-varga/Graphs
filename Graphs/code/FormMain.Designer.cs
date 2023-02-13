
namespace Graphs
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
            this.bResetSamples = new System.Windows.Forms.Button();
            this.lAverageDegreeSamples = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lAverageDegreeValue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lEdgeCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelParameters = new System.Windows.Forms.Panel();
            this.bGenerateGraph = new System.Windows.Forms.Button();
            this.lMaxF = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.bAutoGenerateOnChange = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.bResetArrangement = new System.Windows.Forms.Button();
            this.bStretch = new System.Windows.Forms.Button();
            this.bPauseArrangement = new System.Windows.Forms.Button();
            this.bArrangement = new System.Windows.Forms.Button();
            this.bShowNodes = new System.Windows.Forms.Button();
            this.bGenerateSamples = new System.Windows.Forms.Button();
            this.bShowValues = new System.Windows.Forms.Button();
            this.bGradient = new System.Windows.Forms.Button();
            this.bSort = new System.Windows.Forms.Button();
            this.bShowDegree = new System.Windows.Forms.Button();
            this.pProgressBar = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bTabWattsStrogatz = new System.Windows.Forms.Button();
            this.bTabBarabasiAlbert = new System.Windows.Forms.Button();
            this.bTabErdosRenyi = new System.Windows.Forms.Button();
            this.bChart3 = new System.Windows.Forms.Button();
            this.panelGraph = new DoubleBufferedPanel();
            this.lVersion = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.panelParameters.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChart
            // 
            this.panelChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panelChart.Location = new System.Drawing.Point(30, 400);
            this.panelChart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(530, 250);
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
            this.bChart1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bChart1.Location = new System.Drawing.Point(30, 660);
            this.bChart1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bChart1.Name = "bChart1";
            this.bChart1.Size = new System.Drawing.Size(72, 30);
            this.bChart1.TabIndex = 14;
            this.bChart1.Text = "Degrees";
            this.bChart1.UseVisualStyleBackColor = false;
            this.bChart1.Click += new System.EventHandler(this.bChartDegrees_Click);
            // 
            // bChart2
            // 
            this.bChart2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bChart2.FlatAppearance.BorderSize = 0;
            this.bChart2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bChart2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bChart2.Location = new System.Drawing.Point(108, 660);
            this.bChart2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bChart2.Name = "bChart2";
            this.bChart2.Size = new System.Drawing.Size(147, 30);
            this.bChart2.TabIndex = 15;
            this.bChart2.Text = "Degree distribution";
            this.bChart2.UseVisualStyleBackColor = false;
            this.bChart2.Click += new System.EventHandler(this.bChartDistribution_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel2.Controls.Add(this.bResetSamples);
            this.panel2.Controls.Add(this.lAverageDegreeSamples);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.lAverageDegreeValue);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lEdgeCount);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(30, 320);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(15, 30, 30, 30);
            this.panel2.Size = new System.Drawing.Size(530, 50);
            this.panel2.TabIndex = 20;
            // 
            // bResetSamples
            // 
            this.bResetSamples.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bResetSamples.BackgroundImage = global::Graphs.Properties.Resources.reset_samples;
            this.bResetSamples.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bResetSamples.FlatAppearance.BorderSize = 0;
            this.bResetSamples.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bResetSamples.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bResetSamples.ForeColor = System.Drawing.Color.Transparent;
            this.bResetSamples.Location = new System.Drawing.Point(476, 13);
            this.bResetSamples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bResetSamples.Name = "bResetSamples";
            this.bResetSamples.Size = new System.Drawing.Size(25, 25);
            this.bResetSamples.TabIndex = 38;
            this.toolTip1.SetToolTip(this.bResetSamples, "Reset distribution samples");
            this.bResetSamples.UseVisualStyleBackColor = false;
            this.bResetSamples.Click += new System.EventHandler(this.bResetSamples_Click);
            // 
            // lAverageDegreeSamples
            // 
            this.lAverageDegreeSamples.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lAverageDegreeSamples.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lAverageDegreeSamples.Location = new System.Drawing.Point(427, 0);
            this.lAverageDegreeSamples.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lAverageDegreeSamples.Name = "lAverageDegreeSamples";
            this.lAverageDegreeSamples.Size = new System.Drawing.Size(45, 50);
            this.lAverageDegreeSamples.TabIndex = 5;
            this.lAverageDegreeSamples.Text = "-";
            this.lAverageDegreeSamples.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label9.Location = new System.Drawing.Point(303, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 50);
            this.label9.TabIndex = 4;
            this.label9.Text = "Distribution samples:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lAverageDegreeValue
            // 
            this.lAverageDegreeValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lAverageDegreeValue.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lAverageDegreeValue.Location = new System.Drawing.Point(262, 0);
            this.lAverageDegreeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lAverageDegreeValue.Name = "lAverageDegreeValue";
            this.lAverageDegreeValue.Size = new System.Drawing.Size(35, 50);
            this.lAverageDegreeValue.TabIndex = 3;
            this.lAverageDegreeValue.Text = "-";
            this.lAverageDegreeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(154, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 50);
            this.label8.TabIndex = 2;
            this.label8.Text = "Average degree:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lEdgeCount
            // 
            this.lEdgeCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lEdgeCount.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lEdgeCount.Location = new System.Drawing.Point(67, 0);
            this.lEdgeCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lEdgeCount.Name = "lEdgeCount";
            this.lEdgeCount.Size = new System.Drawing.Size(81, 50);
            this.lEdgeCount.TabIndex = 1;
            this.lEdgeCount.Text = "-";
            this.lEdgeCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(21, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 50);
            this.label6.TabIndex = 0;
            this.label6.Text = "Edges:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelParameters
            // 
            this.panelParameters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panelParameters.Controls.Add(this.bGenerateGraph);
            this.panelParameters.Controls.Add(this.lMaxF);
            this.panelParameters.Location = new System.Drawing.Point(30, 60);
            this.panelParameters.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelParameters.Name = "panelParameters";
            this.panelParameters.Size = new System.Drawing.Size(530, 160);
            this.panelParameters.TabIndex = 0;
            // 
            // bGenerateGraph
            // 
            this.bGenerateGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(189)))), ((int)(((byte)(0)))));
            this.bGenerateGraph.FlatAppearance.BorderSize = 0;
            this.bGenerateGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGenerateGraph.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bGenerateGraph.Location = new System.Drawing.Point(401, 30);
            this.bGenerateGraph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bGenerateGraph.Name = "bGenerateGraph";
            this.bGenerateGraph.Size = new System.Drawing.Size(100, 100);
            this.bGenerateGraph.TabIndex = 34;
            this.bGenerateGraph.Text = "Generate";
            this.bGenerateGraph.UseVisualStyleBackColor = false;
            this.bGenerateGraph.Click += new System.EventHandler(this.bGenerateGraph_Click);
            // 
            // lMaxF
            // 
            this.lMaxF.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lMaxF.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lMaxF.Location = new System.Drawing.Point(128, 0);
            this.lMaxF.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lMaxF.Name = "lMaxF";
            this.lMaxF.Size = new System.Drawing.Size(0, 0);
            this.lMaxF.TabIndex = 6;
            this.lMaxF.Text = "0";
            this.lMaxF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.panel5.Controls.Add(this.bAutoGenerateOnChange);
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.bResetArrangement);
            this.panel5.Controls.Add(this.bStretch);
            this.panel5.Controls.Add(this.bPauseArrangement);
            this.panel5.Controls.Add(this.bArrangement);
            this.panel5.Controls.Add(this.bShowNodes);
            this.panel5.Controls.Add(this.bGenerateSamples);
            this.panel5.Controls.Add(this.bShowValues);
            this.panel5.Controls.Add(this.bGradient);
            this.panel5.Controls.Add(this.bSort);
            this.panel5.Controls.Add(this.bShowDegree);
            this.panel5.Location = new System.Drawing.Point(30, 220);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(530, 65);
            this.panel5.TabIndex = 28;
            // 
            // bAutoGenerateOnChange
            // 
            this.bAutoGenerateOnChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bAutoGenerateOnChange.BackgroundImage = global::Graphs.Properties.Resources.auto_generate_on_change;
            this.bAutoGenerateOnChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bAutoGenerateOnChange.FlatAppearance.BorderSize = 0;
            this.bAutoGenerateOnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAutoGenerateOnChange.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bAutoGenerateOnChange.ForeColor = System.Drawing.Color.Transparent;
            this.bAutoGenerateOnChange.Location = new System.Drawing.Point(280, 22);
            this.bAutoGenerateOnChange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bAutoGenerateOnChange.Name = "bAutoGenerateOnChange";
            this.bAutoGenerateOnChange.Size = new System.Drawing.Size(25, 25);
            this.bAutoGenerateOnChange.TabIndex = 38;
            this.toolTip1.SetToolTip(this.bAutoGenerateOnChange, "Auto-generate graph when a parameter is changed");
            this.bAutoGenerateOnChange.UseVisualStyleBackColor = false;
            this.bAutoGenerateOnChange.Click += new System.EventHandler(this.bAutoGenerateOnChange_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel8.Location = new System.Drawing.Point(317, 22);
            this.panel8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1, 25);
            this.panel8.TabIndex = 30;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel7.Location = new System.Drawing.Point(237, 22);
            this.panel7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1, 25);
            this.panel7.TabIndex = 29;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel6.Location = new System.Drawing.Point(127, 22);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1, 25);
            this.panel6.TabIndex = 28;
            // 
            // bResetArrangement
            // 
            this.bResetArrangement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bResetArrangement.BackgroundImage = global::Graphs.Properties.Resources.reset;
            this.bResetArrangement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bResetArrangement.FlatAppearance.BorderSize = 0;
            this.bResetArrangement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bResetArrangement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bResetArrangement.ForeColor = System.Drawing.Color.Transparent;
            this.bResetArrangement.Location = new System.Drawing.Point(380, 22);
            this.bResetArrangement.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bResetArrangement.Name = "bResetArrangement";
            this.bResetArrangement.Size = new System.Drawing.Size(25, 25);
            this.bResetArrangement.TabIndex = 30;
            this.toolTip1.SetToolTip(this.bResetArrangement, "Reset arrangement");
            this.bResetArrangement.UseVisualStyleBackColor = false;
            this.bResetArrangement.Click += new System.EventHandler(this.bResetArrangement_Click);
            // 
            // bStretch
            // 
            this.bStretch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bStretch.BackgroundImage = global::Graphs.Properties.Resources.stretch_chart;
            this.bStretch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bStretch.FlatAppearance.BorderSize = 0;
            this.bStretch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStretch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bStretch.ForeColor = System.Drawing.Color.Transparent;
            this.bStretch.Location = new System.Drawing.Point(140, 22);
            this.bStretch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bStretch.Name = "bStretch";
            this.bStretch.Size = new System.Drawing.Size(25, 25);
            this.bStretch.TabIndex = 37;
            this.toolTip1.SetToolTip(this.bStretch, "Stretch chart vertically");
            this.bStretch.UseVisualStyleBackColor = false;
            this.bStretch.Click += new System.EventHandler(this.bStretch_Click);
            // 
            // bPauseArrangement
            // 
            this.bPauseArrangement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bPauseArrangement.BackgroundImage = global::Graphs.Properties.Resources.pause;
            this.bPauseArrangement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bPauseArrangement.FlatAppearance.BorderSize = 0;
            this.bPauseArrangement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPauseArrangement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bPauseArrangement.ForeColor = System.Drawing.Color.Transparent;
            this.bPauseArrangement.Location = new System.Drawing.Point(355, 22);
            this.bPauseArrangement.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bPauseArrangement.Name = "bPauseArrangement";
            this.bPauseArrangement.Size = new System.Drawing.Size(25, 25);
            this.bPauseArrangement.TabIndex = 29;
            this.toolTip1.SetToolTip(this.bPauseArrangement, "Pause/resume arrangement");
            this.bPauseArrangement.UseVisualStyleBackColor = false;
            this.bPauseArrangement.Click += new System.EventHandler(this.bPauseArrangement_Click);
            // 
            // bArrangement
            // 
            this.bArrangement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bArrangement.BackgroundImage = global::Graphs.Properties.Resources.arrange;
            this.bArrangement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bArrangement.FlatAppearance.BorderSize = 0;
            this.bArrangement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bArrangement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bArrangement.ForeColor = System.Drawing.Color.Transparent;
            this.bArrangement.Location = new System.Drawing.Point(330, 22);
            this.bArrangement.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bArrangement.Name = "bArrangement";
            this.bArrangement.Size = new System.Drawing.Size(25, 25);
            this.bArrangement.TabIndex = 36;
            this.toolTip1.SetToolTip(this.bArrangement, "Arrange graph");
            this.bArrangement.UseVisualStyleBackColor = false;
            this.bArrangement.Click += new System.EventHandler(this.bArrangement_Click);
            // 
            // bShowNodes
            // 
            this.bShowNodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bShowNodes.BackgroundImage = global::Graphs.Properties.Resources.show_nodes;
            this.bShowNodes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bShowNodes.FlatAppearance.BorderSize = 0;
            this.bShowNodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bShowNodes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bShowNodes.ForeColor = System.Drawing.Color.Transparent;
            this.bShowNodes.Location = new System.Drawing.Point(30, 22);
            this.bShowNodes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bShowNodes.Name = "bShowNodes";
            this.bShowNodes.Size = new System.Drawing.Size(25, 25);
            this.bShowNodes.TabIndex = 35;
            this.toolTip1.SetToolTip(this.bShowNodes, "Show nodes");
            this.bShowNodes.UseVisualStyleBackColor = false;
            this.bShowNodes.Click += new System.EventHandler(this.bShowNodes_Click);
            // 
            // bGenerateSamples
            // 
            this.bGenerateSamples.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bGenerateSamples.BackgroundImage = global::Graphs.Properties.Resources.generate_samples;
            this.bGenerateSamples.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bGenerateSamples.FlatAppearance.BorderSize = 0;
            this.bGenerateSamples.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGenerateSamples.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bGenerateSamples.ForeColor = System.Drawing.Color.Transparent;
            this.bGenerateSamples.Location = new System.Drawing.Point(250, 22);
            this.bGenerateSamples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bGenerateSamples.Name = "bGenerateSamples";
            this.bGenerateSamples.Size = new System.Drawing.Size(25, 25);
            this.bGenerateSamples.TabIndex = 34;
            this.toolTip1.SetToolTip(this.bGenerateSamples, "Auto-generate graphs");
            this.bGenerateSamples.UseVisualStyleBackColor = false;
            this.bGenerateSamples.Click += new System.EventHandler(this.bGenerateSamples_Click);
            // 
            // bShowValues
            // 
            this.bShowValues.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bShowValues.BackgroundImage = global::Graphs.Properties.Resources.show_chart_values;
            this.bShowValues.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bShowValues.FlatAppearance.BorderSize = 0;
            this.bShowValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bShowValues.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bShowValues.ForeColor = System.Drawing.Color.Transparent;
            this.bShowValues.Location = new System.Drawing.Point(170, 22);
            this.bShowValues.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bShowValues.Name = "bShowValues";
            this.bShowValues.Size = new System.Drawing.Size(25, 25);
            this.bShowValues.TabIndex = 33;
            this.toolTip1.SetToolTip(this.bShowValues, "Show chart values");
            this.bShowValues.UseVisualStyleBackColor = false;
            this.bShowValues.Click += new System.EventHandler(this.bShowValues_Click);
            // 
            // bGradient
            // 
            this.bGradient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bGradient.BackgroundImage = global::Graphs.Properties.Resources.gradient;
            this.bGradient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bGradient.FlatAppearance.BorderSize = 0;
            this.bGradient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGradient.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bGradient.ForeColor = System.Drawing.Color.Transparent;
            this.bGradient.Location = new System.Drawing.Point(60, 22);
            this.bGradient.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bGradient.Name = "bGradient";
            this.bGradient.Size = new System.Drawing.Size(25, 25);
            this.bGradient.TabIndex = 32;
            this.toolTip1.SetToolTip(this.bGradient, "Use gradient edge coloring");
            this.bGradient.UseVisualStyleBackColor = false;
            this.bGradient.Click += new System.EventHandler(this.bGradient_Click);
            // 
            // bSort
            // 
            this.bSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bSort.BackgroundImage = global::Graphs.Properties.Resources.sort_chart;
            this.bSort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bSort.FlatAppearance.BorderSize = 0;
            this.bSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bSort.ForeColor = System.Drawing.Color.Transparent;
            this.bSort.Location = new System.Drawing.Point(200, 22);
            this.bSort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bSort.Name = "bSort";
            this.bSort.Size = new System.Drawing.Size(25, 25);
            this.bSort.TabIndex = 31;
            this.toolTip1.SetToolTip(this.bSort, "Sort chart is descending order");
            this.bSort.UseVisualStyleBackColor = false;
            this.bSort.Click += new System.EventHandler(this.bSort_Click);
            // 
            // bShowDegree
            // 
            this.bShowDegree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.bShowDegree.BackgroundImage = global::Graphs.Properties.Resources.show_degrees;
            this.bShowDegree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bShowDegree.FlatAppearance.BorderSize = 0;
            this.bShowDegree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bShowDegree.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bShowDegree.ForeColor = System.Drawing.Color.Transparent;
            this.bShowDegree.Location = new System.Drawing.Point(90, 22);
            this.bShowDegree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bShowDegree.Name = "bShowDegree";
            this.bShowDegree.Size = new System.Drawing.Size(25, 25);
            this.bShowDegree.TabIndex = 30;
            this.toolTip1.SetToolTip(this.bShowDegree, "Show node degrees");
            this.bShowDegree.UseVisualStyleBackColor = false;
            this.bShowDegree.Click += new System.EventHandler(this.bShowDegree_Click);
            // 
            // pProgressBar
            // 
            this.pProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.pProgressBar.Location = new System.Drawing.Point(30, 285);
            this.pProgressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pProgressBar.Name = "pProgressBar";
            this.pProgressBar.Size = new System.Drawing.Size(530, 5);
            this.pProgressBar.TabIndex = 27;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bTabWattsStrogatz);
            this.splitContainer1.Panel1.Controls.Add(this.bTabBarabasiAlbert);
            this.splitContainer1.Panel1.Controls.Add(this.bTabErdosRenyi);
            this.splitContainer1.Panel1.Controls.Add(this.panel5);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.bChart3);
            this.splitContainer1.Panel1.Controls.Add(this.pProgressBar);
            this.splitContainer1.Panel1.Controls.Add(this.bChart2);
            this.splitContainer1.Panel1.Controls.Add(this.panelChart);
            this.splitContainer1.Panel1.Controls.Add(this.bChart1);
            this.splitContainer1.Panel1.Controls.Add(this.panelParameters);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.splitContainer1.Panel2.Controls.Add(this.panelGraph);
            this.splitContainer1.Panel2.Controls.Add(this.lVersion);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(29, 30, 30, 30);
            this.splitContainer1.Size = new System.Drawing.Size(1280, 720);
            this.splitContainer1.SplitterDistance = 560;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 21;
            // 
            // bTabWattsStrogatz
            // 
            this.bTabWattsStrogatz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bTabWattsStrogatz.FlatAppearance.BorderSize = 0;
            this.bTabWattsStrogatz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTabWattsStrogatz.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bTabWattsStrogatz.Location = new System.Drawing.Point(160, 30);
            this.bTabWattsStrogatz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bTabWattsStrogatz.Name = "bTabWattsStrogatz";
            this.bTabWattsStrogatz.Size = new System.Drawing.Size(60, 30);
            this.bTabWattsStrogatz.TabIndex = 36;
            this.bTabWattsStrogatz.Text = "WS";
            this.bTabWattsStrogatz.UseVisualStyleBackColor = false;
            this.bTabWattsStrogatz.Click += new System.EventHandler(this.buttonTabWattsStrogatz_Click);
            // 
            // bTabBarabasiAlbert
            // 
            this.bTabBarabasiAlbert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bTabBarabasiAlbert.FlatAppearance.BorderSize = 0;
            this.bTabBarabasiAlbert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTabBarabasiAlbert.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bTabBarabasiAlbert.Location = new System.Drawing.Point(95, 30);
            this.bTabBarabasiAlbert.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bTabBarabasiAlbert.Name = "bTabBarabasiAlbert";
            this.bTabBarabasiAlbert.Size = new System.Drawing.Size(60, 30);
            this.bTabBarabasiAlbert.TabIndex = 35;
            this.bTabBarabasiAlbert.Text = "BA";
            this.bTabBarabasiAlbert.UseVisualStyleBackColor = false;
            this.bTabBarabasiAlbert.Click += new System.EventHandler(this.buttonTabBarabasiAlbert_Click);
            // 
            // bTabErdosRenyi
            // 
            this.bTabErdosRenyi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bTabErdosRenyi.FlatAppearance.BorderSize = 0;
            this.bTabErdosRenyi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTabErdosRenyi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bTabErdosRenyi.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bTabErdosRenyi.Location = new System.Drawing.Point(30, 30);
            this.bTabErdosRenyi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bTabErdosRenyi.Name = "bTabErdosRenyi";
            this.bTabErdosRenyi.Size = new System.Drawing.Size(60, 30);
            this.bTabErdosRenyi.TabIndex = 34;
            this.bTabErdosRenyi.Text = "ER";
            this.bTabErdosRenyi.UseVisualStyleBackColor = false;
            this.bTabErdosRenyi.Click += new System.EventHandler(this.buttonTabErdosRenyi_Click);
            // 
            // bChart3
            // 
            this.bChart3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.bChart3.FlatAppearance.BorderSize = 0;
            this.bChart3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bChart3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bChart3.Location = new System.Drawing.Point(261, 660);
            this.bChart3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bChart3.Name = "bChart3";
            this.bChart3.Size = new System.Drawing.Size(207, 30);
            this.bChart3.TabIndex = 21;
            this.bChart3.Text = "Average degree distribution";
            this.bChart3.UseVisualStyleBackColor = false;
            this.bChart3.Click += new System.EventHandler(this.bChartAverageDistribution_Click);
            // 
            // panelGraph
            // 
            this.panelGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGraph.Location = new System.Drawing.Point(30, 30);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
            this.lVersion.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lVersion.Location = new System.Drawing.Point(619, 700);
            this.lVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lVersion.Name = "lVersion";
            this.lVersion.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lVersion.Size = new System.Drawing.Size(100, 20);
            this.lVersion.TabIndex = 9;
            this.lVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(606, 759);
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphs";
            this.ResizeEnd += new System.EventHandler(this.Form_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.Form_SizeChanged);
            this.panel2.ResumeLayout(false);
            this.panelParameters.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelParameters;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.Button bChart3;
        private System.Windows.Forms.Label lAverageDegreeSamples;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lMaxF;
        private System.Windows.Forms.Panel pProgressBar;
        private System.Windows.Forms.Timer timer;
        private DoubleBufferedPanel panelGraph;
        private System.Windows.Forms.Button bPauseArrangement;
        private System.Windows.Forms.Button bResetArrangement;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button bShowDegree;
        private System.Windows.Forms.Button bStretch;
        private System.Windows.Forms.Button bArrangement;
        private System.Windows.Forms.Button bShowNodes;
        private System.Windows.Forms.Button bGenerateSamples;
        private System.Windows.Forms.Button bShowValues;
        private System.Windows.Forms.Button bGradient;
        private System.Windows.Forms.Button bSort;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button bResetSamples;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button bGenerateGraph;
        private System.Windows.Forms.Button bTabErdosRenyi;
        private System.Windows.Forms.Button bTabWattsStrogatz;
        private System.Windows.Forms.Button bTabBarabasiAlbert;
        private System.Windows.Forms.Button bAutoGenerateOnChange;
    }
}

