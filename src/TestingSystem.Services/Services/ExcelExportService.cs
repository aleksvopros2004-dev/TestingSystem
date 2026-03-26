using ClosedXML.Excel;
using System.Data;
using TestingSystem.Core.Models;

namespace TestingSystem.Services.Services;

public class ExcelExportService
{
    /// <summary>
    /// Экспорт общей статистики по тесту
    /// </summary>
    public byte[] ExportTestStatistics(TestStatistics stats)
    {
        using var workbook = new XLWorkbook();

        // Основная статистика
        var summarySheet = workbook.Worksheets.Add("Общая статистика");
        summarySheet.Cell(1, 1).Value = "Показатель";
        summarySheet.Cell(1, 2).Value = "Значение";

        var row = 2;
        summarySheet.Cell(row, 1).Value = "Название теста";
        summarySheet.Cell(row, 2).Value = stats.TestTitle;
        row++;

        summarySheet.Cell(row, 1).Value = "Всего попыток";
        summarySheet.Cell(row, 2).Value = stats.TotalAttempts;
        row++;

        summarySheet.Cell(row, 1).Value = "Завершено попыток";
        summarySheet.Cell(row, 2).Value = stats.CompletedAttempts;
        row++;

        summarySheet.Cell(row, 1).Value = "Средний балл (%)";
        summarySheet.Cell(row, 2).Value = stats.AverageScore.ToString("F1");
        row++;

        summarySheet.Cell(row, 1).Value = "Медианный балл (%)";
        summarySheet.Cell(row, 2).Value = stats.MedianScore.ToString("F1");
        row++;

        summarySheet.Cell(row, 1).Value = "Максимальный балл (%)";
        summarySheet.Cell(row, 2).Value = stats.MaxScore;
        row++;

        summarySheet.Cell(row, 1).Value = "Минимальный балл (%)";
        summarySheet.Cell(row, 2).Value = stats.MinScore;
        row++;

        summarySheet.Cell(row, 1).Value = "Среднее время (мин)";
        summarySheet.Cell(row, 2).Value = stats.AverageTimeMinutes.ToString("F1");
        row++;

        summarySheet.Cell(row, 1).Value = "Макс. время (мин)";
        summarySheet.Cell(row, 2).Value = stats.MaxTimeMinutes.ToString("F1");
        row++;

        summarySheet.Cell(row, 1).Value = "Мин. время (мин)";
        summarySheet.Cell(row, 2).Value = stats.MinTimeMinutes.ToString("F1");

        // Распределение оценок
        var distributionSheet = workbook.Worksheets.Add("Распределение оценок");
        distributionSheet.Cell(1, 1).Value = "Диапазон";
        distributionSheet.Cell(1, 2).Value = "Количество";

        var distData = new[]
        {
            new { Range = "0-20%", Count = stats.Score0_20 },
            new { Range = "20-40%", Count = stats.Score20_40 },
            new { Range = "40-60%", Count = stats.Score40_60 },
            new { Range = "60-80%", Count = stats.Score60_80 },
            new { Range = "80-100%", Count = stats.Score80_100 }
        };

        row = 2;
        foreach (var d in distData)
        {
            distributionSheet.Cell(row, 1).Value = d.Range;
            distributionSheet.Cell(row, 2).Value = d.Count;
            row++;
        }

        // Детальная статистика по вопросам
        var questionsSheet = workbook.Worksheets.Add("Детальная статистика");
        questionsSheet.Cell(1, 1).Value = "ID вопроса";
        questionsSheet.Cell(1, 2).Value = "Текст вопроса";
        questionsSheet.Cell(1, 3).Value = "Тип";
        questionsSheet.Cell(1, 4).Value = "Всего ответов";
        questionsSheet.Cell(1, 5).Value = "Правильных ответов";
        questionsSheet.Cell(1, 6).Value = "Правильных (%)";
        questionsSheet.Cell(1, 7).Value = "Баллов за вопрос";
        questionsSheet.Cell(1, 8).Value = "Средний балл";

        row = 2;
        foreach (var q in stats.QuestionStats)
        {
            questionsSheet.Cell(row, 1).Value = q.QuestionId;
            questionsSheet.Cell(row, 2).Value = q.QuestionText;
            questionsSheet.Cell(row, 3).Value = q.QuestionType switch
            {
                "SingleChoice" => "Один вариант",
                "MultipleChoice" => "Несколько вариантов",
                "TextAnswer" => "Текстовый ответ",
                _ => q.QuestionType
            };
            questionsSheet.Cell(row, 4).Value = q.TotalAnswers;
            questionsSheet.Cell(row, 5).Value = q.CorrectAnswers;
            questionsSheet.Cell(row, 6).Value = q.CorrectPercentage.ToString("F1");
            questionsSheet.Cell(row, 7).Value = q.Points;
            questionsSheet.Cell(row, 8).Value = q.AveragePointsEarned.ToString("F1");
            row++;
        }

        // История попыток
        var attemptsSheet = workbook.Worksheets.Add("История попыток");
        attemptsSheet.Cell(1, 1).Value = "Пользователь";
        attemptsSheet.Cell(1, 2).Value = "Дата";
        attemptsSheet.Cell(1, 3).Value = "Баллы";
        attemptsSheet.Cell(1, 4).Value = "Результат (%)";
        attemptsSheet.Cell(1, 5).Value = "Время";

        row = 2;
        foreach (var attempt in stats.RecentAttempts)
        {
            attemptsSheet.Cell(row, 1).Value = attempt.UserName;
            attemptsSheet.Cell(row, 2).Value = attempt.AttemptDate.ToString("dd.MM.yyyy HH:mm");
            attemptsSheet.Cell(row, 3).Value = $"{attempt.EarnedPoints}/{attempt.TotalPoints}";
            attemptsSheet.Cell(row, 4).Value = attempt.Percentage.ToString("F1");
            attemptsSheet.Cell(row, 5).Value = $"{attempt.TimeSpent.Minutes}:{attempt.TimeSpent.Seconds:D2}";
            row++;
        }

        // Форматирование
        foreach (var sheet in workbook.Worksheets)
        {
            sheet.Columns().AdjustToContents();
        }

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);
        return ms.ToArray();
    }

    /// <summary>
    /// Экспорт результатов конкретного пользователя
    /// </summary>
    public byte[] ExportUserResults(User user, List<UserAttempt> attempts)
    {
        using var workbook = new XLWorkbook();

        var sheet = workbook.Worksheets.Add($"Результаты {user.FullName}");

        sheet.Cell(1, 1).Value = "Дата";
        sheet.Cell(1, 2).Value = "Тест";
        sheet.Cell(1, 3).Value = "Баллы";
        sheet.Cell(1, 4).Value = "Результат (%)";
        sheet.Cell(1, 5).Value = "Время";

        var row = 2;
        foreach (var attempt in attempts)
        {
            sheet.Cell(row, 1).Value = attempt.AttemptDate.ToString("dd.MM.yyyy HH:mm");
            sheet.Cell(row, 2).Value = attempt.UserName; // Здесь должно быть название теста
            sheet.Cell(row, 3).Value = $"{attempt.EarnedPoints}/{attempt.TotalPoints}";
            sheet.Cell(row, 4).Value = attempt.Percentage.ToString("F1");
            sheet.Cell(row, 5).Value = $"{attempt.TimeSpent.Minutes}:{attempt.TimeSpent.Seconds:D2}";
            row++;
        }

        sheet.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);
        return ms.ToArray();
    }

    /// <summary>
    /// Экспорт текстовых ответов с анализом частоты слов
    /// </summary>
    public byte[] ExportTextAnswersWithAnalysis(
        int questionId,
        string questionText,
        IEnumerable<string> answers,
        List<WordFrequency> wordFrequencies)
    {
        using var workbook = new XLWorkbook();

        // Лист с ответами
        var answersSheet = workbook.Worksheets.Add("Ответы");
        answersSheet.Cell(1, 1).Value = "№";
        answersSheet.Cell(1, 2).Value = "Ответ";

        var row = 2;
        foreach (var answer in answers)
        {
            answersSheet.Cell(row, 1).Value = row - 1;
            answersSheet.Cell(row, 2).Value = answer;
            row++;
        }

        // Лист с анализом частоты слов
        var analysisSheet = workbook.Worksheets.Add("Анализ слов");
        analysisSheet.Cell(1, 1).Value = "Слово";
        analysisSheet.Cell(1, 2).Value = "Частота";

        row = 2;
        foreach (var wf in wordFrequencies)
        {
            analysisSheet.Cell(row, 1).Value = wf.NormalizedForm;
            analysisSheet.Cell(row, 2).Value = wf.Count;
            row++;
        }

        analysisSheet.Cell(1, 3).Value = "Облако слов";
        analysisSheet.Cell(2, 3).Value = string.Join(", ",
            wordFrequencies.Take(10).Select(w => $"{w.NormalizedForm}({w.Count})"));

        answersSheet.Columns().AdjustToContents();
        analysisSheet.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);
        return ms.ToArray();
    }
}