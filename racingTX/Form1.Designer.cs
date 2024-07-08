namespace racingTX
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.rfx = new System.Windows.Forms.Button();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.cnt = new System.Windows.Forms.Button();
            this.textBoxReceivedData = new System.Windows.Forms.TextBox();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.dsc = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.httpout = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxseriallog = new System.Windows.Forms.TextBox();
            this.textBoxhttplog = new System.Windows.Forms.TextBox();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxIp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxesp1 = new System.Windows.Forms.TextBox();
            this.textBoxhttppktloss = new System.Windows.Forms.TextBox();
            this.textBoxesp2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxxbeeptkloss = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxhttppppktlost = new System.Windows.Forms.TextBox();
            this.textBoxxbeepktlost = new System.Windows.Forms.TextBox();
            this.textBoxhttppkttotal = new System.Windows.Forms.TextBox();
            this.textBoxxbeepkttotal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonresetsts = new System.Windows.Forms.Button();
            this.label140 = new System.Windows.Forms.Label();
            this.buttonGithub = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxSmall = new System.Windows.Forms.PictureBox();
            this.checkBoxReceiveAsBytes = new System.Windows.Forms.CheckBox();
            this.textBox1dbi = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSmall)).BeginInit();
            this.SuspendLayout();
            // 
            // rfx
            // 
            this.rfx.Location = new System.Drawing.Point(17, 13);
            this.rfx.Name = "rfx";
            this.rfx.Size = new System.Drawing.Size(75, 23);
            this.rfx.TabIndex = 0;
            this.rfx.Text = "Atualizar";
            this.rfx.UseVisualStyleBackColor = true;
            this.rfx.Click += new System.EventHandler(this.rfx_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.Enabled = false;
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(98, 12);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(74, 24);
            this.comboBoxPorts.TabIndex = 1;
            // 
            // cnt
            // 
            this.cnt.Enabled = false;
            this.cnt.Location = new System.Drawing.Point(305, 12);
            this.cnt.Name = "cnt";
            this.cnt.Size = new System.Drawing.Size(75, 23);
            this.cnt.TabIndex = 2;
            this.cnt.Text = "Conectar";
            this.cnt.UseVisualStyleBackColor = true;
            this.cnt.Click += new System.EventHandler(this.cnt_Click);
            // 
            // textBoxReceivedData
            // 
            this.textBoxReceivedData.Location = new System.Drawing.Point(17, 42);
            this.textBoxReceivedData.Multiline = true;
            this.textBoxReceivedData.Name = "textBoxReceivedData";
            this.textBoxReceivedData.Size = new System.Drawing.Size(387, 396);
            this.textBoxReceivedData.TabIndex = 3;
            this.textBoxReceivedData.Visible = false;
            this.textBoxReceivedData.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.Enabled = false;
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Location = new System.Drawing.Point(178, 13);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(121, 24);
            this.comboBoxBaudRate.TabIndex = 4;
            // 
            // dsc
            // 
            this.dsc.Location = new System.Drawing.Point(305, 12);
            this.dsc.Name = "dsc";
            this.dsc.Size = new System.Drawing.Size(99, 23);
            this.dsc.TabIndex = 5;
            this.dsc.Text = "Desconectar";
            this.dsc.UseVisualStyleBackColor = true;
            this.dsc.Visible = false;
            this.dsc.Click += new System.EventHandler(this.dsc_Click);
            // 
            // sendButton
            // 
            this.sendButton.Enabled = false;
            this.sendButton.Location = new System.Drawing.Point(410, 12);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(188, 23);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "Iniciar telemetria";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click_1);
            // 
            // httpout
            // 
            this.httpout.Location = new System.Drawing.Point(410, 42);
            this.httpout.Multiline = true;
            this.httpout.Name = "httpout";
            this.httpout.Size = new System.Drawing.Size(378, 396);
            this.httpout.TabIndex = 7;
            this.httpout.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(604, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Statisticas";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxseriallog
            // 
            this.textBoxseriallog.Location = new System.Drawing.Point(17, 513);
            this.textBoxseriallog.Multiline = true;
            this.textBoxseriallog.Name = "textBoxseriallog";
            this.textBoxseriallog.Size = new System.Drawing.Size(387, 88);
            this.textBoxseriallog.TabIndex = 9;
            this.textBoxseriallog.TextChanged += new System.EventHandler(this.textBoxseriallog_TextChanged);
            // 
            // textBoxhttplog
            // 
            this.textBoxhttplog.Location = new System.Drawing.Point(410, 513);
            this.textBoxhttplog.Multiline = true;
            this.textBoxhttplog.Name = "textBoxhttplog";
            this.textBoxhttplog.Size = new System.Drawing.Size(378, 88);
            this.textBoxhttplog.TabIndex = 10;
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(17, 444);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(70, 20);
            this.checkBoxDebug.TabIndex = 11;
            this.checkBoxDebug.Text = "Debug";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            this.checkBoxDebug.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(98, 444);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(56, 20);
            this.checkBox2.TabIndex = 12;
            this.checkBox2.Text = "VID*";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(410, 444);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(131, 20);
            this.checkBox3.TabIndex = 13;
            this.checkBox3.Text = "Servidor de teste";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 494);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Erros no serial:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(407, 494);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Erros no HTTP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 467);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "*Verificaçao de Integridade de Dados";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(809, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Server Ip:";
            // 
            // textBoxIp
            // 
            this.textBoxIp.Location = new System.Drawing.Point(871, 42);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.Size = new System.Drawing.Size(266, 22);
            this.textBoxIp.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(809, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "Expected variable / esp:";
            // 
            // textBoxesp1
            // 
            this.textBoxesp1.Location = new System.Drawing.Point(967, 64);
            this.textBoxesp1.Name = "textBoxesp1";
            this.textBoxesp1.Size = new System.Drawing.Size(40, 22);
            this.textBoxesp1.TabIndex = 20;
            // 
            // textBoxhttppktloss
            // 
            this.textBoxhttppktloss.Location = new System.Drawing.Point(923, 89);
            this.textBoxhttppktloss.Name = "textBoxhttppktloss";
            this.textBoxhttppktloss.Size = new System.Drawing.Size(56, 22);
            this.textBoxhttppktloss.TabIndex = 21;
            // 
            // textBoxesp2
            // 
            this.textBoxesp2.Location = new System.Drawing.Point(1013, 64);
            this.textBoxesp2.Name = "textBoxesp2";
            this.textBoxesp2.Size = new System.Drawing.Size(40, 22);
            this.textBoxesp2.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(809, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "HTTP Pkt Loss:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(809, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 24;
            this.label7.Text = "Xbee Pkt Loss:";
            // 
            // textBoxxbeeptkloss
            // 
            this.textBoxxbeeptkloss.Location = new System.Drawing.Point(923, 173);
            this.textBoxxbeeptkloss.Name = "textBoxxbeeptkloss";
            this.textBoxxbeeptkloss.Size = new System.Drawing.Size(56, 22);
            this.textBoxxbeeptkloss.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(978, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 16);
            this.label8.TabIndex = 26;
            this.label8.Text = "%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(978, 179);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 16);
            this.label9.TabIndex = 27;
            this.label9.Text = "%";
            // 
            // textBoxhttppppktlost
            // 
            this.textBoxhttppppktlost.Location = new System.Drawing.Point(923, 117);
            this.textBoxhttppppktlost.Name = "textBoxhttppppktlost";
            this.textBoxhttppppktlost.Size = new System.Drawing.Size(56, 22);
            this.textBoxhttppppktlost.TabIndex = 28;
            // 
            // textBoxxbeepktlost
            // 
            this.textBoxxbeepktlost.Location = new System.Drawing.Point(923, 201);
            this.textBoxxbeepktlost.Name = "textBoxxbeepktlost";
            this.textBoxxbeepktlost.Size = new System.Drawing.Size(56, 22);
            this.textBoxxbeepktlost.TabIndex = 29;
            // 
            // textBoxhttppkttotal
            // 
            this.textBoxhttppkttotal.Location = new System.Drawing.Point(923, 145);
            this.textBoxhttppkttotal.Name = "textBoxhttppkttotal";
            this.textBoxhttppkttotal.Size = new System.Drawing.Size(56, 22);
            this.textBoxhttppkttotal.TabIndex = 30;
            // 
            // textBoxxbeepkttotal
            // 
            this.textBoxxbeepkttotal.Location = new System.Drawing.Point(923, 229);
            this.textBoxxbeepkttotal.Name = "textBoxxbeepkttotal";
            this.textBoxxbeepkttotal.Size = new System.Drawing.Size(56, 22);
            this.textBoxxbeepkttotal.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(809, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 16);
            this.label10.TabIndex = 32;
            this.label10.Text = "HTTP Pkt Lost:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(809, 204);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 16);
            this.label11.TabIndex = 33;
            this.label11.Text = "Xbee Pkt Lost:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(809, 148);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 16);
            this.label12.TabIndex = 34;
            this.label12.Text = "HTTP Pkt Total:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(809, 232);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 16);
            this.label13.TabIndex = 35;
            this.label13.Text = "Xbee Pkt Total:";
            // 
            // buttonresetsts
            // 
            this.buttonresetsts.Location = new System.Drawing.Point(812, 289);
            this.buttonresetsts.Name = "buttonresetsts";
            this.buttonresetsts.Size = new System.Drawing.Size(325, 23);
            this.buttonresetsts.TabIndex = 36;
            this.buttonresetsts.Text = "Reseta statiticas";
            this.buttonresetsts.UseVisualStyleBackColor = true;
            this.buttonresetsts.Click += new System.EventHandler(this.button2_Click);
            // 
            // label140
            // 
            this.label140.ForeColor = System.Drawing.SystemColors.Control;
            this.label140.Location = new System.Drawing.Point(809, 582);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(244, 23);
            this.label140.TabIndex = 37;
            this.label140.Text = "Feito por Bruno Bohrer FebRacing 2024";
            this.label140.Visible = false;
            // 
            // buttonGithub
            // 
            this.buttonGithub.Location = new System.Drawing.Point(1067, 12);
            this.buttonGithub.Name = "buttonGithub";
            this.buttonGithub.Size = new System.Drawing.Size(69, 23);
            this.buttonGithub.TabIndex = 38;
            this.buttonGithub.Text = "GitHub";
            this.buttonGithub.UseVisualStyleBackColor = true;
            this.buttonGithub.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(937, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 23);
            this.button2.TabIndex = 39;
            this.button2.Text = "Graficos";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(812, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 23);
            this.button3.TabIndex = 40;
            this.button3.Text = "WIP";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(771, 393);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxSmall
            // 
            this.pictureBoxSmall.Enabled = false;
            this.pictureBoxSmall.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSmall.Image")));
            this.pictureBoxSmall.Location = new System.Drawing.Point(812, 318);
            this.pictureBoxSmall.Name = "pictureBoxSmall";
            this.pictureBoxSmall.Size = new System.Drawing.Size(324, 208);
            this.pictureBoxSmall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSmall.TabIndex = 42;
            this.pictureBoxSmall.TabStop = false;
            this.pictureBoxSmall.Visible = false;
            // 
            // checkBoxReceiveAsBytes
            // 
            this.checkBoxReceiveAsBytes.AutoSize = true;
            this.checkBoxReceiveAsBytes.Checked = true;
            this.checkBoxReceiveAsBytes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReceiveAsBytes.Location = new System.Drawing.Point(160, 444);
            this.checkBoxReceiveAsBytes.Name = "checkBoxReceiveAsBytes";
            this.checkBoxReceiveAsBytes.Size = new System.Drawing.Size(88, 20);
            this.checkBoxReceiveAsBytes.TabIndex = 43;
            this.checkBoxReceiveAsBytes.Text = "API mode";
            this.checkBoxReceiveAsBytes.UseVisualStyleBackColor = true;
            // 
            // textBox1dbi
            // 
            this.textBox1dbi.Location = new System.Drawing.Point(923, 257);
            this.textBox1dbi.Name = "textBox1dbi";
            this.textBox1dbi.Size = new System.Drawing.Size(56, 22);
            this.textBox1dbi.TabIndex = 44;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(809, 260);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 16);
            this.label14.TabIndex = 45;
            this.label14.Text = "Xbee signal(Dbi):";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(849, 532);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(212, 69);
            this.textBox1.TabIndex = 46;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(978, 263);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(28, 16);
            this.label15.TabIndex = 47;
            this.label15.Text = "Dbi";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 614);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBox1dbi);
            this.Controls.Add(this.checkBoxReceiveAsBytes);
            this.Controls.Add(this.pictureBoxSmall);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonGithub);
            this.Controls.Add(this.label140);
            this.Controls.Add(this.buttonresetsts);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxxbeepkttotal);
            this.Controls.Add(this.textBoxhttppkttotal);
            this.Controls.Add(this.textBoxxbeepktlost);
            this.Controls.Add(this.textBoxhttppppktlost);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxxbeeptkloss);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxesp2);
            this.Controls.Add(this.textBoxhttppktloss);
            this.Controls.Add(this.textBoxesp1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxIp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBoxDebug);
            this.Controls.Add(this.textBoxhttplog);
            this.Controls.Add(this.textBoxseriallog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.httpout);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.dsc);
            this.Controls.Add(this.comboBoxBaudRate);
            this.Controls.Add(this.textBoxReceivedData);
            this.Controls.Add(this.cnt);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.rfx);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Telemetria FebRacing";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSmall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rfx;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Button cnt;
        private System.Windows.Forms.TextBox textBoxReceivedData;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Button dsc;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox httpout;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxseriallog;
        private System.Windows.Forms.TextBox textBoxhttplog;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxIp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxesp1;
        private System.Windows.Forms.TextBox textBoxhttppktloss;
        private System.Windows.Forms.TextBox textBoxesp2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxxbeeptkloss;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxhttppppktlost;
        private System.Windows.Forms.TextBox textBoxxbeepktlost;
        private System.Windows.Forms.TextBox textBoxhttppkttotal;
        private System.Windows.Forms.TextBox textBoxxbeepkttotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonresetsts;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.Button buttonGithub;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBoxSmall;
        private System.Windows.Forms.CheckBox checkBoxReceiveAsBytes;
        private System.Windows.Forms.TextBox textBox1dbi;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label15;
    }
}

