namespace Calculator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox_Result;
        private System.Windows.Forms.Label labelCurrentOperation;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_Result = new System.Windows.Forms.TextBox();
            this.labelCurrentOperation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_Result
            // 
            this.textBox_Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.textBox_Result.Location = new System.Drawing.Point(12, 12);
            this.textBox_Result.Name = "textBox_Result";
            this.textBox_Result.Size = new System.Drawing.Size(260, 38);
            this.textBox_Result.TabIndex = 0;
            this.textBox_Result.Text = "0";
            this.textBox_Result.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelCurrentOperation
            // 
            this.labelCurrentOperation.AutoSize = true;
            this.labelCurrentOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelCurrentOperation.Location = new System.Drawing.Point(12, 53);
            this.labelCurrentOperation.Name = "labelCurrentOperation";
            this.labelCurrentOperation.Size = new System.Drawing.Size(0, 20);
            this.labelCurrentOperation.TabIndex = 1;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelCurrentOperation);
            this.Controls.Add(this.textBox_Result);
            this.Name = "Form1";
            this.Text = "Simple Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

            // Adding buttons dynamically
            int startX = 12, startY = 80, buttonWidth = 60, buttonHeight = 40;
            string[] buttonLabels = { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", "0", "C", "CE", "+", "=" };

            for (int i = 0; i < buttonLabels.Length; i++)
            {
                Button button = new Button();
                button.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
                button.Location = new System.Drawing.Point(startX + (i % 4) * buttonWidth, startY + (i / 4) * buttonHeight);
                button.Text = buttonLabels[i];
                button.Click += buttonLabels[i] == "=" ? new EventHandler(this.buttonEquals_Click) :
                                buttonLabels[i] == "C" ? new EventHandler(this.buttonC_Click) :
                                buttonLabels[i] == "CE" ? new EventHandler(this.buttonCE_Click) :
                                "+-*/".Contains(buttonLabels[i]) ? new EventHandler(this.operator_Click) :
                                new EventHandler(this.button_Click);
                this.Controls.Add(button);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
