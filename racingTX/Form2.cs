using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace racingTX
{
    public partial class JsonVariablesForm : Form
    {
        private HttpListener httpListener;
        private List<Chart> charts;

        public JsonVariablesForm()
        {
            InitializeComponent();
            InitializeCharts();
            StartHttpListener();
        }

        private void StartHttpListener()
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:8080/"); // Prefixo para escutar solicitações HTTP

            try
            {
                httpListener.Start();
                Log("Servidor HTTP iniciado. Aguardando conexões...");

                // Iniciar thread para lidar com as solicitações HTTP
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    while (httpListener.IsListening)
                    {
                        try
                        {
                            HttpListenerContext context = httpListener.GetContext();
                            ProcessRequest(context);
                        }
                        catch (Exception ex)
                        {
                            Log($"Erro ao processar solicitação HTTP: {ex.Message}");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Log($"Erro ao iniciar servidor HTTP: {ex.Message}");
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            try
            {
                // Verificar se a solicitação é um POST
                if (context.Request.HttpMethod == "POST")
                {
                    using (StreamReader reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                    {
                        string jsonData = reader.ReadToEnd(); // Ler dados do corpo do POST
                        UpdateChartsWithJson(jsonData); // Atualizar gráficos com os dados JSON recebidos
                    }

                    // Responder ao cliente com uma mensagem simples de sucesso
                    byte[] responseBytes = Encoding.UTF8.GetBytes("POST recebido com sucesso!");
                    context.Response.ContentType = "text/plain";
                    context.Response.ContentLength64 = responseBytes.Length;
                    context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                }
                else
                {
                    // Responder ao cliente se a solicitação não for um POST
                    context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                }
            }
            catch (Exception ex)
            {
                Log($"Erro ao processar solicitação HTTP: {ex.Message}");
            }
            finally
            {
                context.Response.Close();
            }
        }

        private void UpdateChartsWithJson(string json)
        {
            try
            {
                // Desserializar JSON em um objeto TelemetryData
                var telemetryData = Newtonsoft.Json.JsonConvert.DeserializeObject<TelemetryData>(json);

                // Capturar o timestamp atual
                DateTime timestamp = DateTime.Now;

                // Atualizar gráficos com os dados correspondentes e o timestamp
                UpdateChart(charts[0], timestamp, telemetryData.temperatura_motor);
                UpdateChart(charts[1], timestamp, telemetryData.rpm);
                UpdateChart(charts[2], timestamp, telemetryData.pressao_oleo);
                UpdateChart(charts[3], timestamp, telemetryData.tensao_bateria);

                // Atualizar gráficos para Potenciômetros
                UpdateChart(charts[4], timestamp, telemetryData.potenciometros.pot1);
                UpdateChart(charts[5], timestamp, telemetryData.potenciometros.pot2);
                UpdateChart(charts[6], timestamp, telemetryData.potenciometros.pot3);
                UpdateChart(charts[7], timestamp, telemetryData.potenciometros.pot4);

                // Atualizar gráfico para GPS
                UpdateChart(charts[8], timestamp, telemetryData.GPS.Speed);

                // Atualizar gráficos para MHPS
                UpdateChart(charts[9], timestamp, telemetryData.MHPS.MHPS1);
                UpdateChart(charts[10], timestamp, telemetryData.MHPS.MHPS2);

                // Atualizar gráficos para MLX
                UpdateChart(charts[11], timestamp, telemetryData.MLX.MLX1);
                UpdateChart(charts[12], timestamp, telemetryData.MLX.MLX2);
                UpdateChart(charts[13], timestamp, telemetryData.MLX.MLX3);
                UpdateChart(charts[14], timestamp, telemetryData.MLX.MLX4);
            }
            catch (Exception ex)
            {
                Log($"Erro ao atualizar gráficos com JSON: {ex.Message}");
            }
        }

        private void UpdateChart(Chart chart, DateTime timestamp, double dataValue)
        {
            if (chart == null)
            {
                Log("O gráfico não foi inicializado corretamente.");
                return;
            }

            if (chart.InvokeRequired)
            {
                chart.Invoke(new Action(() => UpdateChart(chart, timestamp, dataValue)));
                return;
            }

            try
            {
                // Verifica se chart possui Series e se a primeira série (índice 0) não é null
                if (chart.Series.Count > 0 && chart.Series[0] != null)
                {
                    // Define o intervalo de tempo entre atualizações (em segundos)
                    double intervalSeconds = 1; // Assuming data arrives every 1 second

                    // Interpolação entre o último ponto e o novo ponto
                    DateTime lastTimestamp = DateTime.FromOADate(chart.Series[0].Points.Count > 0 ? chart.Series[0].Points.Last().XValue : timestamp.AddSeconds(-1).ToOADate());
                    double lastValue = chart.Series[0].Points.Count > 0 ? chart.Series[0].Points.Last().YValues[0] : dataValue;

                    // Adiciona pontos interpolados entre o último ponto e o novo ponto
                    for (double t = intervalSeconds; t > 0; t--)
                    {
                        double interpolatedValue = lastValue + (dataValue - lastValue) * (intervalSeconds - t) / intervalSeconds;
                        DateTime interpolatedTime = lastTimestamp.AddSeconds(t - intervalSeconds);
                        chart.Series[0].Points.AddXY(interpolatedTime, interpolatedValue);
                    }

                    // Adiciona o ponto de dados atual
                    chart.Series[0].Points.AddXY(timestamp, dataValue);

                    // Define o intervalo máximo de tempo para manter (1 minuto antes do tempo atual)
                    DateTime removeThreshold = timestamp.AddMinutes(-1);

                    // Remove os pontos de dados mais antigos que o intervalo desejado
                    while (chart.Series[0].Points.Count > 0 && chart.Series[0].Points[0].XValue < removeThreshold.ToOADate())
                    {
                        chart.Series[0].Points.RemoveAt(0);
                    }

                    // Ajusta a área do gráfico para mostrar apenas o último minuto de dados
                    chart.ChartAreas[0].AxisX.Minimum = removeThreshold.ToOADate();
                    chart.ChartAreas[0].AxisX.Maximum = timestamp.ToOADate();

                    // Atualiza o gráfico
                    chart.Invalidate();
                }
                else
                {
                    Log("O gráfico não possui uma série válida.");
                }
            }
            catch (Exception ex)
            {
                Log($"Erro ao atualizar gráfico: {ex.Message}");
            }
        }


        private void InitializeCharts()
        {
            // Cria a lista para armazenar os gráficos
            charts = new List<Chart>();

            // Cria um TableLayoutPanel para organizar os gráficos
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                RowCount = 5,  // 5 linhas para acomodar os 15 gráficos em 3 colunas
                ColumnCount = 3,  // 3 colunas para acomodar os 15 gráficos
                Dock = DockStyle.Fill
            };

            // Ajusta as propriedades do TableLayoutPanel
            for (int i = 0; i < 5; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            }
            for (int i = 0; i < 3; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }

            // Títulos para cada gráfico
            string[] chartTitles = {
        "Temperatura Motor (°C)", "RPM", "Pressao Oleo (PSI)",
        "Tensao Bateria (V)", "Pot1", "Pot2", "Pot3", "Pot4",
        "GPS Speed (Km/H)", "MHPS1 (PSI)", "MHPS2 (PSI)", "MLX1 (°C)", "MLX2 (°C)", "MLX3 (°C)", "MLX4 (°C)"
    };

            // Cria e inicializa 15 gráficos
            for (int i = 0; i < 15; i++)
            {
                Chart chart = new Chart
                {
                    Dock = DockStyle.Fill
                };
                InitializeChart(chart, chartTitles[i]); // Passa o título único
                tableLayoutPanel.Controls.Add(chart, i % 3, i / 3);

                // Adiciona o gráfico à lista
                charts.Add(chart);
            }

            // Adiciona o TableLayoutPanel ao formulário
            this.Controls.Add(tableLayoutPanel);
        }



        private void InitializeChart(Chart chart, string title)
        {
            // Configura a área do gráfico
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(chartArea);

            // Configura a série do gráfico
            Series series = new Series
            {
                Name = "Series1",
                Color = System.Drawing.Color.Blue,
                BorderWidth = 2,
                ChartType = SeriesChartType.Line
            };

            chart.Series.Clear();
            chart.Series.Add(series);

            // Configura o título do gráfico
            chart.Titles.Clear();
            chart.Titles.Add(title);

            // Configura o intervalo do eixo X (de 60 a 0 segundos)
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 60;
            chartArea.AxisX.Interval = 10; // Intervalo de 10 segundos
            chartArea.AxisX.Title = "Tempo (ultimo minuto)"; // Título do eixo X
            chartArea.AxisX.LabelStyle.Enabled = false;
        }

        private void Log(string message)
        {
            // Exemplo: exibir mensagens em um TextBox na interface do usuário
              if (textBox1.InvokeRequired)
              {
                  textBox1.Invoke(new Action(() =>
                  {
                      Log(message);
                  }));
              }
              else
              {
                  textBox1.AppendText($"{message}\n");
              }
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.Cancel; // Ou outro resultado adequado
            }
        }
        // Definição da classe TelemetryData
        protected override void OnClosed(EventArgs e)
        {
            // Encerra o HttpListener e libera recursos
            if (httpListener != null)
            {
                httpListener.Stop();
                httpListener.Close();
            }

            base.OnClosed(e);
        }

        // Classe TelemetryData para desserialização JSON
        // Classe principal para dados de telemetria
        public class TelemetryData
        {
            public DateTime Timestamp { get; set; } // Adiciona um campo para o tempo
            public float temperatura_motor { get; set; }
            public float rpm { get; set; }
            public float pressao_oleo { get; set; }
            public float tensao_bateria { get; set; }
            public MPUData mpu1_gyro { get; set; }
            public MPUData mpu2_gyro { get; set; }
            public MPUData mpu1_acele { get; set; }
            public MPUData mpu2_acele { get; set; }
            public PotenciometrosData potenciometros { get; set; }
            public GPSData GPS { get; set; }
            public MHPSData MHPS { get; set; }
            public MLXData MLX { get; set; }
        }

        // Classe para dados de um MPU (aceleração e giroscópio)
        public class MPUData
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        // Classe para dados de potenciômetros
        public class PotenciometrosData
        {
            public float pot1 { get; set; }
            public float pot2 { get; set; }
            public float pot3 { get; set; }
            public float pot4 { get; set; }
        }

        // Classe para dados de GPS
        public class GPSData
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Speed { get; set; }  // Alterado para double para aceitar valores decimais
            public double Altitude { get; set; }
        }

        // Classe para dados de MHPS
        public class MHPSData
        {
            public float MHPS1 { get; set; }
            public float MHPS2 { get; set; }
        }

        // Classe para dados de MLX
        public class MLXData
        {
            public float MLX1 { get; set; }
            public float MLX2 { get; set; }
            public float MLX3 { get; set; }
            public float MLX4 { get; set; }
        }

        // Classe para dados de sensores (eixo X, Y, Z)
        public class SensorData
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

    }
}