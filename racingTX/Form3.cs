using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json;

namespace racingTX
{
    public partial class Form3 : Form
    {
        private HttpListener httpListener;
        private List<Chart> charts;
        private int counter;
        public Form3()
        {
            InitializeComponent();
            StartHttpListener();
            InitializeChartasg(chart1);
            InitializeChartXtime(chart2, "Acel X MPU 1");
            InitializeChartXtime(chart3, "Acel Y");
            InitializeChartXtime(chart4, "Acel Z");
            InitializeChartXtime(chart7, "Acel X MPU 2");
            InitializeChartXtime(chart6, "Acel Y");
            InitializeChartXtime(chart5, "Acel Z");

            InitializeChartXtimeazul(chart10, "gyro X MPU 2");
            InitializeChartXtimeazul(chart9, "gyro Y");
            InitializeChartXtimeazul(chart8, "gyro Z");
            InitializeChartXtimeazul(chart13, "gyro X MPU 1");
            InitializeChartXtimeazul(chart12, "gyro Y");
            InitializeChartXtimeazul(chart11, "gyro Z");
        }

        private void StartHttpListener()
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:8081/"); // Prefixo para escutar solicitações HTTP

            try
            {
                httpListener.Start();
                Log("Servidor HTTP iniciado. Aguardando conexões...");

                // Iniciar thread para lidar com as solicitações HTTP
                ThreadPool.QueueUserWorkItem(async (state) =>
                {
                    while (httpListener.IsListening)
                    {
                        try
                        {
                            var context = await httpListener.GetContextAsync();
                            await ProcessRequestAsync(context);
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
        private void InitializeChartXtime(Chart chart, string title)
        {
            // Configura a área do gráfico
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(chartArea);

            // Configura a série do gráfico
            Series series = new Series
            {
                Name = "Series1",
                Color = System.Drawing.Color.Red,
                BorderWidth = 2,
                IsVisibleInLegend = false, // Hides the series from the legend
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
        private void InitializeChartXtimeazul(Chart chart, string title)
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
                IsVisibleInLegend = false, // Hides the series from the legend
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
        private void InitializeChartasg(Chart chart)
        {
            if (chart == null)
            {
                Log("O gráfico não foi inicializado corretamente.");
                return;
            }

            // Limpa quaisquer séries ou áreas de gráfico existentes
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            // Adiciona uma área de gráfico
            ChartArea chartArea = new ChartArea("MainChartArea");
            chart.ChartAreas.Add(chartArea);

            // Configura as propriedades da área do gráfico
            chartArea.AxisX.Title = "G";
            chartArea.AxisX.LabelStyle.Format = ""; // Formato padrão para valores de aceleração
            chartArea.AxisX.Interval = 0.5; // Intervalo ajustável conforme necessário
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.Minimum = -1.5; // Limite mínimo do eixo X
            chartArea.AxisX.Maximum = 1.5;  // Limite máximo do eixo X
            chartArea.AxisX.MajorTickMark.Interval = 0.5;
            chartArea.AxisX.MajorTickMark.LineColor = Color.Black;
            chartArea.AxisX.MinorTickMark.Interval = 0.1;
            chartArea.AxisX.MinorTickMark.LineColor = Color.Black;

            chartArea.AxisY.Title = "G";
            chartArea.AxisY.LabelStyle.Format = ""; // Formato padrão para valores de aceleração
            chartArea.AxisY.Interval = 0.5; // Intervalo ajustável conforme necessário
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.Minimum = -1.5; // Limite mínimo do eixo Y
            chartArea.AxisY.Maximum = 1.5;  // Limite máximo do eixo Y
            chartArea.AxisY.MajorTickMark.Interval = 0.5;
            chartArea.AxisY.MajorTickMark.LineColor = Color.Black;
            chartArea.AxisY.MinorTickMark.Interval = 0.1;
            chartArea.AxisY.MinorTickMark.LineColor = Color.Black;

            chartArea.AxisX.Crossing = 0;
            chartArea.AxisY.Crossing = 0;
            chartArea.AxisX.LineColor = Color.Black;
            chartArea.AxisY.LineColor = Color.Black;

            // Adiciona uma série de dados
            Series series = new Series("DataSeries");
            series.ChartType = SeriesChartType.Line; // Tipo de gráfico (linha, ponto, etc.)
            series.XValueType = ChartValueType.Double; // Tipo de valor para o eixo X (número)
            series.YValueType = ChartValueType.Double; // Tipo de valor para o eixo Y (número)
            series.BorderWidth = 2; // Largura da linha do gráfico
            series.MarkerStyle = MarkerStyle.None; // Estilo do marcador para pontos de dados

            chart.Series.Add(series);

            // Configura o estilo do gráfico
            chart.BackColor = Color.White;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.Legends.Clear(); // Remove legendas se não forem necessárias

            // Adiciona texto para legendas personalizadas (opcional)
            TextAnnotation textAnnotation = new TextAnnotation();
            textAnnotation.Text = "GG Diagram";
            textAnnotation.X = 70; // Posição no gráfico (ajustar conforme necessário)
            textAnnotation.Y = 5;
            chart.Annotations.Add(textAnnotation);

            textAnnotation = new TextAnnotation();
            textAnnotation.Text = "Typical Car";
            textAnnotation.X = 60; // Posição no gráfico (ajustar conforme necessário)
            textAnnotation.Y = 90;
            chart.Annotations.Add(textAnnotation);

            textAnnotation = new TextAnnotation();
            textAnnotation.Text = "Aceleração";
            textAnnotation.X = 55; // Posição no gráfico (ajustar conforme necessário)
            textAnnotation.Y = 0;
            chart.Annotations.Add(textAnnotation);

            textAnnotation = new TextAnnotation();
            textAnnotation.Text = "Frenagem";
            textAnnotation.X = 55; // Posição no gráfico (ajustar conforme necessário)
            textAnnotation.Y = 90;
            chart.Annotations.Add(textAnnotation);

            textAnnotation = new TextAnnotation();
            textAnnotation.Text = "Curva à Direita";
            textAnnotation.X = 85; // Posição no gráfico (ajustar conforme necessário)
            textAnnotation.Y = 45;
            chart.Annotations.Add(textAnnotation);

            textAnnotation = new TextAnnotation();
            textAnnotation.Text = "Curva à Esquerda";
            textAnnotation.X = 20; // Posição no gráfico (ajustar conforme necessário)
            textAnnotation.Y = 45;
            chart.Annotations.Add(textAnnotation);

            // Atualiza o gráfico
            chart.Invalidate();
        }

        private async Task ProcessRequestAsync(HttpListenerContext context)
        {
            try
            {
                // Verificar se a solicitação é um POST
                if (context.Request.HttpMethod == "POST")
                {
                    using (StreamReader reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                    {
                        string jsonData = await reader.ReadToEndAsync(); // Ler dados do corpo do POST
                        UpdateChartsWithJson(jsonData); // Atualizar gráficos com os dados JSON recebidos
                    }

                    // Responder ao cliente com uma mensagem simples de sucesso
                    byte[] responseBytes = Encoding.UTF8.GetBytes("POST recebido com sucesso!");
                    context.Response.ContentType = "text/plain";
                    context.Response.ContentLength64 = responseBytes.Length;
                    await context.Response.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
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

        private void UpdateChartbytime(Chart chart, DateTime timestamp, double dataValue)
        {
            if (chart == null)
            {
                Log("O gráfico não foi inicializado corretamente.");
                return;
            }

            if (chart.InvokeRequired)
            {
                chart.Invoke(new Action(() => UpdateChartbytime(chart, timestamp, dataValue)));
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
        private void UpdateChartsWithJson(string json)
        {
            try
            {
                // Desserializar JSON em um objeto TelemetryData
                var telemetryData = JsonConvert.DeserializeObject<TelemetryData>(json);
                DateTime timestamp = DateTime.Now;
                // Atualizar gráficos com os dados correspondentes
                UpdateChart(chart1, telemetryData.mpu1_acele.Y, telemetryData.mpu1_acele.X);
                UpdateChartbytime(chart2, timestamp, telemetryData.mpu1_acele.X);
                UpdateChartbytime(chart3, timestamp, telemetryData.mpu1_acele.Y);
                UpdateChartbytime(chart4, timestamp, telemetryData.mpu1_acele.Z);
                UpdateChartbytime(chart7, timestamp, telemetryData.mpu2_acele.X);
                UpdateChartbytime(chart6, timestamp, telemetryData.mpu2_acele.Y);
                UpdateChartbytime(chart5, timestamp, telemetryData.mpu2_acele.Z);
                UpdateChartbytime(chart8, timestamp, telemetryData.mpu1_gyro.X);
                UpdateChartbytime(chart9, timestamp, telemetryData.mpu1_gyro.Y);
                UpdateChartbytime(chart10, timestamp, telemetryData.mpu1_gyro.Z);
                UpdateChartbytime(chart13, timestamp, telemetryData.mpu2_gyro.X);
                UpdateChartbytime(chart12, timestamp, telemetryData.mpu2_gyro.Y);
                UpdateChartbytime(chart11, timestamp, telemetryData.mpu2_gyro.Z);
            }
            catch (Exception ex)
            {
                Log($"Erro ao atualizar gráficos com JSON: {ex.Message}");
            }
        }

        private void UpdateChart(Chart chart, double xValue, double yValue)
        {
            if (chart == null)
            {
                Log("O gráfico não foi inicializado corretamente.");
                return;
            }

            if (chart.InvokeRequired)
            {
                chart.Invoke(new Action(() => UpdateChart(chart, xValue, yValue)));
                return;
            }

            try
            {
                // Verifica se chart possui Series e se a primeira série (índice 0) não é null
                if (chart.Series.Count > 0 && chart.Series[0] != null)
                {
                    // Limita o eixo X ao intervalo de -1.5 a 1.5
                    if (xValue < -1.5 || xValue > 1.5)
                    {
                        Log("Valor de aceleração X fora do intervalo permitido.");
                        return;
                    }

                    // Adiciona o ponto de dados atual
                    chart.Series[0].Points.AddXY(xValue, yValue);

                    // Garante que o gráfico contenha no máximo 100 pontos de dados
                    while (chart.Series[0].Points.Count > 160)
                    {
                        chart.Series[0].Points.RemoveAt(0);
                    }

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
