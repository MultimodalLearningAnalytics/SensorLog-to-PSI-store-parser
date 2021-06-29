using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Psi;
using Microsoft.Psi.Components;
using System.Threading;

namespace csv_to_psi
{
    public class GeneratorHeartRate : Generator
    {
        private System.IO.StreamReader reader;

        public GeneratorHeartRate(Pipeline p, string fileName)
            : base(p)
        {
            this.OutInt = p.CreateEmitter<int>(this, nameof(this.OutInt));
            this.reader = new System.IO.StreamReader(File.OpenRead(fileName));
            reader.ReadLine();
        }

        public Emitter<int> OutInt { get; }

        protected override DateTime GenerateNext(DateTime currentTime)
        {
            if (reader.EndOfStream)
            {
                return currentTime; // no more data
            }

            string line = this.reader.ReadLine();
            string[] values;


            if (!String.IsNullOrWhiteSpace(line))
            {
                values = line.Split(',');
            }
            else
            {
                return currentTime;
            }


            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(Int64.Parse(values[0]) / 1000000);
            DateTime date = dateTimeOffset.UtcDateTime;

            var originatingTime = date;
            this.OutInt.Post(Int32.Parse(values[2]), originatingTime);

            return originatingTime;
        }
    }
}