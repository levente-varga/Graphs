
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
            components = new System.ComponentModel.Container();
            panelChart = new System.Windows.Forms.Panel();
            bChartDegrees = new System.Windows.Forms.Button();
            bChartDegreeDistribution = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            bResetSamples = new System.Windows.Forms.Button();
            lAverageDegreeSamples = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            lAverageDegreeValue = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            lEdgeCount = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            panelParameters = new System.Windows.Forms.Panel();
            bGenerateGraph = new System.Windows.Forms.Button();
            lMaxF = new System.Windows.Forms.Label();
            panel5 = new System.Windows.Forms.Panel();
            bAutoGenerateOnChange = new System.Windows.Forms.Button();
            panel8 = new System.Windows.Forms.Panel();
            panel7 = new System.Windows.Forms.Panel();
            panel6 = new System.Windows.Forms.Panel();
            bResetArrangement = new System.Windows.Forms.Button();
            bStretch = new System.Windows.Forms.Button();
            bPauseArrangement = new System.Windows.Forms.Button();
            bArrangement = new System.Windows.Forms.Button();
            bShowNodes = new System.Windows.Forms.Button();
            bGenerateSamples = new System.Windows.Forms.Button();
            bShowValues = new System.Windows.Forms.Button();
            bGradient = new System.Windows.Forms.Button();
            bSort = new System.Windows.Forms.Button();
            bShowDegree = new System.Windows.Forms.Button();
            pProgressBar = new System.Windows.Forms.Panel();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            bTabBianconiBarabasi = new System.Windows.Forms.Button();
            bTabWattsStrogatz = new System.Windows.Forms.Button();
            bTabBarabasiAlbert = new System.Windows.Forms.Button();
            bTabErdosRenyi = new System.Windows.Forms.Button();
            bChartAverageDegreeDistribution = new System.Windows.Forms.Button();
            panelGraph = new DoubleBufferedPanel();
            lVersion = new System.Windows.Forms.Label();
            timer = new System.Windows.Forms.Timer(components);
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            panel2.SuspendLayout();
            panelParameters.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // panelChart
            // 
            panelChart.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            panelChart.Location = new System.Drawing.Point(30, 370);
            panelChart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelChart.Name = "panelChart";
            panelChart.Size = new System.Drawing.Size(530, 280);
            panelChart.TabIndex = 9;
            panelChart.MouseDown += panelChart_MouseDown;
            panelChart.MouseEnter += panelChart_MouseEnter;
            panelChart.MouseLeave += panelChart_MouseLeave;
            panelChart.MouseMove += panelChart_MouseMove;
            panelChart.MouseUp += panelChart_MouseUp;
            // 
            // bChartDegrees
            // 
            bChartDegrees.BackColor = System.Drawing.Color.FromArgb(255, 189, 0);
            bChartDegrees.FlatAppearance.BorderSize = 0;
            bChartDegrees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bChartDegrees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bChartDegrees.Location = new System.Drawing.Point(30, 660);
            bChartDegrees.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bChartDegrees.Name = "bChartDegrees";
            bChartDegrees.Size = new System.Drawing.Size(72, 30);
            bChartDegrees.TabIndex = 14;
            bChartDegrees.Text = "Degrees";
            bChartDegrees.UseVisualStyleBackColor = false;
            bChartDegrees.Click += bChartDegrees_Click;
            // 
            // bChartDegreeDistribution
            // 
            bChartDegreeDistribution.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            bChartDegreeDistribution.FlatAppearance.BorderSize = 0;
            bChartDegreeDistribution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bChartDegreeDistribution.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bChartDegreeDistribution.ForeColor = System.Drawing.SystemColors.WindowFrame;
            bChartDegreeDistribution.Location = new System.Drawing.Point(108, 660);
            bChartDegreeDistribution.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bChartDegreeDistribution.Name = "bChartDegreeDistribution";
            bChartDegreeDistribution.Size = new System.Drawing.Size(147, 30);
            bChartDegreeDistribution.TabIndex = 15;
            bChartDegreeDistribution.Text = "Degree distribution";
            bChartDegreeDistribution.UseVisualStyleBackColor = false;
            bChartDegreeDistribution.Click += bChartDegreeDistribution_Click;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            panel2.Controls.Add(bResetSamples);
            panel2.Controls.Add(lAverageDegreeSamples);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(lAverageDegreeValue);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(lEdgeCount);
            panel2.Controls.Add(label6);
            panel2.Location = new System.Drawing.Point(30, 291);
            panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Padding = new System.Windows.Forms.Padding(15, 30, 30, 30);
            panel2.Size = new System.Drawing.Size(530, 50);
            panel2.TabIndex = 20;
            // 
            // bResetSamples
            // 
            bResetSamples.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            bResetSamples.BackgroundImage = Properties.Resources.reset_samples;
            bResetSamples.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bResetSamples.FlatAppearance.BorderSize = 0;
            bResetSamples.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bResetSamples.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bResetSamples.ForeColor = System.Drawing.Color.Transparent;
            bResetSamples.Location = new System.Drawing.Point(476, 13);
            bResetSamples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bResetSamples.Name = "bResetSamples";
            bResetSamples.Size = new System.Drawing.Size(25, 25);
            bResetSamples.TabIndex = 38;
            toolTip1.SetToolTip(bResetSamples, "Delete distribution samples");
            bResetSamples.UseVisualStyleBackColor = false;
            bResetSamples.Click += bResetSamples_Click;
            // 
            // lAverageDegreeSamples
            // 
            lAverageDegreeSamples.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lAverageDegreeSamples.ForeColor = System.Drawing.SystemColors.WindowFrame;
            lAverageDegreeSamples.Location = new System.Drawing.Point(432, 0);
            lAverageDegreeSamples.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lAverageDegreeSamples.Name = "lAverageDegreeSamples";
            lAverageDegreeSamples.Size = new System.Drawing.Size(40, 50);
            lAverageDegreeSamples.TabIndex = 5;
            lAverageDegreeSamples.Text = "-";
            lAverageDegreeSamples.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label9.ForeColor = System.Drawing.SystemColors.WindowFrame;
            label9.Location = new System.Drawing.Point(310, 0);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(122, 50);
            label9.TabIndex = 4;
            label9.Text = "Distribution samples:";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lAverageDegreeValue
            // 
            lAverageDegreeValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lAverageDegreeValue.ForeColor = System.Drawing.SystemColors.WindowFrame;
            lAverageDegreeValue.Location = new System.Drawing.Point(275, 0);
            lAverageDegreeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lAverageDegreeValue.Name = "lAverageDegreeValue";
            lAverageDegreeValue.Size = new System.Drawing.Size(35, 50);
            lAverageDegreeValue.TabIndex = 3;
            lAverageDegreeValue.Text = "-";
            lAverageDegreeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            label8.Location = new System.Drawing.Point(181, 0);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(95, 50);
            label8.TabIndex = 2;
            label8.Text = "Average degree:";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lEdgeCount
            // 
            lEdgeCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lEdgeCount.ForeColor = System.Drawing.SystemColors.WindowFrame;
            lEdgeCount.Location = new System.Drawing.Point(65, 0);
            lEdgeCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lEdgeCount.Name = "lEdgeCount";
            lEdgeCount.Size = new System.Drawing.Size(113, 50);
            lEdgeCount.TabIndex = 1;
            lEdgeCount.Text = "-";
            lEdgeCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            label6.Location = new System.Drawing.Point(21, 0);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(45, 50);
            label6.TabIndex = 0;
            label6.Text = "Edges:";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelParameters
            // 
            panelParameters.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            panelParameters.Controls.Add(bGenerateGraph);
            panelParameters.Controls.Add(lMaxF);
            panelParameters.Location = new System.Drawing.Point(30, 60);
            panelParameters.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelParameters.Name = "panelParameters";
            panelParameters.Size = new System.Drawing.Size(530, 160);
            panelParameters.TabIndex = 0;
            // 
            // bGenerateGraph
            // 
            bGenerateGraph.BackColor = System.Drawing.Color.FromArgb(255, 189, 0);
            bGenerateGraph.FlatAppearance.BorderSize = 0;
            bGenerateGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bGenerateGraph.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bGenerateGraph.Location = new System.Drawing.Point(401, 30);
            bGenerateGraph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bGenerateGraph.Name = "bGenerateGraph";
            bGenerateGraph.Size = new System.Drawing.Size(100, 100);
            bGenerateGraph.TabIndex = 34;
            bGenerateGraph.Text = "Generate";
            bGenerateGraph.UseVisualStyleBackColor = false;
            bGenerateGraph.Click += bGenerateGraph_Click;
            // 
            // lMaxF
            // 
            lMaxF.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lMaxF.ForeColor = System.Drawing.SystemColors.WindowFrame;
            lMaxF.Location = new System.Drawing.Point(128, 0);
            lMaxF.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lMaxF.Name = "lMaxF";
            lMaxF.Size = new System.Drawing.Size(0, 0);
            lMaxF.TabIndex = 6;
            lMaxF.Text = "0";
            lMaxF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            panel5.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            panel5.Controls.Add(bAutoGenerateOnChange);
            panel5.Controls.Add(panel8);
            panel5.Controls.Add(panel7);
            panel5.Controls.Add(panel6);
            panel5.Controls.Add(bResetArrangement);
            panel5.Controls.Add(bStretch);
            panel5.Controls.Add(bPauseArrangement);
            panel5.Controls.Add(bArrangement);
            panel5.Controls.Add(bShowNodes);
            panel5.Controls.Add(bGenerateSamples);
            panel5.Controls.Add(bShowValues);
            panel5.Controls.Add(bGradient);
            panel5.Controls.Add(bSort);
            panel5.Controls.Add(bShowDegree);
            panel5.Location = new System.Drawing.Point(30, 220);
            panel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(530, 65);
            panel5.TabIndex = 28;
            // 
            // bAutoGenerateOnChange
            // 
            bAutoGenerateOnChange.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bAutoGenerateOnChange.BackgroundImage = Properties.Resources.auto_generate_on_change;
            bAutoGenerateOnChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bAutoGenerateOnChange.FlatAppearance.BorderSize = 0;
            bAutoGenerateOnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bAutoGenerateOnChange.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bAutoGenerateOnChange.ForeColor = System.Drawing.Color.Transparent;
            bAutoGenerateOnChange.Location = new System.Drawing.Point(280, 22);
            bAutoGenerateOnChange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bAutoGenerateOnChange.Name = "bAutoGenerateOnChange";
            bAutoGenerateOnChange.Size = new System.Drawing.Size(25, 25);
            bAutoGenerateOnChange.TabIndex = 38;
            toolTip1.SetToolTip(bAutoGenerateOnChange, "Auto-generate graph when a parameter is changed");
            bAutoGenerateOnChange.UseVisualStyleBackColor = false;
            bAutoGenerateOnChange.Click += bAutoGenerateOnChange_Click;
            // 
            // panel8
            // 
            panel8.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            panel8.Location = new System.Drawing.Point(317, 22);
            panel8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(1, 25);
            panel8.TabIndex = 30;
            // 
            // panel7
            // 
            panel7.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            panel7.Location = new System.Drawing.Point(237, 22);
            panel7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(1, 25);
            panel7.TabIndex = 29;
            // 
            // panel6
            // 
            panel6.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            panel6.Location = new System.Drawing.Point(127, 22);
            panel6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(1, 25);
            panel6.TabIndex = 28;
            // 
            // bResetArrangement
            // 
            bResetArrangement.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bResetArrangement.BackgroundImage = Properties.Resources.reset;
            bResetArrangement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bResetArrangement.FlatAppearance.BorderSize = 0;
            bResetArrangement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bResetArrangement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bResetArrangement.ForeColor = System.Drawing.Color.Transparent;
            bResetArrangement.Location = new System.Drawing.Point(380, 22);
            bResetArrangement.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bResetArrangement.Name = "bResetArrangement";
            bResetArrangement.Size = new System.Drawing.Size(25, 25);
            bResetArrangement.TabIndex = 30;
            toolTip1.SetToolTip(bResetArrangement, "Reset arrangement");
            bResetArrangement.UseVisualStyleBackColor = false;
            bResetArrangement.Click += bResetArrangement_Click;
            // 
            // bStretch
            // 
            bStretch.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bStretch.BackgroundImage = Properties.Resources.stretch_chart;
            bStretch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bStretch.FlatAppearance.BorderSize = 0;
            bStretch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bStretch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bStretch.ForeColor = System.Drawing.Color.Transparent;
            bStretch.Location = new System.Drawing.Point(140, 22);
            bStretch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bStretch.Name = "bStretch";
            bStretch.Size = new System.Drawing.Size(25, 25);
            bStretch.TabIndex = 37;
            toolTip1.SetToolTip(bStretch, "Stretch chart vertically");
            bStretch.UseVisualStyleBackColor = false;
            bStretch.Click += bStretch_Click;
            // 
            // bPauseArrangement
            // 
            bPauseArrangement.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bPauseArrangement.BackgroundImage = Properties.Resources.pause;
            bPauseArrangement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bPauseArrangement.FlatAppearance.BorderSize = 0;
            bPauseArrangement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bPauseArrangement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bPauseArrangement.ForeColor = System.Drawing.Color.Transparent;
            bPauseArrangement.Location = new System.Drawing.Point(355, 22);
            bPauseArrangement.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bPauseArrangement.Name = "bPauseArrangement";
            bPauseArrangement.Size = new System.Drawing.Size(25, 25);
            bPauseArrangement.TabIndex = 29;
            toolTip1.SetToolTip(bPauseArrangement, "Pause/resume arrangement");
            bPauseArrangement.UseVisualStyleBackColor = false;
            bPauseArrangement.Click += bPauseArrangement_Click;
            // 
            // bArrangement
            // 
            bArrangement.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bArrangement.BackgroundImage = Properties.Resources.arrange;
            bArrangement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bArrangement.FlatAppearance.BorderSize = 0;
            bArrangement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bArrangement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bArrangement.ForeColor = System.Drawing.Color.Transparent;
            bArrangement.Location = new System.Drawing.Point(330, 22);
            bArrangement.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bArrangement.Name = "bArrangement";
            bArrangement.Size = new System.Drawing.Size(25, 25);
            bArrangement.TabIndex = 36;
            toolTip1.SetToolTip(bArrangement, "Arrange graph");
            bArrangement.UseVisualStyleBackColor = false;
            bArrangement.Click += bArrangement_Click;
            // 
            // bShowNodes
            // 
            bShowNodes.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bShowNodes.BackgroundImage = Properties.Resources.show_nodes;
            bShowNodes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bShowNodes.FlatAppearance.BorderSize = 0;
            bShowNodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bShowNodes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bShowNodes.ForeColor = System.Drawing.Color.Transparent;
            bShowNodes.Location = new System.Drawing.Point(30, 22);
            bShowNodes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bShowNodes.Name = "bShowNodes";
            bShowNodes.Size = new System.Drawing.Size(25, 25);
            bShowNodes.TabIndex = 35;
            toolTip1.SetToolTip(bShowNodes, "Show nodes");
            bShowNodes.UseVisualStyleBackColor = false;
            bShowNodes.Click += bShowNodes_Click;
            // 
            // bGenerateSamples
            // 
            bGenerateSamples.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bGenerateSamples.BackgroundImage = Properties.Resources.generate_samples;
            bGenerateSamples.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bGenerateSamples.FlatAppearance.BorderSize = 0;
            bGenerateSamples.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bGenerateSamples.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bGenerateSamples.ForeColor = System.Drawing.Color.Transparent;
            bGenerateSamples.Location = new System.Drawing.Point(250, 22);
            bGenerateSamples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bGenerateSamples.Name = "bGenerateSamples";
            bGenerateSamples.Size = new System.Drawing.Size(25, 25);
            bGenerateSamples.TabIndex = 34;
            toolTip1.SetToolTip(bGenerateSamples, "Auto-generate graphs");
            bGenerateSamples.UseVisualStyleBackColor = false;
            bGenerateSamples.Click += bGenerateSamples_Click;
            // 
            // bShowValues
            // 
            bShowValues.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bShowValues.BackgroundImage = Properties.Resources.show_chart_values;
            bShowValues.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bShowValues.FlatAppearance.BorderSize = 0;
            bShowValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bShowValues.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bShowValues.ForeColor = System.Drawing.Color.Transparent;
            bShowValues.Location = new System.Drawing.Point(170, 22);
            bShowValues.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bShowValues.Name = "bShowValues";
            bShowValues.Size = new System.Drawing.Size(25, 25);
            bShowValues.TabIndex = 33;
            toolTip1.SetToolTip(bShowValues, "Show chart values");
            bShowValues.UseVisualStyleBackColor = false;
            bShowValues.Click += bShowValues_Click;
            // 
            // bGradient
            // 
            bGradient.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bGradient.BackgroundImage = Properties.Resources.gradient;
            bGradient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bGradient.FlatAppearance.BorderSize = 0;
            bGradient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bGradient.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bGradient.ForeColor = System.Drawing.Color.Transparent;
            bGradient.Location = new System.Drawing.Point(60, 22);
            bGradient.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bGradient.Name = "bGradient";
            bGradient.Size = new System.Drawing.Size(25, 25);
            bGradient.TabIndex = 32;
            toolTip1.SetToolTip(bGradient, "Use gradient edge coloring");
            bGradient.UseVisualStyleBackColor = false;
            bGradient.Click += bGradient_Click;
            // 
            // bSort
            // 
            bSort.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bSort.BackgroundImage = Properties.Resources.sort_chart;
            bSort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bSort.FlatAppearance.BorderSize = 0;
            bSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bSort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bSort.ForeColor = System.Drawing.Color.Transparent;
            bSort.Location = new System.Drawing.Point(200, 22);
            bSort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bSort.Name = "bSort";
            bSort.Size = new System.Drawing.Size(25, 25);
            bSort.TabIndex = 31;
            toolTip1.SetToolTip(bSort, "Sort chart in descending order");
            bSort.UseVisualStyleBackColor = false;
            bSort.Click += bSort_Click;
            // 
            // bShowDegree
            // 
            bShowDegree.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            bShowDegree.BackgroundImage = Properties.Resources.show_degrees;
            bShowDegree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            bShowDegree.FlatAppearance.BorderSize = 0;
            bShowDegree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bShowDegree.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bShowDegree.ForeColor = System.Drawing.Color.Transparent;
            bShowDegree.Location = new System.Drawing.Point(90, 22);
            bShowDegree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bShowDegree.Name = "bShowDegree";
            bShowDegree.Size = new System.Drawing.Size(25, 25);
            bShowDegree.TabIndex = 30;
            toolTip1.SetToolTip(bShowDegree, "Show node degrees");
            bShowDegree.UseVisualStyleBackColor = false;
            bShowDegree.Click += bShowDegree_Click;
            // 
            // pProgressBar
            // 
            pProgressBar.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            pProgressBar.Location = new System.Drawing.Point(30, 285);
            pProgressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pProgressBar.Name = "pProgressBar";
            pProgressBar.Size = new System.Drawing.Size(530, 5);
            pProgressBar.TabIndex = 27;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(bTabBianconiBarabasi);
            splitContainer1.Panel1.Controls.Add(bTabWattsStrogatz);
            splitContainer1.Panel1.Controls.Add(bTabBarabasiAlbert);
            splitContainer1.Panel1.Controls.Add(bTabErdosRenyi);
            splitContainer1.Panel1.Controls.Add(panel5);
            splitContainer1.Panel1.Controls.Add(panel2);
            splitContainer1.Panel1.Controls.Add(bChartAverageDegreeDistribution);
            splitContainer1.Panel1.Controls.Add(pProgressBar);
            splitContainer1.Panel1.Controls.Add(bChartDegreeDistribution);
            splitContainer1.Panel1.Controls.Add(panelChart);
            splitContainer1.Panel1.Controls.Add(bChartDegrees);
            splitContainer1.Panel1.Controls.Add(panelParameters);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            splitContainer1.Panel2.Controls.Add(panelGraph);
            splitContainer1.Panel2.Controls.Add(lVersion);
            splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(29, 30, 30, 30);
            splitContainer1.Size = new System.Drawing.Size(1280, 720);
            splitContainer1.SplitterDistance = 560;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.TabIndex = 21;
            // 
            // bTabBianconiBarabasi
            // 
            bTabBianconiBarabasi.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            bTabBianconiBarabasi.FlatAppearance.BorderSize = 0;
            bTabBianconiBarabasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bTabBianconiBarabasi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bTabBianconiBarabasi.Location = new System.Drawing.Point(225, 30);
            bTabBianconiBarabasi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bTabBianconiBarabasi.Name = "bTabBianconiBarabasi";
            bTabBianconiBarabasi.Size = new System.Drawing.Size(60, 30);
            bTabBianconiBarabasi.TabIndex = 37;
            bTabBianconiBarabasi.Text = "BB";
            toolTip1.SetToolTip(bTabBianconiBarabasi, "Bianconi Barabasi");
            bTabBianconiBarabasi.UseVisualStyleBackColor = false;
            bTabBianconiBarabasi.Click += buttonTabBianconiBarabasi_Click;
            // 
            // bTabWattsStrogatz
            // 
            bTabWattsStrogatz.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            bTabWattsStrogatz.FlatAppearance.BorderSize = 0;
            bTabWattsStrogatz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bTabWattsStrogatz.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bTabWattsStrogatz.Location = new System.Drawing.Point(160, 30);
            bTabWattsStrogatz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bTabWattsStrogatz.Name = "bTabWattsStrogatz";
            bTabWattsStrogatz.Size = new System.Drawing.Size(60, 30);
            bTabWattsStrogatz.TabIndex = 36;
            bTabWattsStrogatz.Text = "WS";
            toolTip1.SetToolTip(bTabWattsStrogatz, "Watts Strogatz");
            bTabWattsStrogatz.UseVisualStyleBackColor = false;
            bTabWattsStrogatz.Click += buttonTabWattsStrogatz_Click;
            // 
            // bTabBarabasiAlbert
            // 
            bTabBarabasiAlbert.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            bTabBarabasiAlbert.FlatAppearance.BorderSize = 0;
            bTabBarabasiAlbert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bTabBarabasiAlbert.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bTabBarabasiAlbert.Location = new System.Drawing.Point(95, 30);
            bTabBarabasiAlbert.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bTabBarabasiAlbert.Name = "bTabBarabasiAlbert";
            bTabBarabasiAlbert.Size = new System.Drawing.Size(60, 30);
            bTabBarabasiAlbert.TabIndex = 35;
            bTabBarabasiAlbert.Text = "BA";
            toolTip1.SetToolTip(bTabBarabasiAlbert, "Barabási Albert");
            bTabBarabasiAlbert.UseVisualStyleBackColor = false;
            bTabBarabasiAlbert.Click += buttonTabBarabasiAlbert_Click;
            // 
            // bTabErdosRenyi
            // 
            bTabErdosRenyi.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
            bTabErdosRenyi.FlatAppearance.BorderSize = 0;
            bTabErdosRenyi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bTabErdosRenyi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bTabErdosRenyi.ForeColor = System.Drawing.SystemColors.ControlText;
            bTabErdosRenyi.Location = new System.Drawing.Point(30, 30);
            bTabErdosRenyi.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bTabErdosRenyi.Name = "bTabErdosRenyi";
            bTabErdosRenyi.Size = new System.Drawing.Size(60, 30);
            bTabErdosRenyi.TabIndex = 34;
            bTabErdosRenyi.Text = "ER";
            toolTip1.SetToolTip(bTabErdosRenyi, "Erdős Rényi");
            bTabErdosRenyi.UseVisualStyleBackColor = false;
            bTabErdosRenyi.Click += buttonTabErdosRenyi_Click;
            // 
            // bChartAverageDegreeDistribution
            // 
            bChartAverageDegreeDistribution.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            bChartAverageDegreeDistribution.FlatAppearance.BorderSize = 0;
            bChartAverageDegreeDistribution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bChartAverageDegreeDistribution.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            bChartAverageDegreeDistribution.ForeColor = System.Drawing.SystemColors.WindowFrame;
            bChartAverageDegreeDistribution.Location = new System.Drawing.Point(261, 660);
            bChartAverageDegreeDistribution.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bChartAverageDegreeDistribution.Name = "bChartAverageDegreeDistribution";
            bChartAverageDegreeDistribution.Size = new System.Drawing.Size(207, 30);
            bChartAverageDegreeDistribution.TabIndex = 21;
            bChartAverageDegreeDistribution.Text = "Average degree distribution";
            bChartAverageDegreeDistribution.UseVisualStyleBackColor = false;
            bChartAverageDegreeDistribution.Click += bChartAverageDegreeDistribution_Click;
            // 
            // panelGraph
            // 
            panelGraph.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelGraph.Location = new System.Drawing.Point(30, 30);
            panelGraph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelGraph.Name = "panelGraph";
            panelGraph.Size = new System.Drawing.Size(660, 660);
            panelGraph.TabIndex = 11;
            panelGraph.MouseDown += panelGraph_MouseDown;
            panelGraph.MouseEnter += panelGraph_MouseEnter;
            panelGraph.MouseLeave += panelGraph_MouseLeave;
            panelGraph.MouseMove += panelGraph_MouseMove;
            panelGraph.MouseUp += panelGraph_MouseUp;
            // 
            // lVersion
            // 
            lVersion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            lVersion.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lVersion.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            lVersion.Location = new System.Drawing.Point(619, 700);
            lVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lVersion.Name = "lVersion";
            lVersion.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            lVersion.Size = new System.Drawing.Size(100, 20);
            lVersion.TabIndex = 9;
            lVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // timer
            // 
            timer.Tick += Update;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            ClientSize = new System.Drawing.Size(1280, 720);
            Controls.Add(splitContainer1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(606, 759);
            Name = "FormMain";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Graphs";
            ResizeEnd += Form_ResizeEnd;
            SizeChanged += Form_SizeChanged;
            panel2.ResumeLayout(false);
            panelParameters.ResumeLayout(false);
            panel5.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Button bChartDegrees;
        private System.Windows.Forms.Button bChartDegreeDistribution;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lAverageDegreeValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lEdgeCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelParameters;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.Button bChartAverageDegreeDistribution;
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
        private System.Windows.Forms.Button bTabBianconiBarabasi;
    }
}

