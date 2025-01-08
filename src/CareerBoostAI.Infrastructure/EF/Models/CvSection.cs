﻿using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvSection
{
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    public uint SequenceIndex { get; set; }
    
    public List<CvSectionItem> SectionItems { get; set; }
    
    public Guid CvId { get; set; }
    public Cv Cv { get; set; }
}