using System;

namespace FGet
{
    internal class ArgumentParser
    {
        private string[] args;

        public ArgumentParser(string[] args)
        {
            this.args = args;
        }

        internal Config Parse()
        {
            var config = new Config();

            for (int i = 0; i < args.Length; i++)
            {
                if (IsTargetPath(i))
                {
                    ReadTargetPath(++i, config);
                    continue;
                }

                config.SourceUrl = new Uri(args[i]);
            }

            return config;
        }

        private void ReadTargetPath(int index, Config config)
        {
            if (index < args.Length)
            {
                config.TargetPath = args[index];
            }
        }

        private bool IsTargetPath(int index)
        {
            return string.Compare(args[index], "-o", true) == 0;
        }
    }
}