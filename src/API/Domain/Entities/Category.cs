﻿namespace Commentor.GivEtPraj.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Case> Cases { get; set; } = new List<Case>();
}