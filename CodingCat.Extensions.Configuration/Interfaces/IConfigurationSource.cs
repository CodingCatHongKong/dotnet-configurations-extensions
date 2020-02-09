using CodingCat.Extensions.Configuration.Enums;
using System;
using System.IO;
using Environment = CodingCat.Extensions.Configuration.Enums.Environment;

namespace CodingCat.Extensions.Configuration.Interfaces
{
    public interface IConfigurationSource
    {
        DirectoryInfo ConfigurationsDirectory { get; }
        Type ConfigurationType { get; }
        Environment Environment { get; }
        FileType FileType { get; }
        bool IsOptional { get; }
    }
}