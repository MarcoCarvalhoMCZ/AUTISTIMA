using Microsoft.ML;
using Microsoft.ML.Data;

namespace AUTistima.Services;

public class SentimentData
{
    [LoadColumn(0)]
    public string? Text { get; set; }

    [LoadColumn(1)]
    public bool Label { get; set; } // true = positivo, false = negativo/triste
}

public class SentimentPrediction
{
    [ColumnName("PredictedLabel")]
    public bool Prediction { get; set; }

    public float Probability { get; set; }

    public float Score { get; set; }
}

public class SentimentService
{
    private readonly MLContext _mlContext;
    private ITransformer? _model;
    private PredictionEngine<SentimentData, SentimentPrediction>? _predictionEngine;
    private readonly ILogger<SentimentService> _logger;

    public SentimentService(ILogger<SentimentService> logger)
    {
        _mlContext = new MLContext();
        _logger = logger;
        TrainModel(); // Treina ao iniciar (em produção, carregar modelo salvo)
    }

    private void TrainModel()
    {
        try
        {
            // Dados de exemplo para treinamento rápido (focado no contexto de mães atípicas)
            var data = new List<SentimentData>
            {
                new SentimentData { Text = "Estou muito feliz com o progresso do meu filho", Label = true },
                new SentimentData { Text = "Hoje foi um dia maravilhoso na escola", Label = true },
                new SentimentData { Text = "Conseguimos o laudo, finalmente!", Label = true },
                new SentimentData { Text = "Ele comeu uma fruta nova, estou radiante", Label = true },
                new SentimentData { Text = "A terapia tem ajudado muito", Label = true },
                
                new SentimentData { Text = "Estou exausta e sem forças", Label = false },
                new SentimentData { Text = "Não sei mais o que fazer com as crises", Label = false },
                new SentimentData { Text = "Me sinto muito sozinha nessa jornada", Label = false },
                new SentimentData { Text = "A escola não aceitou a matrícula, estou arrasada", Label = false },
                new SentimentData { Text = "Chorei muito hoje, está difícil", Label = false }
            };

            var dataView = _mlContext.Data.LoadFromEnumerable(data);

            var pipeline = _mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(SentimentData.Text))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            _model = pipeline.Fit(dataView);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao treinar modelo de sentimento");
        }
    }

    public (bool IsPositive, float Probability) Analyze(string text)
    {
        if (_predictionEngine == null || string.IsNullOrWhiteSpace(text))
            return (true, 0.5f);

        var prediction = _predictionEngine.Predict(new SentimentData { Text = text });
        return (prediction.Prediction, prediction.Probability);
    }
}
