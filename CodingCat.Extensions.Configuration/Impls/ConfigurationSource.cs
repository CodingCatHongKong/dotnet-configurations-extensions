using CodingCat.Extensions.Configuration.Enums;
using CodingCat.Extensions.Configuration.Interfaces;
using System;
using System.IO;
using Environment = CodingCat.Extensions.Configuration.Enums.Environment;

namespace CodingCat.Extensions.Configuration.Impls
{
    public class ConfigurationSource : IConfigurationSource
    {
        public DirectoryInfo ConfigurationsDirectory { get; private set; } = null;
        public Type ConfigurationType { get; private set; }
        public Environment Environment { get; private set; }
        public FileType FileType { get; private set; }
        public bool IsOptional { get; private set; }

        #region Constructor(s)
        public ConfigurationSource(ConfigurationSource source)
        {
            this.ConfigurationsDirectory = source.ConfigurationsDirectory;
            this.ConfigurationType = source.ConfigurationType;
            this.Environment = source.Environment;
            this.FileType = source.FileType;
            this.IsOptional = source.IsOptional;
        }

        public ConfigurationSource(
            Type configurationType,
            Environment environment,
            FileType fileType,
            bool isOptional
        )
        {
            this.ConfigurationType = configurationType;
            this.Environment = environment;
            this.FileType = fileType;
            this.IsOptional = isOptional;
        }

        public ConfigurationSource(
            DirectoryInfo directoryInfo,
            Type configurationType,
            Environment environment,
            FileType fileType,
            bool isOptional
        ) : this(configurationType, environment, fileType, isOptional)
        {
            this.ConfigurationsDirectory = directoryInfo;
        }
        #endregion

        public ConfigurationSource With(DirectoryInfo configurations)
        {
            return new ConfigurationSource(this)
            {
                ConfigurationsDirectory = configurations
            };
        }

        public ConfigurationSource With(Type configurationType)
        {
            return new ConfigurationSource(this)
            {
                ConfigurationType = configurationType
            };
        }

        public ConfigurationSource With<T>()
        {
            return this.With(typeof(T));
        }

        public ConfigurationSource With(Environment environment)
        {
            return new ConfigurationSource(this)
            {
                Environment = environment
            };
        }

        public ConfigurationSource With(FileType fileType)
        {
            return new ConfigurationSource(this)
            {
                FileType = fileType
            };
        }

        public ConfigurationSource With(bool isOptional)
        {
            return new ConfigurationSource(this)
            {
                IsOptional = isOptional
            };
        }
    }

    public class ConfigurationSource<T> : ConfigurationSource
    {
        #region Constructor(s)
        public ConfigurationSource(
            Environment environment,
            FileType fileType,
            bool isOptional
        ) : base(typeof(T), environment, fileType, isOptional) { }

        public ConfigurationSource(
            DirectoryInfo directoryInfo,
            Environment environment,
            FileType fileType,
            bool isOptional
        ) : base(
            directoryInfo,
            typeof(T),
            environment,
            fileType,
            isOptional
        ) { }
        #endregion
    }
}
