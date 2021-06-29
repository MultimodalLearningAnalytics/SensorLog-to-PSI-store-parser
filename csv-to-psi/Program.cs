using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Psi;
using System.Threading;
//using Microsoft.Psi.Audio;
//using Microsoft.Psi.Imaging;
//using Microsoft.Psi.Media;
//using Microsoft.Psi.Speech;
//using System.Windows.Forms;
//using Microsoft.Psi.Data;
//using Microsoft.Psi.Data.Json;

namespace csv_to_psi
{
    class Program
    {
        static void Main(string[] args)
        {
            //var appContentResources = Environment.CurrentDirectory;
            //Console.WriteLine(appContentResources);
            //Console.ReadKey();

            //System.IO.StreamReader reader = new System.IO.StreamReader(File.OpenRead(@"C:\Users\giuse\Downloads\HeartRate.csv"));
            //List<string> listA = new List<String>();
            //List<string> listB = new List<String>();
            //List<string> listC = new List<String>();
            ////List<string> listD = new List<String>();

            //while (!reader.EndOfStream)
            //{
            //    string line = reader.ReadLine();
            //    if (!String.IsNullOrWhiteSpace(line))
            //    {
            //        string[] values = line.Split(',');
            //        if (values.Length >= 3)
            //        {
            //            listA.Add(values[0]);
            //            listB.Add(values[1]);
            //            listC.Add(values[2]);
            //            //listD.Add(values[3]);
            //        }
            //    }
            //}

            //string[] firstlistA = listA.ToArray();
            //string[] firstlistB = listB.ToArray();
            //string[] firstlistC = listC.ToArray();
            ////string[] firstlistD = listD.ToArray();

            ////Console.WriteLine(listA);
            ////Console.WriteLine(firstlistB);
            ////Console.WriteLine(firstlistC);

            ////listB.ForEach(item => Console.Write(item + ", "));
            //List<(int, DateTime)> rawvalues = new List<(int, DateTime)>();

            //for (int i = 1; i < listA.Count; i++)
            //{
            //    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(Int64.Parse(listA[i]) / 1000000);
            //    DateTime date = dateTimeOffset.UtcDateTime;

            //    rawvalues.Add((Int32.Parse(listC[i]), date));
            //}

            //foreach (string value in listA)
            //{
            //    if (value != "time")
            //    {
            //        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(Int64.Parse(value) / 1000000);
            //        DateTime date = dateTimeOffset.UtcDateTime;
            //        Console.WriteLine(date);
            //    }

            //}


            //var dataset = new Dataset("TestDataset");
            //var session = dataset.CreateSession("FirstSession");
            //var partition = 

            //public static IProducer<T> Sequence<T>(Pipeline pipeline, IEnumerable<(T, DateTime)> enumerable, DateTime? startTime = null, bool keepOpen = false);

            // Create the pipeline object with diagnostics enabled
            using (var p = Pipeline.Create(enableDiagnostics: false))
            {
                // Create the store
                var store = PsiStore.Create(p, "demo", "c:\\hearttry");

                GeneratorHeartRate generator = new GeneratorHeartRate(p, @"C:\Users\giuse\Downloads\HeartRate.csv");

                generator.OutInt.Write("heartrate", store);
                
                //Emitter<int> heartrateEmitter = p.CreateEmitter<int>(p, "heartrate-data");

                
                //for (int i = 0; i < rawvalues.Count; i++)
                //{
                //    Generators.

                //    //Console.WriteLine("Logging " + rawvalues[i].Item1 + " on " + rawvalues[i].Item2);
                //    //DateTime ts = rawvalues[i].Item2;
                //    Console.WriteLine("Item " + i);
                //    DateTime now = DateTime.Now;
                //    heartrateEmitter.Post(rawvalues[i].Item1,now);
                //    Thread.Sleep(100);
                //    //    DateTime ts = createTimeStamp(firstListA[i]) - timeDiffPhone; // creating NTP time, createTimeStampIso has to be created by you to convert from string to DateTime. Keep in mind time should be on UTC.
                //    //        ntpts = TimeSync.NtpToSysTime(ts);

                //    //    hearrateEmitter.post(firstListC[i].ToInt(), ntpts); // First convert the firstListC value to int. Not sure if this is the correct way!
                //    //}
                //}
                generator.OutInt.Do((m, e) => { Console.WriteLine($"Received: {m} at {e.OriginatingTime}"); });
                //heartrateEmitter.Write("heartratedata", store);


                //var rawsequence = Generators.Sequence(p, rawvalues, rawvalues[0].Item2);

                ////var sequence = Generators.Sequence(p, 0d, x => x + 0.1, 10000, TimeSpan.FromMilliseconds(100));

                ////var sin = sequence.Select(t => Math.Sin(t));
                ////var cos = sequence.Select(t => Math.Cos(t));

                ////// Write the sin and cos streams to the store
                ////sequence.Write("Sequence", store);
                ////sin.Write("Sin", store);
                ////cos.Write("Cos", store);


                //// Write the sin and cos streams to the store
                //rawsequence.Write("Heart", store);

                //foreach ((int, DateTime) value in rawvalues)
                //{
                //    Console.WriteLine("Read: " + value.Item1 + " at " + value.Item2);
                //}

                ////Write the diagnostics stream to the store
                ////p.Diagnostics.Write("Diagnostics", store);

                //// Run the pipeline
                p.Run();

                Console.WriteLine("press any key to finish recording");
                Console.ReadKey();
            }

            //using (var p = Pipeline.Create(enableDiagnostics: true))
            //{
            //    // Create the store
            //    var store = PsiStore.Create(p, "demo", "c:\\recordings");

            //    var sequence = Generators.Sequence(p, 0d, x => x + 0.1, 10000, TimeSpan.FromMilliseconds(100));

            //    var sin = sequence.Select(t => Math.Sin(t));
            //    var cos = sequence.Select(t => Math.Cos(t));

            //    // Write the sin and cos streams to the store
            //    sequence.Write("Sequence", store);
            //    sin.Write("Sin", store);
            //    cos.Write("Cos", store);

            //    // Create the webcam and write its output to the store as compressed JPEGs
            //    var webcam = new MediaCapture(p, 1920, 1080, 30);
            //    webcam.Out.EncodeJpeg(90, DeliveryPolicy.LatestMessage).Write("Image", store);

            //    // Create the AudioCapture component and write the output to the store
            //    var audio = new AudioCapture(p, new AudioCaptureConfiguration() { Format = WaveFormat.Create16kHz1Channel16BitPcm() });
            //    audio.Write("Audio", store);

            //    // Pipe the audio to a voice activity detector and write its output to the store
            //    var voiceActivityDetector = new SystemVoiceActivityDetector(p);
            //    audio.Out.PipeTo(voiceActivityDetector);
            //    voiceActivityDetector.Out.Write("Voice Activity", store);

            //    // Write the diagnostics stream to the store
            //    p.Diagnostics.Write("Diagnostics", store);

            //    // Run the pipeline
            //    p.Run();

            //    Console.WriteLine("Press any key to finish recording");
            //    Console.ReadKey();
            //}

            //Console.ReadKey();
        }
    }
}
