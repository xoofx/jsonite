using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Textamina.Jsonite.Benchmarks
{
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, processCount: 1, warmupIterationCount: 2)]
    public class BenchGenericDeserialize
    {
        private readonly string testJson;

        public BenchGenericDeserialize()
        {
            testJson = File.ReadAllText("test.json");
        }

        [Benchmark("Textamina.Jsonite")]
        public void TestJsonite()
        {
            var result = Json.Deserialize(testJson);
        }

        [Benchmark("Newtonsoft.Json")]
        public void TestNewtonsoftJson()
        {
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(testJson);
        }

        [Benchmark("System.Text.Json (FastJsonParser)")]
        public void TestSystemTextJson()
        {
            var parser = new System.Text.Json.JsonParser();
            var result = parser.Parse<Dictionary<string, object>>(testJson);
        }

        [Benchmark("ServiceStack.Text")]
        public void TestServiceStackText()
        {
            // Force ServiceStack.Text to deserialize completely the object (otherwise it is deserializing only the first object level, which is not what we want to test here)
            ServiceStack.Text.JsConfig.ConvertObjectTypesIntoStringDictionary = true;
            var result = (Dictionary<string, object>)ServiceStack.StringExtensions.FromJson<object>(testJson);
        }

        [Benchmark("fastJSON")]
        public void TestFastJson()
        {
            var result = fastJSON.JSON.Parse(testJson);
        }

        [Benchmark("Jil")]
        public void TestJil()
        {
            var result = Jil.JSON.Deserialize<Dictionary<string, object>>(testJson);
        }

        [Benchmark("JavaScriptSerializer")]
        public void TestJavaScriptSerializer()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.DeserializeObject(testJson);
        }
    }
}