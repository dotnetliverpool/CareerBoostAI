using System.Diagnostics;
using CareerBoostAI.Domain.Enums;
using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CvFile
{
    public string Name { get; private set; }
    public string FileType { get; private set; }
    public CvStorageMedium StorageMedium { get; private set; }  
    public string StorageAddress { get; private set; } 
    
    private static readonly string[] ValidFileTypes = { "pdf" };

    
    private CvFile(string name, string fileType, CvStorageMedium storageMedium, string storageAddress)
    {
        Name = name;
        FileType = fileType;
        StorageMedium = storageMedium;
        StorageAddress = storageAddress;
    }

    
    public static CvFile Create(string name,  CvStorageMedium storageMedium, string storageAddress)
    {
        
        if (string.IsNullOrEmpty(name))
        {
            throw new EmptyArgumentException("File Name");
        }
        
        if (!IsSupportedFileType(name))
        {
            throw new UnsupportedFileTypeException(name);
        }
        
        if (string.IsNullOrEmpty(storageAddress))
        {
            throw new EmptyArgumentException("File Storage Address");
        }

        return new CvFile(name, ExtractFileTypeFromName(name), storageMedium, storageAddress);
    }

    private static string ExtractFileTypeFromName(string fileName)
    {
        var result =  Path.GetExtension(fileName)?.TrimStart('.').ToLower();
        return string.IsNullOrEmpty(result) ? "" : result;
    }

    public static bool IsSupportedFileType(string fileName)
    {
        if (string.IsNullOrEmpty(ExtractFileTypeFromName(fileName)))
        {
            return false;
        }
        return Array.Exists(ValidFileTypes, ft => ft.Equals(
            ExtractFileTypeFromName(fileName),
            StringComparison.OrdinalIgnoreCase));
    }
    
    

    // Get atomic values for equality checks or comparison
    protected IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return FileType;
        yield return StorageMedium;
        yield return StorageAddress;
    }
}
