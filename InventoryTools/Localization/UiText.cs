using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace InventoryTools.Localization;

public static class UiText
{
    private static readonly IReadOnlyDictionary<string, string> Translations = LoadTranslations();

    public static string T(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var hiddenIdIndex = text.IndexOf("##", StringComparison.Ordinal);
        var visibleText = hiddenIdIndex >= 0 ? text[..hiddenIdIndex] : text;
        var hiddenId = hiddenIdIndex >= 0 ? text[hiddenIdIndex..] : string.Empty;

        return Translations.TryGetValue(visibleText, out var translation)
            ? translation + hiddenId
            : text;
    }

    public static string TF(FormattableString text)
    {
        var format = text.Format;
        var hiddenIdIndex = format.IndexOf("##", StringComparison.Ordinal);
        var visibleFormat = hiddenIdIndex >= 0 ? format[..hiddenIdIndex] : format;
        var hiddenId = hiddenIdIndex >= 0 ? format[hiddenIdIndex..] : string.Empty;

        return Translations.TryGetValue(visibleFormat, out var translation)
            ? string.Format(CultureInfo.CurrentCulture, translation + hiddenId, text.GetArguments())
            : text.ToString();
    }

    private static IReadOnlyDictionary<string, string> LoadTranslations()
    {
        const string resourceName = "InventoryTools.Localization.zh-TW.json";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return new Dictionary<string, string>();
        }

        return JsonSerializer.Deserialize<Dictionary<string, string>>(stream)
               ?? new Dictionary<string, string>();
    }
}
