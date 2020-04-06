namespace BHA
{
    partial class Form1
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
            this.bt_addNode = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bt_confirmNode = new System.Windows.Forms.Button();
            this.bt_removeNode = new System.Windows.Forms.Button();
            this.debug_avaiableNode = new System.Windows.Forms.Label();
            this.debug_NodeID = new System.Windows.Forms.Label();
            this.debug_toolName = new System.Windows.Forms.Label();
            this.debug_toolVoltage = new System.Windows.Forms.Label();
            this.groupBox_Debug = new System.Windows.Forms.GroupBox();
            this.bt_runPython = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox_Debug.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_addNode
            // 
            this.bt_addNode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bt_addNode.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.bt_addNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_addNode.Location = new System.Drawing.Point(7, 34);
            this.bt_addNode.Margin = new System.Windows.Forms.Padding(4);
            this.bt_addNode.Name = "bt_addNode";
            this.bt_addNode.Size = new System.Drawing.Size(83, 59);
            this.bt_addNode.TabIndex = 0;
            this.bt_addNode.Text = "Add Node";
            this.bt_addNode.UseVisualStyleBackColor = false;
            this.bt_addNode.Click += new System.EventHandler(this.bt_addNode_Click);
            // 
            // bt_confirmNode
            // 
            this.bt_confirmNode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bt_confirmNode.Enabled = false;
            this.bt_confirmNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_confirmNode.Location = new System.Drawing.Point(184, 34);
            this.bt_confirmNode.Margin = new System.Windows.Forms.Padding(4);
            this.bt_confirmNode.Name = "bt_confirmNode";
            this.bt_confirmNode.Size = new System.Drawing.Size(78, 59);
            this.bt_confirmNode.TabIndex = 1;
            this.bt_confirmNode.Text = "Confirm";
            this.bt_confirmNode.UseVisualStyleBackColor = false;
            this.bt_confirmNode.Click += new System.EventHandler(this.bt_confirmNode_Click);
            // 
            // bt_removeNode
            // 
            this.bt_removeNode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bt_removeNode.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.bt_removeNode.Enabled = false;
            this.bt_removeNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_removeNode.Location = new System.Drawing.Point(97, 34);
            this.bt_removeNode.Margin = new System.Windows.Forms.Padding(4);
            this.bt_removeNode.Name = "bt_removeNode";
            this.bt_removeNode.Size = new System.Drawing.Size(79, 59);
            this.bt_removeNode.TabIndex = 2;
            this.bt_removeNode.Text = "Remove Node";
            this.bt_removeNode.UseVisualStyleBackColor = false;
            this.bt_removeNode.Click += new System.EventHandler(this.bt_removeNode_Click);
            // 
            // debug_avaiableNode
            // 
            this.debug_avaiableNode.AutoSize = true;
            this.debug_avaiableNode.Location = new System.Drawing.Point(18, 33);
            this.debug_avaiableNode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.debug_avaiableNode.Name = "debug_avaiableNode";
            this.debug_avaiableNode.Size = new System.Drawing.Size(158, 17);
            this.debug_avaiableNode.TabIndex = 3;
            this.debug_avaiableNode.Text = "Debug - Available Node";
            // 
            // debug_NodeID
            // 
            this.debug_NodeID.AutoSize = true;
            this.debug_NodeID.Location = new System.Drawing.Point(18, 73);
            this.debug_NodeID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.debug_NodeID.Name = "debug_NodeID";
            this.debug_NodeID.Size = new System.Drawing.Size(195, 17);
            this.debug_NodeID.TabIndex = 4;
            this.debug_NodeID.Text = "Debug - Tool id at each Node";
            // 
            // debug_toolName
            // 
            this.debug_toolName.AutoSize = true;
            this.debug_toolName.Location = new System.Drawing.Point(18, 109);
            this.debug_toolName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.debug_toolName.Name = "debug_toolName";
            this.debug_toolName.Size = new System.Drawing.Size(158, 17);
            this.debug_toolName.TabIndex = 5;
            this.debug_toolName.Text = "Debug - Tool Name List";
            // 
            // debug_toolVoltage
            // 
            this.debug_toolVoltage.AutoSize = true;
            this.debug_toolVoltage.Location = new System.Drawing.Point(18, 147);
            this.debug_toolVoltage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.debug_toolVoltage.Name = "debug_toolVoltage";
            this.debug_toolVoltage.Size = new System.Drawing.Size(169, 17);
            this.debug_toolVoltage.TabIndex = 6;
            this.debug_toolVoltage.Text = "Debug - Tool Voltage List";
            // 
            // groupBox_Debug
            // 
            this.groupBox_Debug.Controls.Add(this.debug_toolName);
            this.groupBox_Debug.Controls.Add(this.debug_toolVoltage);
            this.groupBox_Debug.Controls.Add(this.debug_avaiableNode);
            this.groupBox_Debug.Controls.Add(this.debug_NodeID);
            this.groupBox_Debug.Location = new System.Drawing.Point(50, 430);
            this.groupBox_Debug.Name = "groupBox_Debug";
            this.groupBox_Debug.Size = new System.Drawing.Size(602, 181);
            this.groupBox_Debug.TabIndex = 7;
            this.groupBox_Debug.TabStop = false;
            this.groupBox_Debug.Text = "Debug Box";
            // 
            // bt_runPython
            // 
            this.bt_runPython.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bt_runPython.Enabled = false;
            this.bt_runPython.Location = new System.Drawing.Point(6, 35);
            this.bt_runPython.Name = "bt_runPython";
            this.bt_runPython.Size = new System.Drawing.Size(83, 59);
            this.bt_runPython.TabIndex = 10;
            this.bt_runPython.Text = "Update Result";
            this.bt_runPython.UseVisualStyleBackColor = false;
            this.bt_runPython.Click += new System.EventHandler(this.bt_runPython_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_addNode);
            this.groupBox1.Controls.Add(this.bt_removeNode);
            this.groupBox1.Controls.Add(this.bt_confirmNode);
            this.groupBox1.Location = new System.Drawing.Point(50, 297);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 103);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add/Remove Tools";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_runPython);
            this.groupBox2.Location = new System.Drawing.Point(330, 297);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(105, 103);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tool Ultilities";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 410);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_Debug);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "BHA Calculation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox_Debug.ResumeLayout(false);
            this.groupBox_Debug.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Button bt_addNode;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button bt_confirmNode;
        private System.Windows.Forms.Button bt_removeNode;
        private System.Windows.Forms.Label debug_avaiableNode;
        private System.Windows.Forms.Label debug_NodeID;
        private System.Windows.Forms.Label debug_toolName;
        private System.Windows.Forms.Label debug_toolVoltage;
        private System.Windows.Forms.GroupBox groupBox_Debug;
        private System.Windows.Forms.Button bt_runPython;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

