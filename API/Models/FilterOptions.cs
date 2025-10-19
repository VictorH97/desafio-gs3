using System;

namespace API.Models;

public class FilterOption
{
    public required CategoryFilterOption Category { get; set; }
}

public class CategoryFilterOption
{
    public required string Category { get; set; }
    public required ManufacturerFilterOption[] Manufacturers { get; set; }
}

public class ManufacturerFilterOption
{
    public required string Manufacturer { get; set; }
    public required ModelFilterOption[] Models { get; set; }
}

public class ModelFilterOption
{
    public required string Model { get; set; }
    public required int[] Years { get; set; }
}
