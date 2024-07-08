using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using System.Timers;


namespace racingTX
{
    public partial class Form1 : Form 
    {
        private bool doone = true;
        private StringBuilder receivedDataBuffer = new StringBuilder();
        private bool isFullScreen = false;
        private System.Timers.Timer timer;
        private bool variableState = false;
        private const int expandedHeight = 660;
        private const int collapsedHeight = 535;
        private const int expandedWidth = 1167;
        private const int collapsedWidth = 818;
        private SerialPort serialPort;
        private bool isSendingData = false;
        private string serverIP;
        private int esp1;
        private int esp2;
        private int httppktlost = 0;
        private int httppkttotal = -1;
        private int xbeepktlost = 0;
        private int xbeepkttotal = 0;
        private StringBuilder receivedData = new StringBuilder();
        private string esp1Data = null;
        private string esp2Data = null;
        private int time;
        private string internalS = "http://localhost:8080/";
        private string internalS2 = "http://localhost:8081/";
        private string db;
        private string esp1aData = null;
        private string esp1bData = null;
        public Form1()
        {
            InitializeComponent();
            LoadBaudRates();
            LoadServerIP();
            colaps();
        }

        private void LoadBaudRates()
        {
            int[] baudRates = { 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 57600, 115200 };
            comboBoxBaudRate.Items.Clear();
            foreach (int baudRate in baudRates)
            {
                comboBoxBaudRate.Items.Add(baudRate);
            }
            comboBoxBaudRate.SelectedItem = 115200;
        }

        private void rfx_Click(object sender, EventArgs e)
        {
            string[] portNames = SerialPort.GetPortNames();
            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(portNames);
            if (portNames.Length > 0)
            {
                comboBoxPorts.SelectedIndex = 0;
                cnt.Enabled = true;
                comboBoxPorts.Enabled = true;
                comboBoxBaudRate.Enabled = true;
            }
            else
            {
                MessageBox.Show("Nenhuma porta serial encontrada.");
            }
        }

        private void cnt_Click(object sender, EventArgs e)
        {
            if (comboBoxPorts.SelectedItem != null && comboBoxBaudRate.SelectedItem != null)
            {
                string portName = comboBoxPorts.SelectedItem.ToString();
                int baudRate = (int)comboBoxBaudRate.SelectedItem;
                httpout.Visible = true;
                pictureBox1.Visible = false;
                textBoxReceivedData.Visible = true;

                try
                {
                    serialPort = new SerialPort(portName)
                    {
                        BaudRate = baudRate,
                        Parity = Parity.None,
                        StopBits = StopBits.One,
                        DataBits = 8,
                        Handshake = Handshake.None,
                        RtsEnable = true
                    };

                    serialPort.Open();
                    MessageBox.Show($"Conectado à {portName} com Baud Rate {baudRate}");
                    cnt.Visible = false;
                    dsc.Visible = true;
                    sendButton.Visible = true;
                    sendButton.Enabled = true;
                    serialPort.DataReceived += SerialPort_DataReceived;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao conectar à porta serial: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma porta serial e uma taxa de transmissão.");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                if (checkBoxReceiveAsBytes.Checked)
                {
                    // Processar dados como bytes
                    int bytesToRead = serialPort.BytesToRead;
                    byte[] buffer = new byte[bytesToRead];
                    serialPort.Read(buffer, 0, bytesToRead);
                    
                    // Converte bytes para string e adiciona ao buffer
                    string data = Encoding.ASCII.GetString(buffer);
                    receivedDataBuffer.Append(data);
                    ExtractPacket(buffer);
                }
                else
                {
                    // Processar dados como string
                    string data = serialPort.ReadExisting();
                    receivedDataBuffer.Append(data);
                }

                ProcessReceivedData();
            }
        }
        public void ExtractPacket(byte[] buffer)
        {
            // Definir a sequência inicial do pacote
            byte[] startSequence = new byte[] { 0x7E, 0x00, 0x06 };

            // Procurar a sequência inicial no buffer
            for (int i = 0; i <= buffer.Length - startSequence.Length; i++)
            {

                bool sequenceFound = true;
                for (int j = 0; j < startSequence.Length; j++)
                {
                    if (buffer[i + j] != startSequence[j])
                    {
                        sequenceFound = false;
                        break;
                    }
                }

                // Se a sequência foi encontrada, verificar se há bytes suficientes para extrair o penúltimo byte
                if (sequenceFound)
                {

                    int packetStart = i;
                    int packetLength = buffer.Length - packetStart;

                    if (packetLength >= 2)
                    {
                        string hexString = BitConverter.ToString(buffer);
                        this.Invoke(new MethodInvoker(delegate {
                            textBox1.Text = hexString;
                        }));
                        GetValueFromHexString(hexString);
                    }
                }
            }
        }
        public void GetValueFromHexString(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                db = "";
                return;
            }

            // Split the string by hyphen
            string[] parts = hexString.Split('-');

            // Check if the expected part exists
            if (parts.Length < 9)
            {
                db = "";
                return;
            }

            // Return the value at the expected position
           int dbhex = -Convert.ToInt32(parts[8], 16); // Index 8 corresponds to the 9th element in the zero-based array
            db = dbhex.ToString();
        }
        private void ProcessReceivedData()
        {
            string data = receivedDataBuffer.ToString();
            if (doone)
            {
                doone = false;
                StartTimer();
            }

            // Procurar por mensagens completas terminadas com '\n'
            while (data.Contains("\n"))
            {
                int index = data.IndexOf("\n");
                string message = data.Substring(0, index + 1);
                data = data.Substring(index + 1);

                if (!string.IsNullOrEmpty(message))
                {
                    string trimmedMessage = message.Trim();

                    // Encontrar a posição de "esp1a", "esp1b" ou "esp2" na mensagem
                    int esp1aPos = trimmedMessage.IndexOf("esp1a");
                    int esp1bPos = trimmedMessage.IndexOf("esp1b");
                    int esp2Pos = trimmedMessage.IndexOf("esp2");

                    if (esp1aPos != -1)
                    {
                        string esp1aMessage = trimmedMessage.Substring(esp1aPos);
                        if (esp1aMessage.StartsWith("esp1a"))
                        {
                            esp1aData = esp1aMessage.Substring(5).Trim();
                        }
                    }

                    if (esp1bPos != -1)
                    {
                        string esp1bMessage = trimmedMessage.Substring(esp1bPos);
                        if (esp1bMessage.StartsWith("esp1b"))
                        {
                            esp1bData = esp1bMessage.Substring(5).Trim();
                        }
                    }

                    if (esp1aData != null && esp1bData != null)
                    {
                        esp1Data = esp1aData + "," + esp1bData;
                        esp1aData = null; // Reset after concatenation
                        esp1bData = null; // Reset after concatenation

                        this.Invoke(new MethodInvoker(delegate
                        {
                            textBoxReceivedData.AppendText("esp1 " + esp1Data + "\n");

                            if (isSendingData && esp1Data != null && esp2Data != null)
                            {
                                SendDataToServer(esp1Data, esp2Data);
                                esp1Data = null;
                                esp2Data = null;
                                xbeepkttotal++;
                            }
                        }));
                    }

                    if (esp2Pos != -1)
                    {
                        string esp2Message = trimmedMessage.Substring(esp2Pos);
                        if (esp2Message.StartsWith("esp2"))
                        {
                            esp2Data = esp2Message.Substring(4).Trim();
                        }

                        this.Invoke(new MethodInvoker(delegate
                        {
                            textBoxReceivedData.AppendText(esp2Message + "\n");

                            if (isSendingData && esp1Data != null && esp2Data != null)
                            {
                                SendDataToServer(esp1Data, esp2Data);
                                esp1Data = null;
                                esp2Data = null;
                                xbeepkttotal++;
                            }
                        }));
                    }
                }
            }

            receivedDataBuffer = new StringBuilder(data);
        }

        public void SendCommand()
        {
            byte[] command = new byte[] { 0x7E, 0x00, 0x04, 0x08, 0x01, 0x44, 0x42, 0x70 };
            try
            {
               // serialPort.Write(command, 0, command.Length);
            }
            catch (Exception ex)
            {
                textBoxseriallog.AppendText("erro ao enviar byte de sinal");
            }
        }
            private async void SendDataToServer(string data, string data2)
        {
            try
            {
                SendCommand();
                if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(data2))
                {
                    textBoxseriallog.AppendText("Erro: Dados não podem ser nulos ou vazios.");
                    xbeepktlost++;
                    return;
                }
                StopTimer();
                httppkttotal++;
                textBoxesp1.Text = esp1.ToString();
                textBoxesp2.Text = esp2.ToString();
                textBoxIp.Text = serverIP;
                textBoxhttppktloss.Text = (((float)httppktlost / httppkttotal) * 100).ToString();
                textBoxxbeeptkloss.Text = (((float)xbeepktlost / xbeepkttotal) * 100).ToString();
                textBoxhttppkttotal.Text = httppkttotal.ToString();
                textBoxhttppppktlost.Text = httppktlost.ToString();
                textBoxxbeepktlost.Text = xbeepktlost.ToString();
                textBoxxbeepkttotal.Text = xbeepkttotal.ToString();
                textBox1dbi.Text = db ;
                HttpClient client = new HttpClient();
                string url = serverIP;
                string jsonTemplate = File.ReadAllText("template.json");

                // Assume `data` and `data2` contain the values separated by a comma
                string[] dataValues1 = data.Split(',');
                string[] dataValues2 = data2.Split(',');

                // Verify if both arrays have the expected length
                if (dataValues1.Length < esp1 || dataValues2.Length < esp2)
                {
                    textBoxseriallog.AppendText("Erro: Dados insuficientes recebidos.");
                    xbeepktlost++;
                    return;
                }

                // Replace placeholders in the template with actual data values
                string jsonData = jsonTemplate;
                for (int i = 0; i < dataValues1.Length; i++)
                {
                    jsonData = jsonData.Replace($"{{dataValues1[{i}]}}", dataValues1[i]);
                }
                for (int i = 0; i < dataValues2.Length; i++)
                {
                    jsonData = jsonData.Replace($"{{dataValues2[{i}]}}", dataValues2[i]);
                }

                // Display the JSON data in the text box
                this.Invoke(new MethodInvoker(delegate
                {
                    httpout.Text = jsonData;
                }));

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var content2 = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var content3 = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content2);
                
                try
                {
                    HttpResponseMessage rsp = await client.PostAsync(internalS, content);
                }
                catch (Exception ex)
                {

                }
                try
                {
                    HttpResponseMessage rsp2 = await client.PostAsync(internalS2, content3);
                }
                catch (Exception ex)
                {

                }
                if (response.IsSuccessStatusCode)
                {
                    textBoxhttplog.AppendText("Dados enviados com sucesso para o servidor.");
                    
                }
                else
                {
                    textBoxhttplog.AppendText($"Erro ao enviar dados para o servidor: {response.StatusCode}");
                    httppktlost++;
                }
            }
            catch (Exception ex)
            {
                textBoxhttplog.AppendText($"Erro ao enviar dados para o servidor: {ex.Message}");
                httppktlost++;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoadServerIP()
        {
            try
            {
                string[] lines = File.ReadAllLines("ip.cfg");

                // Assuming ip.cfg contains three lines: IP address, username, and password
                if (lines.Length >= 3)
                {
                    serverIP = lines[0].Trim(); // Assuming the IP address is on the first line
                    esp1 = int.Parse(lines[1].Trim()); // Assuming the username is on the second line
                    esp2 = int.Parse(lines[2].Trim()); // Assuming the password is on the third line
                    time = int.Parse(lines[3].Trim());
                }
                else
                {
                    throw new Exception("The file 'ip.cfg' does not contain enough lines.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar as informações do arquivo de configuração: {ex.Message}");
                serverIP = "";
                esp1 = 420;
                esp2 = 69;
                time = 1000;
                // Write default values to ip.cfg
                File.WriteAllLines("ip.cfg", new string[] { "ip here", "esp1 numero de variaveis here", "esp2 numero de variaveis here", "tempo medio de transmissao do xbee" });
            }
        }

        private void dsc_Click(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    serialPort.Close();
                    MessageBox.Show("Desconectado com sucesso.");
                    cnt.Visible = true;
                    dsc.Visible = false;
                    sendButton.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao desconectar: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("A porta serial já está desconectada.");
                cnt.Visible = true;
                dsc.Visible = false;
                sendButton.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Inverte o estado da variável
            variableState = !variableState;

            // Troca o texto do botão baseado no estado atual da variável
            if (variableState)
            {
                button1.Text = "App Debug";
                this.Width = expandedWidth;
                pictureBoxSmall.Visible = true;
                pictureBoxSmall.Enabled = true;
            }
            else
            {
                button1.Text = "App Debug";
                this.Width = collapsedWidth;
            }
        }

        private void sendButton_Click_1(object sender, EventArgs e)
        {
            if (!isSendingData)
            {
                isSendingData = true;
                sendButton.Text = "Parar Envio";
            }
            else
            {
                isSendingData = false;
                sendButton.Text = "Iniciar Envio";
            }
        }

        private void colaps()
        {
            this.Height = collapsedHeight;
            this.Width = collapsedWidth;
            textBoxseriallog.Visible = false;
            textBoxhttplog.Visible = false;
            label1.Visible = false;
            label2.Visible = false;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDebug.Checked)
            {
                // Expand the window and show the TextBox
                this.Height = expandedHeight;
                textBoxseriallog.Visible = true;
                textBoxhttplog.Visible = true; 
                label1.Visible = true;
                label2.Visible = true;
            }
            else
            {
                // Collapse the window and hide the TextBox
                this.Height = collapsedHeight;
                textBoxseriallog.Visible = false;
                textBoxhttplog.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                serverIP = "https://httpbin.org/post";
            }
            else
            {
                LoadServerIP();
            }
        }

        private void textBoxseriallog_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                LoadServerIP();
            }
            else
            {
                esp1 = 0;
                esp2 = 0;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StartTimer()
        {
            timer = new System.Timers.Timer(time); // Tempo limite em milissegundos (1,5 segundos)
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true; // Não reinicia automaticamente após expirar
            timer.Start();
        }

        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Incrementa xbeepktloss quando o tempo limite é atingido
            xbeepktlost++;
            xbeepkttotal++;
            this.Invoke(new MethodInvoker(delegate {
                textBoxxbeeptkloss.Text = (((float)xbeepktlost / xbeepkttotal) * 100).ToString();
                textBoxxbeepktlost.Text = xbeepktlost.ToString();
                textBoxxbeepkttotal.Text = xbeepkttotal.ToString();
            }));
            // Faça o que for necessário aqui quando nenhum pacote é recebido dentro do limite de tempo
        }

        private void button2_Click(object sender, EventArgs e)
        {
            xbeepktlost = 0;
            xbeepkttotal = 0;
            httppktlost = 0;
            httppkttotal = 0;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           System.Diagnostics.Process.Start("https://github.com/");
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized && !isFullScreen)
            {
                int screenHeight = Screen.FromControl(this).Bounds.Height;
                isFullScreen = true;
                checkBoxDebug.Checked = true;
                httpout.Height = screenHeight - 263;
                textBoxReceivedData.Height = screenHeight - 263;
                checkBoxDebug.Location = new Point( 17, screenHeight - 217); 
                checkBox2.Location = new Point(98, screenHeight - 217);
                checkBox3.Location = new Point(410, screenHeight - 217);
                checkBoxReceiveAsBytes.Location = new Point(160, screenHeight - 217);
                label3.Location = new Point(14, screenHeight - 194);
                label1.Location = new Point(14, screenHeight - 167);
                label2.Location = new Point(407, screenHeight - 167);
                textBoxhttplog.Location = new Point(410, screenHeight - 148);
                textBoxseriallog.Location = new Point(17, screenHeight - 148);
            }
            else if (this.WindowState != FormWindowState.Maximized && isFullScreen)
            {
                int screenHeightsmall = 661;
                isFullScreen = false;
                checkBoxDebug.Checked = false;
                httpout.Height = 396;
                textBoxReceivedData.Height = 396;
                checkBoxDebug.Location = new Point(17, screenHeightsmall - 217);
                checkBox2.Location = new Point(98, screenHeightsmall - 217);
                checkBox3.Location = new Point(410, screenHeightsmall - 217);
                checkBoxReceiveAsBytes.Location = new Point(160, screenHeightsmall - 217);
                label3.Location = new Point(14, screenHeightsmall - 194);
                label1.Location = new Point(14, screenHeightsmall - 167);
                label2.Location = new Point(407, screenHeightsmall - 167);
                textBoxhttplog.Location = new Point(410, screenHeightsmall - 148);
                textBoxseriallog.Location = new Point(17, screenHeightsmall - 148);
                this.Invoke(new MethodInvoker(delegate {
                    this.Height = collapsedHeight;
                    this.Width = collapsedWidth;
                }));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Resize += new EventHandler(MainForm_Resize);
            checkBoxDebug.CheckedChanged += new EventHandler(CheckBoxFullScreen_CheckedChanged);
        }

        private void CheckBoxFullScreen_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDebug.Checked && this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else if (!checkBoxDebug.Checked && this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            JsonVariablesForm form2 = new JsonVariablesForm();

            // Show Form2
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
