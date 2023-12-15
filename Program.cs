using Microsoft.CognitiveServices.Speech;
using System.Diagnostics;

namespace AzureSTT
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RecognizeSpeech();
            Console.WriteLine("End");
        }

        private static async Task RecognizeSpeech()
        {
            var watch = Stopwatch.StartNew();

            var configuration = SpeechConfig.FromSubscription("37f7fbca88bc408ca9d7a53c3ba0ab4e", "centralindia");
            using (var recog = new SpeechRecognizer(configuration))
            {
                await Recurring(watch, recog);
            }
        }

        private static async Task Recurring(Stopwatch watch, SpeechRecognizer recog)
        {
            Console.WriteLine("Speak Something...");
            var result = await recog.RecognizeOnceAsync();
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                watch.Stop();
                Console.WriteLine(result.Text);
                if (!watch.IsRunning)
                {
                    watch.Restart(); // Reset time to 0 and start measuring
                    await Recurring(watch, recog);
                }
            }
        }

    }
}