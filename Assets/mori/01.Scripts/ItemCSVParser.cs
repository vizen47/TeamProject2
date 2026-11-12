#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ItemCSVParser
{
    public const string CsvPath = "Assets/mori/09.Data/ItemData.csv";
    private const string DataFolder = "Assets/mori/09.Data";
    private const string OutputPath = DataFolder + "/Items";

    private static readonly string[] Headers =
        { "id", "name", "info", "price", "hunger", "thirst", "uses", "type" };

    [MenuItem("Tools/Items/Sync CSV")]
    public static void Import()
    {
        string text;
        try
        {
            text = File.ReadAllText(CsvPath);
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException)
        {
            return;
        }

        var rows = ParseCSV(text);
        if (rows == null || rows.Count == 0 ||
            !Headers.SequenceEqual(rows[0], StringComparer.OrdinalIgnoreCase))
            return;

        var items = new List<(int id, string name, string info, int price,
            float hunger, float thirst, int uses, ItemType type)>();
        var ids = new HashSet<int>();
        var culture = CultureInfo.InvariantCulture;
        const NumberStyles decimalStyle = NumberStyles.Float | NumberStyles.AllowThousands;

        for (int i = 1; i < rows.Count; i++)
        {
            string[] row = rows[i];
            if (row.Length == 1 && row[0].Length == 0)
                continue;
            if (row.Length != Headers.Length)
                return;

            if (!int.TryParse(row[0], NumberStyles.Integer, culture, out int id) ||
                !int.TryParse(row[3], NumberStyles.Integer, culture, out int price) ||
                !float.TryParse(row[4], decimalStyle, culture, out float hunger) ||
                !float.TryParse(row[5], decimalStyle, culture, out float thirst) ||
                !int.TryParse(row[6], NumberStyles.Integer, culture, out int uses) ||
                !ids.Add(id))
                return;

            ItemType? type = row[7].ToLowerInvariant() switch
            {
                "food" => ItemType.Food,
                "drink" => ItemType.Drink,
                "tool" => ItemType.Tool,
                "misc" => ItemType.Misc,
                _ => null
            };
            if (!type.HasValue)
                return;

            items.Add((id, row[1], row[2], price, hunger, thirst, uses, type.Value));
        }

        if (!AssetDatabase.IsValidFolder(OutputPath))
            AssetDatabase.CreateFolder(DataFolder, "Items");

        int created = 0;
        int updated = 0;
        foreach (var row in items)
        {
            string path = $"{OutputPath}/Item_{row.id}.asset";
            var asset = AssetDatabase.LoadMainAssetAtPath(path);
            if (asset != null && asset is not ItemDataSO)
                continue;

            bool isNew = asset == null;
            var item = isNew ? ScriptableObject.CreateInstance<ItemDataSO>() : (ItemDataSO)asset;
            item.itemID = row.id;
            item.itemName = row.name;
            item.itemDescription = row.info;
            item.price = row.price;
            item.hunger = row.hunger;
            item.thirst = row.thirst;
            item.uses = row.uses;
            item.itemType = row.type;

            if (isNew)
            {
                AssetDatabase.CreateAsset(item, path);
                created++;
            }
            else
            {
                EditorUtility.SetDirty(item);
                updated++;
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"CSV파일 동기화 완료, 신규파일 {created}개, 변경된 파일 {updated}개");
    }

    private static List<string[]> ParseCSV(string text)
    {
        var rows = new List<string[]>();
        var fields = new List<string>();
        var field = new StringBuilder();
        bool quoted = false;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (c == '"')
            {
                if (quoted && i + 1 < text.Length && text[i + 1] == '"')
                {
                    field.Append('"');
                    i++;
                }
                else
                {
                    quoted = !quoted;
                }

                continue;
            }

            if (quoted || (c != ',' && c != '\r' && c != '\n'))
            {
                field.Append(c);
                continue;
            }

            fields.Add(field.ToString().Trim());
            field.Clear();
            if (c == ',')
                continue;

            if (c == '\r' && i + 1 < text.Length && text[i + 1] == '\n')
                i++;

            rows.Add(fields.ToArray());
            fields.Clear();
        }

        if (quoted)
            return null;

        if (field.Length > 0 || fields.Count > 0)
        {
            fields.Add(field.ToString().Trim());
            rows.Add(fields.ToArray());
        }

        return rows;
    }
}

public class ItemCSVPostprocessor : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(
        string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (!importedAssets.Contains(ItemCSVParser.CsvPath) && !movedAssets.Contains(ItemCSVParser.CsvPath))
            return;

        EditorApplication.delayCall -= ItemCSVParser.Import;
        EditorApplication.delayCall += ItemCSVParser.Import;
    }
}
#endif
