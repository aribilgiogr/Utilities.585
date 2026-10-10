using CsvHelper;
using CsvHelper.Configuration;
using MiniExcelLibs;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Utilities._585.Models;

namespace Utilities._585.Helpers
{
    public static class FileImportAdapter
    {
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters = { new JsonStringEnumConverter() }
        };

        public static async Task<Reply<IEnumerable<T>>> FromJsonCollectionAsync<T>(Stream stream) where T : class
        {
            try
            {
                var records = await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream, options);
                if (records == null) return Reply<IEnumerable<T>>.Fail("No records found in the JSON stream.");
                return Reply<IEnumerable<T>>.Success(records);
            }
            catch (Exception ex)
            {
                return Reply<IEnumerable<T>>.Fail(ex.Message);
            }
        }

        public static async Task<Reply<T>> FromJsonAsync<T>(Stream stream) where T : class
        {
            try
            {
                var record = await JsonSerializer.DeserializeAsync<T>(stream, options);
                if (record == null) return Reply<T>.Fail("No record found in the JSON stream.");
                return Reply<T>.Success(record);
            }
            catch (Exception ex)
            {
                return Reply<T>.Fail(ex.Message);
            }
        }

        public static async Task<Reply<IEnumerable<T>>> FromCSVAsync<T>(Stream stream) where T : class
        {
            try
            {
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                var records = await csv.GetRecordsAsync<T>().ToListAsync();
                return Reply<IEnumerable<T>>.Success(records);
            }
            catch (Exception ex)
            {
                return Reply<IEnumerable<T>>.Fail(ex.Message);
            }
        }

        public static async Task<Reply<IEnumerable<T>>> FromExcelAsync<T>(Stream stream) where T : class, new()
        {
            try
            {
                var records = await stream.QueryAsync<T>();
                if (records == null) return Reply<IEnumerable<T>>.Fail("No records found in the Excel stream.");
                return Reply<IEnumerable<T>>.Success(records);
            }
            catch (Exception ex)
            {
                return Reply<IEnumerable<T>>.Fail(ex.Message);
            }
        }
    }
}
