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
                if (IsOption(i, "-o"))
                {
                    ReadTargetPath(++i, config);
                    continue;
                }

                if (IsOption(i, "-h"))
                {
                    config.Help = true;
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

        private bool IsOption(int index, string option)
        {
            return string.Compare(args[index], option, true) == 0;
        }
    }
}