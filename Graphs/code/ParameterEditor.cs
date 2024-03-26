using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Graphs
{
    [SupportedOSPlatform("windows")]
    public class ParameterEditor
    {
        public ParameterEditor(string name, double minimumValue, double maximumValue, double initialValue, double valueStep, int valueResolution = 0)
        {
            Name = name;

            ValueResolution = Math.Max(Math.Min(valueResolution, 2), 0);
            valueScale = Math.Pow(10, ValueResolution);

            MinimumValue = Math.Abs(Scale(minimumValue));
            MaximumValue = Math.Abs(Scale(maximumValue));
            if (MinimumValue > MaximumValue)
            {
                int temp = MinimumValue;
                MinimumValue = MaximumValue;
                MaximumValue = temp;
            }

            InitialValue = Math.Max(Math.Min(Scale(initialValue), MaximumValue), MinimumValue);
            ValueStep = Math.Max(Math.Abs(Scale(valueStep)), 1);

            SetupControls();
        }

        public ParameterEditor(ParameterEditor other)
        {
            Name = other.Name;
            ValueResolution = other.ValueResolution;
            MinimumValue = other.MinimumValue;
            MaximumValue = other.MaximumValue;
            InitialValue = other.InitialValue;
            ValueStep = other.ValueStep;
            SavedValue = other.SavedValue;

            SetupControls();
        }

        private void SetupControls()
        {
            SetupTrackBar();
            SetupLabelName();
            SetupLabelValue();
        }

        public delegate void OnValueChangedEventHandler(double value);
        public event OnValueChangedEventHandler valueChanged;

        private double valueScale;
        private double previousValue;

        // The value that was used for the last generation of a graph
        public double SavedValue { get; private set; }
        public string Name { get; }
        public double ValueAndSave
        {
            get
            {
                SaveValue();
                return Value;
            }
        }
        private double Value { get => UnScale(valueOfTrackBar()); }
        public int MinimumValue { get; }
        public int MaximumValue { get; }
        public int InitialValue { get; }
        public int ValueStep { get; }
        public int ValueResolution { get; }

        private int Scale(double value) => (int)(value * valueScale);
        private double UnScale(double value) => value / valueScale;

        private TrackBar trackBar = new ColoredTrackBar();
        private TextBox labelValue = new TextBox();
        private Label labelName = new Label();

        private void SetupTrackBar()
        {
            ((System.ComponentModel.ISupportInitialize)(trackBar)).BeginInit();
            trackBar.LargeChange = 1;
            trackBar.Location = new Point(65, 0);
            trackBar.Margin = new Padding(0);
            trackBar.Name = "trackBarMeanDegree";
            trackBar.AutoSize = false;
            trackBar.Size = new Size(205, 22);
            trackBar.TabStop = false;
            trackBar.TickStyle = TickStyle.None;
            trackBar.Scroll += new EventHandler(TrackBarScroll);
            ((System.ComponentModel.ISupportInitialize)(trackBar)).EndInit();

            trackBar.Minimum = 0;
            trackBar.Maximum = (MaximumValue - MinimumValue) / ValueStep;
            trackBar.Value = Math.Max(Math.Min((InitialValue - MinimumValue) / ValueStep, trackBar.Maximum), 0);

            previousValue = trackBar.Value;
        }

        private void SetupLabelValue()
        {
            labelValue.BackColor = Colors.darkGrey;
            labelValue.ForeColor = Colors.fontWhite;
            labelValue.BorderStyle = BorderStyle.None;
            labelValue.CausesValidation = false;
            labelValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelValue.Location = new Point(0, 0);
            labelValue.Margin = new Padding(0);
            labelValue.Name = "labelValue";
            labelValue.Size = new Size(35, 18);
            labelValue.TabStop = false;
            labelValue.Text = Value.ToString($"F{ValueResolution}");
            labelValue.TextAlign = HorizontalAlignment.Center;
            //this.toolTip1.SetToolTip(labelValue, "Probability of two nodes being connected with an edge");
            labelValue.KeyDown += new KeyEventHandler(labelValueKeyDown);
        }

        private void SetupLabelName()
        {
            Size size = new Size(90, 18);

            labelName.BackColor = Colors.foregroundDark;
            labelName.ForeColor = Colors.fontWhite;
            labelName.BorderStyle = BorderStyle.None;
            labelName.CausesValidation = false;
            labelName.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            labelName.Location = new Point(0, 0);
            labelName.Margin = new Padding(0);
            labelName.Padding = new Padding(2, 0, 2, 1);
            labelName.Name = "labelName";
            labelName.Size = size;
            //labelName.AutoSize = true;
            labelName.TabStop = false;
            labelName.Text = Name;
            labelName.TextAlign = ContentAlignment.MiddleLeft;
            labelName.CreateGraphics();

            labelName.Paint += (sender, e) =>
            {
                //return;
                Debug.WriteLine("gradient");
                LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(0), new Point(labelName.Size.Width, 0), Colors.foregroundDark, Colors.darkGrey);
                Brush brush = new SolidBrush(labelName.ForeColor);

                Blend blend = new Blend();
                blend.Factors = new float[] { 0.0f, 0.0f, 1.0f };
                blend.Positions = new float[] { 0.0f, 0.9f, 1.0f };
                gradientBrush.Blend = blend;
                e.Graphics.FillRectangle(gradientBrush, new Rectangle(new Point(0), labelName.Size));

                e.Graphics.DrawString(labelName.Text, labelName.Font, brush, new Point(5, 2));

                brush.Dispose();
                gradientBrush.Dispose();
            };

        }

        private void HandleValueChange()
        {
            previousValue = trackBar.Value;
            valueChanged(trackBar.Value);
        }

        private double valueOfTrackBar() => trackBar.Value * ValueStep + MinimumValue;

        private void TrackBarScroll(object sender, EventArgs e)
        {
            labelValue.Text = Value.ToString($"F{ValueResolution}");
            labelValue.Update();

            if (trackBar.Value != previousValue) HandleValueChange();
        }

        private void labelValueKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double enteredValue = double.Parse(labelValue.Text.ToString());
                    int scaledValue = Scale(enteredValue);
                    int snappedValue = scaledValue - scaledValue % ValueStep;
                    int cappedValue = Math.Max(Math.Min(scaledValue, MaximumValue), MinimumValue);
                    int convertedValue = (cappedValue - MinimumValue) / ValueStep;

                    if (trackBar.Value != convertedValue)
                    {
                        trackBar.Value = convertedValue;
                        labelValue.Text = UnScale(cappedValue).ToString($"F{ValueResolution}");
                        HandleValueChange();
                    }
                }
                catch (FormatException)
                {
                    labelValue.Text = Value.ToString($"F{ValueResolution}");
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        public void SaveValue()
        {
            SavedValue = Value;
        }

        public void AddToControl(Control control) => AddToControl(control, new Point(0, 0));
        public void AddToControl(Control control, Point position)
        {
            control.Controls.Add(labelValue);
            control.Controls.Add(labelName);
            control.Controls.Add(trackBar);
            labelName.Location = position + new Size(0, 0);
            labelValue.Location = position + new Size(90, 0);
            trackBar.Location = position + new Size(145, -2);
            labelValue.Update();
            labelName.Update();
            trackBar.Update();
        }

        public void RemoveFromControl(Control control)
        {
            control.Controls.Remove(labelValue);
            control.Controls.Remove(labelName);
            control.Controls.Remove(trackBar);
        }
    }
}
